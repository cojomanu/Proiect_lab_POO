namespace Magazin_online;

public class Comanda
{
    public int numar_comanda { get; set; }
    public List<ProdusGeneric> Cos_produse { get; private set; }
    public string nume_persoana { get; private set; }
    public string numar_telefon { get; private set; }
    public string email { get; private set; }
    public string adresa_livrare { get; private set; }
    public string status { get; private set; }
    public DateTime data_livrare { get; private set; }

    private static int contor = -1;
    

    public Comanda(List<ProdusGeneric> cos, string nume_persoana, string numar_telefon, string email, string adresa_livrare)
    {
        contor++;
        this.numar_comanda = contor;
        this.Cos_produse = cos;
        this.nume_persoana = nume_persoana;
        this.numar_telefon = numar_telefon;
        this.email = email;
        this.adresa_livrare = adresa_livrare;
        data_livrare = DateTime.Now.AddDays(2);
        if (data_livrare < DateTime.Now)
        {
            status = "Livrat";
        }
        else
        {
            status = "In asteptare";
        }
    }

    public void setStatus(String status)
    {
        this.status = status;
    }

    public void set_Data_livrare(DateTime data_noua)
    {
        this.data_livrare = data_noua;
    }

    public override string ToString()
    {
        return $"Comanda {numar_comanda} contine {Cos_produse} pentru {nume_persoana}, {numar_telefon}, {email}, {adresa_livrare}, {email}, adresa: {adresa_livrare}, status: {status}, data_livrare: {data_livrare} ";
    }

   

}