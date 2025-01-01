using Magazin_online;
namespace Magazin_online;
public static class ErrorHandler
{
    public static void HandleException(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Eroare: {ex.Message}");
        Console.ResetColor();
    }
}
