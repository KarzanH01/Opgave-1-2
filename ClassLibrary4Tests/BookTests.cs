using Microsoft.VisualStudio.TestTools.UnitTesting;
using ClassLibrary4;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace ClassLibrary4.Tests
{
    [TestClass()]
    public class BookTests
    {
        #region BookTest field
        private readonly Book _book = new() { Id = 1, Title = "Learn to code", Price = 295 };
        private readonly Book _nullTitle = new() { Id = 2, Price = 155 };
        private readonly Book _lessThan3 = new Book() { Id = 3, Title = "mo", Price = 249 };
        private readonly Book _price = new Book() { Id = 4, Title = "The hunger games", Price = 1399 };
        #endregion


        #region ToStringTest field
        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual("{Id=1, Title=Learn to code, Price=295}", _book.ToString());
        }
        #endregion


        #region ValidateTitle field
        [TestMethod()]
        public void validateTitleTest()
        {
            _book.validateTitle();
            Assert.ThrowsException<ArgumentException>(() => _lessThan3.validateTitle());
            Assert.ThrowsException<ArgumentNullException>(() => _nullTitle.validateTitle());
        }
        #endregion


        #region ValidatePrice field
        [TestMethod()]
        public void validatePriceTest()
        {
            _book.validatePrice();
            Assert.ThrowsException<ArgumentOutOfRangeException>(() => _price.validatePrice());
        }
        #endregion


        #region ValidateTest field
        [TestMethod()]
        public void validateTest()
        {
            _book.validate();
        }
        #endregion
    }
}