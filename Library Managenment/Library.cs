using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library_Management
{
    class Library
    {
        private Dictionary<int,Book> books;
        private string filePath;
        public const string fileDelimiter = "\t";
        public void AddBook(Book book,bool showFeedback)
        {
            books[book.ID] = book;
            if (showFeedback)
            {
                Console.WriteLine("Book added successfully!");
            }
        }

        public void ViewAllBooks()
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

        public Library(string filePath)
        {
            books = new Dictionary<int, Book>();
            this.filePath = filePath;
            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    string? line;
                    int lineCount = 0;
                    while ((line = reader.ReadLine()) != null)
                    {
                        lineCount++;
                        string[] bookInfo = line.Split("\t");
                        try
                        {
                            AddBook(new Book(Convert.ToInt32(bookInfo[0]), bookInfo[1], bookInfo[2], Convert.ToBoolean(bookInfo[3])),false);
                        }
                        catch (FormatException)
                        {
                            //ignore empty lines
                            if (!line.Equals(""))
                            {
                                Console.WriteLine("Invalid format for book on line " + lineCount);
                            }
                        }
                        catch (IndexOutOfRangeException)
                        {
                            Console.WriteLine("Insufficient info for book on line " + lineCount);
                        }

                    }
                }
            }
            catch (FileNotFoundException)
            {
                File.Create(filePath).Close();
            }
        }

        public int GetBookCount()
        {
            return books.Count;
        }
        
        //Allows the same method to be used to view the available books as well as the borrowed books
        private void ViewSpecificBookType(bool isBorrowed)
        {
            string bookType = isBorrowed ? "Borrowed" : "Available";
            //think of a way to do this without iterating over the list twice.
            int bookCount = 0;
            foreach (KeyValuePair<int, Book> bookPairing in books)
            {
                if (bookPairing.Value.IsBorrowed == isBorrowed) bookCount++;
            }
            if (bookCount <= 0)
            {
                Console.WriteLine("\nNo " + bookType.ToLower() + " books in library");
                return;
            }
            Console.WriteLine("\n" + bookType + " Books in the library:");

            foreach (KeyValuePair<int, Book> bookPairing in books)
            {
                if (bookPairing.Value.IsBorrowed == isBorrowed)
                {
                    bookPairing.Value.PrintBookInfo();
                }
            }
        }

        public void ViewAvailableBooks()
        {
            ViewSpecificBookType(false);
        }

        public void ViewBorrowedBooks()
        {
            ViewSpecificBookType(true);
        }

        public void SaveToFile()
        {
            string[] tasksAsStrings = books.Select(b => b.Value.ToString()).ToArray();
            File.WriteAllLines(filePath, tasksAsStrings);
        }

        //make multiple libraries possible and each library is defined by the file name.
        //Define set folder to store all libraries
        //store 
        //view books in stock
        //make them login and then be able to view the books they've borrowed personally
        //TODO List all borrowed books and list all 
    }
}
