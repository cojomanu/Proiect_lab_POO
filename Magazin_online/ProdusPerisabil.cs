namespace Magazin_online;

public class ProdusPerisabil: ProdusGeneric
{
    public DateTime DataExpirare { get; private set; }
    public string ConditiiDeDepozitare { get; private set; }

    public ProdusPerisabil(string nume, decimal pret, DateTime dataExpirare, string conditiiDeDepozitare) : base(nume,
        pret)
    {
        this.DataExpirare = dataExpirare;
        this.ConditiiDeDepozitare = conditiiDeDepozitare;
    }
    
    public override string ToString() => $"Produsul perisabil : {Nume} - {Pret} lei - data expirare: {DataExpirare} - conditii de depozitare: {ConditiiDeDepozitare}";
}