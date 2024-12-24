using System.Net.Http.Headers;
using System.Text.Json.Serialization.Metadata;

namespace Magazin_online;

public abstract class ProdusGeneric
{
    public string Nume { get; private set; }
    public decimal Pret { get; private set; }

    public ProdusGeneric(string nume, decimal pret)
    {
        this.Nume = nume;
        this.Pret = pret;
    }

    public override string ToString() => $"Produsul : {Nume} - {Pret} lei ";
}