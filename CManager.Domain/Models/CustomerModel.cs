namespace CManager.Domain.Models;

public class CustomerModel
{
    public string Id { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNr { get; set; } = null!;
    public CustomerAddressModel Address { get; set; } = null!;
}
