using Magazin_online;
namespace Magazin_online;
// public class ErrorHandler
// {
//     // Metoda de validare a prețului unui produs
//     public static bool ValidatePrice(decimal price)
//     {
//         if (price <= 0)
//         {
//             throw new ArgumentException("Prețul produsului trebuie să fie un număr pozitiv.");
//         }
//         if (price > 1000000)
//         {
//             throw new ArgumentException("Prețul produsului nu poate depăși 1.000.000.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a stocului unui produs
//     public static bool ValidateStock(int stock)
//     {
//         if (stock < 0)
//         {
//             throw new ArgumentException("Stocul nu poate fi negativ.");
//         }
//         if (stock > 100000)
//         {
//             throw new ArgumentException("Stocul produsului nu poate depăși 100.000.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a datei de expirare a unui produs
//     public static bool ValidateExpirationDate(DateTime expirationDate)
//     {
//         if (expirationDate < DateTime.Now)
//         {
//             throw new ArgumentException("Data de expirare nu poate fi trecută.");
//         }
//         if (expirationDate > DateTime.Now.AddYears(5))
//         {
//             throw new ArgumentException("Data de expirare nu poate fi mai mare de 5 ani.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a adresei de livrare
//     public static bool ValidateShippingAddress(string address)
//     {
//         if (string.IsNullOrEmpty(address))
//         {
//             throw new ArgumentException("Adresa de livrare este obligatorie.");
//         }
//         if (address.Length < 10)
//         {
//             throw new ArgumentException("Adresa de livrare trebuie să aibă cel puțin 10 caractere.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a căutării unui produs
//     public static bool ValidateProductExists(string productName, List<string> availableProducts)
//     {
//         if (string.IsNullOrEmpty(productName))
//         {
//             throw new ArgumentException("Numele produsului nu poate fi gol.");
//         }
//         if (!availableProducts.Contains(productName))
//         {
//             throw new ArgumentException($"Produsul '{productName}' nu există în magazin.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a coșului de cumpărături
//     public static bool ValidateCart(List<string> cartItems)
//     {
//         if (cartItems == null || cartItems.Count == 0)
//         {
//             throw new ArgumentException("Coșul de cumpărături este gol. Adaugă produse înainte de a plasa comanda.");
//         }
//         return true;
//     }
//
//     // Metoda de validare a comenzii
//     public static bool ValidateOrder(string shippingAddress, List<string> cartItems)
//     {
//         ValidateShippingAddress(shippingAddress);
//         ValidateCart(cartItems);
//         return true;
//     }
//
//     // Metoda pentru gestionarea excepțiilor și afisarea erorilor
//     public static void HandleException(Exception ex)
//     {
//         Console.ForegroundColor = ConsoleColor.Red; // Schimbăm culoarea textului pentru a evidenția eroarea
//         Console.WriteLine($"Eroare: {ex.Message}");
//         Console.ResetColor(); // Resetăm culoarea pentru a nu afecta alte mesaje
//     }
// }


public static class ErrorHandler
{
    public static void HandleException(Exception ex)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"Eroare: {ex.Message}");
        Console.ResetColor();
    }
}
