using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface IGetAll
{
    List<CustomerModel> GetAllCustomers();
}
