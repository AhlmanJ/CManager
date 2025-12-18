
/*
 
!! Using chatGPT !! 
---------------------------------------------------------------------------------
1.

In this file, I have used chatGPT to create the repository to delete a customer.

--------------------------------------------------------------------------------
2.

I also got help to debug this code = (c => c.FirstName == FirstName); 

I kept getting this error message when searching for a specific customer by first name:
"System.NullReferenceException: 'Object reference not set to an instance of an object.'
 customer was null."
- The answer was that I needed to convert the string so that all letters were in lowercase.
- I also needed to check if the result was " null ", and then throw a exception if it was.
- To get more information about " Null " and how to deal with it, i both watched Youtube tutorials and asked chatGPT.

- A source reference to the YouTube video where they talk about this is available if needed.
*/


using CManager.Domain.Models;
using System.Text.Json;

namespace CManager.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _filePath;
    private readonly string _directoryPath;
    private readonly JsonSerializerOptions _jsonOptions;

    public CustomerRepository(string directoryPath = "Data", string fileName = "List.json")
    {  
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);

        _jsonOptions = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true,
        };
    }

    public bool CreateCustomer(List<CustomerModel> Customers)
    {
        if(Customers == null)
            return false;

        try
        {
            var json = JsonSerializer.Serialize(Customers, _jsonOptions);

            if(!Directory.Exists(_directoryPath))
                Directory.CreateDirectory(_directoryPath);

            File.WriteAllText(_filePath, json);

            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when saving new customer: { ex.Message}");
            return false;
        }
    }

    public List<CustomerModel> GetAllCustomers()
    {
        if(!File.Exists(_filePath))
        {
            return [];
        }

        try
        {
            var json = File.ReadAllText(_filePath);
            var customers = JsonSerializer.Deserialize<List<CustomerModel>>(json, _jsonOptions);
            return customers ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when trying to load all customers: {ex.Message}");
            throw;
        }
    }

    /*----------------------------- Created by chatGPT! (But my own comments ) ----------------------------------*/

    public bool DeleteCustomer(string email)
    {
        // Retrieves all customers.
        List<CustomerModel> customers = GetAllCustomers();

        // Iterates through the CustomerModel list to find the object with the correct email.
        CustomerModel customerModelToRemove = customers.FirstOrDefault(c => c.Email.ToLower() == email)!;


        // If the correct email is found, that item is deleted and then the updated list is saved and the method returns a bool "true".
        if (customerModelToRemove != null)
        {
            customers.Remove(customerModelToRemove);
            CreateCustomer(customers);
            return true;
        }

        // If the correct email does not exist, FALSE is returned and nothing happens to the list.
        return false;
    }

    /* ------------------------------------ chatGPT code END --------------------------------------------------- */




    // Thanks to chatGPT's help in creating the DeleteCustomer() method, I was able to understand how to build this code as I couldn't find any good information about this on Google or Youtube.

    public CustomerModel GetCustomerByName(string name)
    {
        
        List<CustomerModel> customers = GetAllCustomers();
        CustomerModel customerToDisplay = customers.FirstOrDefault(c => c.FirstName.ToLower() == name)!; // Code debugged with help by chatGPT!

        if (customerToDisplay is null)
        {
            throw new Exception($"Could not find the customer {name}");
        }
        
        return customerToDisplay;
    }
}
