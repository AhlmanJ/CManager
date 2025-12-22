using CManager.Business.Helpers;
using CManager.Domain.Models;

namespace CManager.Business.Factories;

public static class CustomerFactory
{
    public static CustomerModel Create(string firstName, string lastName, string email, string phoneNr, string streetAddress, string zipCode, string city)
    {
        CustomerModel customerModel = new()
        {
            Id = CustomerIdGenerator.GenerateGuidId(),
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            PhoneNr = phoneNr,
            Address = new CustomerAddressModel
            {
                StreetAddress = streetAddress,
                ZipCode = zipCode,
                City = city
            }
        };

        return customerModel;
    }

}
