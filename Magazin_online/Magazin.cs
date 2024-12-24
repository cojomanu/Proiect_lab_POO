namespace Magazin_online;

public  abstract class Magazin
{
    public List<ProdusGeneric> Produse { get;private set; }

    public string Nume { get; private set; }

    public Magazin(string nume)
    {
        this.Nume = nume;
        Produse = new List<ProdusGeneric>();
    }
    
    public abstract void AdaugaProdus(ProdusGeneric produs);
    public abstract void AfiseazaProduse();


}