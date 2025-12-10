using CManager.Domain.Models;

namespace CManager.Application.Services;

public interface ICustomerSerice
{
    bool AddNewCustomer(string firstName, string lastName, string email, string phoneNr, string StreetAddress, string ZipCode, string City);

    IEnumerable<CustomerModel> GetAllCustomers();

    CustomerModel GetCustomerById(int Id);

    CustomerModel GetCustomerByName(string name);

    CustomerModel UpdateCustomer(int Id, CustomerModel newCustomerModel);

    bool DeleteCustomer(string email);
}


