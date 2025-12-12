
/*
------------------------------------------------------- DEBUG help by CHATGPT ------------------------------------------------------------------------------ 

 In my method "GetCustomerByName()" I tried to use a foreach loop to print all the information about the user but it resulted in all the users being printed. 
 ChatGPT helped me debug the code and then gave me the information to remove the foreach loop. Then it worked as intended.
 
 ----------------------------------------------------------------------------------------------------------------------------------------------------------- 
 */



using CManager.Application.Services;


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
        Console.Write("First name: ");
        var firstName = Console.ReadLine();
        Console.Write("Last name: ");
        var lastName = Console.ReadLine();
        Console.Write("Email: ");
        var email = Console.ReadLine();
        Console.Write("Phonenumber: ");
        var phoneNr = Console.ReadLine(); 
        Console.Write("Street address: ");
        var streetAddress = Console.ReadLine();
        Console.Write("Zipcode: ");
        var zipCode = Console.ReadLine();
        Console.Write("City: ");
        var city = Console.ReadLine();

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

        Console.WriteLine("");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();
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
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
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

            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
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
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
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
        string email =Console.ReadLine().ToLower();

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

        Console.WriteLine("");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();

    }

    public void GetCustomerByName()
    {
        var customers = _customerService.GetAllCustomers(out bool hasError);

        Console.Clear();
        Console.WriteLine("Get all information about a Customer:");
        Console.WriteLine("");
        Console.Write("Enter customer First name: ");
        string name = Console.ReadLine().ToLower();

        if (name == null || name == "")
        {
            Console.Clear();
            Console.WriteLine("Input cannot be empty!");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();

            return;
        }
        else if (!customers.Any())
        {
            Console.Clear();
            Console.WriteLine("No customers to delete.");
            Console.WriteLine("");
            Console.WriteLine("Press any key to continue....");
            Console.ReadKey();
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

        Console.WriteLine("");
        Console.WriteLine("Press any key to continue....");
        Console.ReadKey();
    }
}
