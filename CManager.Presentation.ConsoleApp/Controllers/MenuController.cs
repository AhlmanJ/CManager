
/*
------------------------------------------------------- DEBUG help by CHATGPT ------------------------------------------------------------------------------ 

 In my method "GetCustomerByName()" I tried to use a foreach loop to print all the information about the user but it resulted in all the users being printed. 
 ChatGPT helped me debug the code and then gave me the information to remove the foreach loop. Then it worked as intended.
 
 ----------------------------------------------------------------------------------------------------------------------------------------------------------- 
 */



using CManager.Business.Services;
using CManager.Business.Validators;
using CManager.Presentation.ConsoleApp.Helpers;

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
            Console.WriteLine("3. View All Info About One Customer.");
            Console.WriteLine("4. Delete Customer.");
            Console.WriteLine("Q. Exit. ");
            Console.WriteLine("");
            Console.WriteLine("=====================");
            Console.WriteLine("");
            Console.Write("INPUT: ");
            menuInput = Console.ReadLine()!.ToLower();

            switch (menuInput)
            {
                case "1":
                    CreateCustomer();
                    break;
                case "2":
                    GetAllCustomers();
                    break;
                case "3":
                    GetCustomerByEmail();
                    break; 
                case "4":
                    DeleteCustomer();
                    break;
                case "q":
                    Console.Clear();
                    Console.WriteLine("The program is shutting down...");
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

        ReadKey.SystemHolder();
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
            ReadKey.SystemHolder();
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

            ReadKey.SystemHolder();
        }
    }

    private void DeleteCustomer()
    {
        var customers = _customerService.GetAllCustomers(out bool hasError);

        if (!customers.Any())
        {
            Console.Clear();
            Console.WriteLine("No customers to delete.");
            ReadKey.SystemHolder();
            return;
        }

        Console.Clear();
        Console.WriteLine("Delete customer from list:");
        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Customers in the system:");
        Console.WriteLine("");

        foreach (var customer in customers)
            {
                Console.WriteLine($"Name: {customer.FirstName} {customer.LastName}");
                Console.WriteLine($"Email: {customer.Email}");
                Console.WriteLine("-----------------------------------------------");
            }

        Console.WriteLine("Enter (E)xit if you want to return to Main menu.");
        Console.WriteLine("");
        Console.Write("Enter customer Email to delete: ");
        string email = Console.ReadLine()!.ToLower();

        if (email == "e")
        {
            Console.Clear();
            Console.WriteLine("Returning to Main menu..");
        }

        var result = _customerService.DeleteCustomer(email);

        if (result)
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Customer {email} has been deleted.");
        }
        else if (email != "e")
        {
            Console.Clear();
            Console.WriteLine("");
            Console.WriteLine($"Somethig went wrong, customer {email} was not deleted.");
        }

        ReadKey.SystemHolder();
    }

    public void GetCustomerByEmail()
    {
        var customers = _customerService.GetAllCustomers(out bool hasError);

        Console.Clear();
        Console.WriteLine("Get all information about a Customer:");
        Console.WriteLine("");

        if (!customers.Any())
        {
            Console.Clear();
            Console.WriteLine("No customers to display.");
            ReadKey.SystemHolder();
            return;
        }

        Console.WriteLine("-----------------------------------------------");
        Console.WriteLine("Enter (E)xit if you want to return to Main menu.");
        Console.WriteLine("");
        Console.Write("Enter customer Email: ");
        string email = Console.ReadLine()!.ToLower();

        if (email == "e")
        {
            Console.Clear();
            Console.WriteLine("Returning to Main menu..");
        }
        else
        {
            try
            {
                var customer = _customerService.GetCustomerByEmail(email);

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
            catch (Exception)
            {
                Console.Clear();
                Console.WriteLine($"The requested customer '{email}' could not be found.");

            }
        }

        ReadKey.SystemHolder();
    }
}
