﻿namespace Magazin_online;

public class AdministrareMagazin:Administrator
{
    private Magazin _magazin;
    private List<Comanda> comenzi=new List<Comanda>();
    public AdministrareMagazin(Magazin magazin)
    {
        _magazin = magazin;
    }
    // // public void Adaugare_produs(ProdusGeneric produs)
    // {
    //     _magazin.Produse.Add(produs);
    // // }
    public void Adaugare_produs(ProdusGeneric produs)
    {
        try
        {
            // Validarea prețului
            Validare.ValidarePret(produs.Pret);
            
            Console.WriteLine(Validare.ValidarePret(produs.Pret));
        
            // Validarea stocului
            Validare.ValidareStoc(produs.Stoc);

            _magazin.Produse.Add(produs);
            
            Console.WriteLine("Produsul a fost adăugat cu succes.");
        }
        catch (ArgumentException ex)
        {
            // Gestionarea erorilor și afișarea mesajului corespunzător
            ErrorHandler.HandleException(ex);
        }
    }

    

    public void Adaugare_comanda_in_lista_comenzi(Comanda comanda)
    {
        comenzi.Add(comanda);
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

    public void Modificare_stoc_produs_pe_stoc(string nume_produs, int crestere)
    {
        int select = -1;
        for (int i=0;i<_magazin.Produse.Count;i++)
        {
            if(_magazin.Produse[i].Nume == nume_produs)
                select=i;
        }
        if (select != -1)
        {
            _magazin.Produse[select].Modificare_stoc(crestere);
            Console.WriteLine("Stoc modificat cu succes");
        }
        else
        {
            Console.WriteLine("Produsul tastat nu exista");
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
        comenzi[care_comanda].set_Data_livrare(noua_data);
        Console.WriteLine("Data livrare modificata cu succes");
    }
}