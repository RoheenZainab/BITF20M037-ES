using System;
using System.Collections.Generic;


class Location
{
    private float latitude;
    private float longitude;

    public float Latitude
    {
        get { return latitude; }
        set
        {
            if (value < -90.0f || value > 90.0f)
            {
                throw new ArgumentOutOfRangeException(nameof(Latitude), "Latitude must be between -90 and 90 degrees.");
            }
            latitude = value;
        }
    }

        public float Longitude
        {
            get { return longitude; }
            set
            {
               if (value < -180.0f || value > 180.0f)
               {
                   throw new ArgumentOutOfRangeException(nameof(Longitude), "Longitude must be between -180 and 180.");
               }
               longitude = value;
            }
        }

        public void SetLocation(float latitude, float longitude)
        {
            Latitude = latitude;
            Longitude = longitude;
        }
}


class Vehicle
{
    private string type;
    private string model;
    private string licensePlate;

    public string Type
    {
        get { return type; }
        set
        {
            type = value;
        }
    }

    public string Model
    {
        get { return model; }
        set
        {
            model = value;
        }
    }

    public string LicensePlate
    {
        get { return licensePlate; }
        set
        {
            licensePlate = value;
        }
    }
}

class Driver
{
    public string Name{ get; set; }
    public int Age{ get; set; }
    public string Gender{ get; set; }
    public string Address{ get; set; }
    public string PhoneNo{ get; set; }
    public Location CurrentLocation{ get; set; }
    public Vehicle Vehicle{ get; set; }
    public List<int> Rating{ get; set; }
    public bool Availability{ get; set; }

    public void UpdateAvailability()
    {
        Availability = isAvailable;
    }

    public double GetRating()
    {
        return Rating.Count > 0 ? Rating.Average() : 0;
    }

    public void UpdateLocation(float latitude, float longitude)
    {
        CurrentLocation.SetLocation(latitude, longitude);
    }
}


class Passenger
{
    public string Name{ get; set; }
    public string PhoneNo{ get; set; }

    public void BookRide()
    {
        Console.WriteLine($"{Name} has booked a ride.");
    }

    public void GiveRating(int rating)
    {
        if (rating >= 1 && rating <= 5)
        {
            Console.WriteLine($"Passenger {Name} gave a rating of {rating}.");
        }
        else
        {
            Console.WriteLine("Invalid rating. Please provide a rating between 1 and 5.");
        }
    }
}


class Ride
{
    public Location StartLocation{ get; set; }
    public Location EndLocation{ get; set; }
    public int Price{ get; set; }
    public Driver Driver{ get; set; }
    public Passenger Passenger{ get; set; }

    public void AssignPassenger(Passenger passenger)
    {
        Passenger = passenger;
    }

    public void AssignDriver(List<Driver> availableDrivers)
    {
        if (availableDrivers == null || availableDrivers.Count == 0)
        {
            Console.WriteLine("No available drivers at the moment.");
            return;
        }

        Dictionary<Driver, double> driverDistances = new Dictionary<Driver, double>();
        foreach(Driver driver in availableDrivers)
        {
            double distance = CalculateDistance(StartLocation, driver.CurrentLocation);
            driverDistances[driver] = distance;
        }

        Console.WriteLine($"Assigned driver: {Driver.Name}");
    }


    public void GetLocations(Location startLocation, Location endLocation)
    {
        StartLocation = startLocation;
        EndLocation = endLocation;
    }

    public void CalculatePrice(float fuelPrice, float fuelAverage, float commission)
    {
        double distance = CalculateDistance(StartLocation, EndLocation);
        Price = (int)((distance * fuelPrice) / fuelAverage + commission);
    }

    
}


class Admin
{
    public List<Driver> Drivers{ get; set; }

    public void AddDriver(Driver driver)
    {
        Drivers.Add(driver);
    }

    public void UpdateDriver(Driver, string newName, int newAge, string newAddress)
    {

        if (existingDriver != null)
        {
            existingDriver.Name = newName;
            existingDriver.Age = newAge;
            existingDriver.Address = newAddress;

            Console.WriteLine($"Driver {existingDriver.Name}'s information updated.");
        }
        else
        {
            Console.WriteLine("Driver not found.");
        }
    }

    public void RemoveDriver(int driverId)
    {
      Driver driverToRemove = Drivers.FirstOrDefault(d = > d.DriverId == driverId);

      if (driverToRemove != null)
      {
        Drivers.Remove(driverToRemove);
        Console.WriteLine($"Driver with ID {driverId} removed.");
      }
      else
      {
        Console.WriteLine($"Driver with ID {driverId} not found.");
      }
    }
}

                         
class Program
{
    static List<Driver> registeredDrivers = new List<Driver>();
    static List<Ride> rides = new List<Ride>();

    static void Main(string[] args)
    {
        while (true)
        {
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("WELCOME TO MYRIDE");
            Console.WriteLine("---------------------------------------------------------------------------------------------------------------------");
            Console.WriteLine("1. Book a Ride");
            Console.WriteLine("2. Enter as Driver");
            Console.WriteLine("3. Enter as Admin");
            Console.WriteLine("4. Exit");
            Console.Write("Press 1 to 4 to select an option: ");

            string choice = Console.ReadLine();
            switch (choice)
            {
            case "1":
                BookARide();
                break;
            case "2":              
                EnterAsDriver();
                break;
            case "3":               
                EnterAsAdmin();
                break;
            case "4":              
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
            }

            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }

    static void BookARide()
    {
        Console.WriteLine("Book a Ride");
        
        Console.Write("Enter Name: ");
        string passengerName = Console.ReadLine();
        Console.Write("Enter Phone no: ");
        string passengerPhoneNo = Console.ReadLine();
        Console.Write("Enter Start Location (latitude,longitude): ");
        string[] startLocationInput = Console.ReadLine().Split(',');
        float startLatitude = float.Parse(startLocationInput[0]);
        float startLongitude = float.Parse(startLocationInput[1]);
        Console.Write("Enter End Location (latitude,longitude): ");
        string[] endLocationInput = Console.ReadLine().Split(',');
        float endLatitude = float.Parse(endLocationInput[0]);
        float endLongitude = float.Parse(endLocationInput[1]);
        Console.Write("Enter Ride Type: ");
        string rideType = Console.ReadLine();


        int price = CalculatePrice(distance, fuelPrice, fuelAverage, commission);

        static int CalculatePrice(float distance, float fuelPrice, float fuelAverage, float commission)
        {
            return (int)((distance * fuelPrice) / fuelAverage + commission);
        }

        
        Console.WriteLine("--------------------------------------------------");
        Console.WriteLine("THANK YOU");
        Console.WriteLine($"Price for this ride is: {price}");

       
        Console.Write("Enter 'Y' if you want to Book the ride, enter 'N' if you want to cancel operation: ");
        string bookChoice = Console.ReadLine();
        if (bookChoice.ToLower() == "y")
        {
            Console.WriteLine("Happy Travel...!");

            Console.Write("Give rating out of 5: ");
            int rating = int.Parse(Console.ReadLine());
        else
        {
            Console.WriteLine("Operation canceled.");
        }
    }

    

    static void EnterAsDriver()
    {
        Console.WriteLine("Enter as Driver");
        
        Console.Write("Enter ID: ");
        int driverId = int.Parse(Console.ReadLine());
        Console.Write("Enter Name: ");
        string driverName = Console.ReadLine();

        
        Driver driver = registeredDrivers.FirstOrDefault(d = > d.Id == driverId && d.Name == driverName);
        if (driver != null)
        {
            Console.WriteLine($"Hello {driver.Name}!");

            while (true)
            {
                Console.WriteLine("Driver Options:");
                Console.WriteLine("1. Change Availability");
                Console.WriteLine("2. Change Location");
                Console.WriteLine("3. Exit as Driver");
                Console.Write("Select an option (1-3): ");

                string driverChoice = Console.ReadLine();

                switch (driverChoice)
                {
                case "1":           
                    Console.Write("Change availability to (Available/Unavailable): ");
                    string availabilityInput = Console.ReadLine();
                    bool isAvailable = availabilityInput.ToLower() == "available";
                    driver.UpdateAvailability(isAvailable);
                    Console.WriteLine($"Availability updated to {(isAvailable ? "Available" : "Unavailable")}");
                    break;

                case "2":                   
                    Console.Write("Enter your current Location (latitude,longitude): ");
                    string[] locationInput = Console.ReadLine().Split(',');
                    float newLatitude = float.Parse(locationInput[0]);
                    float newLongitude = float.Parse(locationInput[1]);
                    driver.UpdateLocation(newLatitude, newLongitude);
                    Console.WriteLine($"Location updated to ({newLatitude},{newLongitude})");
                    break;

                case "3":                 
                    return;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
                }
            }
        }
        else
        {
            Console.WriteLine("Driver not registered. Returning to the main menu.");
        }
    }

    static void EnterAsAdmin()
    {       
        Console.WriteLine("Enter as Admin");

        while (true)
        {
            Console.WriteLine("Admin Menu:");
            Console.WriteLine("1. Add Driver");
            Console.WriteLine("2. Remove Driver");
            Console.WriteLine("3. Update Driver");
            Console.WriteLine("4. Search Driver");
            Console.WriteLine("5. Exit as Admin");
            Console.Write("Select an option (1-5): ");

            string adminChoice = Console.ReadLine();

            switch (adminChoice)
            {
            case "1":               
                AddDriver();
                break;

            case "2":                
                RemoveDriver();
                break;

            case "3":               
                UpdateDriver();
                break;

            case "4":                
                SearchDriver();
                break;

            case "5":              
                return;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
            }
        }
    }
}

