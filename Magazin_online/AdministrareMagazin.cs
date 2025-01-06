namespace Magazin_online;

public class AdministrareMagazin:Administrator
{
    
    private Magazin _magazin;
    private List<Comanda> comenzi=new List<Comanda>();
    public AdministrareMagazin(Magazin magazin)
    {
        _magazin = magazin;
    }

    public static ProdusGeneric CreazaProdusDinFisier(string path)
        {
            try
            {
                // Citirea datelor din fișier
                string[] lines = File.ReadAllLines(path);

                // Presupunem că fișierul conține un produs pe linie
                foreach (var line in lines)
                {
                    var parts = line.Split(',');

                    if (parts.Length >= 3)
                    {
                        string nume = parts[0].Trim();
                        decimal pret = decimal.Parse(parts[1].Trim());
                        int stoc = int.Parse(parts[2].Trim());

                        // Verificăm tipul produsului (pentru a crea un obiect de tipul corect)
                        if (parts.Length == 3) // ProdusGeneric
                        {
                            return new ProdusGeneric(nume, pret, stoc);
                        }
                        else if (parts.Length == 5) // ProdusElectrocasnic
                        {
                            string clasaEficienta = parts[3].Trim();
                            int putereMaxima = int.Parse(parts[4].Trim());
                            return new ProdusElectrocasnic(nume, pret, stoc, clasaEficienta, putereMaxima);
                        }
                        else if (parts.Length == 5) // ProdusPerisabil
                        {
                            DateTime dataExpirare = DateTime.Parse(parts[3].Trim());
                            string conditiiDepozitare = parts[4].Trim();
                            return new ProdusPerisabil(nume, pret, stoc, dataExpirare, conditiiDepozitare);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Formatul fișierului nu este valid.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la citirea fișierului: {ex.Message}");
            }

            return null;
        }

    public void Adaugare_produs_generic(ProdusGeneric produs)
    {
        try
        {
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);

            _magazin.Produse.Add(produs);
            Console.WriteLine("Produsul a fost adăugat cu succes.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }
    
    public void Adaugare_produs_perisabil(ProdusPerisabil produs)
    {
        try
        {
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareDataExpirare(produs.DataExpirare);
            Validare.ValidareConditiiPastrare(produs.ConditiiDeDepozitare);

        
            _magazin.Produse.Add(produs);
            
            Console.WriteLine("Produsul a fost adăugat cu succes.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }
    
    public void Adaugare_produs_electrocasnic(ProdusElectrocasnic produs)
    {
        try
        {
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareClasaEficienta(produs.ClasaDeEficientaEnergetica,produs.PutereMaximaConsumata);

            _magazin.Produse.Add(produs);
            Console.WriteLine("Produsul a fost adăugat cu succes.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }

   
    public void Adaugare_comanda_in_lista_comenzi(Comanda comanda)
    {
        try
        {
            Validare.ValidareComanda(comanda.nume_persoana,comanda.email,comanda.adresa_livrare);
            comenzi.Add(comanda);

        }
        
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
        
        
    }

    public void Stergere_produs_pe_stoc(string nume_produs)
    {
        
        
        int select=-1;
        for (int i=0;i<_magazin.Produse.Count;i++)
        {
            if(_magazin.Produse[i].Nume == nume_produs)
                select=i;
        }

        if (select != -1)
        {
            _magazin.Produse.Remove(_magazin.Produse[select]);
            Console.WriteLine("Produsul a fost eliminat cu succes");
        }
        else
        {
            Console.WriteLine("Produsul tastat nu exista");
        }
    }
    
    public void Modificare_stoc_produs_pe_stoc(string nume_produs, int crestereSAUscadere)
    {
        try
        {
            ProdusGeneric produs_ales = _magazin.Produse.FirstOrDefault(p => p.Nume == nume_produs);

            if (produs_ales == null)
            {
                Console.WriteLine("Produsul tastat nu exista");
                return;
            }

            Validare.ValidareCantitateStoc(produs_ales.Stoc, crestereSAUscadere);

            produs_ales.Modificare_stoc(crestereSAUscadere);
            Console.WriteLine("Stoc modificat cu succes");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }


    public void Vizualizare_comenzi_plasate()
    {
        foreach(var comanda in comenzi)
            Console.WriteLine(comanda);
    }

    public void Procesare_comenzi_status(int care_comanda,int ok)
    {
        if (ok==1)
        {
            comenzi[care_comanda].setStatus("In curs de livrare");
        }
    }

    public void Procesare_comenzi_data_livrare(int care_comanda, DateTime noua_data)
    {
        try
        {
            Comanda comanda_aleasa = comenzi.FirstOrDefault(p => p.numar_comanda== care_comanda);
            Validare.ValidareDataLivrare(comanda_aleasa.data_livrare, noua_data);
            comanda_aleasa.set_Data_livrare(noua_data);
            Console.WriteLine("Data livrare modificata cu succes");
        }
        
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
        
    }
}