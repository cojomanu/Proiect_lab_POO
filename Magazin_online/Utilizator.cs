namespace Magazin_online;

public interface Utilizator
{
    public void AdaugaProdus(ProdusGeneric produs);
    public void AfiseazaProduse();
    public void CautareProdusDupaNume(string nume);
    public void OrdonareProduseDupaPret();
}