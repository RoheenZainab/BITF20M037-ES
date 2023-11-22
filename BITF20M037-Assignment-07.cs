//1
//Example1
using System;

// Target interface
public interface ITarget
{
    void Request();
}

// Adaptee (existing class to be adapted)
public class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Specific Request");
    }
}

// Adapter class (object adapter)
public class ObjectAdapter : ITarget
{
    private Adaptee adaptee;

    public ObjectAdapter(Adaptee adaptee)
    {
        this.adaptee = adaptee;
    }

    public void Request()
    {
        adaptee.SpecificRequest();
    }
}

// Client
public class Client
{
    public void Main()
    {
        Adaptee adaptee = new Adaptee();
        ITarget target = new ObjectAdapter(adaptee);
        target.Request();
    }
}


//Example2
using System;

// Target interface
public interface ITarget
{
    void Request();
}

// Adaptee (existing class to be adapted)
public class Adaptee
{
    public void SpecificRequest()
    {
        Console.WriteLine("Specific Request");
    }
}

// Adapter class (class adapter)
public class ClassAdapter : Adaptee, ITarget
{
    public void Request()
    {
        SpecificRequest();
    }
}

// Client
public class Client
{
    public void Main()
    {
        ITarget target = new ClassAdapter();
        target.Request();
    }
}



//2
//Example1
using System;
using System.Collections.Generic;

// Component interface
public abstract class Graphic
{
    public abstract void Draw();
}

// Leaf class
public class Circle : Graphic
{
    public override void Draw()
    {
        Console.WriteLine("Drawing Circle");
    }
}

// Leaf class
public class Square : Graphic
{
    public override void Draw()
    {
        Console.WriteLine("Drawing Square");
    }
}

// Composite class
public class CompositeGraphic : Graphic
{
    private List<Graphic> graphics = new List<Graphic>();

    public void Add(Graphic graphic)
    {
        graphics.Add(graphic);
    }

    public void Remove(Graphic graphic)
    {
        graphics.Remove(graphic);
    }

    public override void Draw()
    {
        Console.WriteLine("Drawing Composite Graphic");
        foreach (var graphic in graphics)
        {
            graphic.Draw();
        }
    }
}

// Client
public class Client
{
    public void DrawAllGraphics(Graphic graphic)
    {
        graphic.Draw();
    }
}


//Example2
using System;
using System.Collections.Generic;

// Component interface
public abstract class DocumentElement
{
    public abstract void Print();
}

// Leaf class
public class Text : DocumentElement
{
    private string content;

    public Text(string content)
    {
        this.content = content;
    }

    public override void Print()
    {
        Console.WriteLine($"Text: {content}");
    }
}

// Leaf class
public class Image : DocumentElement
{
    private string source;

    public Image(string source)
    {
        this.source = source;
    }

    public override void Print()
    {
        Console.WriteLine($"Image: {source}");
    }
}

// Composite class
public class Section : DocumentElement
{
    private List<DocumentElement> elements = new List<DocumentElement>();

    public void Add(DocumentElement element)
    {
        elements.Add(element);
    }

    public void Remove(DocumentElement element)
    {
        elements.Remove(element);
    }

    public override void Print()
    {
        Console.WriteLine("Section:");
        foreach (var element in elements)
        {
            element.Print();
        }
    }
}

// Client
public class Client
{
    public void PrintDocument(DocumentElement documentElement)
    {
        documentElement.Print();
    }
}



//3
//Example1
using System;

// Subject interface
public interface IImage
{
    void Display();
}

// RealSubject class
public class RealImage : IImage
{
    private string filename;

    public RealImage(string filename)
    {
        this.filename = filename;
        LoadImageFromDisk();
    }

    private void LoadImageFromDisk()
    {
        Console.WriteLine($"Loading image from disk: {filename}");
    }

    public void Display()
    {
        Console.WriteLine($"Displaying image: {filename}");
    }
}

// Proxy class
public class ProxyImage : IImage
{
    private RealImage realImage;
    private string filename;

    public ProxyImage(string filename)
    {
        this.filename = filename;
    }

    public void Display()
    {
        if (realImage == null)
        {
            realImage = new RealImage(filename);
        }
        realImage.Display();
    }
}

// Client
public class Client
{
    public void DisplayImage(IImage image)
    {
        image.Display();
    }
}


//Example2
using System;

// Subject interface
public interface ISensitiveData
{
    void AccessData();
}

// RealSubject class
public class RealSensitiveData : ISensitiveData
{
    public void AccessData()
    {
        Console.WriteLine("Accessing sensitive data...");
    }
}

// Proxy class
public class ProtectionProxy : ISensitiveData
{
    private RealSensitiveData realSensitiveData;
    private string password;

    public ProtectionProxy(string password)
    {
        this.password = password;
    }

    private bool AuthenticateUser()
    {
        // Simulate user authentication logic
        return password == "secret";
    }

    public void AccessData()
    {
        if (AuthenticateUser())
        {
            if (realSensitiveData == null)
            {
                realSensitiveData = new RealSensitiveData();
            }
            realSensitiveData.AccessData();
        }
        else
        {
            Console.WriteLine("Authentication failed. Access denied.");
        }
    }
}

// Client
public class Client
{
    public void TryAccessData(ISensitiveData sensitiveData)
    {
        sensitiveData.AccessData();
    }
}



//4
//Example1
using System;
using System.Collections.Generic;

// Flyweight interface
public interface ICharacterFormat
{
    void Format(string text);
}

// Concrete Flyweight
public class CharacterFormat : ICharacterFormat
{
    private char character;
    private ConsoleColor color;

    public CharacterFormat(char character, ConsoleColor color)
    {
        this.character = character;
        this.color = color;
    }

    public void Format(string text)
    {
        Console.ForegroundColor = color;
        Console.Write($"{character}:{text} ");
        Console.ResetColor();
    }
}

// Flyweight Factory
public class CharacterFormatFactory
{
    private Dictionary<char, ICharacterFormat> characterFormats = new Dictionary<char, ICharacterFormat>();

    public ICharacterFormat GetCharacterFormat(char character, ConsoleColor color)
    {
        if (!characterFormats.ContainsKey(character))
        {
            characterFormats[character] = new CharacterFormat(character, color);
        }
        return characterFormats[character];
    }
}

// Client
public class TextEditor
{
    private CharacterFormatFactory formatFactory = new CharacterFormatFactory();

    public void FormatText(string text)
    {
        foreach (char c in text)
        {
            ICharacterFormat characterFormat = formatFactory.GetCharacterFormat(c, ConsoleColor.Green);
            characterFormat.Format(text);
        }
        Console.WriteLine(); // New line for better readability
    }
}


//Example2
using System;
using System.Collections.Generic;

// Flyweight interface
public interface IMusicInstrument
{
    void Play(string note);
}

// Concrete Flyweight
public class Guitar : IMusicInstrument
{
    public void Play(string note)
    {
        Console.WriteLine($"Playing guitar note: {note}");
    }
}

// Flyweight Factory
public class MusicInstrumentFactory
{
    private Dictionary<string, IMusicInstrument> instruments = new Dictionary<string, IMusicInstrument>();

    public IMusicInstrument GetInstrument(string instrumentType)
    {
        if (!instruments.ContainsKey(instrumentType))
        {
            switch (instrumentType)
            {
                case "guitar":
                    instruments[instrumentType] = new Guitar();
                    break;
                // Additional instrument types can be added here
                default:
                    throw new ArgumentException($"Unknown instrument type: {instrumentType}");
            }
        }
        return instruments[instrumentType];
    }
}

// Client
public class MusicPlayer
{
    private MusicInstrumentFactory instrumentFactory = new MusicInstrumentFactory();

    public void PlayMusic(string instrumentType, string notes)
    {
        IMusicInstrument instrument = instrumentFactory.GetInstrument(instrumentType);

        foreach (char note in notes)
        {
            instrument.Play(note.ToString());
        }
        Console.WriteLine(); // New line for better readability
    }
}



//5
//Example1
using System;

// Subsystem components
public class Amplifier
{
    public void TurnOn()
    {
        Console.WriteLine("Amplifier is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("Amplifier is OFF");
    }
}

public class DvdPlayer
{
    public void Play()
    {
        Console.WriteLine("DVD Player is playing");
    }

    public void Stop()
    {
        Console.WriteLine("DVD Player stopped");
    }
}

public class Projector
{
    public void TurnOn()
    {
        Console.WriteLine("Projector is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("Projector is OFF");
    }
}

// Facade
public class HomeTheaterFacade
{
    private Amplifier amplifier;
    private DvdPlayer dvdPlayer;
    private Projector projector;

    public HomeTheaterFacade(Amplifier amplifier, DvdPlayer dvdPlayer, Projector projector)
    {
        this.amplifier = amplifier;
        this.dvdPlayer = dvdPlayer;
        this.projector = projector;
    }

    public void WatchMovie()
    {
        Console.WriteLine("Get ready to watch a movie...");
        amplifier.TurnOn();
        dvdPlayer.Play();
        projector.TurnOn();
    }

    public void EndMovie()
    {
        Console.WriteLine("Shutting down the home theater...");
        amplifier.TurnOff();
        dvdPlayer.Stop();
        projector.TurnOff();
    }
}

// Client
public class Client
{
    public void EnjoyMovie(HomeTheaterFacade homeTheater)
    {
        homeTheater.WatchMovie();
        Console.WriteLine("Movie is playing...");
        homeTheater.EndMovie();
    }
}


//Example2
using System;

// Subsystem components
public class CPU
{
    public void Start()
    {
        Console.WriteLine("CPU is starting");
    }

    public void Shutdown()
    {
        Console.WriteLine("CPU is shutting down");
    }
}

public class Memory
{
    public void Initialize()
    {
        Console.WriteLine("Memory is initializing");
    }

    public void Release()
    {
        Console.WriteLine("Memory is releasing resources");
    }
}

public class HardDrive
{
    public void Spin()
    {
        Console.WriteLine("Hard Drive is spinning");
    }

    public void Stop()
    {
        Console.WriteLine("Hard Drive stopped");
    }
}

// Facade
public class ComputerFacade
{
    private CPU cpu;
    private Memory memory;
    private HardDrive hardDrive;

    public ComputerFacade(CPU cpu, Memory memory, HardDrive hardDrive)
    {
        this.cpu = cpu;
        this.memory = memory;
        this.hardDrive = hardDrive;
    }

    public void StartComputer()
    {
        Console.WriteLine("Starting the computer...");
        cpu.Start();
        memory.Initialize();
        hardDrive.Spin();
        Console.WriteLine("Computer started successfully");
    }

    public void ShutDownComputer()
    {
        Console.WriteLine("Shutting down the computer...");
        cpu.Shutdown();
        memory.Release();
        hardDrive.Stop();
        Console.WriteLine("Computer shut down");
    }
}

// Client
public class Client
{
    public void OperateComputer(ComputerFacade computerFacade)
    {
        computerFacade.StartComputer();
        // Computer is operational...
        computerFacade.ShutDownComputer();
    }
}



//6
//Example1
using System;

// Implementor interface
public interface IDrawAPI
{
    void DrawCircle(int radius, int x, int y);
}

// Concrete Implementor A
public class DrawAPI1 : IDrawAPI
{
    public void DrawCircle(int radius, int x, int y)
    {
        Console.WriteLine($"Drawing Circle[API1] at ({x}, {y}) with radius {radius}");
    }
}

// Concrete Implementor B
public class DrawAPI2 : IDrawAPI
{
    public void DrawCircle(int radius, int x, int y)
    {
        Console.WriteLine($"Drawing Circle[API2] at ({x}, {y}) with radius {radius}");
    }
}

// Abstraction
public abstract class Shape
{
    protected IDrawAPI drawAPI;

    protected Shape(IDrawAPI drawAPI)
    {
        this.drawAPI = drawAPI;
    }

    public abstract void Draw();
}

// Refined Abstraction
public class Circle : Shape
{
    private int radius;
    private int x;
    private int y;

    public Circle(int radius, int x, int y, IDrawAPI drawAPI) : base(drawAPI)
    {
        this.radius = radius;
        this.x = x;
        this.y = y;
    }

    public override void Draw()
    {
        drawAPI.DrawCircle(radius, x, y);
    }
}

// Client
public class Client
{
    public void DrawCircleWithAPI(Shape shape)
    {
        shape.Draw();
    }
}


//Example2
using System;

// Implementor interface
public interface IDevice
{
    void TurnOn();
    void TurnOff();
    void SetChannel(int channel);
}

// Concrete Implementor A
public class TV : IDevice
{
    private int channel;

    public void TurnOn()
    {
        Console.WriteLine("TV is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("TV is OFF");
    }

    public void SetChannel(int channel)
    {
        this.channel = channel;
        Console.WriteLine($"Set TV channel to {channel}");
    }
}

// Concrete Implementor B
public class Radio : IDevice
{
    private int channel;

    public void TurnOn()
    {
        Console.WriteLine("Radio is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("Radio is OFF");
    }

    public void SetChannel(int channel)
    {
        this.channel = channel;
        Console.WriteLine($"Set Radio channel to {channel}");
    }
}

// Abstraction
public abstract class RemoteControl
{
    protected IDevice device;

    protected RemoteControl(IDevice device)
    {
        this.device = device;
    }

    public abstract void TurnOn();
    public abstract void TurnOff();
    public abstract void SetChannel(int channel);
}

// Refined Abstraction
public class BasicRemoteControl : RemoteControl
{
    public BasicRemoteControl(IDevice device) : base(device)
    {
    }

    public override void TurnOn()
    {
        device.TurnOn();
    }

    public override void TurnOff()
    {
        device.TurnOff();
    }

    public override void SetChannel(int channel)
    {
        device.SetChannel(channel);
    }
}

// Client
public class Client
{
    public void UseRemoteControl(RemoteControl remoteControl)
    {
        remoteControl.TurnOn();
        remoteControl.SetChannel(5);
        remoteControl.TurnOff();
    }
}



//7
//Example1
using System;

// Component interface
public interface ICoffee
{
    string GetDescription();
    double GetCost();
}

// Concrete component
public class SimpleCoffee : ICoffee
{
    public string GetDescription()
    {
        return "Simple Coffee";
    }

    public double GetCost()
    {
        return 2.0;
    }
}

// Decorator
public abstract class CoffeeDecorator : ICoffee
{
    protected ICoffee decoratedCoffee;

    protected CoffeeDecorator(ICoffee coffee)
    {
        this.decoratedCoffee = coffee;
    }

    public virtual string GetDescription()
    {
        return decoratedCoffee.GetDescription();
    }

    public virtual double GetCost()
    {
        return decoratedCoffee.GetCost();
    }
}

// Concrete Decorator A
public class MilkDecorator : CoffeeDecorator
{
    public MilkDecorator(ICoffee coffee) : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Milk";
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.5;
    }
}

// Concrete Decorator B
public class SugarDecorator : CoffeeDecorator
{
    public SugarDecorator(ICoffee coffee) : base(coffee)
    {
    }

    public override string GetDescription()
    {
        return $"{base.GetDescription()}, Sugar";
    }

    public override double GetCost()
    {
        return base.GetCost() + 0.2;
    }
}

// Client
public class CoffeeShop
{
    public void ServeCoffee(ICoffee coffee)
    {
        Console.WriteLine($"Order: {coffee.GetDescription()}");
        Console.WriteLine($"Cost: ${coffee.GetCost()}");
    }
}


//Example2
using System;

// Component interface
public interface INotifier
{
    void SendNotification(string message);
}

// Concrete component
public class EmailNotifier : INotifier
{
    public void SendNotification(string message)
    {
        Console.WriteLine($"Sending email notification: {message}");
    }
}

// Decorator
public abstract class NotifierDecorator : INotifier
{
    protected INotifier decoratedNotifier;

    protected NotifierDecorator(INotifier notifier)
    {
        this.decoratedNotifier = notifier;
    }

    public virtual void SendNotification(string message)
    {
        decoratedNotifier.SendNotification(message);
    }
}

// Concrete Decorator A
public class SMSDecorator : NotifierDecorator
{
    public SMSDecorator(INotifier notifier) : base(notifier)
    {
    }

    public override void SendNotification(string message)
    {
        base.SendNotification(message);
        Console.WriteLine($"Sending SMS notification: {message}");
    }
}

// Concrete Decorator B
public class SlackDecorator : NotifierDecorator
{
    public SlackDecorator(INotifier notifier) : base(notifier)
    {
    }

    public override void SendNotification(string message)
    {
        base.SendNotification(message);
        Console.WriteLine($"Sending Slack notification: {message}");
    }
}

// Client
public class NotificationClient
{
    public void NotifyUser(INotifier notifier, string message)
    {
        notifier.SendNotification(message);
    }
}
