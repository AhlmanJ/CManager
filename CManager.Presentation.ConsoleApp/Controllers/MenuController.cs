
/*
------------------------------------------------------- DEBUG help by CHATGPT ------------------------------------------------------------------------------ 

 In my method "GetCustomerByName()" I tried to use a foreach loop to print all the information about the user but it resulted in all the users being printed. 
 ChatGPT helped me debug the code and then gave me the information to remove the foreach loop. Then it worked as intended.
 
 ----------------------------------------------------------------------------------------------------------------------------------------------------------- 
 */



using CManager.Application.Services;
using CManager.Application.Validators;

namespace CManager.Presentation.ConsoleApp.Controllers;

public class MenuController
{
    private readonly ICustomerService _customerService;

    public MenuController(ICustomerService customerService)
    {
        _customerService = customerService;
    }

    public void MainMenu()
    {
        string menuInput = "";

        do
        {
            Console.Clear();
            Console.WriteLine("         CManager          ");
            Console.WriteLine("Customer Management System ");
            Console.WriteLine("");
            Console.WriteLine("===== MAIN MENU =====");
            Console.WriteLine("");
            Console.WriteLine("1. Create Customer.");
            Console.WriteLine("2. View All Customers.");
            Console.WriteLine("3. View One Customer.");
            Console.WriteLine("4. Remove Customer.");
            Console.WriteLine("Q. Exit. ");
            Console.WriteLine("");
            Console.WriteLine("=====================");
            Console.WriteLine("");
            Console.Write("INPUT: ");
            menuInput = Console.ReadLine().ToLower();

            switch (menuInput)
            {
                case "1":
                    CreateCustomer();
                    break;
                case "2":
                    GetAllCustomers();
                    break;
                case "3":
                    GetCustomerByName();
                    break; 
                case "4":
                    DeleteCustomer();
                    break;
                case "q":
                    Console.Clear();
                    Console.WriteLine("The program is shut down...");
                    Console.WriteLine("Thank you for using CManager!");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    break;
                default:
                    Console.WriteLine("Not a valid input!");
                    Console.WriteLine("");
                    Console.WriteLine("Press any key to continue....");
                    Console.ReadKey();
                    break;
            }

        }while (menuInput != "q");
    }

    private void CreateCustomer()
    {
        Console.Clear();
        Console.WriteLine("Create new customer:");
        Console.WriteLine("");

        var firstName = InputValidator.ValidateInput("First name: ");
        var lastName = InputValidator.ValidateInput("Last name: ");
        var email = EmailValidator.ValidateEmail("Email: ");
        var phoneNr = PhoneNrValidator.ValidatePhoneNr("Phone number: ");
        var streetAddress = InputValidator.ValidateInput("Street address: ");
        var zipCode = InputValidator.ValidateInput("Zipcode: ");
        var city = InputValidator.ValidateInput("City: ");

        var result = _customerService.CreateCustomer(firstName, lastName, email, phoneNr, streetAddress, zipCode, city );

        if (result)
        {
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("Customer created:");
            Console.WriteLine($"Name: {firstName} {lastName}.");
        }
        else
        {
            Console.WriteLine("Something went wrong! Customer was not created, please try again.");
        }

        SystemHolder();
    }

    private void GetAllCustomers()
    {
        Console.Clear();
        
        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
            Console.WriteLine("Could not display all customers. Please try again in a moment.");

        if (!customers.Any())
        {
            Console.WriteLine("The list is empty!");
            Console.WriteLine("");
            SystemHolder();
        }
        else
        {
            Console.WriteLine("All customers:");
            Console.WriteLine("");

            foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine("-----------------------------------------------");
            }

            SystemHolder();
        }
    }

    private void DeleteCustomer()
    {
        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (hasError)
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine("Could not display all customers. Please try again in a moment.");

        if (!customers.Any())
        {
            Console.Clear();
            Console.WriteLine("No customers to delete.");
            SystemHolder();
            return;
        }

        Console.Clear();
        Console.WriteLine("Delete customer from list:");
        Console.WriteLine("");
        Console.WriteLine("Customers in the system:");
        Console.WriteLine("");

        foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine("-----------------------------------------------");
            }

        Console.WriteLine("");
        Console.Write("Enter customer Email: ");
        string email = Console.ReadLine().ToLower();

        var result = _customerService.DeleteCustomer(email);

        if (result)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Customer {email} has been deleted.");
        }
        else
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Somethig went wrong, customer {email} was not deleted.");
        }

        SystemHolder();
    }

    public void GetCustomerByName()
    {
        var customers = _customerService.GetAllCustomers(out bool hasError);

        Console.Clear();
        Console.WriteLine("Get all information about a Customer:");
        Console.WriteLine("");
        Console.Write("Enter customer First name: ");
        string name = Console.ReadLine().ToLower();

        if (!customers.Any())
        {
            Console.Clear();
            Console.WriteLine("No customers to delete.");
            SystemHolder();
            return;
        }

        try
        {
            var customer = _customerService.GetCustomerByName(name);

            Console.WriteLine("");
            Console.Clear();
            Console.WriteLine("-----------------------------------------------");

            // ------------ ChatGPT helpt me by removing a foreach-loop here! --------------------

            Console.WriteLine($"Customer Information:");
            Console.WriteLine("");
            Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
            Console.WriteLine($"ID: {customer.Id}");
            Console.WriteLine($"Phone: {customer.PhoneNr}");
            Console.WriteLine($"Email: {customer.Email}");
            Console.WriteLine($"Address: {customer.Address.StreetAddress}");
            Console.WriteLine($"ZipCode: {customer.Address.ZipCode}");
            Console.WriteLine($"City: {customer.Address.City}");
            Console.WriteLine("");
            Console.WriteLine("-----------------------------------------------");
            Console.WriteLine("");
        }
        catch (Exception ex)
        {
            Console.Clear();
            Console.WriteLine($"The requested customer '{name}' could not be found.");
            
        }

        SystemHolder();
    }

    public void SystemHolder()
    {
        Console.WriteLine("");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();
    }
}
