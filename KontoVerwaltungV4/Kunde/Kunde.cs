using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace KontoVerwaltungV4.Kunde
{
    public class Kunde
    {
        public int ID { get; set; }

        [Required] public string Vorname { get; set; }

        [Required] public string Nachname { get; set; }

        [Required] public string Adresse { get; set; }

        [Required] public int Plz { get; set; }

        [Required] public string Ort { get; set; }

        public string TelefonNummer { get; set; }

        [Required] public bool DatenschutzErklärung { get; set; }

        public virtual ICollection<Konto.Konto> KontoList { get; } = new List<Konto.Konto>();


        //TODO:Parameterlosen Konstruktor entfernen (aufloeßen des der entity. Fehler im Framework)
        public Kunde()
        {
        }

        public Kunde(string vorname, string nachname, string adresse, int plz, string ort,
            string telefonNummer, bool datenschutzErklärung)
        {
            Vorname = vorname;
            Nachname = nachname;
            Adresse = adresse;
            Plz = plz;
            Ort = ort;
            TelefonNummer = telefonNummer;
            DatenschutzErklärung = datenschutzErklärung;
        }
    }
}