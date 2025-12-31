using CManager.Domain.Models;

namespace CManager.Business.Services;

public interface ICustomerService
{
    bool CreateCustomer(string firstName, string lastName, string email, string phoneNr, string StreetAddress, string ZipCode, string City);

    IEnumerable<CustomerModel> GetAllCustomers(out bool hasError);

    CustomerModel GetCustomerByEmail(string email);

    bool DeleteCustomer(string email);

    bool UpdateCustomer(CustomerModel customer);
}