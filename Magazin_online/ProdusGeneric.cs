using System.Net.Http.Headers;
using System.Text.Json.Serialization.Metadata;

namespace Magazin_online;

public abstract class ProdusGeneric
{
    public string Nume { get; private set; }
    public decimal Pret { get; private set; }
    
    public int Stoc { get; private set; }

    public ProdusGeneric(string nume, decimal pret,int stoc)
    {
        this.Nume = nume;
        this.Pret = pret;
        this.Stoc = stoc;
    }

    public override string ToString() => $"Produsul : {Nume} - {Pret} lei ";
}