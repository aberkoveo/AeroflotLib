namespace Integra.Persistence;

public class DbInitializer
{
    public static void Initialize(SupportRequestDBContext context)
    {
        context.Database.EnsureCreated();
    }
}