/*
 
!! Using chatGPT !! 
In this file, I have used chatGPT to create the service to delete a customer!

 */

using CManager.Business.Factories;
using CManager.Domain.Models;
using CManager.Infrastructure.Repositories;

namespace CManager.Business.Services;

public class CustomerService : ICustomerService
{

    private readonly ICustomerRepository _customerRepository;

    public CustomerService(ICustomerRepository customerRepository)
    {  
        _customerRepository = customerRepository;
    }

    public bool CreateCustomer(string firstName, string lastName, string email, string phoneNr, string streetAddress, string zipCode, string city)
    {
        var newCustomer = CustomerFactory.Create(firstName, lastName, email, phoneNr, streetAddress, zipCode, city);
        var result = _customerRepository.CreateCustomer(newCustomer);
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
        // Creates a check so that the input parameter is not Null. If the input is Null, the method returns "false".
        if (email == null)
        {
            return false;
        }

        // Sends the email parameter to the "DeleteCustomer" method. Retrieves the result from the repository method "DeleteCustomer" and saves it in the variable result.
        bool result = _customerRepository.DeleteCustomer(email);

        // Returns the result from the method "DeleteCustomer".
        return result;
    }

    /* ------------------------------------ chatGPT code END --------------------------------------------------- */

    // Thanks to chatGPT's help in creating the DeleteCustomer() method, I was able to understand how to build this code as I couldn't find any good information about this on Google or Youtube.
    public CustomerModel GetCustomerByEmail(string email)
    {
       
        var CustomerModel = _customerRepository.GetCustomerByEmail(email);

        return CustomerModel;
    }

    public bool UpdateCustomer(CustomerModel customer)
    {
        if (customer == null) return false;

        // A "Null or Whitespace check" for all parameters in the object.
        if( string.IsNullOrWhiteSpace(customer.FirstName)||
            string.IsNullOrWhiteSpace(customer.LastName)|| 
            string.IsNullOrWhiteSpace(customer.Email)|| 
            string.IsNullOrWhiteSpace(customer.PhoneNr)||
            string.IsNullOrWhiteSpace(customer.Address.StreetAddress)||
            string.IsNullOrWhiteSpace(customer.Address.StreetAddress)||
            string.IsNullOrWhiteSpace(customer.Address.City)) { return false; }

        var newCustomerInfo = customer;
        var result = _customerRepository.UpdateCustomer(newCustomerInfo);

        return result;
    }
}
