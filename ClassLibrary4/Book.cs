using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary4
{
    public class Book
    {
        #region Properties field
        public int Id { get; set; }
        public string Title { get; set; }
        public double Price { get; set; }
        #endregion

        #region Defuelt konstruktør field
        public Book()
        {

        }
        #endregion


        #region Konstruktør field
        public Book(int id, string title, double price)
        {
            Id = id;
            Title = title;
            Price = price;
        }
        #endregion


        #region ToString method field
        public override string ToString()
        {
            return $"{{{nameof(Id)}={Id.ToString()}, {nameof(Title)}={Title}, {nameof(Price)}={Price.ToString()}}}";
        }
        #endregion


        #region ValidateTitle field
        public void validateTitle()
        {
            if (Title == null) 
            {
                throw new ArgumentNullException("title is null");
            }

            if (Title.Length <= 3)
            {
                throw new ArgumentException("Not null and a least 3 character" + Title);
            }
        }
        #endregion


        #region ValidatePrice field
        public void validatePrice()
        {
            if (Price < 0 || Price > 1200)
            {
                throw new ArgumentOutOfRangeException("price has to be between 0 and 1200" + Price);
            }
        }
        #endregion

        #region Validate field
        public void validate()
        {
            validateTitle();
            validatePrice();    
        }
        #endregion
    }
}
