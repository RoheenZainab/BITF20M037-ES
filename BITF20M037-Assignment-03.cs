//Optional arguments
//a.

using System;

class Program
{
    static void Main()
    {
        // Calling Greet with no arguments
        Greet(); // This will display "Hello, World!" on the console

        // Calling Greet with custom arguments
        Greet("Hi", "Alice"); // This will display "Hi, Alice!" on the console
    }

    static void Greet(string greeting = "Hello", string name = "World")
    {
        Console.WriteLine($"{greeting}, {name}!");
    }
}


//b.
using System;

class Program
{
    static void Main()
    {
        // Calling CalculateArea with no arguments
        double defaultArea = CalculateArea(); // This will calculate the area of a 1x1 rectangle (default values)

        // Calling CalculateArea with custom arguments
        double customArea = CalculateArea(4.5, 3.2); // This will calculate the area of a 4.5x3.2 rectangle

        Console.WriteLine($"Default Area: {defaultArea}");
        Console.WriteLine($"Custom Area: {customArea}");
    }

    static double CalculateArea(double length = 1.0, double width = 1.0)
    {
        // Calculate the area of the rectangle
        double area = length * width;
        return area;
    }
}


//c.
using System;

class Program
{
    static void Main()
    {
        // Calling the first overload with two integers
        int sum1 = AddNumbers(5, 7); // This will return 12

        // Calling the second overload with three integers
        int sum2 = AddNumbers(10, 20, 30); // This will return 60

        Console.WriteLine($"Sum1: {sum1}");
        Console.WriteLine($"Sum2: {sum2}");
    }

    // First overload
    static int AddNumbers(int num1, int num2)
    {
        return num1 + num2;
    }

    // Second overload with an optional third parameter
    static int AddNumbers(int num1, int num2, int num3 = 0)
    {
        return num1 + num2 + num3;
    }
}


//d.
using System;

class Book
{
    public string Title { get; }
    public string Author { get; }

    public Book(string title, string author = "Unknown")
    {
        Title = title;
        Author = author;
    }
}

class Program
{
    static void Main()
    {
        // Creating instances of the Book class with and without specifying the author
        Book book1 = new Book("Book 1", "Author 1");
        Book book2 = new Book("Book 2"); // Author will default to "Unknown"

        // Displaying the details of the books
        Console.WriteLine($"Book 1: Title = {book1.Title}, Author = {book1.Author}");
        Console.WriteLine($"Book 2: Title = {book2.Title}, Author = {book2.Author}");
    }
}


//Generics 
//a.
using System;
using System.Collections.Generic;

class MyList<T>
{
    private List<T> items;

    public MyList()
    {
        items = new List<T>();
    }

    public void Add(T item)
    {
        items.Add(item);
    }

    public bool Remove(T item)
    {
        return items.Remove(item);
    }

    public void Display()
    {
        Console.WriteLine("List Elements:");
        foreach (T item in items)
        {
            Console.WriteLine(item);
        }
    }
}

class Program
{
    static void Main()
    {
        // Create an instance of MyList<int> to store integers
        MyList<int> intList = new MyList<int>();
        intList.Add(1);
        intList.Add(2);
        intList.Add(3);
        intList.Display();

        // Create an instance of MyList<string> to store strings
        MyList<string> stringList = new MyList<string>();
        stringList.Add("Apple");
        stringList.Add("Banana");
        stringList.Add("Cherry");
        stringList.Display();

        // Remove an element from the stringList
        stringList.Remove("Banana");
        stringList.Display();
    }
}


//b.
using System;

class Program
{
    static void Main()
    {
        // Testing Swap with integers
        int a = 5;
        int b = 10;
        Console.WriteLine($"Before Swap: a = {a}, b = {b}");
        Swap(ref a, ref b);
        Console.WriteLine($"After Swap: a = {a}, b = {b}");

        // Testing Swap with strings
        string str1 = "Hello";
        string str2 = "World";
        Console.WriteLine($"Before Swap: str1 = {str1}, str2 = {str2}");
        Swap(ref str1, ref str2);
        Console.WriteLine($"After Swap: str1 = {str1}, str2 = {str2}");
    }

    static void Swap<T>(ref T a, ref T b)
    {
        T temp = a;
        a = b;
        b = temp;
    }
}


//c.
using System;

class Program
{
    static void Main()
    {
        int[] intArray = { 1, 2, 3, 4, 5 };
        double[] doubleArray = { 1.1, 2.2, 3.3, 4.4, 5.5 };
        string[] stringArray = { "Hello", "World" };

        int intSum = CalculateSum(intArray);
        double doubleSum = CalculateSum(doubleArray);

        // The following line will result in a compilation error since strings do not support addition.
        // double stringSum = CalculateSum(stringArray);

        Console.WriteLine($"Sum of intArray: {intSum}");
        Console.WriteLine($"Sum of doubleArray: {doubleSum}");
    }

    static T CalculateSum<T>(T[] array) where T : struct, IConvertible, IComparable, IComparable<T>, IEquatable<T>
    {
        if (typeof(T) == typeof(int) || typeof(T) == typeof(long) || typeof(T) == typeof(double))
        {
            dynamic sum = default(T);
            foreach (T element in array)
            {
                sum += element;
            }
            return sum;
        }
        else
        {
            throw new ArgumentException("Unsupported data type for addition.");
        }
    }
}


//d.
using System;
using System.Collections.Generic;

class Program
{
    static Dictionary<int, string> studentDatabase = new Dictionary<int, string>();

    static void Main()
    {
        // Adding initial student records to the dictionary
        studentDatabase.Add(101, "Alice");
        studentDatabase.Add(102, "Bob");
        studentDatabase.Add(103, "Charlie");
        studentDatabase.Add(104, "David");

        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Menu:");
            Console.WriteLine("1. View the student database");
            Console.WriteLine("2. Search for a student by ID");
            Console.WriteLine("3. Update a student's name");
            Console.WriteLine("4. Exit");

            Console.Write("Please select an option (1-4): ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    DisplayStudentDatabase();
                    break;

                case "2":
                    SearchStudentByID();
                    break;

                case "3":
                    UpdateStudentName();
                    break;

                case "4":
                    exit = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    static void DisplayStudentDatabase()
    {
        Console.WriteLine("Student Database:");
        foreach (var student in studentDatabase)
        {
            Console.WriteLine($"Student ID: {student.Key}, Name: {student.Value}");
        }
    }

    static void SearchStudentByID()
    {
        Console.Write("Enter Student ID to search: ");
        if (int.TryParse(Console.ReadLine(), out int studentID))
        {
            if (studentDatabase.ContainsKey(studentID))
            {
                Console.WriteLine($"Student ID: {studentID}, Name: {studentDatabase[studentID]}");
            }
            else
            {
                Console.WriteLine("Student not found in the database.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid Student ID (an integer).");
        }
    }

    static void UpdateStudentName()
    {
        Console.Write("Enter Student ID to update: ");
        if (int.TryParse(Console.ReadLine(), out int studentID))
        {
            if (studentDatabase.ContainsKey(studentID))
            {
                Console.Write("Enter the new name: ");
                string newName = Console.ReadLine();
                studentDatabase[studentID] = newName;
                Console.WriteLine("Student name updated successfully.");
            }
            else
            {
                Console.WriteLine("Student not found in the database.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid Student ID (an integer).");
        }
    }
}
