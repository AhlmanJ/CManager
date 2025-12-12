using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface ICustomerRepository
{
    bool CreateCustomer(List<CustomerModel> Customers);
    List<CustomerModel> GetAllCustomers();
    CustomerModel GetCustomerByName(string FirstName);
    bool DeleteCustomer(string Email);
}
