erDiagram
Zamówienie || --o{
    Pojazdy: numer_vin
    Pojazdy || --o{
        Fabryka: id_fabryki
    Zamówienie {
        GUID id_zamowienia
        string imie_klienta
        string nazwisko_klienta
        string email_klienta
        GUID numer_vin
        }
    Pojazdy {
        GUID numer_vin
        date data_produkcji
        string status
        int wartosc
        string model
            enum fabryka
        int moc
        }
    Fabryka{
        int id_fabryki
        string miejscowosc
        }

