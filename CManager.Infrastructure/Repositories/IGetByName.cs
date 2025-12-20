using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface IGetByName
{
    CustomerModel GetCustomerByName(string FirstName);
}
