namespace EquipmentManagement.Application.Generators;

public static class CodeGenerator
{
    public static string GenerateUniqCode()
    {
        return Guid.NewGuid().ToString("N");
    }

    public static int GenerateMobileActiveCode()
    {
        return new Random().Next(100000, 999999);
    }
}
