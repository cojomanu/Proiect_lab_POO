using System.Data.Common;
using System.Net.Http.Headers;
using System.Text.Json.Serialization.Metadata;

namespace Magazin_online;

public class ProdusGeneric
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

    public void Modificare_stoc(int modificare)
    {
        this.Stoc += modificare;
    }

    public override string ToString() => $"Produsul : {Nume} - {Pret} lei, {Stoc} bucati pe stoc ";
}