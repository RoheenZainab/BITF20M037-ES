//1
//Example1
using System;

// Abstract class with the template method
public abstract class CoffeeTemplate
{
    // Template method defining the algorithm
    public void BrewCoffee()
    {
        BoilWater();
        BrewGroundCoffee();
        PourInCup();
        AddCondiments();
        Console.WriteLine("Coffee is ready!");
    }

    // Primitive operations (to be implemented by subclasses)
    protected abstract void BoilWater();
    protected abstract void BrewGroundCoffee();
    protected abstract void PourInCup();

    // Hook method (optional operation with a default implementation)
    protected virtual void AddCondiments()
    {
        Console.WriteLine("Adding sugar and milk");
    }
}

// Concrete class implementing the template method
public class BlackCoffee : CoffeeTemplate
{
    protected override void BoilWater()
    {
        Console.WriteLine("Boiling water");
    }

    protected override void BrewGroundCoffee()
    {
        Console.WriteLine("Brewing black coffee");
    }

    protected override void PourInCup()
    {
        Console.WriteLine("Pouring black coffee into cup");
    }
}

// Concrete class implementing the template method with different condiments
public class CoffeeWithCondiments : CoffeeTemplate
{
    protected override void BoilWater()
    {
        Console.WriteLine("Boiling water");
    }

    protected override void BrewGroundCoffee()
    {
        Console.WriteLine("Brewing coffee with condiments");
    }

    protected override void PourInCup()
    {
        Console.WriteLine("Pouring coffee with condiments into cup");
    }

    protected override void AddCondiments()
    {
        Console.WriteLine("Adding sugar, milk, and cinnamon");
    }
}

// Client
public class CoffeeClient
{
    public void EnjoyCoffee(CoffeeTemplate coffee)
    {
        coffee.BrewCoffee();
    }
}


//Example2
using System;

// Abstract class with the template method
public abstract class DocumentGenerator
{
    // Template method defining the algorithm
    public void GenerateDocument()
    {
        CreateHeader();
        AddContent();
        CreateFooter();
        Console.WriteLine("Document generated successfully!");
    }

    // Primitive operations (to be implemented by subclasses)
    protected abstract void CreateHeader();
    protected abstract void AddContent();
    protected abstract void CreateFooter();
}

// Concrete class implementing the template method
public class PlainTextDocument : DocumentGenerator
{
    protected override void CreateHeader()
    {
        Console.WriteLine("Generating Plain Text Document Header");
    }

    protected override void AddContent()
    {
        Console.WriteLine("Adding Plain Text Content");
    }

    protected override void CreateFooter()
    {
        Console.WriteLine("Generating Plain Text Document Footer");
    }
}

// Concrete class implementing the template method with different content format
public class HTMLDocument : DocumentGenerator
{
    protected override void CreateHeader()
    {
        Console.WriteLine("Generating HTML Document Header");
    }

    protected override void AddContent()
    {
        Console.WriteLine("Adding HTML Content");
    }

    protected override void CreateFooter()
    {
        Console.WriteLine("Generating HTML Document Footer");
    }
}

// Client
public class DocumentClient
{
    public void GenerateDocument(DocumentGenerator document)
    {
        document.GenerateDocument();
    }
}



//2
//Example1
using System;
using System.Collections.Generic;

// Mediator interface
public interface IChatMediator
{
    void SendMessage(string message, IUser sender);
    void AddUser(IUser user);
}

// Colleague interface
public interface IUser
{
    void ReceiveMessage(string message);
    void SendMessage(string message);
}

// Concrete Mediator
public class ChatMediator : IChatMediator
{
    private List<IUser> users = new List<IUser>();

    public void AddUser(IUser user)
    {
        users.Add(user);
    }

    public void SendMessage(string message, IUser sender)
    {
        foreach (var user in users)
        {
            if (user != sender)
            {
                user.ReceiveMessage($"{sender.GetName()}: {message}");
            }
        }
    }
}

// Concrete Colleague
public class ChatUser : IUser
{
    private IChatMediator mediator;
    private string name;

    public ChatUser(IChatMediator mediator, string name)
    {
        this.mediator = mediator;
        this.name = name;
        mediator.AddUser(this);
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"{name} received message: {message}");
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"{name} sending message: {message}");
        mediator.SendMessage(message, this);
    }

    public string GetName()
    {
        return name;
    }
}

// Client
public class ChatClient
{
    public void StartChat()
    {
        IChatMediator mediator = new ChatMediator();

        IUser user1 = new ChatUser(mediator, "User1");
        IUser user2 = new ChatUser(mediator, "User2");
        IUser user3 = new ChatUser(mediator, "User3");

        user1.SendMessage("Hello, everyone!");
        user2.SendMessage("Hi there!");
    }
}


//Example2
using System;
using System.Collections.Generic;

// Mediator interface
public interface IAirTrafficControl
{
    void RegisterFlight(Flight flight);
    void SendMessage(string message, Flight sender);
}

// Colleague interface
public class Flight
{
    private IAirTrafficControl mediator;
    private string flightNumber;

    public Flight(IAirTrafficControl mediator, string flightNumber)
    {
        this.mediator = mediator;
        this.flightNumber = flightNumber;
        mediator.RegisterFlight(this);
    }

    public void SendMessage(string message)
    {
        Console.WriteLine($"Flight {flightNumber} sending message: {message}");
        mediator.SendMessage(message, this);
    }

    public void ReceiveMessage(string message)
    {
        Console.WriteLine($"Flight {flightNumber} received message: {message}");
    }
}

// Concrete Mediator
public class AirTrafficControl : IAirTrafficControl
{
    private List<Flight> flights = new List<Flight>();

    public void RegisterFlight(Flight flight)
    {
        flights.Add(flight);
    }

    public void SendMessage(string message, Flight sender)
    {
        foreach (var flight in flights)
        {
            if (flight != sender)
            {
                flight.ReceiveMessage(message);
            }
        }
    }
}

// Client
public class AirTrafficControlClient
{
    public void ManageFlights()
    {
        IAirTrafficControl mediator = new AirTrafficControl();

        Flight flight1 = new Flight(mediator, "ABC123");
        Flight flight2 = new Flight(mediator, "XYZ789");

        flight1.SendMessage("Traffic ahead, changing course.");
        flight2.SendMessage("Acknowledged, changing course.");
    }
}



//3
//Example1
using System;

// Handler interface
public interface IApprover
{
    void ProcessRequest(PurchaseRequest request);
}

// Concrete Handler
public class Manager : IApprover
{
    private readonly decimal approvalLimit = 10000;

    public void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount <= approvalLimit)
        {
            Console.WriteLine($"Manager approves purchase request #{request.RequestNumber}");
        }
        else
        {
            Console.WriteLine($"Manager passes request #{request.RequestNumber} to Director");
        }
    }
}

// Concrete Handler
public class Director : IApprover
{
    private readonly decimal approvalLimit = 25000;

    public void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount <= approvalLimit)
        {
            Console.WriteLine($"Director approves purchase request #{request.RequestNumber}");
        }
        else
        {
            Console.WriteLine($"Director passes request #{request.RequestNumber} to Vice President");
        }
    }
}

// Concrete Handler
public class VicePresident : IApprover
{
    private readonly decimal approvalLimit = 50000;

    public void ProcessRequest(PurchaseRequest request)
    {
        if (request.Amount <= approvalLimit)
        {
            Console.WriteLine($"Vice President approves purchase request #{request.RequestNumber}");
        }
        else
        {
            Console.WriteLine($"Vice President passes request #{request.RequestNumber} to President");
        }
    }
}

// Concrete Handler
public class President : IApprover
{
    public void ProcessRequest(PurchaseRequest request)
    {
        Console.WriteLine($"President approves purchase request #{request.RequestNumber}");
    }
}

// Request class
public class PurchaseRequest
{
    public int RequestNumber { get; }
    public decimal Amount { get; }

    public PurchaseRequest(int requestNumber, decimal amount)
    {
        RequestNumber = requestNumber;
        Amount = amount;
    }
}

// Client
public class PurchaseClient
{
    public void MakePurchaseRequest(IApprover approver, PurchaseRequest request)
    {
        approver.ProcessRequest(request);
    }
}


//Example2
using System;

// Handler interface
public abstract class Logger
{
    protected Logger nextLogger;

    public void SetNextLogger(Logger nextLogger)
    {
        this.nextLogger = nextLogger;
    }

    public void LogMessage(LogLevel level, string message)
    {
        if (this.Level <= level)
        {
            Write(message);
        }

        if (nextLogger != null)
        {
            nextLogger.LogMessage(level, message);
        }
    }

    protected abstract void Write(string message);

    protected abstract LogLevel Level { get; }
}

// Concrete Handler
public class ConsoleLogger : Logger
{
    protected override void Write(string message)
    {
        Console.WriteLine($"Console Logger: {message}");
    }

    protected override LogLevel Level => LogLevel.Info;
}

// Concrete Handler
public class FileLogger : Logger
{
    protected override void Write(string message)
    {
        Console.WriteLine($"File Logger: {message}");
    }

    protected override LogLevel Level => LogLevel.Warning;
}

// Concrete Handler
public class EmailLogger : Logger
{
    protected override void Write(string message)
    {
        Console.WriteLine($"Email Logger: {message}");
    }

    protected override LogLevel Level => LogLevel.Error;
}

// Log levels
public enum LogLevel
{
    Info,
    Warning,
    Error
}

// Client
public class LoggerClient
{
    public void LogMessages(Logger logger, LogLevel level)
    {
        logger.LogMessage(level, "This is an information message.");
        logger.LogMessage(level, "This is a warning message.");
        logger.LogMessage(level, "This is an error message.");
    }
}



//4
//Example1
using System;
using System.Collections.Generic;

// Subject (Observable) interface
public interface IStockMarket
{
    void RegisterObserver(IStockObserver observer);
    void RemoveObserver(IStockObserver observer);
    void NotifyObservers(string stockSymbol, decimal price);
}

// Concrete Subject (Observable)
public class StockMarket : IStockMarket
{
    private readonly List<IStockObserver> observers = new List<IStockObserver>();

    public void RegisterObserver(IStockObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IStockObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(string stockSymbol, decimal price)
    {
        foreach (var observer in observers)
        {
            observer.Update(stockSymbol, price);
        }
    }

    public void StockPriceChanged(string stockSymbol, decimal price)
    {
        NotifyObservers(stockSymbol, price);
    }
}

// Observer interface
public interface IStockObserver
{
    void Update(string stockSymbol, decimal price);
}

// Concrete Observer
public class StockViewer : IStockObserver
{
    private readonly string name;

    public StockViewer(string name)
    {
        this.name = name;
    }

    public void Update(string stockSymbol, decimal price)
    {
        Console.WriteLine($"{name} - Stock: {stockSymbol}, Price: {price}");
    }
}

// Client
public class StockMarketClient
{
    public void MonitorStocks()
    {
        IStockMarket stockMarket = new StockMarket();

        IStockObserver viewer1 = new StockViewer("Viewer 1");
        IStockObserver viewer2 = new StockViewer("Viewer 2");

        stockMarket.RegisterObserver(viewer1);
        stockMarket.RegisterObserver(viewer2);

        stockMarket.StockPriceChanged("AAPL", 150.50m);
        stockMarket.StockPriceChanged("GOOGL", 2500.75m);

        stockMarket.RemoveObserver(viewer1);

        stockMarket.StockPriceChanged("MSFT", 300.20m);
    }
}


//Example2
using System;
using System.Collections.Generic;

// Subject (Observable) interface
public interface IWeatherStation
{
    void RegisterObserver(IWeatherObserver observer);
    void RemoveObserver(IWeatherObserver observer);
    void NotifyObservers(float temperature, float humidity, float pressure);
}

// Concrete Subject (Observable)
public class WeatherStation : IWeatherStation
{
    private readonly List<IWeatherObserver> observers = new List<IWeatherObserver>();

    public void RegisterObserver(IWeatherObserver observer)
    {
        observers.Add(observer);
    }

    public void RemoveObserver(IWeatherObserver observer)
    {
        observers.Remove(observer);
    }

    public void NotifyObservers(float temperature, float humidity, float pressure)
    {
        foreach (var observer in observers)
        {
            observer.Update(temperature, humidity, pressure);
        }
    }

    public void SetWeatherData(float temperature, float humidity, float pressure)
    {
        NotifyObservers(temperature, humidity, pressure);
    }
}

// Observer interface
public interface IWeatherObserver
{
    void Update(float temperature, float humidity, float pressure);
}

// Concrete Observer
public class DisplayDevice : IWeatherObserver
{
    private readonly string name;

    public DisplayDevice(string name)
    {
        this.name = name;
    }

    public void Update(float temperature, float humidity, float pressure)
    {
        Console.WriteLine($"{name} - Temperature: {temperature}°C, Humidity: {humidity}%, Pressure: {pressure}hPa");
    }
}

// Client
public class WeatherClient
{
    public void DisplayWeatherInfo()
    {
        IWeatherStation weatherStation = new WeatherStation();

        IWeatherObserver display1 = new DisplayDevice("Display 1");
        IWeatherObserver display2 = new DisplayDevice("Display 2");

        weatherStation.RegisterObserver(display1);
        weatherStation.RegisterObserver(display2);

        weatherStation.SetWeatherData(25.5f, 60.0f, 1012.0f);

        weatherStation.RemoveObserver(display1);

        weatherStation.SetWeatherData(22.0f, 65.5f, 1010.5f);
    }
}



//5
//Example1
using System;
using System.Collections.Generic;

// Strategy interface
public interface ISortStrategy<T>
{
    void Sort(List<T> list);
}

// Concrete Strategies
public class BubbleSort<T> : ISortStrategy<T> where T : IComparable<T>
{
    public void Sort(List<T> list)
    {
        Console.WriteLine("Applying Bubble Sort");
        list.Sort();
    }
}

public class QuickSort<T> : ISortStrategy<T> where T : IComparable<T>
{
    public void Sort(List<T> list)
    {
        Console.WriteLine("Applying Quick Sort");
        list.Sort();
        // Additional quick sort implementation
    }
}

// Context
public class SortContext<T>
{
    private ISortStrategy<T> sortStrategy;

    public void SetSortStrategy(ISortStrategy<T> strategy)
    {
        sortStrategy = strategy;
    }

    public void ExecuteSort(List<T> list)
    {
        sortStrategy.Sort(list);
    }
}

// Client
public class SortingClient
{
    public void PerformSort()
    {
        List<int> numbers = new List<int> { 5, 2, 8, 1, 9, 4 };

        SortContext<int> context = new SortContext<int>();
        context.SetSortStrategy(new BubbleSort<int>());
        context.ExecuteSort(numbers);

        context.SetSortStrategy(new QuickSort<int>());
        context.ExecuteSort(numbers);
    }
}


//Example2
using System;

// Strategy interface
public interface IPaymentStrategy
{
    void ProcessPayment(decimal amount);
}

// Concrete Strategies
public class CreditCardPayment : IPaymentStrategy
{
    private readonly string cardNumber;
    private readonly string expiryDate;
    private readonly string cvv;

    public CreditCardPayment(string cardNumber, string expiryDate, string cvv)
    {
        this.cardNumber = cardNumber;
        this.expiryDate = expiryDate;
        this.cvv = cvv;
    }

    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing credit card payment of ${amount} with Card Number: {cardNumber}");
    }
}

public class PayPalPayment : IPaymentStrategy
{
    private readonly string email;

    public PayPalPayment(string email)
    {
        this.email = email;
    }

    public void ProcessPayment(decimal amount)
    {
        Console.WriteLine($"Processing PayPal payment of ${amount} with Email: {email}");
    }
}

// Context
public class PaymentContext
{
    private IPaymentStrategy paymentStrategy;

    public void SetPaymentStrategy(IPaymentStrategy strategy)
    {
        paymentStrategy = strategy;
    }

    public void MakePayment(decimal amount)
    {
        paymentStrategy.ProcessPayment(amount);
    }
}

// Client
public class PaymentClient
{
    public void MakePayments()
    {
        PaymentContext paymentContext = new PaymentContext();

        // Credit Card Payment
        paymentContext.SetPaymentStrategy(new CreditCardPayment("1234-5678-9012-3456", "12/23", "123"));
        paymentContext.MakePayment(50.75m);

        // PayPal Payment
        paymentContext.SetPaymentStrategy(new PayPalPayment("user@example.com"));
        paymentContext.MakePayment(30.50m);
    }
}



//6
//Example1
using System;

// Command interface
public interface ICommand
{
    void Execute();
}

// Concrete Command
public class LightOnCommand : ICommand
{
    private readonly Light light;

    public LightOnCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOn();
    }
}

// Concrete Command
public class LightOffCommand : ICommand
{
    private readonly Light light;

    public LightOffCommand(Light light)
    {
        this.light = light;
    }

    public void Execute()
    {
        light.TurnOff();
    }
}

// Receiver
public class Light
{
    public void TurnOn()
    {
        Console.WriteLine("Light is ON");
    }

    public void TurnOff()
    {
        Console.WriteLine("Light is OFF");
    }
}

// Invoker
public class RemoteControl
{
    private ICommand command;

    public void SetCommand(ICommand command)
    {
        this.command = command;
    }

    public void PressButton()
    {
        command.Execute();
    }
}

// Client
public class RemoteControlClient
{
    public void UseRemoteControl()
    {
        Light light = new Light();
        ICommand lightOnCommand = new LightOnCommand(light);
        ICommand lightOffCommand = new LightOffCommand(light);

        RemoteControl remote = new RemoteControl();

        remote.SetCommand(lightOnCommand);
        remote.PressButton();

        remote.SetCommand(lightOffCommand);
        remote.PressButton();
    }
}


//Example2
using System;

// Command interface
public interface ITextCommand
{
    void Execute();
}

// Concrete Command
public class CopyCommand : ITextCommand
{
    private readonly TextEditor textEditor;

    public CopyCommand(TextEditor textEditor)
    {
        this.textEditor = textEditor;
    }

    public void Execute()
    {
        textEditor.Copy();
    }
}

// Concrete Command
public class CutCommand : ITextCommand
{
    private readonly TextEditor textEditor;

    public CutCommand(TextEditor textEditor)
    {
        this.textEditor = textEditor;
    }

    public void Execute()
    {
        textEditor.Cut();
    }
}

// Concrete Command
public class PasteCommand : ITextCommand
{
    private readonly TextEditor textEditor;

    public PasteCommand(TextEditor textEditor)
    {
        this.textEditor = textEditor;
    }

    public void Execute()
    {
        textEditor.Paste();
    }
}

// Receiver
public class TextEditor
{
    public void Copy()
    {
        Console.WriteLine("Text is copied to clipboard");
    }

    public void Cut()
    {
        Console.WriteLine("Text is cut to clipboard");
    }

    public void Paste()
    {
        Console.WriteLine("Text is pasted from clipboard");
    }
}

// Invoker
public class TextEditorInvoker
{
    private ITextCommand command;

    public void SetCommand(ITextCommand command)
    {
        this.command = command;
    }

    public void ExecuteCommand()
    {
        command.Execute();
    }
}

// Client
public class TextEditorClient
{
    public void UseTextEditor()
    {
        TextEditor textEditor = new TextEditor();
        ITextCommand copyCommand = new CopyCommand(textEditor);
        ITextCommand cutCommand = new CutCommand(textEditor);
        ITextCommand pasteCommand = new PasteCommand(textEditor);

        TextEditorInvoker invoker = new TextEditorInvoker();

        invoker.SetCommand(copyCommand);
        invoker.ExecuteCommand();

        invoker.SetCommand(cutCommand);
        invoker.ExecuteCommand();

        invoker.SetCommand(pasteCommand);
        invoker.ExecuteCommand();
    }
}



//7
//Example1
using System;

// Context
public class ATM
{
    private IATMState currentState;

    public ATM()
    {
        // Initial state is NoCardState
        currentState = new NoCardState(this);
    }

    public IATMState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public void InsertCard()
    {
        currentState.InsertCard();
    }

    public void EjectCard()
    {
        currentState.EjectCard();
    }

    public void InsertPin(int pin)
    {
        currentState.InsertPin(pin);
    }

    public void WithdrawCash(int amount)
    {
        currentState.WithdrawCash(amount);
    }
}

// State interface
public interface IATMState
{
    void InsertCard();
    void EjectCard();
    void InsertPin(int pin);
    void WithdrawCash(int amount);
}

// Concrete States
public class NoCardState : IATMState
{
    private readonly ATM atm;

    public NoCardState(ATM atm)
    {
        this.atm = atm;
    }

    public void InsertCard()
    {
        Console.WriteLine("Card inserted");
        atm.CurrentState = new CardInsertedState(atm);
    }

    public void EjectCard()
    {
        Console.WriteLine("No card to eject");
    }

    public void InsertPin(int pin)
    {
        Console.WriteLine("Please insert a card first");
    }

    public void WithdrawCash(int amount)
    {
        Console.WriteLine("Please insert a card first");
    }
}

public class CardInsertedState : IATMState
{
    private readonly ATM atm;

    public CardInsertedState(ATM atm)
    {
        this.atm = atm;
    }

    public void InsertCard()
    {
        Console.WriteLine("Card is already inserted");
    }

    public void EjectCard()
    {
        Console.WriteLine("Card ejected");
        atm.CurrentState = new NoCardState(atm);
    }

    public void InsertPin(int pin)
    {
        if (pin == 1234) // Mock PIN for simplicity
        {
            Console.WriteLine("PIN is correct");
            atm.CurrentState = new HasPinState(atm);
        }
        else
        {
            Console.WriteLine("Incorrect PIN");
        }
    }

    public void WithdrawCash(int amount)
    {
        Console.WriteLine("Please enter the correct PIN first");
    }
}

public class HasPinState : IATMState
{
    private readonly ATM atm;

    public HasPinState(ATM atm)
    {
        this.atm = atm;
    }

    public void InsertCard()
    {
        Console.WriteLine("Card is already inserted");
    }

    public void EjectCard()
    {
        Console.WriteLine("Card ejected");
        atm.CurrentState = new NoCardState(atm);
    }

    public void InsertPin(int pin)
    {
        Console.WriteLine("PIN is already entered");
    }

    public void WithdrawCash(int amount)
    {
        Console.WriteLine($"Withdrawal of ${amount} is successful");
        // Additional logic for dispensing cash
        atm.CurrentState = new NoCardState(atm);
    }
}

// Client
public class ATMClient
{
    public void UseATM()
    {
        ATM atm = new ATM();

        atm.InsertCard();
        atm.EjectCard();

        atm.InsertCard();
        atm.InsertPin(1234);
        atm.WithdrawCash(1000);
    }
}


//Example2
using System;

// Context
public class TCPConnection
{
    private ITCPState currentState;

    public TCPConnection()
    {
        // Initial state is ClosedState
        currentState = new ClosedState(this);
    }

    public ITCPState CurrentState
    {
        get { return currentState; }
        set { currentState = value; }
    }

    public void Open()
    {
        currentState.Open();
    }

    public void Close()
    {
        currentState.Close();
    }

    public void Acknowledge()
    {
        currentState.Acknowledge();
    }
}

// State interface
public interface ITCPState
{
    void Open();
    void Close();
    void Acknowledge();
}

// Concrete States
public class ClosedState : ITCPState
{
    private readonly TCPConnection tcpConnection;

    public ClosedState(TCPConnection tcpConnection)
    {
        this.tcpConnection = tcpConnection;
    }

    public void Open()
    {
        Console.WriteLine("TCP Connection opened");
        tcpConnection.CurrentState = new OpenState(tcpConnection);
    }

    public void Close()
    {
        Console.WriteLine("TCP Connection is already closed");
    }

    public void Acknowledge()
    {
        Console.WriteLine("TCP Connection is closed, no acknowledgment");
    }
}

public class OpenState : ITCPState
{
    private readonly TCPConnection tcpConnection;

    public OpenState(TCPConnection tcpConnection)
    {
        this.tcpConnection = tcpConnection;
    }

    public void Open()
    {
        Console.WriteLine("TCP Connection is already open");
    }

    public void Close()
    {
        Console.WriteLine("TCP Connection closed");
        tcpConnection.CurrentState = new ClosedState(tcpConnection);
    }

    public void Acknowledge()
    {
        Console.WriteLine("Acknowledgment sent for the open connection");
    }
}

// Client
public class TCPClient
{
    public void UseTCPConnection()
    {
        TCPConnection tcpConnection = new TCPConnection();

        tcpConnection.Open();
        tcpConnection.Acknowledge();

        tcpConnection.Close();
        tcpConnection.Acknowledge();
    }
}



//8
//Example1
using System;
using System.Collections.Generic;

// Element interface
public interface IShape
{
    void Accept(IVisitor visitor);
}

// Concrete Elements
public class Circle : IShape
{
    public double Radius { get; }

    public Circle(double radius)
    {
        Radius = radius;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitCircle(this);
    }
}

public class Square : IShape
{
    public double Side { get; }

    public Square(double side)
    {
        Side = side;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitSquare(this);
    }
}

// Visitor interface
public interface IVisitor
{
    void VisitCircle(Circle circle);
    void VisitSquare(Square square);
}

// Concrete Visitor
public class AreaVisitor : IVisitor
{
    public void VisitCircle(Circle circle)
    {
        double area = Math.PI * Math.Pow(circle.Radius, 2);
        Console.WriteLine($"Area of Circle: {area}");
    }

    public void VisitSquare(Square square)
    {
        double area = Math.Pow(square.Side, 2);
        Console.WriteLine($"Area of Square: {area}");
    }
}

// Client
public class ShapesClient
{
    public void CalculateAreas()
    {
        List<IShape> shapes = new List<IShape>
        {
            new Circle(5),
            new Square(4)
        };

        IVisitor areaVisitor = new AreaVisitor();

        foreach (var shape in shapes)
        {
            shape.Accept(areaVisitor);
        }
    }
}


//Example2
using System;
using System.Collections.Generic;

// Element interface
public interface IDocumentNode
{
    void Accept(IVisitor visitor);
}

// Concrete Elements
public class Heading : IDocumentNode
{
    public string Text { get; }

    public Heading(string text)
    {
        Text = text;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitHeading(this);
    }
}

public class Paragraph : IDocumentNode
{
    public string Text { get; }

    public Paragraph(string text)
    {
        Text = text;
    }

    public void Accept(IVisitor visitor)
    {
        visitor.VisitParagraph(this);
    }
}

// Visitor interface
public interface IVisitor
{
    void VisitHeading(Heading heading);
    void VisitParagraph(Paragraph paragraph);
}

// Concrete Visitor
public class HTMLVisitor : IVisitor
{
    public void VisitHeading(Heading heading)
    {
        Console.WriteLine($"<h1>{heading.Text}</h1>");
    }

    public void VisitParagraph(Paragraph paragraph)
    {
        Console.WriteLine($"<p>{paragraph.Text}</p>");
    }
}

// Client
public class DocumentClient
{
    public void GenerateHTML()
    {
        List<IDocumentNode> documentNodes = new List<IDocumentNode>
        {
            new Heading("Visitor Design Pattern"),
            new Paragraph("The Visitor design pattern allows you to define a new operation without changing the classes of the elements on which it operates.")
        };

        IVisitor htmlVisitor = new HTMLVisitor();

        foreach (var node in documentNodes)
        {
            node.Accept(htmlVisitor);
        }
    }
}



//9
//Example1
using System;
using System.Collections.Generic;

// Context
public class Context
{
    public Dictionary<string, int> Variables { get; } = new Dictionary<string, int>();
}

// Abstract Expression
public interface IExpression
{
    int Interpret(Context context);
}

// Terminal Expression
public class NumberExpression : IExpression
{
    private readonly int value;

    public NumberExpression(int value)
    {
        this.value = value;
    }

    public int Interpret(Context context)
    {
        return value;
    }
}

// Terminal Expression
public class VariableExpression : IExpression
{
    private readonly string variableName;

    public VariableExpression(string variableName)
    {
        this.variableName = variableName;
    }

    public int Interpret(Context context)
    {
        if (context.Variables.ContainsKey(variableName))
        {
            return context.Variables[variableName];
        }
        else
        {
            throw new ArgumentException($"Variable {variableName} not found in the context");
        }
    }
}

// Non-terminal Expression
public class AdditionExpression : IExpression
{
    private readonly IExpression left;
    private readonly IExpression right;

    public AdditionExpression(IExpression left, IExpression right)
    {
        this.left = left;
        this.right = right;
    }

    public int Interpret(Context context)
    {
        return left.Interpret(context) + right.Interpret(context);
    }
}

// Non-terminal Expression
public class SubtractionExpression : IExpression
{
    private readonly IExpression left;
    private readonly IExpression right;

    public SubtractionExpression(IExpression left, IExpression right)
    {
        this.left = left;
        this.right = right;
    }

    public int Interpret(Context context)
    {
        return left.Interpret(context) - right.Interpret(context);
    }
}

// Client
public class InterpreterClient
{
    public void InterpretArithmeticExpression()
    {
        // Context with variables
        Context context = new Context();
        context.Variables["x"] = 10;
        context.Variables["y"] = 5;

        // Expression: x + (y - 2)
        IExpression expression = new AdditionExpression(
            new VariableExpression("x"),
            new SubtractionExpression(
                new VariableExpression("y"),
                new NumberExpression(2)
            )
        );

        int result = expression.Interpret(context);
        Console.WriteLine($"Result: {result}");
    }
}


//Example2
using System;
using System.Collections.Generic;

// Context
public class QueryContext
{
    public List<string> Data { get; } = new List<string>();
}

// Abstract Expression
public interface IQueryExpression
{
    List<string> Interpret(QueryContext context);
}

// Terminal Expression
public class AllDataExpression : IQueryExpression
{
    public List<string> Interpret(QueryContext context)
    {
        return context.Data;
    }
}

// Terminal Expression
public class FilterExpression : IQueryExpression
{
    private readonly IQueryExpression source;
    private readonly string filterCriteria;

    public FilterExpression(IQueryExpression source, string filterCriteria)
    {
        this.source = source;
        this.filterCriteria = filterCriteria;
    }

    public List<string> Interpret(QueryContext context)
    {
        List<string> filteredData = new List<string>();

        foreach (var item in source.Interpret(context))
        {
            if (item.Contains(filterCriteria))
            {
                filteredData.Add(item);
            }
        }

        return filteredData;
    }
}

// Non-terminal Expression
public class JoinExpression : IQueryExpression
{
    private readonly IQueryExpression left;
    private readonly IQueryExpression right;

    public JoinExpression(IQueryExpression left, IQueryExpression right)
    {
        this.left = left;
        this.right = right;
    }

    public List<string> Interpret(QueryContext context)
    {
        List<string> joinedData = new List<string>();
        joinedData.AddRange(left.Interpret(context));
        joinedData.AddRange(right.Interpret(context));

        return joinedData;
    }
}

// Client
public class QueryInterpreterClient
{
    public void InterpretQuery()
    {
        // Context with data
        QueryContext context = new QueryContext();
        context.Data.Add("Alice");
        context.Data.Add("Bob");
        context.Data.Add("Charlie");
        context.Data.Add("David");

        // Query: (AllData + (Filter: "a")) - (Filter: "l")
        IQueryExpression query = new SubtractionExpression(
            new JoinExpression(
                new AllDataExpression(),
                new FilterExpression(
                    new AllDataExpression(),
                    "a"
                )
            ),
            new FilterExpression(
                new AllDataExpression(),
                "l"
            )
        );

        List<string> result = query.Interpret(context);
        Console.WriteLine("Result: " + string.Join(", ", result));
    }
}



//10
//Example1
using System;
using System.Collections;

// Aggregate interface
public interface IAggregate
{
    IIterator CreateIterator();
}

// Iterator interface
public interface IIterator
{
    bool HasNext();
    object Next();
}

// Concrete Aggregate
public class ConcreteAggregate : IAggregate
{
    private readonly int[] elements;

    public ConcreteAggregate(int[] elements)
    {
        this.elements = elements;
    }

    public IIterator CreateIterator()
    {
        return new ConcreteIterator(this);
    }

    public int GetElement(int index)
    {
        return elements[index];
    }

    public int Count
    {
        get { return elements.Length; }
    }
}

// Concrete Iterator
public class ConcreteIterator : IIterator
{
    private readonly ConcreteAggregate aggregate;
    private int currentIndex;

    public ConcreteIterator(ConcreteAggregate aggregate)
    {
        this.aggregate = aggregate;
        currentIndex = 0;
    }

    public bool HasNext()
    {
        return currentIndex < aggregate.Count;
    }

    public object Next()
    {
        if (HasNext())
        {
            return aggregate.GetElement(currentIndex++);
        }
        else
        {
            throw new InvalidOperationException("No more elements");
        }
    }
}

// Client
public class IteratorClient
{
    public void IterateCollection()
    {
        int[] elements = { 1, 2, 3, 4, 5 };

        ConcreteAggregate aggregate = new ConcreteAggregate(elements);
        IIterator iterator = aggregate.CreateIterator();

        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
    }
}


//Example2
using System;
using System.Collections;
using System.Collections.Generic;

// Aggregate interface
public interface IAggregate<T>
{
    IIterator<T> CreateIterator();
}

// Iterator interface
public interface IIterator<T>
{
    bool HasNext();
    T Next();
}

// Concrete Aggregate
public class CustomCollection<T> : IAggregate<T>
{
    private readonly List<T> elements = new List<T>();

    public void AddElement(T element)
    {
        elements.Add(element);
    }

    public IIterator<T> CreateIterator()
    {
        return new CustomIterator(this);
    }

    public T GetElement(int index)
    {
        return elements[index];
    }

    public int Count
    {
        get { return elements.Count; }
    }
}

// Concrete Iterator
public class CustomIterator<T> : IIterator<T>
{
    private readonly CustomCollection<T> collection;
    private int currentIndex;

    public CustomIterator(CustomCollection<T> collection)
    {
        this.collection = collection;
        currentIndex = 0;
    }

    public bool HasNext()
    {
        return currentIndex < collection.Count;
    }

    public T Next()
    {
        if (HasNext())
        {
            return collection.GetElement(currentIndex++);
        }
        else
        {
            throw new InvalidOperationException("No more elements");
        }
    }
}

// Client
public class CustomIteratorClient
{
    public void IterateCustomCollection()
    {
        CustomCollection<string> customCollection = new CustomCollection<string>();
        customCollection.AddElement("Apple");
        customCollection.AddElement("Banana");
        customCollection.AddElement("Cherry");

        IIterator<string> iterator = customCollection.CreateIterator();

        while (iterator.HasNext())
        {
            Console.WriteLine(iterator.Next());
        }
    }
}



//11
//Example1
using System;
using System.Collections.Generic;

// Originator
public class TextEditor
{
    private string content;

    public TextEditor(string content)
    {
        this.content = content;
    }

    public void SetContent(string content)
    {
        this.content = content;
    }

    public string GetContent()
    {
        return content;
    }

    public TextEditorMemento CreateMemento()
    {
        return new TextEditorMemento(content);
    }

    public void RestoreMemento(TextEditorMemento memento)
    {
        content = memento.GetSavedContent();
    }
}

// Memento
public class TextEditorMemento
{
    private readonly string savedContent;

    public TextEditorMemento(string content)
    {
        savedContent = content;
    }

    public string GetSavedContent()
    {
        return savedContent;
    }
}

// Caretaker
public class History
{
    private readonly Stack<TextEditorMemento> mementos = new Stack<TextEditorMemento>();

    public void SaveState(TextEditorMemento memento)
    {
        mementos.Push(memento);
    }

    public TextEditorMemento Undo()
    {
        if (mementos.Count > 0)
        {
            return mementos.Pop();
        }
        else
        {
            return null;
        }
    }
}

// Client
public class TextEditorClient
{
    public void UseTextEditor()
    {
        TextEditor textEditor = new TextEditor("Initial content");
        History history = new History();

        // Save state
        history.SaveState(textEditor.CreateMemento());

        // Modify content
        textEditor.SetContent("Modified content");

        // Save state
        history.SaveState(textEditor.CreateMemento());

        // Undo to previous state
        TextEditorMemento previousState = history.Undo();

        if (previousState != null)
        {
            textEditor.RestoreMemento(previousState);
            Console.WriteLine($"Undone to previous state: {textEditor.GetContent()}");
        }
        else
        {
            Console.WriteLine("No previous state available");
        }
    }
}


//Example2
using System;
using System.Collections.Generic;

// Originator
public class ChessGame
{
    private char[,] board;

    public ChessGame()
    {
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        board = new char[,]
        {
            { 'R', 'N', 'B', 'Q', 'K', 'B', 'N', 'R' },
            { 'P', 'P', 'P', 'P', 'P', 'P', 'P', 'P' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { ' ', ' ', ' ', ' ', ' ', ' ', ' ', ' ' },
            { 'p', 'p', 'p', 'p', 'p', 'p', 'p', 'p' },
            { 'r', 'n', 'b', 'q', 'k', 'b', 'n', 'r' }
        };
    }

    public void MovePiece(int fromRow, int fromCol, int toRow, int toCol)
    {
        // Perform the move logic (not implemented in this example)
        // ...

        PrintBoard();
    }

    private void PrintBoard()
    {
        Console.WriteLine("Current Chess Board:");
        for (int row = 0; row < 8; row++)
        {
            for (int col = 0; col < 8; col++)
            {
                Console.Write(board[row, col] + " ");
            }
            Console.WriteLine();
        }
        Console.WriteLine();
    }

    public ChessGameMemento CreateMemento()
    {
        return new ChessGameMemento(board);
    }

    public void RestoreMemento(ChessGameMemento memento)
    {
        board = memento.GetSavedBoard();
        PrintBoard();
    }
}

// Memento
public class ChessGameMemento
{
    private readonly char[,] savedBoard;

    public ChessGameMemento(char[,] board)
    {
        savedBoard = (char[,])board.Clone();
    }

    public char[,] GetSavedBoard()
    {
        return (char[,])savedBoard.Clone();
    }
}

// Caretaker
public class ChessGameHistory
{
    private readonly Stack<ChessGameMemento> mementos = new Stack<ChessGameMemento>();

    public void SaveState(ChessGameMemento memento)
    {
        mementos.Push(memento);
    }

    public ChessGameMemento Undo()
    {
        if (mementos.Count > 0)
        {
            return mementos.Pop();
        }
        else
        {
            return null;
        }
    }
}

// Client
public class ChessGameClient
{
    public void PlayChess()
    {
        ChessGame chessGame = new ChessGame();
        ChessGameHistory history = new ChessGameHistory();

        // Save initial state
        history.SaveState(chessGame.CreateMemento());

        // Make a move
        chessGame.MovePiece(6, 4, 4, 4);

        // Save state after the move
        history.SaveState(chessGame.CreateMemento());

        // Undo to previous state
        ChessGameMemento previousState = history.Undo();

        if (previousState != null)
        {
            chessGame.RestoreMemento(previousState);
            Console.WriteLine("Undone to previous state:");
        }
        else
        {
            Console.WriteLine("No previous state available");
        }
    }
}
