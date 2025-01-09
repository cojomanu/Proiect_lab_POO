namespace Magazin_online;

public class Cos
{
    public List<string> Cos_produse{get;set;}

    public Cos()
    {
        Cos_produse = new List<string>();
    }
    public void AdaugareProdusInCos(string produs)
    {
        Cos_produse.Add(produs);
        //Console.WriteLine($"Produsul {produs} a fost adaugat in cos");
    }
    
    public void Golire_cos()
    {
        Cos_produse.Clear();
    }

    public override string ToString()
    {
        string afisare="Cos: ";
        foreach (var produs in Cos_produse)
            afisare = afisare + produs+", ";
        
        return afisare;
    }
}