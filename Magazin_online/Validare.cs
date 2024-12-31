using System.Text.RegularExpressions;

namespace Magazin_online;

    public static class Validare
    {
        // public static bool ValidarePret(decimal pret)
        // {
        //     if (pret <= 0)
        //         throw new ArgumentException("Prețul produsului trebuie să fie un număr pozitiv.");
        //     if (pret > Config.PretMaxim)
        //         throw new ArgumentException($"Prețul produsului nu poate depăși {Config.PretMaxim:N}.");
        //     return true;
        // }
        
        // Validare nume produs (doar litere mici si fara caractere speciale)
        public static void ValidareNumeProdus(string nume)
        {
            if (string.IsNullOrWhiteSpace(nume))
                throw new ArgumentException("Numele produsului nu poate fi gol.");

            // Verifica daca numele contine doar litere mici si nu are spatii sau caractere speciale
            if (!Regex.IsMatch(nume, @"^[a-z]+$"))
                throw new ArgumentException("Numele produsului trebuie sa contina doar litere mici, fara spatii sau caractere speciale.");
        }
        
        public static bool ValidarePret(decimal pret)
        {
            if (pret <= 0)
                throw new ArgumentException("Prețul produsului trebuie să fie un număr pozitiv.");
            if (pret > Config.PretMaxim)
                throw new ArgumentException($"Prețul produsului nu poate depăși {Config.PretMaxim:N}.");
            return true;
        }


        public static bool ValidareStoc(int stoc)
        {
            if (stoc < 0)
                throw new ArgumentException("Stocul nu poate fi negativ.");
            if (stoc > Config.StocMaxim)
                throw new ArgumentException($"Stocul produsului nu poate depăși {Config.StocMaxim}.");
            return true;
        }

        public static bool ValidareDataExpirare(DateTime dataExpirare)
        {
            if (dataExpirare < DateTime.Now)
                throw new ArgumentException("Data de expirare nu poate fi trecută.");
            if (dataExpirare > DateTime.Now.AddYears(Config.AniMaximExpirare))
                throw new ArgumentException($"Data de expirare nu poate fi mai mare de {Config.AniMaximExpirare} ani.");
            return true;
        }

        public static bool ValidareAdresaLivrare(string adresa)
        {
            if (string.IsNullOrEmpty(adresa))
                throw new ArgumentException("Adresa de livrare este obligatorie.");
            if (adresa.Length < Config.LungimeMinimaAdresa)
                throw new ArgumentException($"Adresa de livrare trebuie să aibă cel puțin {Config.LungimeMinimaAdresa} caractere.");
            return true;
        }

        public static bool ValidareExistaProdus(string numeProdus, HashSet<string> produseDisponibile)
        {
            if (string.IsNullOrEmpty(numeProdus))
                throw new ArgumentException("Numele produsului nu poate fi gol.");
            if (!produseDisponibile.Contains(numeProdus))
                throw new ArgumentException($"Produsul '{numeProdus}' nu există în magazin.");
            return true;
        }

        public static bool ValidareCos(List<string> produseDinCos)
        {
            if (produseDinCos == null || produseDinCos.Count == 0)
                throw new ArgumentException("Coșul de cumpărături este gol. Adaugă produse înainte de a plasa comanda.");
            return true;
        }

        public static bool ValidareComanda(string adresaLivrare, List<string> produseDinCos)
        {
            ValidareAdresaLivrare(adresaLivrare);
            ValidareCos(produseDinCos);
            return true;
        }
        
        public static void ValidareClasaEficienta(string clasaEficienta)
        {
            if (string.IsNullOrWhiteSpace(clasaEficienta))
                throw new ArgumentException("Clasa de eficienta nu poate fi goala.");

            // Verifica daca clasa contine doar litere mari
            if (!Regex.IsMatch(clasaEficienta, @"^[A-Z]+$"))
                throw new ArgumentException("Clasa de eficienta trebuie sa contina doar litere mari.");
        }

        // Validare conditii de pastrare pentru produsele perisabile
        public static void ValidareConditiiPastrare(string conditiiPastrare)
        {
            if (string.IsNullOrWhiteSpace(conditiiPastrare))
                throw new ArgumentException("Conditiile de pastrare nu pot fi goale.");
        }
        
        public static void ValidareCantitateStoc(int cantitate, int stocCurent)
        {
            if (cantitate <= 0)
                throw new ArgumentException("Cantitatea trebuie să fie un număr pozitiv.");

            if (cantitate > stocCurent)
                throw new ArgumentException("Cantitatea de stoc scazuta nu poate depasi stocul disponibil.");
        }
        
        public static void ValidareDataLivrare(DateTime dataLivrare, DateTime dataInitialaComanda)
        {
            DateTime dataCurenta = DateTime.Now;
            DateTime dataLimita = dataInitialaComanda.AddDays(14);  // 2 saptamani de la data initiala a comenzii

            if (dataLivrare < dataCurenta)
            {
                throw new ArgumentException("Data de livrare nu poate fi mai devreme decat data curenta.");
            }

            if (dataLivrare < dataInitialaComanda)
            {
                throw new ArgumentException("Data de livrare nu poate fi mai devreme decat data initiala a comenzii.");
            }

            if (dataLivrare > dataLimita)
            {
                throw new ArgumentException("Data de livrare nu poate fi mai tarziu de doua saptamani de la data initiala a comenzii.");
            }
        }

    }

