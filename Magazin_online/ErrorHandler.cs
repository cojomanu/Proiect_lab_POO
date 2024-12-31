namespace Magazin_online;

public static class ErrorHandler
{
    public static void HandleException(Exception ex)
    {
        Console.WriteLine($"An error occurred: {ex.Message}");
    }

    public static int TryParseInt(string input)
    {
        try
        {
            return int.Parse(input);
        }
        catch (FormatException ex)
        {
            HandleException(ex);
            return -1;
        }
    }

    public static decimal TryParseDecimal(string input)
    {
        try
        {
            return decimal.Parse(input);
        }
        catch (FormatException ex)
        {
            HandleException(ex);
            return -1m;
        }
    }

    public static DateTime TryParseDateTime(string input)
    {
        try
        {
            return DateTime.Parse(input);
        }
        catch (FormatException ex)
        {
            HandleException(ex);
            return DateTime.MinValue;
        }
    }
}
