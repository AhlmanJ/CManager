using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface IGetByEmail
{
    CustomerModel GetCustomerByEmail(string email);
}
