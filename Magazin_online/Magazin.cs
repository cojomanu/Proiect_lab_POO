namespace Magazin_online;

public class Magazin
{
    public List<ProdusGeneric> Produse { get;private set; }

    public string Nume { get; private set; }

    public Magazin(string nume)
    {
        this.Nume = nume;
        Produse = new List<ProdusGeneric>();
    }
    
  
}