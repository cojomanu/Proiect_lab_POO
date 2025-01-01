namespace Magazin_online;

public interface Utilizator
{
    public void AfisareProduse();
    public void InspectareProdus(string produs);
    public void CautareProdusDupaNume(string nume);
    public void OrdonareProduseDupaPretCrescator();
    public void OrdonareProduseDupaPretDescrescator();
}