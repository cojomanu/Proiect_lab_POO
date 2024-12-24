namespace Magazin_online;

public class AdministrareMagazin: Magazin
{
    
    public AdministrareMagazin(string nume) : base(nume) { }
    public override void AdaugaProdus(ProdusGeneric produs)
    {
        Produse.Add(produs);
    }

    public override void AfiseazaProduse()
    {
        Console.WriteLine("Produsele sunt : ");
        foreach (var produs in Produse)
        {
            Console.WriteLine(produs);
        }
    }
}