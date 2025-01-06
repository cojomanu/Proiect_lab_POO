namespace Magazin_online;

public class AdministrareMagazin:Administrator
{
    
    private Magazin _magazin;
    private List<Comanda> comenzi=new List<Comanda>();
    public AdministrareMagazin(Magazin magazin)
    {
        _magazin = magazin;
    }

    public static void CreazaProdusDinFisier(string path, AdministrareMagazin comenziAdministrator)
{
    bool formatInvalid = false; // Indicator pentru erori de format

    try
    {
        // Citirea datelor din fișier
        string[] lines = File.ReadAllLines(path);

        foreach (var line in lines)
        {
            var parts = line.Split(',');

            // Verificăm dacă linia are suficiente informații pentru a crea un produs
            if (parts.Length >= 3)
            {
                string nume = parts[0].Trim();
                decimal pret = decimal.Parse(parts[1].Trim());
                int stoc = int.Parse(parts[2].Trim());

                // Verificăm tipul produsului pe baza câmpurilor
                if (parts.Length == 3) // ProdusGeneric
                {
                    ProdusGeneric produs = new ProdusGeneric(nume, pret, stoc);
                    comenziAdministrator.Adaugare_produs_generic(produs); // Adăugare produs generic
                }
                else if (parts.Length == 5) // ProdusElectrocasnic
                {
                    string clasaEficienta = parts[3].Trim();
                    int putereMaxima = int.Parse(parts[4].Trim());
                    ProdusElectrocasnic produs = new ProdusElectrocasnic(nume, pret, stoc, clasaEficienta, putereMaxima);
                    comenziAdministrator.Adaugare_produs_electrocasnic(produs); // Adăugare produs electrocasnic
                }
                else if (parts.Length == 5) // ProdusPerisabil
                {
                    DateTime dataExpirare = DateTime.Parse(parts[3].Trim());
                    string conditiiDepozitare = parts[4].Trim();
                    ProdusPerisabil produs = new ProdusPerisabil(nume, pret, stoc, dataExpirare, conditiiDepozitare);
                    comenziAdministrator.Adaugare_produs_perisabil(produs); // Adăugare produs perisabil
                }
            }
            else
            {
                // Dacă linia are format incorect, marcăm că există o eroare de format
                formatInvalid = true;
            }
        }

        // Dacă a existat o eroare de format, afișăm un mesaj la sfârșit
        if (formatInvalid)
        {
            Console.WriteLine("Unele linii din fișier au un format incorect și nu au fost procesate.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Eroare la citirea fișierului: {ex.Message}");
    }
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

    //
    // public void Adaugare_comanda_in_lista_comenzi(Comanda comanda)
    // {
    //     try
    //     {
    //         Validare.ValidareComanda(comanda.nume_persoana,comanda.email,comanda.adresa_livrare);
    //         comenzi.Add(comanda);
    //
    //     }
    //     
    //     catch (ArgumentException ex)
    //     {
    //         Console.WriteLine($"Eroare: {ex.Message}");
    //     }
    //     
    //     
    // }
    public void SalveazaComandaInFisier(string path)
    {
        try
        {
            // Deschidem fișierul pentru a adăuga informațiile comenzii
            using (StreamWriter sw = new StreamWriter(path, true)) // true pentru a adăuga la fișier
            {
                foreach (var comanda in comenzi)
                {
                    // Salvăm detaliile comenzii
                    sw.WriteLine($"Comanda plasata pe: {DateTime.Now}");
                    sw.WriteLine($"Nume: {comanda.nume_persoana}");
                    sw.WriteLine($"Numar telefon: {comanda.numar_telefon}");
                    sw.WriteLine($"Email: {comanda.email}");
                    sw.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
                    sw.WriteLine($"Status comanda: {comanda.status}");

                    // // Salvăm produsele din coș
                    // foreach (var produs in comanda.produse)
                    // {
                    //     sw.WriteLine($"Produs: {produs.Nume}, Pret: {produs.Pret} lei, Stoc: {produs.Stoc}");
                    // }
                    //
                    // sw.WriteLine(new string('-', 40)); // Linie de separare pentru claritate
                }

                Console.WriteLine("Comenzile au fost salvate cu succes.");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la salvarea comenzilor: {ex.Message}");
        }
    }

    public void Adaugare_comanda_in_lista_comenzi(Comanda comanda)
    {
        try
        {
            // Validăm comanda
            Validare.ValidareComanda(comanda.nume_persoana, comanda.email, comanda.adresa_livrare);
            // Adăugăm comanda în lista de comenzi
            comenzi.Add(comanda);

            // Salvăm comenzile într-un fișier
            string path = "C:\\Users\\POWERUSER\\RiderProjects\\Proiect magazin online\\Magazin_online\\comenzi.txt"; // Calea către fișierul de comenzi
            SalveazaComandaInFisier(path);

            Console.WriteLine("Comanda a fost adăugată cu succes.");
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