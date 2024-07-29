using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    class Book
    {
        //TODO add more properties to the books
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }

        public Book(int id, string title, string author)
        {
            ID = id;
            Title = title;
            Author = author;
            IsBorrowed = false;

        }
        public void PrintBookInfo()
        {
            Console.WriteLine("ID: " + ID);
            Console.WriteLine("Title: " + Title);
            Console.WriteLine("Author: " + Author);
            string borrowedStatus = IsBorrowed ? "Borrowed" : "Available";
            Console.WriteLine("Status: " + borrowedStatus + "\n");
        }
    }
}
