﻿namespace Magazin_online;

public class AdministrareMagazin:Administrator
{
    private Magazin _magazin;
    private List<Comanda> comenzi=new List<Comanda>();
    public AdministrareMagazin(Magazin magazin)
    {
        _magazin = magazin;
    }
    
    static string path = "C:\\Users\\POWERUSER\\RiderProjects\\Proiect_magazin_online\\Magazin_online\\produse.txt";
    static string path_comenzi = "C:\\Users\\POWERUSER\\RiderProjects\\Proiect_magazin_online\\Magazin_online\\comenzi.txt";

//     public void IncarcaComenziDinFisier()
// {
//     if (!File.Exists(path_comenzi))
//     {
//         Console.WriteLine("Fisierul cu comenzile plasate nu exista.");
//         return;
//     }
//
//     string[] liniiComenzi = File.ReadAllLines(path_comenzi);
//     List<Comanda> listaComenzi = new List<Comanda>();
//
//     int numarComanda = 0;
//     string nume = "";
//     string telefon = "";
//     string email = "";
//     string adresa = "";
//     string status = "";
//     DateTime dataLivrare = DateTime.Now;
//
//     foreach (var linie in liniiComenzi)
//     {
//         if (linie.StartsWith("Numar comanda:"))
//         {
//             // Daca exista o comanda anterioara, o adaugam in lista
//             if (!string.IsNullOrEmpty(nume))
//             {
//                 Comanda comanda = new Comanda(new Cos(), nume, telefon, email, adresa); // Cos ignorat
//                 comanda.setStatus(status);
//                 comanda.set_Data_livrare(dataLivrare);
//                 comanda.numar_comanda = numarComanda;
//                 listaComenzi.Add(comanda);
//             }
//             
//             numarComanda = 0;
//             nume = "";
//             telefon = "";
//             email = "";
//             adresa = "";
//             status = "";
//             
//             string numarComandaText = linie.Replace("Numar comanda:", "").Trim();
//             int.TryParse(numarComandaText, out numarComanda);
//         }
//         else if (linie.StartsWith("Comanda plasata pe:"))
//         {
//             string dataComandaText = linie.Replace("Comanda plasata pe:", "").Trim();
//             DateTime.TryParse(dataComandaText, out dataLivrare);
//         }
//         else if (linie.StartsWith("Nume:"))
//         {
//             nume = linie.Replace("Nume:", "").Trim();
//         }
//         else if (linie.StartsWith("Numar telefon:"))
//         {
//             telefon = linie.Replace("Numar telefon:", "").Trim();
//         }
//         else if (linie.StartsWith("Email:"))
//         {
//             email = linie.Replace("Email:", "").Trim();
//         }
//         else if (linie.StartsWith("Adresa de livrare:"))
//         {
//             adresa = linie.Replace("Adresa de livrare:", "").Trim();
//         }
//         else if (linie.StartsWith("Status comanda:"))
//         {
//             status = linie.Replace("Status comanda:", "").Trim();
//         }
//     }
//     if (!string.IsNullOrEmpty(nume))
//     {
//         Comanda comanda = new Comanda(new Cos(), nume, telefon, email, adresa); // Cos ignorat
//         comanda.setStatus(status);
//         comanda.set_Data_livrare(dataLivrare);
//         comanda.numar_comanda = numarComanda;
//         listaComenzi.Add(comanda);
//     }
//     
//     comenzi = listaComenzi;
//
//     Console.WriteLine("Comenzile au fost incarcate cu succes din fisier.");
// }


public void IncarcaComenziDinFisier()
{
    if (!File.Exists(path_comenzi))
    {
        Console.WriteLine("Fisierul cu comenzile plasate nu exista.");
        return;
    }

    string[] liniiComenzi = File.ReadAllLines(path_comenzi);
    List<Comanda> listaComenzi = new List<Comanda>();

    int numarComanda = 0;
    string nume = "";
    string telefon = "";
    string email = "";
    string adresa = "";
    string status = "";
    DateTime dataLivrare = DateTime.Now;
    Cos cosCurent = new Cos(); // Cosul curent al comenzii

    foreach (var linie in liniiComenzi)
    {
        if (linie.StartsWith("Numar comanda:"))
        {
            // Daca exista o comanda anterioara, o adaugam in lista
            if (!string.IsNullOrEmpty(nume))
            {
                Comanda comanda = new Comanda(cosCurent, nume, telefon, email, adresa);
                comanda.setStatus(status);
                comanda.set_Data_livrare(dataLivrare);
                comanda.numar_comanda = numarComanda;
                listaComenzi.Add(comanda);
            }

            // Resetam variabilele pentru o noua comanda
            numarComanda = 0;
            nume = "";
            telefon = "";
            email = "";
            adresa = "";
            status = "";
            cosCurent = new Cos(); // Cream un nou cos pentru comanda urmatoare

            string numarComandaText = linie.Replace("Numar comanda:", "").Trim();
            int.TryParse(numarComandaText, out numarComanda);
        }
        else if (linie.StartsWith("Comanda plasata pe:"))
        {
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
        else if (linie.StartsWith("Cos:"))
        {
            string cosText = linie.Replace("Cos:", "").Trim();
            string[] produseNume = cosText.Split(", ");

            foreach (var numeProdus in produseNume)
            {
                cosCurent.AdaugareProdusInCos(numeProdus); // Adaugam numele produsului direct in lista din Cos
            }
        }
    }

    // Adaugam ultima comanda, daca exista
    if (!string.IsNullOrEmpty(nume))
    {
        Comanda comanda = new Comanda(cosCurent, nume, telefon, email, adresa);
        comanda.setStatus(status);
        comanda.set_Data_livrare(dataLivrare);
        comanda.numar_comanda = numarComanda;
        listaComenzi.Add(comanda);
    }

    comenzi = listaComenzi;

    Console.WriteLine("Comenzile au fost incarcate cu succes din fisier.");
}



    public void EliminaLiniiDuplicate()
    {
        try
        {
            string[] lines = File.ReadAllLines(path);

            HashSet<string> uniqueLines = new HashSet<string>();
            
            foreach (var line in lines)
            {
                uniqueLines.Add(line.Trim()); 
            }
            
            File.WriteAllLines(path, uniqueLines);
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare la manipularea fisierului: {ex.Message}");
        }
    }

public static void CreazaProdusDinFisier(AdministrareMagazin comenziAdministrator)
{
    bool formatInvalid = false; 

    try
    {
        string[] lines = File.ReadAllLines(path);
        foreach (var line in lines)
        {
            var parts = line.Split(',');
            
            if (parts.Length >= 3)
            {
                string nume = parts[0].Trim();
                decimal pret;
                int stoc;
                
                if (!decimal.TryParse(parts[1].Trim(), out pret) || !int.TryParse(parts[2].Trim(), out stoc))
                {
                    formatInvalid = true;
                    continue;
                }

                if (parts.Length == 3) // ProdusGeneric
                {
                    ProdusGeneric produs = new ProdusGeneric(nume, pret, stoc);
                    comenziAdministrator.Adaugare_produs_generic(produs); 
                }
                else if (parts.Length == 5) // Poate fi ProdusElectrocasnic sau ProdusPerisabil
                {
                    string alPatruleaCamp = parts[3].Trim();
                    string alCincileaCamp = parts[4].Trim();

                    DateTime dataExpirare;
                    if (DateTime.TryParse(alPatruleaCamp, out dataExpirare))
                    {
                        // Daca este o data valida, este ProdusPerisabil
                        ProdusPerisabil produs = new ProdusPerisabil(nume, pret, stoc, dataExpirare, alCincileaCamp);
                        comenziAdministrator.Adaugare_produs_perisabil(produs); 
                    }
                    else
                    {
                        // Altfel, este ProdusElectrocasnic
                        int putereMaxima;
                        if (!int.TryParse(alCincileaCamp, out putereMaxima))
                        {
                            formatInvalid = true;
                            continue; // Sare peste aceasta linie daca puterea maxima nu este valida
                        }

                        string clasaEficienta = alPatruleaCamp;
                        ProdusElectrocasnic produs = new ProdusElectrocasnic(nume, pret, stoc, clasaEficienta, putereMaxima);
                        comenziAdministrator.Adaugare_produs_electrocasnic(produs); 
                    }
                }
                else
                {
                   
                    formatInvalid = true;
                }
            }
            else
            {
                formatInvalid = true;
            }
        }
        
        if (formatInvalid)
        {
            Console.WriteLine("Unele linii din fisier au un format incorect si nu au fost procesate.");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Eroare la citirea fisierului: {ex.Message}");
    }
}

    public void Adaugare_produs_generic(ProdusGeneric produs)
    {
        try
        {
            EliminaLiniiDuplicate();
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
            Console.WriteLine($"Eroare la salvarea fisierului: {ex.Message}");
        }
    }
    
    
    public void Adaugare_produs_perisabil(ProdusPerisabil produs)
    {
        try
        {
            EliminaLiniiDuplicate();
            
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
                        Console.WriteLine("Produsul exista deja in fisier. Nu a fost adaugat.");
                        return;
                    }
                }
            }
            
            File.AppendAllText(path, produsData + Environment.NewLine);
            Console.WriteLine("Produsul perisabil a fost adaugat cu succes si salvat in fisier.");
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
        catch (IOException ex)
        {
            Console.WriteLine($"Eroare la salvarea fisierului: {ex.Message}");
        }
    }
    
    
    public void Adaugare_produs_electrocasnic(ProdusElectrocasnic produs)
{
    try
    {
        EliminaLiniiDuplicate();
        
        Validare.ValidareNumeProdus(produs.Nume);
        Validare.ValidarePret(produs.Pret);
        Validare.ValidareStoc(produs.Stoc);
        Validare.ValidareClasaEficienta(produs.ClasaDeEficientaEnergetica, produs.PutereMaximaConsumata);
        
        _magazin.Produse.Add(produs);
        
        string produsData = $"{produs.Nume}, {produs.Pret}, {produs.Stoc}, {produs.ClasaDeEficientaEnergetica}, {produs.PutereMaximaConsumata}";
        
        if (File.Exists(path))
        {
            string[] lines = File.ReadAllLines(path);
            
            foreach (var line in lines)
            {
                if (line.Trim() == produsData)
                {
                    Console.WriteLine("Produsul exista deja in fisier. Nu a fost adaugat.");
                    return;
                }
            }
        }
        
        File.AppendAllText(path, produsData + Environment.NewLine);
        Console.WriteLine("Produsul electrocasnic a fost adaugat cu succes si salvat in fisier.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Eroare: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Eroare la salvarea fisierului: {ex.Message}");
    }
}
   
    
    public void SalveazaComandaInFisier(string path, Comanda comanda)
    {
        try
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                sw.WriteLine($"Numar comanda: {comanda.numar_comanda}");
                sw.WriteLine($"Comanda plasata pe: {comanda.data_livrare.ToString("dd.MM.yyyy HH:mm:ss")}");
                sw.WriteLine($"Data estimata pentru livrare: {comanda.data_livrare.AddDays(2).ToString("dd.MM.yyyy")}");
                sw.WriteLine($"Numar telefon: {comanda.numar_telefon}");
                sw.WriteLine($"Email: {comanda.email}");
                sw.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
                sw.WriteLine($"Status comanda: {comanda.status}");

                // Salvam linia cu produsele din Cos
                if (comanda.Cos_produse.Cos_produse != null && comanda.Cos_produse.Cos_produse.Count > 0)
                {
                    sw.WriteLine($"Cos: {string.Join(", ", comanda.Cos_produse.Cos_produse)}");
                }
                else
                {
                    sw.WriteLine("Cos: (gol)");
                }

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

            Console.WriteLine("Comanda a fost adaugata cu succes.");
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
            Console.WriteLine("Produsul a fost eliminat din colectie cu succes.");
            
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
                Console.WriteLine("Produsul a fost eliminat si din fisier.");
            }
            else
            {
                Console.WriteLine("Produsul nu a fost gasit in fisier.");
            }
        }
        else
        {
            Console.WriteLine("Produsul tastat nu exista in colectie.");
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
                // Modificam linia corespunzatoare produsului
                updatedLines.Add(produsDataModificat);
            }
            else
            {
                // Adaugam linia neschimbata
                updatedLines.Add(line);
            }

        }
        
        File.WriteAllLines(path, updatedLines);
        Console.WriteLine("Fisierul a fost actualizat cu succes.");
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Eroare: {ex.Message}");
    }
    catch (IOException ex)
    {
        Console.WriteLine($"Eroare la manipularea fisierului: {ex.Message}");
    }
}
    public void Vizualizare_comenzi_plasate()
    {

        if (!File.Exists(path_comenzi))
        {
            Console.WriteLine("Fisierul cu comenzile plasate nu exista.");
            return;
        }

        string[] liniiComenzi = File.ReadAllLines(path_comenzi);
        if (liniiComenzi.Length == 0)
        {
            Console.WriteLine("Nu exista comenzi plasate.");
            return;
        }

        Console.WriteLine("Comenzile plasate sunt:");
        Console.WriteLine(new string('-', 50));

        List<string> comandaCurenta = new List<string>();

        foreach (var linie in liniiComenzi)
        {
            if (string.IsNullOrWhiteSpace(linie))
            {
                AfiseazaComanda(comandaCurenta);
                comandaCurenta.Clear(); 
            }
            else
            {
                comandaCurenta.Add(linie);
            }
        }
        
        if (comandaCurenta.Count > 0)
        {
            AfiseazaComanda(comandaCurenta);
        }

        Console.WriteLine(new string('-', 50));
    }

    public void AfiseazaComanda(List<string> comanda)
    {
        foreach (var linie in comanda)
        {
            Console.WriteLine(linie);
        }
        Console.WriteLine();
    }


    // private void ActualizeazaFisierComenzi(string fisierComenzi)
    // {
    //     try
    //     {
    //         using (StreamWriter writer = new StreamWriter(fisierComenzi))
    //         {
    //             foreach (var comanda in comenzi)
    //             {
    //                 writer.WriteLine($"Numar comanda: {comanda.numar_comanda}");
    //                 writer.WriteLine($"Comanda plasata pe: {comanda.data_livrare.ToString("dd.MM.yyyy HH:mm:ss")}");
    //                 writer.WriteLine($"Data estimata pentru livrare: {comanda.estimare_data_livrare.ToString("dd.MM.yyyy")}");
    //                 writer.WriteLine($"Nume: {comanda.nume_persoana}");
    //                 writer.WriteLine($"Numar telefon: {comanda.numar_telefon}");
    //                 writer.WriteLine($"Email: {comanda.email}");
    //                 writer.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
    //                 writer.WriteLine($"Status comanda: {comanda.status}");
    //                 writer.WriteLine();
    //             }
    //         }
    //
    //         Console.WriteLine("Fisierul a fost actualizat cu succes.");
    //     }
    //     catch (Exception ex)
    //     {
    //         Console.WriteLine($"Eroare la actualizarea fisierului: {ex.Message}");
    //     }
    // }
    
    private void ActualizeazaFisierComenzi(string fisierComenzi)
    {
        try
        {
            using (StreamWriter writer = new StreamWriter(fisierComenzi))
            {
                foreach (var comanda in comenzi)
                {
                    writer.WriteLine($"Numar comanda: {comanda.numar_comanda}");
                    writer.WriteLine($"Comanda plasata pe: {comanda.data_livrare.ToString("dd.MM.yyyy HH:mm:ss")}");
                    writer.WriteLine($"Data estimata pentru livrare: {comanda.estimare_data_livrare.ToString("dd.MM.yyyy")}");
                    writer.WriteLine($"Nume: {comanda.nume_persoana}");
                    writer.WriteLine($"Numar telefon: {comanda.numar_telefon}");
                    writer.WriteLine($"Email: {comanda.email}");
                    writer.WriteLine($"Adresa de livrare: {comanda.adresa_livrare}");
                    writer.WriteLine($"Status comanda: {comanda.status}");

                    // Salvăm linia cu produsele din Cos
                    if (comanda.Cos_produse.Cos_produse != null && comanda.Cos_produse.Cos_produse.Count > 0)
                    {
                        writer.WriteLine($"Cos: {string.Join(", ", comanda.Cos_produse.Cos_produse)}");
                    }
                    else
                    {
                        writer.WriteLine("Cos: (gol)");
                    }

                    writer.WriteLine(); // Linie goală pentru separarea comenzilor
                }
            }

            Console.WriteLine("Fisierul a fost actualizat cu succes.");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Eroare la actualizarea fisierului: {ex.Message}");
        }
    }


    
    public void Procesare_comenzi_status(int care_comanda, int ok)
    {
        if (care_comanda < 0 || care_comanda >= comenzi.Count)
        {
            Console.WriteLine("Comanda specificata nu exista.");
            return;
        }

        if (ok == 1)
        {
            comenzi[care_comanda].setStatus("In curs de livrare");
            Console.WriteLine($"Statusul comenzii {care_comanda} a fost schimbat in 'In curs de livrare'.");
            
            ActualizeazaFisierComenzi(path_comenzi);
        }
        else
        {
            Console.WriteLine("Operatiunea a fost anulata.");
        }
    }
    public void Procesare_comenzi_data_livrare(int care_comanda, DateTime noua_data)
    {
        try
        {
            Comanda comanda_aleasa = comenzi.FirstOrDefault(p => p.numar_comanda == care_comanda);

            if (comanda_aleasa == null)
            {
                Console.WriteLine("Comanda specificata nu exista.");
                return;
            }

            Validare.ValidareDataLivrare(noua_data, comanda_aleasa.estimare_data_livrare);
            comanda_aleasa.set_Data_livrare(noua_data);
            Console.WriteLine("Data livrare modificata cu succes.");
            
            ActualizeazaFisierComenzi(path_comenzi);
        }
        catch (ArgumentException ex)
        {
            Console.WriteLine($"Eroare: {ex.Message}");
        }
    }
}