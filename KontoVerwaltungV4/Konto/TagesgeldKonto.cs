namespace KontoVerwaltungV4.Konto
{
    public class TagesgeldKonto : Konto
    {
        public double TagesgeldKontoZinsSatz { get; set; }


        public TagesgeldKonto(string kontoNummer, string pin, double betrag, double tagesgeldKontoZinsSatz)
        {
            KontoNummer = kontoNummer;
            Pin = pin;
            Betrag = betrag;
            TagesgeldKontoZinsSatz = tagesgeldKontoZinsSatz;
        }

        public TagesgeldKonto(string kontoNummer, string pin, double betrag, double tagesgeldKontoZinsSatz,
            Kunde.Kunde inhaber) :
            this(kontoNummer, pin, betrag, tagesgeldKontoZinsSatz)
        {
            Inhaber = inhaber;
        }

        public override double BerechneZins()
        {
            var zins = Betrag * TagesgeldKontoZinsSatz;

            return zins;
        }
    }
}