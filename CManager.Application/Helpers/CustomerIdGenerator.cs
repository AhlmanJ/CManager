namespace CManager.Application.Helpers;

public static class CustomerIdGenerator
{
    public static Guid GenerateGuidId() => Guid.NewGuid();
}
