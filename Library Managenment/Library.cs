using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    class Library
    {
        private Dictionary<int,Book> books;
        public void AddBook(Book book)
        {
            books[book.ID] = book;
            Console.WriteLine("Book added successfully!");
        }

        public void ViewBooks()
        {
            if(books.Count <= 0)
            {
                Console.WriteLine("\nNo books in library");
                return;
            }
            Console.WriteLine("\nBooks in the library:");
            foreach (KeyValuePair<int, Book> bookPairing in books)
            {
                bookPairing.Value.PrintBookInfo();
            }

        }

        public void BorrowBook(int id){
            if (ContainsID(id))
            {
                if (books[id].IsBorrowed)
                {
                    Console.WriteLine("Book had already been borrowed.");
                }
                else
                {
                    books[id].IsBorrowed = true;
                    Console.WriteLine("Book borrowed successfully!");
                }
            }
            else
            {
                Console.WriteLine("Book with ID {0} not found in Library.",id);
            }
        }


        public void ReturnBook(int id)
        {
            if (ContainsID(id))
            {
                if (!books[id].IsBorrowed)
                {
                    Console.WriteLine("Book cannot be returned as it has not been borrowed.");
                }
                else
                {
                    books[id].IsBorrowed = false;
                    Console.WriteLine("Book returned successfully!");
                }
            }
            else
            {
                Console.WriteLine("Book with ID {0} not found in Library.", id);
            }
        }

        public bool ContainsID(int id)
        {
            return books.ContainsKey(id);
        }

        public Library()
        {
            books = new Dictionary<int, Book>();
        }

        public int GetBookCount()
        {
            return books.Count;
        }

        //make multiple libraries possible and each library is defined by the file name.
        //Define set folder to store all libraries
        //store 
        //view books in stock
        //make them login and then be able to view the books they've borrowed personally
        //TODO List all borrowed books and list all 
    }
}
