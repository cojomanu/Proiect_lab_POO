namespace DefaultNamespace;

public class ComenziUtilizator: Utilizator
{ 
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