using System.Text.RegularExpressions;

namespace Magazin_online;

public static class Validare
{
    public static void ValidareNumeProdus(string nume)
    {
        if (string.IsNullOrWhiteSpace(nume))
            ErrorHandler.Throw(new ArgumentException("Numele produsului nu poate fi gol."));

        if (!Regex.IsMatch(nume, @"^[a-z]+$"))
            ErrorHandler.Throw(new ArgumentException(
                "Numele produsului trebuie sa contina doar litere mici, fara spatii sau caractere speciale."));
    }

    public static void ValidarePret(decimal pret)
    {
        if (pret <= 0)
            ErrorHandler.Throw(new ArgumentException("Pretul produsului trebuie sa fie un numar pozitiv."));
        
        if (pret > Config.PretMaxim)
            ErrorHandler.Throw(new ArgumentException($"Pretul produsului nu poate depasi {Config.PretMaxim:N}."));
    }

    public static void ValidareStoc(int stoc)
    {
        if (stoc < 0)
            ErrorHandler.Throw(new ArgumentException("Stocul nu poate fi negativ."));
        
        if (stoc > Config.StocMaxim)
            ErrorHandler.Throw(new ArgumentException($"Stocul produsului nu poate depasi {Config.StocMaxim}."));
    }

    public static void ValidareDataExpirare(DateTime dataExpirare)
    {
        if (dataExpirare < DateTime.Now)
            ErrorHandler.Throw(new ArgumentException("Data de expirare nu poate fi trecuta."));
        
        if (dataExpirare > DateTime.Now.AddYears(Config.AniMaximExpirare))
            ErrorHandler.Throw(new ArgumentException($"Data de expirare nu poate fi mai mare de {Config.AniMaximExpirare} ani."));
    }

    public static void ValidareAdresaLivrare(string adresa)
    {
        if (string.IsNullOrEmpty(adresa))
            ErrorHandler.Throw(new ArgumentException("Adresa de livrare este obligatorie."));
        
        if (adresa.Length < Config.LungimeMinimaAdresa)
            ErrorHandler.Throw(new ArgumentException($"Adresa de livrare trebuie sa aiba cel putin {Config.LungimeMinimaAdresa} caractere."));
    }

    public static void ValidareNumeClient(string nume)
    {
        if (string.IsNullOrEmpty(nume))
            ErrorHandler.Throw(new ArgumentException("Numele clientului este obligatoriu."));
        
        if (!Regex.IsMatch(nume, @"^[a-zA-Z\s]+$"))
            ErrorHandler.Throw(new ArgumentException("Numele clientului poate contine doar litere si spatii."));
    }

    public static void ValidareEmail(string email)
    {
        if (string.IsNullOrEmpty(email))
            ErrorHandler.Throw(new ArgumentException("Adresa de e-mail este obligatorie."));
        
        if (!Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            ErrorHandler.Throw(new ArgumentException("Adresa de e-mail nu este valida."));
    }

    public static void ValidareCos(List<ProdusGeneric> produseDinCos)
    {
        if (produseDinCos == null || produseDinCos.Count == 0)
            ErrorHandler.Throw(new ArgumentException("Cosul de cumparaturi este gol. Adauga produse inainte de a plasa comanda."));
    }

    public static bool ValidareComanda(string numeClient, string email, string adresaLivrare)
    {
        ValidareNumeClient(numeClient);
        ValidareEmail(email);
        ValidareAdresaLivrare(adresaLivrare);
        return true;
    }

    public static void ValidareClasaEficienta(string clasaEficienta, int putere_maxima)
    {
        if (string.IsNullOrWhiteSpace(clasaEficienta))
            ErrorHandler.Throw(new ArgumentException("Clasa de eficienta nu poate fi goala."));

        if (!Regex.IsMatch(clasaEficienta, @"^[A-Z]+$"))
            ErrorHandler.Throw(new ArgumentException("Clasa de eficienta trebuie sa contina doar litere mari."));
        
        if (putere_maxima > Config.PutereMaximaAparatElectrocasnic)
            ErrorHandler.Throw(new ArgumentException("Puterea maxima a fost depasita!"));
    }

    public static void ValidareConditiiPastrare(string conditiiPastrare)
    {
        if (string.IsNullOrWhiteSpace(conditiiPastrare))
            ErrorHandler.Throw(new ArgumentException("Conditiile de depozitare nu pot fi goale."));
        
        if (!Regex.IsMatch(conditiiPastrare, @"^[a-z]+$"))
            ErrorHandler.Throw(new ArgumentException("Conditiile de depozitare trebuie sa contina doar litere mici."));
    }

    public static void ValidareCantitateStoc(int cantitate, int stocCurent)
    {
        if (cantitate <= 0)
            ErrorHandler.Throw(new ArgumentException("Cantitatea trebuie sa fie un numar pozitiv."));
        
        if (cantitate > stocCurent)
            ErrorHandler.Throw(new ArgumentException("Cantitatea de stoc scazuta nu poate depasi stocul disponibil."));
    }

    public static void ValidareDataLivrare(DateTime dataLivrare, DateTime dataInitialaComanda)
    {
        DateTime dataCurenta = DateTime.Now;
        DateTime dataLimita = dataInitialaComanda.AddDays(14); // 2 saptamani de la data initiala a comenzii

        if (dataLivrare < dataCurenta)
            ErrorHandler.Throw(new ArgumentException("Data de livrare nu poate fi mai devreme decat data curenta."));
        
        if (dataLivrare < dataInitialaComanda)
            ErrorHandler.Throw(new ArgumentException("Data de livrare nu poate fi mai devreme decat data initiala a comenzii."));
        
        if (dataLivrare > dataLimita)
            ErrorHandler.Throw(new ArgumentException("Data de livrare nu poate fi mai tarziu de doua saptamani de la data initiala a comenzii."));
    }
}
