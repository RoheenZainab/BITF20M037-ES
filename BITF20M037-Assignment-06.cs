//1
//Example1
public class SimpleSingleton
{
    private static SimpleSingleton instance;

    private SimpleSingleton() { }

    public static SimpleSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SimpleSingleton();
            }
            return instance;
        }
    }
}

//Example2
public class ThreadSafeSingleton
{
    private static ThreadSafeSingleton instance;
    private static readonly object lockObject = new object();

    private ThreadSafeSingleton() { }

    public static ThreadSafeSingleton Instance
    {
        get
        {
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        instance = new ThreadSafeSingleton();
                    }
                }
            }
            return instance;
        }
    }
}



//2
//Example1
// Product interface
public interface IProduct
{
    void Display();
}

// Concrete Product A
public class ConcreteProductA : IProduct
{
    public void Display()
    {
        Console.WriteLine("Concrete Product A");
    }
}

// Concrete Product B
public class ConcreteProductB : IProduct
{
    public void Display()
    {
        Console.WriteLine("Concrete Product B");
    }
}

// Factory class
public class SimpleFactory
{
    public IProduct CreateProduct(string productType)
    {
        switch (productType)
        {
            case "A":
                return new ConcreteProductA();
            case "B":
                return new ConcreteProductB();
            default:
                throw new ArgumentException("Invalid product type");
        }
    }
}

//Example2
// Product interface
public interface IProduct
{
    void Display();
}

// Concrete Product A
public class ConcreteProductA : IProduct
{
    public void Display()
    {
        Console.WriteLine("Concrete Product A");
    }
}

// Concrete Product B
public class ConcreteProductB : IProduct
{
    public void Display()
    {
        Console.WriteLine("Concrete Product B");
    }
}

// Creator interface
public interface ICreator
{
    IProduct CreateProduct();
}

// Concrete Creator A
public class ConcreteCreatorA : ICreator
{
    public IProduct CreateProduct()
    {
        return new ConcreteProductA();
    }
}

// Concrete Creator B
public class ConcreteCreatorB : ICreator
{
    public IProduct CreateProduct()
    {
        return new ConcreteProductB();
    }
}



//3
//Example1
// Abstract product A
public interface IProductA
{
    void Display();
}

// Concrete product A1
public class ConcreteProductA1 : IProductA
{
    public void Display()
    {
        Console.WriteLine("Concrete Product A1");
    }
}

// Concrete product A2
public class ConcreteProductA2 : IProductA
{
    public void Display()
    {
        Console.WriteLine("Concrete Product A2");
    }
}

// Abstract product B
public interface IProductB
{
    void Print();
}

// Concrete product B1
public class ConcreteProductB1 : IProductB
{
    public void Print()
    {
        Console.WriteLine("Concrete Product B1");
    }
}

// Concrete product B2
public class ConcreteProductB2 : IProductB
{
    public void Print()
    {
        Console.WriteLine("Concrete Product B2");
    }
}

// Abstract factory
public interface IAbstractFactory
{
    IProductA CreateProductA();
    IProductB CreateProductB();
}

// Concrete factory 1
public class ConcreteFactory1 : IAbstractFactory
{
    public IProductA CreateProductA()
    {
        return new ConcreteProductA1();
    }

    public IProductB CreateProductB()
    {
        return new ConcreteProductB1();
    }
}

// Concrete factory 2
public class ConcreteFactory2 : IAbstractFactory
{
    public IProductA CreateProductA()
    {
        return new ConcreteProductA2();
    }

    public IProductB CreateProductB()
    {
        return new ConcreteProductB2();
    }
}

//Example2
// Abstract product Shape
public interface IShape
{
    void Draw();
}

// Concrete product Circle
public class Circle : IShape
{
    public void Draw()
    {
        Console.WriteLine("Drawing Circle");
    }
}

// Concrete product Square
public class Square : IShape
{
    public void Draw()
    {
        Console.WriteLine("Drawing Square");
    }
}

// Abstract product Color
public interface IColor
{
    void Fill();
}

// Concrete product Red
public class Red : IColor
{
    public void Fill()
    {
        Console.WriteLine("Filling with Red color");
    }
}

// Concrete product Blue
public class Blue : IColor
{
    public void Fill()
    {
        Console.WriteLine("Filling with Blue color");
    }
}

// Abstract factory
public interface IAbstractShapeAndColorFactory
{
    IShape CreateShape();
    IColor CreateColor();
}

// Concrete factory for creating Red shapes
public class RedShapeFactory : IAbstractShapeAndColorFactory
{
    public IShape CreateShape()
    {
        return new Circle();
    }

    public IColor CreateColor()
    {
        return new Red();
    }
}

// Concrete factory for creating Blue shapes
public class BlueShapeFactory : IAbstractShapeAndColorFactory
{
    public IShape CreateShape()
    {
        return new Square();
    }

    public IColor CreateColor()
    {
        return new Blue();
    }
}



//4
//Example1
// Product
public class Product
{
    private string part1;
    private string part2;

    public void SetPart1(string part)
    {
        part1 = part;
    }

    public void SetPart2(string part)
    {
        part2 = part;
    }

    public void Display()
    {
        Console.WriteLine($"Part 1: {part1}, Part 2: {part2}");
    }
}

// Builder interface
public interface IBuilder
{
    void BuildPart1();
    void BuildPart2();
    Product GetResult();
}

// Concrete Builder
public class ConcreteBuilder : IBuilder
{
    private Product product = new Product();

    public void BuildPart1()
    {
        product.SetPart1("Builder Part 1");
    }

    public void BuildPart2()
    {
        product.SetPart2("Builder Part 2");
    }

    public Product GetResult()
    {
        return product;
    }
}

// Director
public class Director
{
    public void Construct(IBuilder builder)
    {
        builder.BuildPart1();
        builder.BuildPart2();
    }
}

//Example2
// Product
public class Meal
{
    private List<string> items = new List<string>();

    public void AddItem(string item)
    {
        items.Add(item);
    }

    public void Display()
    {
        Console.WriteLine("Meal items: " + string.Join(", ", items));
    }
}

// Builder interface
public interface IMealBuilder
{
    void BuildMainCourse();
    void BuildSide();
    void BuildDrink();
    Meal GetResult();
}

// Concrete Builder for a Veg Meal
public class VegMealBuilder : IMealBuilder
{
    private Meal meal = new Meal();

    public void BuildMainCourse()
    {
        meal.AddItem("Veg Burger");
    }

    public void BuildSide()
    {
        meal.AddItem("Fries");
    }

    public void BuildDrink()
    {
        meal.AddItem("Coke");
    }

    public Meal GetResult()
    {
        return meal;
    }
}

// Concrete Builder for a Non-Veg Meal
public class NonVegMealBuilder : IMealBuilder
{
    private Meal meal = new Meal();

    public void BuildMainCourse()
    {
        meal.AddItem("Chicken Burger");
    }

    public void BuildSide()
    {
        meal.AddItem("Onion Rings");
    }

    public void BuildDrink()
    {
        meal.AddItem("Pepsi");
    }

    public Meal GetResult()
    {
        return meal;
    }
}

// Director
public class Waiter
{
    public Meal Construct(IMealBuilder builder)
    {
        builder.BuildMainCourse();
        builder.BuildSide();
        builder.BuildDrink();
        return builder.GetResult();
    }
}



//5
//Example1
using System;

// Prototype interface
public interface ICloneablePrototype
{
    ICloneablePrototype Clone();
}

// Concrete prototype
public class ConcretePrototype : ICloneablePrototype
{
    private string data;

    public ConcretePrototype(string data)
    {
        this.data = data;
    }

    public ICloneablePrototype Clone()
    {
        return new ConcretePrototype(this.data);
    }

    public void Display()
    {
        Console.WriteLine($"Data: {data}");
    }
}

// Client
public class Client
{
    public void Operation(ICloneablePrototype prototype)
    {
        ICloneablePrototype clone = prototype.Clone();
        clone.Display();
    }
}

//Example2
using System;
using System.Collections.Generic;

// Prototype interface
public interface IPrototype
{
    IPrototype Clone();
}

// Concrete prototype
public class ConcretePrototype : IPrototype
{
    private string data;

    public ConcretePrototype(string data)
    {
        this.data = data;
    }

    public IPrototype Clone()
    {
        return new ConcretePrototype(this.data);
    }

    public void Display()
    {
        Console.WriteLine($"Data: {data}");
    }
}

// Prototype Registry
public class PrototypeRegistry
{
    private Dictionary<string, IPrototype> prototypes = new Dictionary<string, IPrototype>();

    public void AddPrototype(string key, IPrototype prototype)
    {
        prototypes[key] = prototype;
    }

    public IPrototype GetPrototype(string key)
    {
        if (prototypes.ContainsKey(key))
        {
            return prototypes[key].Clone();
        }
        else
        {
            Console.WriteLine($"Prototype with key '{key}' not found.");
            return null;
        }
    }
}

// Client
public class Client
{
    public void Operation(PrototypeRegistry registry, string prototypeKey)
    {
        IPrototype prototype = registry.GetPrototype(prototypeKey);
        if (prototype != null)
        {
            prototype.Display();
        }
    }
}

