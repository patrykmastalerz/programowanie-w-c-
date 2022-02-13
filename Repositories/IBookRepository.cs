using LibApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibApp.Repositories
{
    public interface IBookRepository
    {
        void AddBook(Book book);

        void UpdateBook(Book book);

        void DeleteBook(int id);

        IEnumerable<Book> GetAllBooks();

        Book GetBookById(int id);

        void Save();
    }
}
