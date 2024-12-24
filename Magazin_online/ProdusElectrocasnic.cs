﻿namespace Magazin_online;

public class ProdusElectrocasnic: ProdusGeneric
{
    public string ClasaDeEficientaEnergetica { get; private set; }
    public int PutereMaximaConsumata { get; private set; }

    public ProdusElectrocasnic(string nume, decimal pret,string clasaDeEficientaEnergetica, int putereMaximaConsumata ) : base(nume,
        pret)
    {
        this.ClasaDeEficientaEnergetica = clasaDeEficientaEnergetica;
        this.PutereMaximaConsumata = putereMaximaConsumata;
    }
    
    public override string ToString() => $"Produsul electrocasnic : {Nume} - {Pret} lei - : clasa de efiecienta energetica{ClasaDeEficientaEnergetica}  - putere maxima consumata{PutereMaximaConsumata} W";
}