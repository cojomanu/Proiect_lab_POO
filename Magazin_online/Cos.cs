namespace Magazin_online;

public class Cos
{
    public List<ProdusGeneric> Cos_produse{get;set;}
    public void AdaugareProdusInCos(ProdusGeneric produs)
    {
        Cos_produse.Add(produs);
        Console.WriteLine($"Produsul {produs} a fost adaugat in cos");
    }

    public override string ToString()
    {
        string afisare="Cos: ";
        foreach (var produs in Cos_produse)
            afisare = afisare + produs.Nume;
        
        return afisare;
    }
}