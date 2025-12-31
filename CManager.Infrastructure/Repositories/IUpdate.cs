using CManager.Domain.Models;

namespace CManager.Infrastructure.Repositories;

public interface IUpdate
{
    bool UpdateCustomer(CustomerModel customer);
}
