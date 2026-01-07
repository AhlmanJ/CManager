namespace CManager.Domain.Models;

public class CustomerModel
{
    public Guid Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = string.Empty;
    public string PhoneNr { get; set; } = null!;
    public CustomerAddressModel Address { get; set; } = null!;
}
