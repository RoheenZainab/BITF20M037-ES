//1
using System;

class Program
{
    static void Main()
    {
        string firstName, lastName;
        
        Console.Write("Enter your first name: ");
        firstName = Console.ReadLine();
        
        Console.Write("Enter your last name: ");
        lastName = Console.ReadLine();
                
        string fullName = firstName + " " + lastName;
               
        Console.WriteLine("Your full name is: " + fullName);
    }
}


//2
using System;

class Program
{
    static void Main()
    {
        string sentence = "This is an example sentence.";
        DisplayLast5Characters(sentence);
    }

    static void DisplayLast5Characters(string input)
    {
        if (input.Length >= 5)
        {
            string last5Characters = input.Substring(input.Length - 5);
            Console.WriteLine("Last 5 characters: " + last5Characters);
        }
        else
        {
            Console.WriteLine("The input string is too short to extract the last 5 characters.");
        }
    }
}

//3
using System;

class Program
{
    static void Main()
    {
        double temperature;
        string city;

        Console.Write("Enter the current temperature (in degrees Celsius): ");
        if (double.TryParse(Console.ReadLine(), out temperature))
        {
            Console.Write("Enter the name of your city: ");
            city = Console.ReadLine();

            string message = $"The temperature in {city} is {temperature} degrees Celsius.";
            Console.WriteLine(message);
        }
        else
        {
            Console.WriteLine("Invalid temperature input. Please enter a valid number.");
        }
    }
}


//4
using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5 };
        
        Console.WriteLine("Elements of the 'numbers' array:");
        foreach (int number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}


//5
//a
using System;

class Program
{
    static void Main()
    {
        string[] fruits = { "Apple", "Banana", "Orange", "Grapes", "Strawberry" };

        Console.WriteLine("Fruits using for loop:");
        IterateWithForLoop(fruits);
    }

    static void IterateWithForLoop(string[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            Console.WriteLine(array[i]);
        }
    }
}
//b
using System;

class Program
{
    static void Main()
    {
        string[] colors = { "Red", "Blue", "Green", "Yellow", "Orange" };

        Console.WriteLine("Colors using foreach loop:");
        IterateWithForeachLoop(colors);
    }

    static void IterateWithForeachLoop(string[] array)
    {
        foreach (string item in array)
        {
            Console.Write(item + ", ");
        }
        Console.WriteLine(); // Add a line break after printing all colors
    }
}


//6
using System;

class Program
{
    static void Main()
    {
        int[] scores = { 85, 92, 78, 95, 89, 88, 77, 93, 84, 90 };

        Console.WriteLine("Sum of test scores: " + CalculateSum(scores));
    }

    static int CalculateSum(int[] array)
    {
        int sum = 0;
        int index = 0;

        do
        {
            sum += array[index];
            index++;
        } while (index < array.Length);

        return sum;
    }
}


//7
using System;

class Program
{
    static void Main()
    {
        int[] values = { 12, 45, 76, 34, 98, 23, 56, 87, 9, 65 };

        int maxValue = FindMaxValue(values);
        
        Console.WriteLine("Maximum value in the array: " + maxValue);
    }

    static int FindMaxValue(int[] array)
    {
        int max = int.MinValue; // Initialize max to the smallest possible integer value

        int index = 0;
        while (index < array.Length)
        {
            if (array[index] > max)
            {
                max = array[index];
            }
            index++;
        }

        return max;
    }
}


//8
using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

        ReverseArrayInPlace(numbers);

        Console.WriteLine("Reversed array:");
        foreach (int number in numbers)
        {
            Console.Write(number + " ");
        }
    }

    static void ReverseArrayInPlace(int[] array)
    {
        int left = 0;
        int right = array.Length - 1;

        while (left < right)
        {
            int temp = array[left];
            array[left] = array[right];
            array[right] = temp;

            left++;
            right--;
        }
    }
}


//9
//a
using System;

class Program
{
    static void Main()
    {
        int x = 42;
        object boxedValue = x;
        int y = (int)boxedValue;

        Console.WriteLine("Unboxed integer 'y': " + y);
    }
}
//b
using System;

class Program
{
    static void Main()
    {
        double doubleValue = 3.14159;
        object boxedValue = doubleValue;
        double unboxedValue = (double)boxedValue;

        Console.WriteLine("Unboxed double 'unboxedValue': " + unboxedValue);
    }
}


//10
//a
using System;

class Program
{
    static void Main()
    {
        int[] numbers = { 2, 4, 6, 8, 10 };

        foreach (int num in numbers)
        {
            object boxedValue = num;
            int unboxedValue = (int)boxedValue;
            int squaredValue = unboxedValue * unboxedValue;

            Console.WriteLine($"Original: {unboxedValue}, Squared: {squaredValue}");
        }
    }
}
//b
using System;
using System.Collections.Generic;

class Program
{
    static void Main()
    {
        List<object> mixedList = new List<object>();

        mixedList.Add(42);            // Add an integer
        mixedList.Add(3.14159);       // Add a double
        mixedList.Add('A');           // Add a character

        foreach (object item in mixedList)
        {
            if (item is int)
            {
                int intValue = (int)item;
                Console.WriteLine($"Integer: {intValue}");
            }
            else if (item is double)
            {
                double doubleValue = (double)item;
                Console.WriteLine($"Double: {doubleValue}");
            }
            else if (item is char)
            {
                char charValue = (char)item;
                Console.WriteLine($"Character: {charValue}");
            }
        }
    }
}


//11
//a
using System;

class Program
{
    static void Main()
    {
        dynamic myVariable;
        myVariable = 42;
        Console.WriteLine("Value: " + myVariable);

        myVariable = "Hello, Dynamic!";
        Console.WriteLine("Value: " + myVariable);
    }
}
//b
using System;

class Program
{
    static void Main()
    {
        dynamic myVariable2;

        myVariable2 = 42;
        Console.WriteLine("Type: " + myVariable2.GetType());

        myVariable2 = 3.14159;
        Console.WriteLine("Type: " + myVariable2.GetType());

        myVariable2 = DateTime.Now;
        Console.WriteLine("Type: " + myVariable2.GetType());

        myVariable2 = "Hello, Dynamic!";
        Console.WriteLine("Type: " + myVariable2.GetType());
    }
}


