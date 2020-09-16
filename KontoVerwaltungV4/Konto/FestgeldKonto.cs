namespace KontoVerwaltungV4.Konto
{
    public class FestgeldKonto : Konto
    {
        public double FestgeldKontoZinsSatz { get; set; }

        public FestgeldKonto(string kontoNummer, string pin, double betrag, double festgeldKontoZinsSatz)
        {
            KontoNummer = kontoNummer;
            Pin = pin;
            Betrag = betrag;
            FestgeldKontoZinsSatz = festgeldKontoZinsSatz;
        }

        public FestgeldKonto(string kontoNummer, string pin, double betrag, double festgeldKontoZinsSatz,
            Kunde.Kunde inhaber) :
            this(kontoNummer, pin, betrag, festgeldKontoZinsSatz)
        {
            Inhaber = inhaber;
        }

        public override double BerechneZins()
        {
            var zins = Betrag * FestgeldKontoZinsSatz;

            return zins;
        }
    }
}