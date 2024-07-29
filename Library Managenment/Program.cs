using System.Diagnostics;
using System.Threading.Tasks;

namespace Library_Management;

class Program
{
    private static bool running = true;
    private static Library currentLibrary;
    
    static void Main(string[] args)
    {
        string? selectedOption;
        InitialiseLibrary();
        //make the tasks persist (should appear when you open a new window) and read it from file system
        while (running)
        {
            PrintMenu();
            selectedOption = Console.ReadLine();
            switch (selectedOption)
            {
                case "1":
                    AddBook();
                    break;
                case "2":
                    currentLibrary.ViewAllBooks();
                    PressToContinue();
                    break;
                case "3":
                    currentLibrary.ViewAvailableBooks();
                    PressToContinue();
                    break;
                case "4":
                    currentLibrary.ViewBorrowedBooks();
                    PressToContinue();
                    break;
                case "5":
                    BorrowBook();
                    break;
                case "6":
                    ReturnBook();
                    break;
                case "7":
                    Console.WriteLine("Exiting the application");
                    running = false;
                    break;
                default:
                    Console.WriteLine($"Invalid option \"{selectedOption}\" chosen! Please select one of the options provided.");
                    continue;
            }
        }
    }

    private static void InitialiseLibrary()
    {
        currentLibrary = new Library("currentLibrary.lb");
        //Console.WriteLine("Loading Library...");
    }

    private static void PrintMenu()
    {
        Console.WriteLine("\nWelcome to the Library Management System" +
            "\n1. Add a new book" +
            "\n2. View all books" +
            "\n3. View available books" +
            "\n4. View borrowed books" +
            "\n5. Borrow a book" +
            "\n6. Return a book" +
            "\n7. Exit\n");
    }
    private static void AddBook()
    {
        int id = getValidBookID("Enter book ID");
        //Decided to place it before the user enters any more info
        if (currentLibrary.ContainsID(id))
        {
            Console.WriteLine("Library already has a book with this ID!");
            return;
        }
        string? enteredTitle = "";
        while (enteredTitle.Equals(""))
        {
            Console.WriteLine("Enter book title");
            enteredTitle = Console.ReadLine();
        }

        string? enteredAuthor = "";
        while (enteredAuthor.Equals(""))
        {
            Console.WriteLine("Enter book author");
            enteredAuthor = Console.ReadLine();
        }
        currentLibrary.AddBook(new Book(id,enteredTitle,enteredAuthor),true);
        currentLibrary.SaveToFile();
        PressToContinue();
    }

    private static int getValidBookID(String promptText)
    {
        Console.WriteLine(promptText);
        string? enteredID = Console.ReadLine();
        int id = -1;
        bool validID = int.TryParse(enteredID, out id);
        //if the try parse fails, then the ID will always be -1, so won't pass as valid
        validID = validID && id >= 0;
        while (!validID)
        {
            Console.WriteLine("Invalid ID entered. Enter a non-negative integer.");
            enteredID = Console.ReadLine();
            validID = int.TryParse(enteredID, out id);
        }
        return id;
    }
    private static void BorrowBook()
    {
        ExecuteBookTransaction("borrow", b => currentLibrary.BorrowBook(b));
    }

    //if true is passed in, it treats it as borrowing and if false is passed in, as returning
    private static void ReturnBook()
    {
        ExecuteBookTransaction("return", b => currentLibrary.ReturnBook(b));
    }

    private static void ExecuteBookTransaction(string transactionName, Action<int> action)
    {
        if (currentLibrary.GetBookCount() == 0)
        {
            Console.WriteLine("No books in library");
            PressToContinue();
            return;
        }
        int id = getValidBookID("Enter book ID to " + transactionName);
        action(id);
        currentLibrary.SaveToFile();
        PressToContinue();
    }

    private static void PressToContinue()
    {
        Console.WriteLine("Press any key to continue");
        Console.ReadKey();
    }
}
