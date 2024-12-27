// See https://aka.ms/new-console-template for more information
using Magazin_online;
Console.WriteLine("Hello, World!");
Console.WriteLine("Produse");


ProdusGeneric pita = new ProdusPerisabil("pita",5,1,new DateTime(2024, 12, 31),"uscat si racoros");

Console.WriteLine(pita);

Magazin magazin1 = new Magazin("magazin1");

magazin1.AdaugaProdus(pita);
magazin1.AfiseazaProduse();




