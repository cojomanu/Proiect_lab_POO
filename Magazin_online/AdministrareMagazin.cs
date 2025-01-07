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

    public void IncarcaComenziDinFisier(string fisierComenzi)
{
    if (!File.Exists(fisierComenzi))
    {
        Console.WriteLine("Fișierul cu comenzile plasate nu există.");
        return;
    }

    string[] liniiComenzi = File.ReadAllLines(fisierComenzi);
    List<Comanda> listaComenzi = new List<Comanda>();

    string nume = "";
    string telefon = "";
    string email = "";
    string adresa = "";
    string status = "";
    DateTime dataLivrare = DateTime.Now;

    foreach (var linie in liniiComenzi)
    {
        if (linie.StartsWith("Comanda plasata pe:"))
        {
            // Dacă există o comandă anterioară, o adăugăm în listă
            if (!string.IsNullOrEmpty(nume))
            {
                Comanda comanda = new Comanda(new List<ProdusGeneric>(), nume, telefon, email, adresa); // Cos ignorat
                comanda.setStatus(status);
                comanda.set_Data_livrare(dataLivrare);
                listaComenzi.Add(comanda);
            }

            // Resetăm variabilele pentru o nouă comandă
            nume = "";
            telefon = "";
            email = "";
            adresa = "";
            status = "";

            // Parsăm data comenzii
            string dataComandaText = linie.Replace("Comanda plasata pe:", "").Trim();
            DateTime.TryParse(dataComandaText, out dataLivrare);
        }
        else if (linie.StartsWith("Nume:"))
        {
            nume = linie.Replace("Nume:", "").Trim();
        }
        else if (linie.StartsWith("Numar telefon:"))
        {
            telefon = linie.Replace("Numar telefon:", "").Trim();
        }
        else if (linie.StartsWith("Email:"))
        {
            email = linie.Replace("Email:", "").Trim();
        }
        else if (linie.StartsWith("Adresa de livrare:"))
        {
            adresa = linie.Replace("Adresa de livrare:", "").Trim();
        }
        else if (linie.StartsWith("Status comanda:"))
        {
            status = linie.Replace("Status comanda:", "").Trim();
        }
    }

    // Adăugăm ultima comandă, dacă există
    if (!string.IsNullOrEmpty(nume))
    {
        Comanda comanda = new Comanda(new List<ProdusGeneric>(), nume, telefon, email, adresa); // Cos ignorat
        comanda.setStatus(status);
        comanda.set_Data_livrare(dataLivrare);
        listaComenzi.Add(comanda);
    }

    // Setăm lista locală `comenzi`
    this.comenzi = listaComenzi;
}

    public void EliminaLiniiDuplicate(string path)
    {
        try
        {
            // Citim toate liniile din fișier
            string[] lines = File.ReadAllLines(path);

            // Folosim un HashSet pentru a păstra doar liniile unice
            HashSet<string> uniqueLines = new HashSet<string>();

            // Adăugăm fiecare linie din fișier în HashSet (doar liniile unice vor fi păstrate)
            foreach (var line in lines)
            {
                uniqueLines.Add(line.Trim()); // trimite spațiile suplimentare de la începutul și sfârșitul fiecărei linii
            }

            // Scriem înapoi fișierul cu doar liniile unice
            File.WriteAllLines(path, uniqueLines);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare la manipularea fișierului: {ex.Message}");
        }
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
                decimal pret;
                int stoc;

                // Validăm și parsează pretul și stocul
                if (!decimal.TryParse(parts[1].Trim(), out pret) || !int.TryParse(parts[2].Trim(), out stoc))
                {
                    formatInvalid = true;
                    continue; // Sare peste această linie dacă există erori de parsing
                }

                if (parts.Length == 3) // ProdusGeneric
                {
                    ProdusGeneric produs = new ProdusGeneric(nume, pret, stoc);
                    comenziAdministrator.Adaugare_produs_generic(produs); // Adăugare produs generic
                }
                else if (parts.Length == 5) // Poate fi ProdusElectrocasnic sau ProdusPerisabil
                {
                    string alPatruleaCamp = parts[3].Trim();
                    string alCincileaCamp = parts[4].Trim();

                    DateTime dataExpirare;
                    // Încearcă să parsezi al patrulea câmp ca dată
                    if (DateTime.TryParse(alPatruleaCamp, out dataExpirare))
                    {
                        // Dacă este o dată validă, este ProdusPerisabil
                        ProdusPerisabil produs = new ProdusPerisabil(nume, pret, stoc, dataExpirare, alCincileaCamp);
                        comenziAdministrator.Adaugare_produs_perisabil(produs); // Adăugare produs perisabil
                    }
                    else
                    {
                        // Altfel, este ProdusElectrocasnic
                        int putereMaxima;
                        if (!int.TryParse(alCincileaCamp, out putereMaxima))
                        {
                            formatInvalid = true;
                            continue; // Sare peste această linie dacă puterea maximă nu este validă
                        }

                        string clasaEficienta = alPatruleaCamp;
                        ProdusElectrocasnic produs = new ProdusElectrocasnic(nume, pret, stoc, clasaEficienta, putereMaxima);
                        comenziAdministrator.Adaugare_produs_electrocasnic(produs); // Adăugare produs electrocasnic
                    }
                }
                else
                {
                    // Dacă numărul de părți nu corespunde niciunei categorii, marchează format invalid
                    formatInvalid = true;
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
            EliminaLiniiDuplicate(path);
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            
            _magazin.Produse.Add(produs);
            
            string produsData = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}";
            File.AppendAllText(path, produsData + Environment.NewLine);
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
    
    
    public void Adaugare_produs_perisabil(ProdusPerisabil produs)
    {
        try
        {
            EliminaLiniiDuplicate(path);

            // Validare produs
            Validare.ValidareNumeProdus(produs.Nume);
            Validare.ValidarePret(produs.Pret);
            Validare.ValidareStoc(produs.Stoc);
            Validare.ValidareDataExpirare(produs.DataExpirare);
            Validare.ValidareConditiiPastrare(produs.ConditiiDeDepozitare);

            // Adăugare produs la colecția de produse
            _magazin.Produse.Add(produs);

            // Salvare produs în fișier
            string produsData = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}, {produs.DataExpirare.ToString("yyyy-MM-dd")}, {produs.ConditiiDeDepozitare}";
        
            // Verificăm dacă fișierul există și dacă produsul este deja în fișier
            if (File.Exists(path))
            {
                string[] lines = File.ReadAllLines(path);

                // Verificăm dacă produsul deja există în fișier
                foreach (var line in lines)
                {
                    if (line.Trim() == produsData)
                    {
                        Console.WriteLine("Produsul există deja în fișier. Nu a fost adăugat.");
                        return;
                    }
                }
            }

            // Adăugăm produsul în fișier
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
    
    
    public void Adaugare_produs_electrocasnic(ProdusElectrocasnic produs)
{
    try
    {
        EliminaLiniiDuplicate(path);

        // Validare produs
        Validare.ValidareNumeProdus(produs.Nume);
        Validare.ValidarePret(produs.Pret);
        Validare.ValidareStoc(produs.Stoc);
        Validare.ValidareClasaEficienta(produs.ClasaDeEficientaEnergetica, produs.PutereMaximaConsumata);

        // Adăugare produs la colecția de produse
        _magazin.Produse.Add(produs);

        // Crearea șirului de date al produsului pentru fișier
        string produsData = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}, {produs.ClasaDeEficientaEnergetica}, {produs.PutereMaximaConsumata}";

        // Verificăm dacă fișierul există și dacă produsul este deja în fișier
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);

            // Verificăm dacă produsul deja există în fișier
            foreach (var line in lines)
            {
                if (line.Trim() == produsData)
                {
                    Console.WriteLine("Produsul există deja în fișier. Nu a fost adăugat.");
                    return;
                }
            }
        }

        // Adăugăm produsul în fișier
        File.AppendAllText(path, produsData + Environment.NewLine);
        Console.WriteLine("Produsul electrocasnic a fost adăugat cu succes și salvat în fișier.");
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
    public void SalveazaComandaInFisier(string path,Comanda comanda)
    {
        try
        {
            // Deschidem fișierul pentru a adăuga informațiile comenzii
            using (StreamWriter sw = new StreamWriter(path, true)) // true pentru a adăuga la fișier
            {
                    // Salvăm detaliile comenzii
                    sw.WriteLine($"Comanda plasata pe: {DateTime.Now}");
                    sw.WriteLine($"Nume: {comanda.nume_persoana}");
                    sw.WriteLine($"Numar telefon: {comanda.numar_telefon}");
                    sw.WriteLine($"Email: {comanda.email}");
                    sw.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
                    sw.WriteLine($"Status comanda: {comanda.status}");
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
    
        // Căutăm produsul în colecția de produse
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
            // Obținem produsul pentru a-l elimina din fișier
            ProdusGeneric produsDeSters = _magazin.Produse[select];

            // Îl eliminăm din colecția de produse
            _magazin.Produse.RemoveAt(select);
            Console.WriteLine("Produsul a fost eliminat din colecție cu succes.");

            // Citim liniile din fișier
            string[] lines = File.ReadAllLines(path);
        
            // Creăm o listă pentru liniile care trebuie păstrate
            List<string> linesToKeep = new List<string>();

            // Creăm șirul de date al produsului pentru a fi comparat cu liniile din fișier
            string produsData = $"{produsDeSters.Nume}, {produsDeSters.Pret}, {produsDeSters.Stoc}";

            // Adăugăm toate liniile din fișier, exceptând linia care corespunde produsului
            foreach (var line in lines)
            {
                if (line.Trim() != produsData)
                {
                    linesToKeep.Add(line);
                }
            }

            // Dacă linia respectivă a fost găsită și eliminată, rescriem fișierul fără ea
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
        // Căutăm produsul în colecția de produse
        ProdusGeneric produs_ales = _magazin.Produse.FirstOrDefault(p => p.Nume == nume_produs);

        if (produs_ales == null)
        {
            Console.WriteLine("Produsul tastat nu exista");
            return;
        }

        // Validăm cantitatea de stoc pentru modificare
        Validare.ValidareCantitateStoc(produs_ales.Stoc, crestereSAUscadere);

        // Modificăm stocul produsului
        produs_ales.Modificare_stoc(crestereSAUscadere);
        Console.WriteLine("Stoc modificat cu succes");

        // Citim toate liniile din fișier
        string[] lines = File.ReadAllLines(path);

        // Creăm lista de linii pentru fișier cu produsele actualizate
        List<string> updatedLines = new List<string>();

        // Creăm șirul de date al produsului care a fost modificat
        string produsDataModificat = $"{produs_ales.Nume}, {produs_ales.Pret}, {produs_ales.Stoc}";

        // Adăugăm liniile la lista de linii actualizate, cu înlocuirea liniei care corespunde produsului modificat
        foreach (var line in lines)
        {
            string[] parts = line.Split(',');

            if (parts.Length >= 3 && parts[0].Trim() == produs_ales.Nume)
            {
                // Modificăm linia corespunzătoare produsului
                updatedLines.Add(produsDataModificat);
            }
            else
            {
                // Adăugăm linia neschimbată
                updatedLines.Add(line);
            }

        }

        // Rescriem fișierul cu noile linii (produsele actualizate)
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
    public void Vizualizare_comenzi_plasate()
    {

        if (!File.Exists(path_comenzi))
        {
            Console.WriteLine("Fișierul cu comenzile plasate nu există.");
            return;
        }

        string[] liniiComenzi = File.ReadAllLines(path_comenzi);
        if (liniiComenzi.Length == 0)
        {
            Console.WriteLine("Nu există comenzi plasate.");
            return;
        }

        Console.WriteLine("Comenzile plasate sunt:");
        Console.WriteLine(new string('-', 50));

        List<string> comandaCurenta = new List<string>();

        foreach (var linie in liniiComenzi)
        {
            if (string.IsNullOrWhiteSpace(linie))
            {
                // Dacă întâlnim o linie goală, afișăm comanda curentă.
                AfiseazaComanda(comandaCurenta);
                comandaCurenta.Clear(); // Resetăm lista pentru următoarea comandă.
            }
            else
            {
                // Adăugăm linia în comanda curentă.
                comandaCurenta.Add(linie);
            }
        }

        // Afișează ultima comandă dacă nu este urmată de o linie goală.
        if (comandaCurenta.Count > 0)
        {
            AfiseazaComanda(comandaCurenta);
        }

        Console.WriteLine(new string('-', 50));
    }

    public void AfiseazaComanda(List<string> comanda)
    {
        // Parcurge fiecare linie din comanda curentă și o afișează.
        foreach (var linie in comanda)
        {
            Console.WriteLine(linie);
        }
        Console.WriteLine(); // Linie goală între comenzi pentru claritate.
    }


    private void ActualizeazaFisierComenzi(string fisierComenzi)
    {
        using (StreamWriter writer = new StreamWriter(fisierComenzi))
        {
            foreach (var comanda in comenzi)
            {
                writer.WriteLine($"Comanda plasata pe: {comanda.data_livrare.ToString("G")}");
                writer.WriteLine($"Nume: {comanda.nume_persoana}");
                writer.WriteLine($"Numar telefon: {comanda.numar_telefon}");
                writer.WriteLine($"Email: {comanda.email}");
                writer.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
                writer.WriteLine($"Status comanda: {comanda.status}");
                writer.WriteLine(); // Linie goală între comenzi
            }
        }

        Console.WriteLine("Fișierul a fost actualizat cu succes.");
    }

    
    public void Procesare_comenzi_status(int care_comanda, int ok)
    {
        if (care_comanda < 0 || care_comanda >= comenzi.Count)
        {
            Console.WriteLine("Comanda specificată nu există.");
            return;
        }

        if (ok == 1)
        {
            comenzi[care_comanda].setStatus("In curs de livrare");
            Console.WriteLine($"Statusul comenzii {care_comanda} a fost schimbat în 'In curs de livrare'.");

            // Actualizăm fișierul
            ActualizeazaFisierComenzi(path_comenzi);
        }
        else
        {
            Console.WriteLine("Operațiunea a fost anulată.");
        }
    }
    public void Procesare_comenzi_data_livrare(int care_comanda, DateTime noua_data)
    {
        try
        {

            Comanda comanda_aleasa = comenzi.FirstOrDefault(p => p.numar_comanda == care_comanda);

            if (comanda_aleasa == null)
            {
                Console.WriteLine("Comanda specificată nu există.");
                return;
            }

            Validare.ValidareDataLivrare(comanda_aleasa.data_livrare, noua_data);
            comanda_aleasa.set_Data_livrare(noua_data);
            Console.WriteLine("Data livrare modificată cu succes.");

            // Actualizăm fișierul
            ActualizeazaFisierComenzi(path_comenzi);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }

}