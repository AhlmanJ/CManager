
/*
 
!! Using chatGPT !! 
In this file, I have used chatGPT to create the service to delete a customer!

 */



using CManager.Application.Helpers;
using CManager.Application.Services;
using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;

namespace CManager.Application.Services;

public class CustomerService : ICustomerService
{

    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {  
        _customerRepository = customerRepository;
    }

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNr, string streetAddress, string zipCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = CustomerIdGenerator.GenerateGuidId(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNr = phoneNr,
            Address = new CustomerAddressModel
            {
                StreetAddress = streetAddress,
                ZipCode = zipCode,
                City = city
            }
        };

        var customers = _customerRepository.GetAllCustomers();
        customers.Add(customerModel);
        var result = _customerRepository.CreateCustomer(customers);
        return result;
    }

    public IEnumerable<CustomerModel> GetAllCustomers(out bool hasError)
    {
        hasError = false;

        try
        {
            var customers = _customerRepository.GetAllCustomers();
            return customers;
        }
        catch (Exception)
        {
            hasError = true;
            return [];
        }
    }

    /*----------------------------- Created by chatGPT! (But my own comments ) ----------------------------------*/

    public bool DeleteCustomer(string email)
    {

        // Sends the email parameter to the "DeleteCustomer" method. Retrieves the result from the repository method "DeleteCustomer" and saves it in the variable result.
        bool result = _customerRepository.DeleteCustomer(email);

        // Returns the result from the method "DeleteCustomer".
        return result;
    }

    /* ------------------------------------ chatGPT code END --------------------------------------------------- */

    public CustomerModel GetCustomerByName(string name)
    {
        var CustomerModel = _customerRepository.GetCustomerByName(name);
        return CustomerModel;
    }
}
