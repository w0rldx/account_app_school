using System;

namespace KontoVerwaltungV4.Transaktionen
{
    public class Transaktion
    {
        public int ID { get; set; }
        public double Betrag { get; set; }
        public string Empfaenger { get; set; }
        public string Beschreibung { get; set; }
        public Types Type { get; set; }
        public Guid IdNummer { get; set; }

        /// <summary>
        ///     Erstellen einer Trasaktion
        /// </summary>
        /// <param name="betrag"></param>
        /// <param name="empfaenger"></param>
        /// <param name="sender"></param>
        /// <param name="beschreibung"></param>
        public Transaktion(double betrag, string empfaenger, Types type, string beschreibung)
        {
            Betrag = betrag;
            Empfaenger = empfaenger;
            Beschreibung = beschreibung;
            IdNummer = Guid.NewGuid();
            Type = type;
        }

        public override string ToString()
        {
            if (Type == Types.Einzahlung)
                return $"{Betrag}€ wurde Einbezahlt\n";
            if (Type == Types.Auszahlung)
                return $"{Betrag}€ wurden Ausbezahlt\n";
            if (Type == Types.Ueberweisung && Betrag > 0)
                return $"{Betrag}€ wurde erhalten. Beschreibung: {Beschreibung}!!\n";
            if (Type == Types.Ueberweisung && Betrag < 0)
                return $"{Betrag}€ wurde nach {Empfaenger} überwiesen. Beschreibung: {Beschreibung}!!\n";
            if (Type == Types.Ueberweisung && Betrag > 0 && Beschreibung == "")
                return $"{Betrag}€ wurde erhalten!!\n";
            if (Type == Types.Ueberweisung && Betrag < 0 && Beschreibung == "")
                return $"{Betrag}€ wurde nach {Empfaenger} überwiesen!!\n";
            if (Type == Types.Zinsen)
                return $"{Betrag}€ wurden als Zinsen Gutgeschrieben!\n";
            return "";
        }
    }
}