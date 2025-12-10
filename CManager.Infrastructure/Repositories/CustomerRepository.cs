
/*
 
!! Using chatGPT !! 
In this file, I have used chatGPT to create the repository to delete a customer.

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

    public bool SaveCustomers(List<CustomerModel> Customers)
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

    public bool DeleteCustomer(string Email)
    {
        // Retrieves all customers.
        List<CustomerModel> customers = GetAllCustomers();

        // Iterates through the CustomerModel list to find the object with the correct email.
        CustomerModel customerModelToRemove = customers.FirstOrDefault(c => c.Email == Email)!;


        // If the correct email is found, that item is deleted and then the updated list is saved.
        if (customerModelToRemove != null)
        {
            customers.Remove(customerModelToRemove);
            SaveCustomers(customers);
            return true;
        }

        // If the correct email does not exist, FALSE is returned and nothing happens to the list.
        return false;
    }

/* ------------------------------------ chatGPT code END --------------------------------------------------- */


    public CustomerModel GetCustomerByName(string FirstName)
    {
        List<CustomerModel> customers = GetAllCustomers();

        CustomerModel customerToDisplay = customers.FirstOrDefault(c => c.FirstName == FirstName)!; ;

        return customerToDisplay;
    }

}
