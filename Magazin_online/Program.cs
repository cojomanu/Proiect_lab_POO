﻿// See https://aka.ms/new-console-template for more information
using System;
using System.IO;
using Magazin_online;
 
Console.ForegroundColor = ConsoleColor.DarkRed;
Console.WriteLine("Eroare cu rosu semnifica semnalarea acesteia , nu este aruncata exceptia care opreste opearatia curenta");
Console.ResetColor();
Console.WriteLine("Eroare cu alb semnifica ca eroarea scrisa cu rosu a fost aruncata si astfel operatia curenta gresita a fost oprita");
Console.WriteLine();
Console.WriteLine();

void mesaj_caractere()
{
    Console.ForegroundColor = ConsoleColor.DarkYellow;
    Console.WriteLine("Introduceti un numar (nu caractere):");
    Console.ResetColor();
}

Magazin magazin1 = new Magazin("magazin1");
string parola = "parola";
bool exit = false;
bool sunt_user = false;
ComenziUtilizator comenziUtilizator = new ComenziUtilizator(magazin1);
AdministrareMagazin comenziAdministrator = new AdministrareMagazin(magazin1);
AdministrareMagazin.CreazaProdusDinFisier(comenziAdministrator);
comenziAdministrator.EliminaLiniiDuplicate();
comenziAdministrator.IncarcaComenziDinFisier();
while (!exit)
{
    Cos cos= new Cos();
    Console.WriteLine();
    Console.ForegroundColor = ConsoleColor.DarkMagenta;
    Console.WriteLine("***Magazinu lu' Lucas***");
    Console.WriteLine("Alegeti Modul de operare: '1'-utilizator '2'-administrator!");
        int mod;
        while (true)
        {
            mesaj_caractere();
            string input = Console.ReadLine();
    
            if (int.TryParse(input, out mod))
            {
                break;
            }
            else
            {
                Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
            }
        }
        Console.ResetColor();

        switch (mod)
        {
            case 1:
                sunt_user = true;
                // Cos cos= new Cos();
                bool iesire_utilizator = false;
                while (!iesire_utilizator)
                {
                    Console.ForegroundColor = ConsoleColor.DarkGreen;
                    Console.WriteLine("*Optiuni de utilizator*");
                    Console.WriteLine("1.Vizualizeaza toate produsele magazinului");
                    Console.WriteLine("2.Inspecteaza un anumit produs");
                    Console.WriteLine("3.Cauta un produs dupa nume");
                    Console.WriteLine("4.Ordoneaza produsele dupa pret");
                    Console.WriteLine("5.Adauga un produs in cos");
                    Console.WriteLine("6.Efectueaza o comanda");
                    Console.WriteLine("7.Revenire la meniu mod utilizare");
                    Console.ResetColor();
                    int optiune_utilizator;
                    while (true)
                    {
                        mesaj_caractere();
                        string input = Console.ReadLine();
    
                        if (int.TryParse(input, out optiune_utilizator))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                        }
                    }
                    switch (optiune_utilizator)
                    {
                        case 1:
                            comenziUtilizator.AfisareProduse();
                            break;
                        case 2:
                            Console.WriteLine("Scrieti numele produsului pe care doriti sa-l inspectati:");
                            string produs_inspectat=Console.ReadLine();
                            comenziUtilizator.InspectareProdus(produs_inspectat);
                            break;
                        case 3:
                            Console.WriteLine("Ce produs cautati?");
                            string produs_cautat=Console.ReadLine();
                            comenziUtilizator.CautareProdusDupaNume(produs_cautat);
                            break;
                        case 4:
                            Console.WriteLine("Cum sortati? 1-crescator 2-descrescator");
                            int optiune_sortare;
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();
    
                                if (int.TryParse(input, out optiune_sortare))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }
                            switch (optiune_sortare)
                            {
                                case 1:
                                    Console.WriteLine("Produsele sortate crescator");
                                    comenziUtilizator.OrdonareProduseDupaPretCrescator();
                                    comenziUtilizator.AfisareProduse();
                                    break;
                                case 2:    
                                    Console.WriteLine("Produsele sortate descrescator");
                                    comenziUtilizator.OrdonareProduseDupaPretDescrescator();
                                    comenziUtilizator.AfisareProduse();
                                    break;
                                default:
                                    Console.WriteLine("Optiune sortare invalida");
                                    break;
                            }
                            break;
                        case 5:
                            Console.WriteLine("Ce produs adaugati in cos?");
                            string produs_de_adaugat=Console.ReadLine();
                            int care_produs=-1;
                            for (int i=0;i<magazin1.Produse.Count;i++)
                                if (magazin1.Produse[i].Nume == produs_de_adaugat)
                                    care_produs = i;
                            if (care_produs!=-1)
                            {
                                cos.AdaugareProdusInCos(magazin1.Produse[care_produs].Nume);
                                Console.WriteLine("Produs adaugat in cos!");
                            }
                            else
                                Console.WriteLine("Produsul selectat nu exista");
                            break;
                        case 6:
                            string nume, numar_telefon, email, adresa_livrare;
                            Console.WriteLine("Nume:"); 
                            nume = Console.ReadLine();
                            Console.WriteLine("Numar telefon:");
                            numar_telefon = Console.ReadLine();
                            Console.WriteLine("Email:"); 
                            email = Console.ReadLine();
                            Console.WriteLine("Adresa livrare:"); 
                            adresa_livrare = Console.ReadLine();
                            Console.WriteLine("Doriti sa plasati comanda 1-da, 2-no");
                            int plasare;
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();

                                if (int.TryParse(input, out plasare))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }

                            try
                            {
                                Validare.ValidareCos(cos.Cos_produse);
                                switch (plasare)
                                {
                                    case 1:
                                        // Cream o comanda cu produsele din cos si informatiile utilizatorului
                                        Comanda comandaUtilizator = new Comanda(cos, nume, numar_telefon, email, adresa_livrare);

                                        // Adaugam comanda in lista de comenzi
                                        comenziAdministrator.Adaugare_comanda_in_lista_comenzi(comandaUtilizator);
                                        
                                        // cos.Golire_cos();
                                        break;
                                    case 2:
                                        Console.WriteLine("Comanda nu a fost plasata");
                                        break;
                                    default:
                                        Console.WriteLine("Optiune invalida");
                                        break;
                                }
                            }
                            catch (ArgumentException ex)
                            {
                                Console.WriteLine($"Eroare: {ex.Message}");
                            }

                            break;
                        case 7:
                            iesire_utilizator = true;
                            // cos.Golire_cos();
                            break;
                        default:
                            Console.WriteLine("Optiune invalida");
                            break;
                    }
                }
                
                break;
            case 2:
                bool parola_corecta = false;
                Console.WriteLine("Introduceti parola:");
                int incercari=0;
                while (incercari < 3)
                {
                    string incercareparola = Console.ReadLine();
                    if (incercareparola != parola)
                    {
                        Console.WriteLine($"Parola gresita, mai aveti {2-incercari} incercari");
                        incercari++;
                    }
                    else
                    {
                        Console.WriteLine("Parola Corecta");
                        parola_corecta = true;
                        break;
                    }
                }
                bool iesire_administrator = false;
                while (!iesire_administrator && parola_corecta)
                {
                    Console.WriteLine("**Optiuni de administrator**");
                    Console.WriteLine("1.Adauga un nou produs in stoc");
                    Console.WriteLine("2.Scoate un produs din stoc");
                    Console.WriteLine("3.Schimba numarul de bucati ale unui produs din stoc");
                    Console.WriteLine("4.Vizualizare comenzi plasate");
                    Console.WriteLine("5.Proceseaza o comanda");
                    Console.WriteLine("6.Revenire la meniu mod utilizare");
                    int optiune_administrator;
                    while (true)
                    {
                        mesaj_caractere();
                        string input = Console.ReadLine();
    
                        if (int.TryParse(input, out optiune_administrator))
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                        }
                    }
                    switch (optiune_administrator)
                    {
                        case 1:
                            string nume;
                            decimal pret;
                            int stoc;
                            Console.WriteLine("Introdu numele produsului:");
                            nume=Console.ReadLine();
                            Console.WriteLine("Introdu Pretul produsului:");
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();
    
                                if (decimal.TryParse(input, out pret))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }
                            Console.WriteLine("Introdu stocul produsului:");
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();
    
                                if (int.TryParse(input, out stoc))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }
                            Console.WriteLine("Produs generic-1,produs perisabil-2,produs electrocasnic-3:");
                            int optiune_produs;
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();
    
                                if (int.TryParse(input, out optiune_produs))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }
                            switch (optiune_produs)
                            {
                               case 1:
                                   ProdusGeneric produs_generic = new ProdusGeneric(nume, pret, stoc);
                                   comenziAdministrator.Adaugare_produs_generic(produs_generic);
                                   comenziUtilizator.AfisareProduse();
                                   break;
                               case 2:
                                   DateTime data_expirare;
                                   string conditii_pastrare;
                                   Console.WriteLine("Introduceti data de expirare a produsului:");
                                   while (true)
                                   {
                                       string input = Console.ReadLine();
                                       if (DateTime.TryParse(input, out data_expirare))
                                       {
                                           break;  
                                       }
                                       else
                                       {
                                           Console.WriteLine("Data introdusa nu este valida. Va rugam sa introduceti o data corecta (ex: 31/12/2024).");
                                       }
                                   }

                                   Console.WriteLine("Introduceti conditiile de pastrare ale produsului:");
                                   conditii_pastrare = Console.ReadLine();
                                   ProdusPerisabil produs_perisabil = new ProdusPerisabil(nume, pret, stoc, data_expirare, conditii_pastrare);
                                   comenziAdministrator.Adaugare_produs_perisabil(produs_perisabil);
                                   comenziUtilizator.AfisareProduse();
                                   break;
                               case 3:
                                   string clasa_eficienta;
                                   Console.WriteLine("Introduceti clasa de eficienta a produsului:");
                                   clasa_eficienta=Console.ReadLine();
                                   Console.WriteLine("Introduceti puterea maxima a produsului:");
                                   int putere_maxima;
                                   while (true)
                                   {
                                       mesaj_caractere();
                                       string input = Console.ReadLine();
    
                                       if (int.TryParse(input, out putere_maxima))
                                       {
                                           break;
                                       }
                                       else
                                       {
                                           Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                       }
                                   }
                                   ProdusElectrocasnic produs_electrocasnic = new ProdusElectrocasnic(nume, pret, stoc,clasa_eficienta,putere_maxima);
                                   comenziAdministrator.Adaugare_produs_electrocasnic(produs_electrocasnic);
                                   comenziUtilizator.AfisareProduse();
                                   break;
                               default:
                                   Console.WriteLine("Va rugam alegeti o alta optiune");
                                   break;
                            }
                            break;
                        case 2:
                            string produs_pentru_stergere;
                            Console.WriteLine("Introduceti numele produsului pe care doriti sa il stergeti din stoc:");
                            produs_pentru_stergere=Console.ReadLine();
                            comenziAdministrator.Stergere_produs_pe_stoc(produs_pentru_stergere);
                            comenziUtilizator.AfisareProduse();
                            break;
                        case 3:
                            Console.WriteLine("Carui produs doriti sa ii modificati stocul?");
                            string produs_pentru_modificat_stoc = Console.ReadLine();
                            Console.WriteLine("Doriti sa cresteti-1 sau sa scadeti-2 stocul?");
                            int optiune_stoc=int.Parse(Console.ReadLine());
                            switch (optiune_stoc)
                            {
                                case 1:
                                    Console.WriteLine("Cu cat doriti sa cresteti stocul?:");
                                    int crestere_stoc;
                                    while (true)
                                    {
                                        mesaj_caractere();
                                        string input = Console.ReadLine();
    
                                        if (int.TryParse(input, out crestere_stoc))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                        }
                                    }
                                    comenziAdministrator.Modificare_stoc_produs_pe_stoc(produs_pentru_modificat_stoc,crestere_stoc);
                                    comenziUtilizator.AfisareProduse();
                                    break;
                                case 2:
                                    Console.WriteLine("Cu cat doriti sa scadeti stocul?:");
                                    int scadere_stoc;
                                    while (true)
                                    {
                                        mesaj_caractere();
                                        string input = Console.ReadLine();
    
                                        if (int.TryParse(input, out scadere_stoc))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                        }
                                    }
                                    comenziAdministrator.Modificare_stoc_produs_pe_stoc(produs_pentru_modificat_stoc,-scadere_stoc);
                                    comenziUtilizator.AfisareProduse();
                                    break;
                                default:
                                    Console.WriteLine("Optiune invalida");
                                    break;
                            }
                            break;
                        case 4:
                            comenziAdministrator.Vizualizare_comenzi_plasate();
                            break;
                        case 5:
                            Console.WriteLine("Ce comanda doriti sa procesati? ");
                            int care_comanda;
                            while (true)
                            {
                                mesaj_caractere();
                                string input = Console.ReadLine();
                            
                                if (int.TryParse(input, out care_comanda))
                                {
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                }
                            }
                            
                            bool iesire_procesare = false;
                            while (!iesire_procesare)
                            {
                                Console.WriteLine();
                                Console.WriteLine("Doriti sa 1-schimbati statusul, 2-schimbati data livrarii, 0-iesire meniu procesare:");
                                int optiune_procesare;
                                while (true)
                                {
                                    mesaj_caractere();
                                    string input = Console.ReadLine();
                            
                                    if (int.TryParse(input, out optiune_procesare))
                                    {
                                        break;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                    }
                                }
                                
                                switch (optiune_procesare)
                                {
                                    case 0:
                                        iesire_procesare=true;
                                        break;
                                    case 1:
                                        int ok_modificare;
                                        while (true)
                                        {
                                            Console.WriteLine("Schimbati statusul in 'in curs de livrare'? 1-da");
                                            mesaj_caractere();
                                            string input = Console.ReadLine();
                            
                                            if (int.TryParse(input, out ok_modificare))
                                            {
                                                break;
                                            }
                                            else
                                            {
                                                Console.WriteLine("Input invalid. Va rugam sa introduceti un numar.");
                                            }
                                        }
                                        comenziAdministrator.Procesare_comenzi_status(care_comanda,ok_modificare);
                                        break;
                                    case 2:
                                    Console.WriteLine("Introduceti noua data de livrare (format: dd/MM/yyyy):");
                                    DateTime noua_data_livrare;
                                    while (true)
                                    {
                                        string input = Console.ReadLine();
                                        if (DateTime.TryParseExact(input, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out noua_data_livrare))
                                        {
                                            break;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Data introdusa nu este valida. Va rugam sa introduceti o data corecta in formatul dd/MM/yyyy.");
                                        }
                                    }
                                    comenziAdministrator.Procesare_comenzi_data_livrare(care_comanda, noua_data_livrare);

                                    break;
                                    default:
                                        Console.WriteLine("Optiune invalida");
                                        break;
                                }
                                
                            }
                            break;
                        case 6:
                            iesire_administrator = true;
                            if(sunt_user==false)
                                cos.Golire_cos();
                            break;
                        default:
                            Console.WriteLine("Optiune invalida");
                            break;
                    }
                }
                break;
        }
    }