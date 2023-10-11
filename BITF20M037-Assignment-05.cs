using System;
using System.Data;
using System.Data.SqlClient;

class Program
{
  static void Main(string[] args)
  {
    string connectionString = "Server=localhost;Database=AssignmentFive;User Id=Username;Password=P@ssword;";

    using (SqlConnection connection = new SqlConnection(connectionString))
    {
        connection.Open();

        while (true)
        {
            Console.WriteLine("Choose an option:");
            Console.WriteLine("1. Insert Employee");
            Console.WriteLine("2. Update Employee");
            Console.WriteLine("3. Delete Employee");
            Console.WriteLine("4. Select Employee by ID");
            Console.WriteLine("5. Read and Print All Employees");
            Console.WriteLine("6. Exit");

            int choice;
            if (!int.TryParse(Console.ReadLine(), out choice))
            {
                Console.WriteLine("Invalid choice. Please enter a valid option.");
                continue;
            }

            switch (choice)
            {
                case 1:
                    Console.WriteLine("Insert an Employee:");
                    Console.Write("First Name: ");
                    string firstName = Console.ReadLine();
                    Console.Write("Last Name: ");
                    string lastName = Console.ReadLine();
                    Console.Write("Email: ");
                    string email = Console.ReadLine();
                    Console.Write("Primary Phone Number: ");
                    string primaryPhoneNumber = Console.ReadLine();
                    Console.Write("Secondary Phone Number (optional, press Enter to skip): ");
                    string secondaryPhoneNumber = Console.ReadLine();
                    Console.Write("Created By: ");
                    string createdBy = Console.ReadLine();
                    DateTime createdOn = DateTime.Now;

                    InsertEmployee(connection, firstName, lastName, email, primaryPhoneNumber, secondaryPhoneNumber, createdBy, createdOn);
                    break;

                case 2:
                    Console.Write("Enter the ID of the Employee to update: ");
                    if (long.TryParse(Console.ReadLine(), out long employeeId))
                    {
                       // Check if the employee with the given ID exists before proceeding
                       bool employeeExists = CheckEmployeeExists(connection, employeeId);

                    if (employeeExists)
                    {
                       Console.WriteLine("Enter the updated information for the Employee:");

                       Console.Write("First Name: ");
                       string updatedFirstName = Console.ReadLine();

                       Console.Write("Last Name: ");
                       string updatedLastName = Console.ReadLine();

                       Console.Write("Email: ");
                       string updatedEmail = Console.ReadLine();

                       Console.Write("Primary Phone Number: ");
                       string updatedPrimaryPhoneNumber = Console.ReadLine();

                       Console.Write("Secondary Phone Number (optional, press Enter to skip): ");
                       string updatedSecondaryPhoneNumber = Console.ReadLine();

                       Console.Write("Modified By: ");
                       string modifiedBy = Console.ReadLine();
                       DateTime modifiedOn = DateTime.Now;

                      // Call the UpdateEmployeeById method to update the employee record
                      UpdateEmployeeById(connection, employeeId, updatedFirstName, updatedLastName, updatedEmail, updatedPrimaryPhoneNumber, updatedSecondaryPhoneNumber, modifiedBy, modifiedOn);
                    }
                    else
                    {
                        Console.WriteLine($"No employee record found with ID {employeeId}.");
                    }
             }
             else
             {
                 Console.WriteLine("Invalid input for Employee ID.");
             }
             break;


                case 3:
                    Console.Write("Enter the ID of the Employee to delete: ");
                    if (long.TryParse(Console.ReadLine(), out long employeeIdToDelete))
                    {
                        DeleteEmployeeById(connection, employeeIdToDelete);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Employee ID.");
                    }
                    break;

                case 4:
                    Console.Write("Enter the ID of the Employee to select: ");
                    if (long.TryParse(Console.ReadLine(), out long employeeIdToSelect))
                    {
                        SelectEmployeeById(connection, employeeIdToSelect);
                    }
                    else
                    {
                        Console.WriteLine("Invalid input for Employee ID.");
                    }
                    break;

                case 5:
                    ReadAndPrintAllEmployees(connection);
                    break;

                case 6:
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
 }


         
    static void CreateEmployee(SqlConnection connection, string firstName, string lastName, string email, string primaryPhoneNumber, string secondaryPhoneNumber, string createdBy, DateTime createdOn)
    {
        using (SqlCommand cmd = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, PrimaryPhoneNumber, SecondaryPhoneNumber, CreatedBy, CreatedOn) VALUES (@FirstName, @LastName, @Email, @PrimaryPhoneNumber, @SecondaryPhoneNumber, @CreatedBy, @CreatedOn)", connection))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PrimaryPhoneNumber", primaryPhoneNumber);
            cmd.Parameters.AddWithValue("@SecondaryPhoneNumber", (object)secondaryPhoneNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
            cmd.Parameters.AddWithValue("@CreatedOn", createdOn);

            cmd.ExecuteNonQuery();
        }
    }

    static void ReadAndPrintAllEmployees(SqlConnection connection)
    {
        using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees", connection))
        {
            using (SqlDataReader reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    // Read and print each column
                    Console.WriteLine($"ID: {reader["ID"]}");
                    Console.WriteLine($"FirstName: {reader["FirstName"]}");
                    Console.WriteLine($"LastName: {reader["LastName"]}");
                    Console.WriteLine($"Email: {reader["Email"]}");
                    Console.WriteLine($"PrimaryPhoneNumber: {reader["PrimaryPhoneNumber"]}");
                    Console.WriteLine($"SecondaryPhoneNumber: {reader["SecondaryPhoneNumber"]}");
                    Console.WriteLine($"CreatedBy: {reader["CreatedBy"]}");
                    Console.WriteLine($"CreatedOn: {reader["CreatedOn"]}");
                    Console.WriteLine($"ModifiedBy: {reader["ModifiedBy"]}");
                    Console.WriteLine($"ModifiedOn: {reader["ModifiedOn"]}");
                    Console.WriteLine("------------------------------------");
                }
            }
        }
    }

    static void InsertEmployee(SqlConnection connection, string firstName, string lastName, string email, string primaryPhoneNumber, string secondaryPhoneNumber, string createdBy, DateTime createdOn)
    {
        using (SqlCommand cmd = new SqlCommand("INSERT INTO Employees (FirstName, LastName, Email, PrimaryPhoneNumber, SecondaryPhoneNumber, CreatedBy, CreatedOn) VALUES (@FirstName, @LastName, @Email, @PrimaryPhoneNumber, @SecondaryPhoneNumber, @CreatedBy, @CreatedOn)", connection))
        {
            cmd.Parameters.AddWithValue("@FirstName", firstName);
            cmd.Parameters.AddWithValue("@LastName", lastName);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@PrimaryPhoneNumber", primaryPhoneNumber);
            cmd.Parameters.AddWithValue("@SecondaryPhoneNumber", (object)secondaryPhoneNumber ?? DBNull.Value);
            cmd.Parameters.AddWithValue("@CreatedBy", createdBy);
            cmd.Parameters.AddWithValue("@CreatedOn", createdOn);

            cmd.ExecuteNonQuery();

            Console.WriteLine("Employee record inserted successfully.");
        }
    }
   
   static void DeleteEmployeeById(SqlConnection connection, long employeeId)
   {
    using (SqlCommand cmd = new SqlCommand("DELETE FROM Employees WHERE ID = @EmployeeId", connection))
    {
        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
        int rowsAffected = cmd.ExecuteNonQuery();
        
        if (rowsAffected > 0)
        {
            Console.WriteLine($"Employee record with ID {employeeId} deleted successfully.");
        }
        else
        {
            Console.WriteLine($"No employee record found with ID {employeeId}.");
        }
     }


   static void SelectEmployeeById(SqlConnection connection, long employeeId)
   {
    using (SqlCommand cmd = new SqlCommand("SELECT * FROM Employees WHERE ID = @EmployeeId", connection))
    {
        cmd.Parameters.AddWithValue("@EmployeeId", employeeId);
        
        using (SqlDataReader reader = cmd.ExecuteReader())
        {
            if (reader.Read())
            {
                Console.WriteLine($"ID: {reader["ID"]}");
                Console.WriteLine($"FirstName: {reader["FirstName"]}");
                Console.WriteLine($"LastName: {reader["LastName"]}");
                Console.WriteLine($"Email: {reader["Email"]}");
                Console.WriteLine($"PrimaryPhoneNumber: {reader["PrimaryPhoneNumber"]}");
                Console.WriteLine($"SecondaryPhoneNumber: {reader["SecondaryPhoneNumber"]}");
                Console.WriteLine($"CreatedBy: {reader["CreatedBy"]}");
                Console.WriteLine($"CreatedOn: {reader["CreatedOn"]}");
                Console.WriteLine($"ModifiedBy: {reader["ModifiedBy"]}");
                Console.WriteLine($"ModifiedOn: {reader["ModifiedOn"]}");
            }
            else
            {
                Console.WriteLine($"No employee record found with ID {employeeId}.");
            }
         }
     }

  


}
