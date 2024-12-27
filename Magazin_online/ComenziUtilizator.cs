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
}