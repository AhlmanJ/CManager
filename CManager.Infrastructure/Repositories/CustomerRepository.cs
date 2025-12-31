
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
using CManager.Infrastructure.serialization;

namespace CManager.Infrastructure.Repositories;

public class CustomerRepository : ICustomerRepository
{
    private readonly string _filePath;
    private readonly string _directoryPath;

    public CustomerRepository(string directoryPath = "Data", string fileName = "List.json")
    {  
        _directoryPath = directoryPath;
        _filePath = Path.Combine(_directoryPath, fileName);
    }

    public bool CreateCustomer(List<CustomerModel> Customers)
    {
        if(Customers == null)
            return false;

        try
        {
            var json = JsonDataFormatter.serialize(Customers);

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
            var customers = JsonDataFormatter.Deserialize<List<CustomerModel>>(json);
            return customers ?? [];
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error when trying to load all customers: {ex.Message}");
            throw;
        }
    }

    /*----------------------------- Created by chatGPT! (Except "Retrieves all customers" and my own comments ) ----------------------------------*/

    public bool DeleteCustomer(string email)
    {

        // Retrieves all customers.
        var json = File.ReadAllText(_filePath);
        var customers = JsonDataFormatter.Deserialize<List<CustomerModel>>(json);

        if (customers is null) 
        {
            return false;
        }

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

    public CustomerModel GetCustomerByEmail(string email)
    {

        var json = File.ReadAllText(_filePath);
        var customers = JsonDataFormatter.Deserialize<List<CustomerModel>>(json);
        CustomerModel customerToDisplay = customers!.FirstOrDefault(c => c.Email.ToLower() == email)!; // Code debugged with help by chatGPT!

        if (customerToDisplay is null)
        {
            throw new Exception($"Could not find the customer {email}");
        }

        return customerToDisplay;
    }



// To create this repository, I have been helped by articles on the internet (Source reference is available if necessary.) and also chatGPT.
    public bool UpdateCustomer(CustomerModel customer)
    {
        var json = File.ReadAllText(_filePath);

        var customers = JsonDataFormatter.Deserialize<List<CustomerModel>>(json);
        if(customers is null) 
             return false;

        var oldCustomerInfo = customers.FirstOrDefault(c => c.Id == customer.Id);

        if (oldCustomerInfo == null) 
            return false;

        // In this part, I have done the same thing as both the internet and chatGPT examples have shown.
        oldCustomerInfo.FirstName = customer.FirstName;
        oldCustomerInfo.LastName = customer.LastName;
        oldCustomerInfo.Email = customer.Email;
        oldCustomerInfo.PhoneNr = customer.PhoneNr;
        oldCustomerInfo.Address.StreetAddress = customer.Address.StreetAddress;
        oldCustomerInfo.Address.ZipCode = customer.Address.ZipCode;
        oldCustomerInfo.Address.City = customer.Address.City;
        
        File.WriteAllText(_filePath, JsonDataFormatter.serialize(customers));
        
        return true;
    }
}
