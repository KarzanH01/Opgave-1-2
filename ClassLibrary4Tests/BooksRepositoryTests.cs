using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary4.Tests
{

    [TestClass()]
    public class BooksRepositoryTests
    {
        #region BooksTests field
        private readonly Book _book = new() { Id = 1, Title = "Learn to code", Price = 295 };
        private readonly Book _nullTitle = new() { Id = 2, Price = 155 };
        private readonly Book _lessThan3 = new Book() { Id = 3, Title = "mo", Price = 249 };
        private readonly Book _price = new Book() { Id = 4, Title = "The hunger games", Price = 1399 };

        private List<Book> _books;
        private BooksRepository _bookRepo;
        #endregion



        #region TestInitialize field
        [TestInitialize]
        public void TestInitialize()
        {
            // Opret en instans af din BookService (afhænger af din implementering)
            _bookRepo = new BooksRepository();

            // Opret en liste af bøger til brug i testene
            _books = new List<Book>
        {
            new Book { Id = 1, Title = "Book1", Price = 200 },
            new Book { Id = 2, Title = "Book2", Price = 300 },
            new Book { Id = 3, Title = "Book3", Price = 400 },
        };
        }
        #endregion



        #region GetAllTestOk field
        [TestMethod]

        [DataRow("Book1", 299)]
        [DataRow("Book2", 399)]
        [DataRow("Book3", 499)]
        [DataRow("Book4", 599)]
        public void GetAllTestOk(string title, double price)
        {
            //Arrange 
            var repository = new BooksRepository();
            var books = new List<Book>
            {
                new Book { Id = 1, Title = "Hunger Games1", Price = 200 },
                new Book { Id = 2, Title = "Hunger Games2", Price = 300 },
                new Book { Id = 3, Title = "Hunger Games3", Price = 350 },
                new Book { Id = 4, Title = "Hunger Games4", Price = 400 }

            };

            foreach (var book in books)
            {
                repository.Add(book);
            }

            //Act
            var filterBook1 = repository.GetAll(titleIncludes: title, priceMax: price, orderBy: "title_asc");
            var filterBook2 = repository.GetAll(titleIncludes: title, priceMax: price, orderBy: "title_desc");
            var filterBook3 = repository.GetAll(titleIncludes: title, priceMax: price, orderBy: "price_asc");
            var filterBook4 = repository.GetAll(titleIncludes: title, priceMax: price, orderBy: "price_desc");

            //Assert
            Assert.IsNotNull(filterBook1);
            Assert.IsNotNull(filterBook2);
            Assert.IsNotNull(filterBook3);
            Assert.IsNotNull(filterBook4);
            #endregion
            }

            #region GetById Returns Book field
            [TestMethod]
            [DataRow("Book1", 120)]
            [DataRow("Book2", 240)]
            [DataRow("Book3", 360)]
            public void GetByIdTestOk(string title, double price)
            {
                //Arrange 
                var repository = new BooksRepository();

                var expectedBook = new Book() { Title = title, Price = price };
                repository.Add(expectedBook);

                // Act
                var result = repository.GetById(expectedBook.Id);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(expectedBook.Id, result.Id);
                Assert.AreEqual(expectedBook.Title, result.Title);
                Assert.AreEqual(expectedBook.Price, result.Price);
            }
            #endregion


            #region AddToBookList field
            [TestMethod]
            [DataRow("NewBook", 500)]
            [DataRow("AnotherBook", 600)]
            public void Add_Returns_CorrectBook(string title, double price)
            {
                // Arrange
                var bookToAdd = new Book { Title = title, Price = price };

                // Act
                var addedBook = _bookRepo.Add(bookToAdd);

                // Assert
                Assert.IsNotNull(addedBook);
                Assert.AreEqual(title, addedBook.Title);
                Assert.AreEqual(price, addedBook.Price);

                // Check that the book was added to the internal list
                var BookInList = _bookRepo.GetAll().FirstOrDefault(b => b.Id == addedBook.Id);
                Assert.IsNotNull(BookInList);
                Assert.AreEqual(bookToAdd.Title, BookInList.Title);
                Assert.AreEqual(bookToAdd.Price, BookInList.Price);
            }
            #endregion


            #region RemoveBook field
            [TestMethod]
            [DataRow(1)]
            [DataRow(2)]
            public void Remove_Removes_ExistingBook(int id)
            {
                // Act
                Book? removedBook = _bookRepo.Remove(id);

                // Assert
                Assert.IsNotNull(removedBook);
                Assert.AreEqual(id, removedBook.Id);

                // Check that the book was removed from the internal list
                CollectionAssert.DoesNotContain(_books, removedBook);
            }

            [TestMethod]
            [DataRow(4)]
            [DataRow(5)]
            public void Remove_Returns_NullForNonExistingBook(int id)
            {
                // Act
                Book? removedBook = _bookRepo.Remove(id);

                // Assert
                Assert.IsNull(removedBook);
            }
            #endregion


            #region UpdateBook field
            [TestMethod]
            [DataRow("UpdatedBook", 500)]
            [DataRow("AnotherUpdatedBook", 600)]
            public void Update_Updates_ExistingBook(string newTitle, double newPrice)
            {
                // Arrange
                var repository = new BooksRepository();
                var existingBook = new Book() { Title = "The hunger games 6", Price = 645 };
                repository.Add(existingBook);
                var updatedBook = new Book() { Title = newTitle, Price = newPrice };

                // Act
                var result = repository.Update(existingBook.Id, updatedBook);

                // Assert
                Assert.IsNotNull(result);
                Assert.AreEqual(existingBook.Id, result.Id);
                Assert.AreEqual(existingBook.Title, result.Title);
                Assert.AreEqual(existingBook.Price, result.Price);

                // Check that the book was updated in the internal list
                var updatedBookInRepo = repository.GetById(existingBook.Id);
                Assert.IsNotNull(updatedBookInRepo);
                Assert.AreEqual(updatedBook.Title, updatedBookInRepo.Title);
                Assert.AreEqual(updatedBook.Price, updatedBookInRepo.Price);
            }

            [TestMethod]
            [DataRow(4, "NewBook", 50)] // Test med ikke-eksisterende id
            [DataRow(5, "AnotherNewBook", 60)] // Test med ikke-eksisterende id
            public void Update_Returns_NullForNonExistingBook(int id, string newTitle, double newPrice)
            {
                // Arrange
                Book updatedBook = new Book { Title = newTitle, Price = newPrice };

                // Act
                Book? result = _bookRepo.Update(id, updatedBook);

                // Assert
                Assert.IsNull(result);
            }
            #endregion

        }
    }