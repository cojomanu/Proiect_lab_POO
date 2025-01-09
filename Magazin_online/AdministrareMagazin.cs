namespace Magazin_online;

public class AdministrareMagazin:Administrator
{
    
    private Magazin _magazin;
    private List<Comanda> comenzi=new List<Comanda>();
    public AdministrareMagazin(Magazin magazin)
    {
        _magazin = magazin;
    }
    
    static string path = "C:\\Users\\lucas\\RiderProjects\\Proiect_lab_POO\\Magazin_online\\produse.txt";
    static string path_comenzi = "C:\\Users\\lucas\\RiderProjects\\Proiect_lab_POO\\Magazin_online\\comenzi.txt"; // Calea către fișierul de comenzi

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

    public void SalveazaComandaInFisier(string path, Comanda comanda)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"Număr comandă: {comanda.numar_comanda}");
                sw.WriteLine($"Comanda plasată pe: {comanda.data_livrare.ToString("dd.MM.yyyy HH:mm:ss")}");
                sw.WriteLine($"Data estimată pentru livrare: {comanda.data_livrare.AddDays(2).ToString("dd.MM.yyyy")}"); 
                sw.WriteLine($"Număr telefon: {comanda.numar_telefon}");
                sw.WriteLine($"Email: {comanda.email}");
                sw.WriteLine($"Adresă de livrare: {comanda.adresa_livrare}");
                sw.WriteLine($"Status comandă: {comanda.status}");
                sw.WriteLine(); 
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
            Validare.ValidareComanda(comanda.nume_persoana, comanda.email, comanda.adresa_livrare);
            
            comenzi.Add(comanda);
            
            SalveazaComandaInFisier(path_comenzi, comanda);

            Console.WriteLine("Comanda a fost adăugată cu succes.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }

    public void Stergere_produs_pe_stoc(string nume_produs)
    {
        int select = -1;
        
        for (int i = 0; i < _magazin.Produse.Count; i++)
        {
            if (_magazin.Produse[i].Nume == nume_produs)
            {
                select = i;
                break;
            }
        }
    
        if (select != -1)
        {
            ProdusGeneric produsDeSters = _magazin.Produse[select];
    
            _magazin.Produse.RemoveAt(select);
            Console.WriteLine("Produsul a fost eliminat din colecție cu succes.");
            
            string[] lines = File.ReadAllLines(path);
            
            List<string> linesToKeep = new List<string>();
            
            string produsData = $"{produsDeSters.Nume}, {produsDeSters.Pret}, {produsDeSters.Stoc}";
            
            foreach (var line in lines)
            {
                if (line.Trim() != produsData)
                {
                    linesToKeep.Add(line);
                }
            }
            
            if (linesToKeep.Count != lines.Length)
            {
                File.WriteAllLines(path, linesToKeep);
                Console.WriteLine("Produsul a fost eliminat și din fișier.");
            }
            else
            {
                Console.WriteLine("Produsul nu a fost găsit în fișier.");
            }
        }
        else
        {
            Console.WriteLine("Produsul tastat nu există în colecție.");
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
        
        Validare.ValidareCantitateStoc(crestereSAUscadere,produs_ales.Stoc );
        
        produs_ales.Modificare_stoc(crestereSAUscadere);
        Console.WriteLine("Stoc modificat cu succes");
        
        string[] lines = File.ReadAllLines(path);
        
        List<string> updatedLines = new List<string>();
        
        string produsDataModificat = $"{produs_ales.Nume}, {produs_ales.Pret}, {produs_ales.Stoc}";

        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            if (parts.Length >= 3 && parts[0].Trim() == produs_ales.Nume)
            {
                // Modificam linia corespunzătoare produsului
                updatedLines.Add(produsDataModificat);
            }
            else
            {
                // Adaugam linia neschimbata
                updatedLines.Add(line);
            }

        }
        
        File.WriteAllLines(path, updatedLines);
        Console.WriteLine("Fișierul a fost actualizat cu succes.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Eroare: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Eroare la manipularea fișierului: {ex.Message}");
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
    

public void Adaugare_produs_perisabil(ProdusPerisabil produs)
    {
        try
        {
            EliminaLiniiDuplicate(path);
            
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareDataExpirare(produs.DataExpirare);
            Validare.ValidareConditiiPastrare(produs.ConditiiDeDepozitare);
            
            _magazin.Produse.Add(produs);
            
            string produsData = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}, {produs.DataExpirare.ToString("yyyy-MM-dd")}, {produs.ConditiiDeDepozitare}";
            
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);
                
                foreach (var line in lines)
                {
                    if (line.Trim() == produsData)
                    {
                        Console.WriteLine("Produsul există deja în fișier. Nu a fost adăugat.");
                        return;
                    }
                }
            }
            
            File.AppendAllText(path, produsData + Environment.NewLine);
            Console.WriteLine("Produsul perisabil a fost adăugat cu succes și salvat în fișier.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare la salvarea fișierului: {ex.Message}");
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

    private void ActualizeazaFisierComenzi(string fisierComenzi)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fisierComenzi))
            {
                foreach (var comanda in comenzi)
                {
                    writer.WriteLine($"Număr comandă: {comanda.numar_comanda}");
                    writer.WriteLine($"Comanda plasată pe: {comanda.data_livrare.ToString("dd.MM.yyyy HH:mm:ss")}");
                    writer.WriteLine($"Data estimată pentru livrare: {comanda.data_livrare.AddDays(2).ToString("dd.MM.yyyy")}");
                    writer.WriteLine($"Nume: {comanda.nume_persoana}");
                    writer.WriteLine($"Număr telefon: {comanda.numar_telefon}");
                    writer.WriteLine($"Email: {comanda.email}");
                    writer.WriteLine($"Adresă de livrare: {comanda.adresa_livrare}");
                    writer.WriteLine($"Status comandă: {comanda.status}");
                    writer.WriteLine();
                }
            }

            Console.WriteLine("Fișierul a fost actualizat cu succes.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la actualizarea fișierului: {ex.Message}");
        }
    }
