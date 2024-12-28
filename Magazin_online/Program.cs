// See https://aka.ms/new-console-template for more information

using Magazin_online;
using Magazin_online;
Console.WriteLine("Hello, World!");
Console.WriteLine("Produse");


ProdusGeneric paine = new ProdusPerisabil("paine",5,1,new DateTime(2024, 12, 31),"uscat si racoros");
ProdusGeneric apa = new ProdusPerisabil("apa",1,1,new DateTime(2024, 10, 31),"uscat si racoros");

Magazin magazin1 = new Magazin("magazin1");

ComenziUtilizator comenziUtilizator = new ComenziUtilizator(magazin1);

comenziUtilizator.AdaugaProdus(paine);
comenziUtilizator.AdaugaProdus(apa);
comenziUtilizator.AfiseazaProduse();
comenziUtilizator.OrdonareProduseDupaPret();
comenziUtilizator.AfiseazaProduse();
Console.WriteLine("xox");









