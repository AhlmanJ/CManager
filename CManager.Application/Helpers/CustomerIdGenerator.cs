namespace CManager.Business.Helpers;

public static class CustomerIdGenerator
{
    public static Guid GenerateGuidId() => Guid.NewGuid();
}
