using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary4
{
    public interface IBooksRepository
    {
        IEnumerable<Book> GetAll(double? priceMax = null, string? titleIncludes = null, string? orderBy = null);

        Book? GetById(int id);

        Book Add(Book book);

        Book? Remove(int id);

        Book? Update(int id, Book book);
    }
}
