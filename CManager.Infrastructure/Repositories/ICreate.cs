using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface ICreate
{
    bool CreateCustomer(List<CustomerModel> Customers);
}
