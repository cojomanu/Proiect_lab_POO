using Magazin_online;
namespace Magazin_online;
public static class ErrorHandler
{
    public static void HandleException(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Eroare: {ex.Message} (cu rosu e avertizare , cu alb e trhown exception)");
        Console.ResetColor();
    }

    public static void Throw(Exception ex)
    {
        
        HandleException(ex);
        throw ex; // Re-aruncă excepția
    }
}
