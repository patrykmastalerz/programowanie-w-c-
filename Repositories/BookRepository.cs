using LibApp.Data;
using LibApp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repositories
{
    public class BookRepository : IBookRepository
    {

        private readonly ApplicationDbContext _context;
        public BookRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void AddBook(Book book)
        {
            _context.Books.Add(book);
        }

        public void DeleteBook(int id)
        {
            var book = this.GetBookById(id);
            _context.Books.Remove(book);
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _context.Books
                .Include(x => x.Genre)
                .ToList();

        }

        public Book GetBookById(int id)
        {
            return _context.Books.Include(x => x.Genre).SingleOrDefault(x => x.Id == id);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void UpdateBook(Book book)
        {
            var bookToUpdate = _context.Books.Find(book.Id);

            bookToUpdate.Name = book.Name;
            bookToUpdate.AuthorName = book.AuthorName;
            bookToUpdate.GenreId = book.GenreId;
            bookToUpdate.ReleaseDate = book.ReleaseDate;
            bookToUpdate.DateAdded = book.DateAdded;
            bookToUpdate.NumberInStock = book.NumberInStock;
        }
    }
}
