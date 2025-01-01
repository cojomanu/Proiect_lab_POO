# Proiect_lab_POO
Proiectul nr 1 : "Magazin online"

Dezvoltati o aplicatie pentru un magazin online.

(~2p)
Utilizatorul va putea efectua urmatoarele operatii:
- sa vizualizeze lista tuturor produselor
- sa inspecteze un produs pentru a vedea descrierea, numarul de bucati din stoc si alte detalii ale produsului
- sa caute un produs dupa nume
- sa ordoneze rezultatele dupa pret (crescator si descrescator)
- sa adauge un produs in cos
- sa efectueze o comanda (cu nume, numar de telefon, email si adresa de livrare)
- sesiunea unui utilizator nu se va salva la inchiderea programului

(~2p)
Administratorul va putea efectua urmatoarele operatii:
- sa introduca produse noi pe stoc
- sa scoata produse din stoc
- sa creasca/scada numarul de bucati dintr-un produs
- sa vizualizeze toate comenzile plasate (comenzile care au data de livrare in trecut vor fi marcate ca "livrat")
- sa proceseze comenzi (sa le schimbe statusul din "in asteptare" in "in curs de livrare" si sa le seteze data livrarii)

(~2p)
Produsele pot fi de 3 tipuri:
- generice (denumire, pret)
- perisabile (denumire, pret, data expirarii, conditii de pastrare)
- electrocasnice (clasa de eficienta energetica, puterea maxima consumata)

(~1p)
Creati un meniu in consola pentru a putea folosi toate aceste optiuni. Programul trebuie sa poata trece de la utilizator la administrator fara a fi repornit. (Optional: puteti realiza o interfata grafica in ce tehnologie doriti (WPF, MAUI, Windows Forms etc.), cu conditia ca limbajul de programare folosit sa fie C#).

Manu G: (~2p): La inchiderea programului, starea programului (exceptand partea de utilizator) trebuie salvata intr-un fisier (text, csv, alte formate) sau mai multe, iar la pornirea programului, acesta va cauta fisierul si va incarca toate datele din el. In cazul in care fisierul nu exista, programul va porni fara a incarca nimic, dar la inchidere va salva noile date intr-un fisier nou. Pentru acest scop se va implementa un serviciu separat care primeste toate datele de salvat/incarcat si salveaza/incarca. Functionalitatea de salvare si incarcare nu se va implementa in interiorul claselor ce ofera functionalitatea principala a programului.

Functionalitatea trebuie separata de interfata. Clasele aferente functionalitatilor de mai sus trebuie sa nu contina cod ce tine de interfata (scris/citit din consola etc.). Programul trebuie sa nu se poata termina necontrolat (cu exceptii). Toate exceptiile posibile trebuie prinse si tratate.

Oficiu: 1p
