using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ClassLibrary4
{
    public class BooksRepository : IBooksRepository
    {
        #region List of books field
        //Oprette en liste af book objekter 
        private int _nextId = 1;
        private readonly List<Book> _books = new();
        #endregion

        #region BooksRepository field
        public BooksRepository()
        {
            _books.Add(new Book() { Id = _nextId++, Title = "Money Maker", Price = 199 });
            _books.Add(new Book() { Id = _nextId++, Title = "Learn to code", Price = 295 });
        }
        #endregion


        public IEnumerable<Book> GetAll(double? priceMax = null, string? titleIncludes = null, string? orderBy = null)
        {
            #region Filtering field
            IEnumerable<Book> result = new List<Book>(_books);
            // Filtering
            if (priceMax != null)
            {
                result = result.Where(b => b.Price <= priceMax);
            }
            if (titleIncludes != null)
            {
                result = result.Where(b => b.Title.Contains(titleIncludes));
            }
            #endregion
            
            #region Ordering/Sorting field
            // Ordering aka. sorting
            if (orderBy != null)
            {
                orderBy = orderBy.ToLower();
                switch (orderBy)
                {
                    case "title": // fall through to next case
                    case "title_asc":
                        result = result.OrderBy(b => b.Title);
                        break;
                    case "title_desc":
                        result = result.OrderByDescending(b => b.Title);
                        break;
                    case "price":
                    case "price_asc":
                        result = result.OrderBy(b => b.Price);
                        break;
                    case "price_desc":
                        result = result.OrderByDescending(b => b.Price);
                        break;
                    default:
                        throw new ArgumentException("Unknown sort order: " + orderBy);
                       
                }
            }
            return result;
            #endregion
        }


        #region Get book by id field
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Book? GetById(int id)
        {
            return _books.FirstOrDefault(books => books.Id == id);
        }
        #endregion


        #region Add book to book list field
        //Add to book list
        public Book Add(Book book)
        {
            book.validate();
            book.Id = _nextId++;
            _books.Add(book);
            return book;
        }
        #endregion


        #region Remove book from book list field
        //Remove from book list
        public Book? Remove(int id)
        {
            Book? book = GetById(id);
            if (book == null)
            {
                return null;
            }
            _books.Remove(book);
            return book;
        }
        #endregion


        #region Update book list field
        //Update on book list
        public Book? Update(int id, Book book)
        {
            book.validate();
            Book? existingBook = GetById(id);
            if (existingBook == null)
            {
                return null;
            }
            existingBook.Title = book.Title;
            existingBook.Price = book.Price;
            return existingBook;
        }
        #endregion
    }
}
