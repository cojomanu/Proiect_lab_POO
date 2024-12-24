namespace Magazin_online;

public class AdministrareMagazin
{
    private List<ProdusGeneric> Produse = new List<ProdusGeneric>();

    public void AdaugaProdus(ProdusGeneric produs)
    {
        Produse.Add(produs);
    }

    public void AfiseazaProduse()
    {
        Console.WriteLine("Produsele sunt : ");
        foreach (var produs in Produse)
        {
            Console.WriteLine(produs);
        }
    }
}