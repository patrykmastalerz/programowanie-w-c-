using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Repositories;
using Microsoft.AspNetCore.Authorization;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        [Authorize(Roles = "User,StoreManager,Owner")]
        public IActionResult Index()
        {
            var books = _bookRepository.GetAllBooks();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _bookRepository.GetBookById(id);

            if (book == null)
            {
                return Content("Book not found");
            }

            return View(book);
        }

        [Authorize(Roles="StoreManager,Owner")]
        public IActionResult Edit(int id)
        {
            var book = _bookRepository.GetBookById(id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genre.ToList()
            };

            return View("BookForm", viewModel);
        }

        [Authorize(Roles="StoreManager,Owner")]
        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = _context.Genre.ToList()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult Save(Book book)
        {
            if (!ModelState.IsValid)
            {
                return New();
            }

            if (book.Id == 0)
            {
                book.DateAdded = DateTime.Now;
                _bookRepository.AddBook(book);
            }
            else
            {
                _bookRepository.UpdateBook(book);
            }

            try
            {
                _bookRepository.Save();
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e);
            }

            return RedirectToAction("Index", "Books");
        }


    }
}
