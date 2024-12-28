namespace Magazin_online;

public class ComenziUtilizator : Utilizator
{
    private Magazin _magazin;

    public ComenziUtilizator(Magazin magazin)
    {
        _magazin = magazin;
    }

    public void AdaugaProdus(ProdusGeneric produs)
    {
        _magazin.Produse.Add(produs);
    }

    public void AfiseazaProduse()
    {
        Console.WriteLine("Produsele din magazin sunt: ");
        foreach (var produs in _magazin.Produse)
        {
            Console.WriteLine(produs);
        }
    }

    public void CautareProdusDupaNume(string nume)
    {
        foreach (var produs in _magazin.Produse)
        {
            if (produs.Nume == nume)
                Console.WriteLine($"Da produsul {produs} se afla in magazin!");
            else
                Console.WriteLine("Produs inexistent!");
        }
    }

    public void OrdonareProduseDupaPret()
    {
        _magazin.Produse.Sort((p1, p2) => p1.Pret.CompareTo(p2.Pret));
        Console.WriteLine("Goron e tampit");
    }
}