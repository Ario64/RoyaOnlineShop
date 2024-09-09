namespace OnlineShop.Core.Generators;

public class NameGenerator
{
    public static string GenerateUniqueName()
    {
        return Guid.NewGuid().ToString().Replace("-","");
    }
}