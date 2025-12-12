using CManager.Domain.Models;

namespace CManager.Application.Services;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNr, string StreetAddress, string ZipCode, string City);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    CustomerModel GetCustomerByName(string name);

    bool DeleteCustomer(string email);
}