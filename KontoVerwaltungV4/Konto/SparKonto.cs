using System.Windows;

namespace KontoVerwaltungV4.Konto
{
    public class SparKonto : Konto
    {
        public double SparKontoZinsSatz { get; set; }

        private SparKonto(string kontoNummer, string pin, double betrag, double sparKontoZinsSatz)
        {
            KontoNummer = kontoNummer;
            Pin = pin;
            Betrag = betrag;
            SparKontoZinsSatz = sparKontoZinsSatz;
        }

        public SparKonto(string kontoNummer, string pin, double betrag, double sparKontoZinsSatz, Kunde.Kunde inhaber) :
            this(kontoNummer, pin, betrag, sparKontoZinsSatz)
        {
            Inhaber = inhaber;
        }

        public override double BerechneZins()
        {
            MessageBox.Show("Dieser Kontotype Kann keine Zinsen Berechnen!");

            return 0;
        }

        public override void VerrechneZins(double zins)
        {
            MessageBox.Show("Dieser Kontotype Kann keine Zinsen Verrechnen!");
        }
    }
}