using System.Xml.Schema;

namespace Magazin_online;

public class ComenziUtilizator : Utilizator
{
    private Magazin _magazin;

    public ComenziUtilizator(Magazin magazin)
    {
        _magazin = magazin;
    }

    public void InspectareProdus(string produs_cautat)
    {
        bool gasit = false;
        foreach (var produs in _magazin.Produse)
        {
            if (produs.Nume == produs_cautat)
            {
                Console.WriteLine(produs);
                gasit = true;
            }
        }
        if(!gasit)
            Console.WriteLine("Produs inexistent!");
        Console.WriteLine();
    }

    public void AfisareProduse()
    {
        Console.WriteLine("Produsele din magazin sunt: ");
        foreach (var produs in _magazin.Produse)
        {
            Console.WriteLine(produs);
        }
        Console.WriteLine();
    }

    public void CautareProdusDupaNume(string nume)
    {
        bool gasit = false;
        foreach (var produs in _magazin.Produse)
        {
            if (produs.Nume == nume)
            {
                Console.WriteLine($"Da, produsul {produs.Nume} se afla in magazin!");
                gasit = true;
            }
        }
        if(!gasit)
            Console.WriteLine("Produs inexistent!");
        Console.WriteLine();
    }

    public void OrdonareProduseDupaPretCrescator()
    {
        _magazin.Produse.Sort((p1, p2) => p1.Pret.CompareTo(p2.Pret));
    }

    public void OrdonareProduseDupaPretDescrescator()
    {
        _magazin.Produse.Sort((p1, p2) => p2.Pret.CompareTo(p1.Pret));
    }
}