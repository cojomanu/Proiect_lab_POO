namespace Magazin_online;

    public static class Validare
    {
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
    }

