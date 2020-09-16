namespace KontoVerwaltungV4.Konto
{
    public class GiroKonto : Konto
    {
        public double GiroKontoZinsSatz { get; set; }

        public GiroKonto(string kontoNummer, string pin, double betrag, double giroKontoZinsSatz)
        {
            KontoNummer = kontoNummer;
            Pin = pin;
            Betrag = betrag;
            GiroKontoZinsSatz = giroKontoZinsSatz;
        }

        public GiroKonto(string kontoNummer, string pin, double betrag, double giroKontoZinsSatz, Kunde.Kunde inhaber) :
            this(kontoNummer, pin, betrag, giroKontoZinsSatz)
        {
            Inhaber = inhaber;
        }

        public override double BerechneZins()
        {
            var zins = Betrag * GiroKontoZinsSatz;

            return zins;
        }
    }
}