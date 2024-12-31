namespace Magazin_online;

public class ProdusElectrocasnic: ProdusGeneric
{
    public string ClasaDeEficientaEnergetica { get; private set; }
    public int PutereMaximaConsumata { get; private set; }

    public ProdusElectrocasnic(string nume, decimal pret,int stoc, string clasaDeEficientaEnergetica, int putereMaximaConsumata ) : base(nume, pret,stoc)
    {
        this.ClasaDeEficientaEnergetica = clasaDeEficientaEnergetica;
        this.PutereMaximaConsumata = putereMaximaConsumata;
    }
    
    public override string ToString() => $"Produsul electrocasnic : {Nume} - {Pret} lei, {Stoc} bucati pe stoc  - : clasa de efiecienta energetica{ClasaDeEficientaEnergetica}  - putere maxima consumata{PutereMaximaConsumata} W";
}