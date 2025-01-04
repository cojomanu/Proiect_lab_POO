namespace Magazin_online;

public interface Administrator
{
    public void Adaugare_produs_generic(ProdusGeneric produs);   
    public void Adaugare_produs_perisabil(ProdusPerisabil produs);
    public void Adaugare_produs_electrocasnic(ProdusElectrocasnic produs);
    
    public void Stergere_produs_pe_stoc(string nume_produs);
    public void Modificare_stoc_produs_pe_stoc(string nume_produs, int crestere);
    
    public void Vizualizare_comenzi_plasate();
    
    public void Procesare_comenzi_status(int care_comanda, int ok);
    public void Procesare_comenzi_data_livrare(int care_comanda, DateTime noua_data);
    
    public void Adaugare_comanda_in_lista_comenzi(Comanda comanda);
}


