using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using KontoVerwaltungV4.Transaktionen;

namespace KontoVerwaltungV4.Konto
{
    public abstract class Konto
    {
        

        private string _pin;

        public int ID { get; set; }

        [Column("pin_encrypt")]
        public string Pin
        {
            get => Crypto.DecryptPin(_pin);
            set => _pin = Crypto.EncryptPin(value);
        }

        [Required] public string KontoNummer { get; set; }

        public double Betrag { get; set; }

        [ForeignKey("KontoID")]
        public virtual ICollection<Transaktion> TransactionsList { get; } =
            new List<Transaktion>(); //virtual aufgrund EFCore Framework LazyLoad

        //KUNDE
        [Required] public virtual Kunde.Kunde Inhaber { get; set; } //virtual aufgrund EFCore Framework LazyLoad

        

        public abstract double BerechneZins();

        public virtual void VerrechneZins(double zins)
        {
            TransactionsList.Add(new Transaktion(zins, KontoNummer, Types.Zinsen, "Zinsen"));
            Betrag = Betrag + zins;
        }

        public void DestroyTransactions()
        {
            TransactionsList.Clear();
        }
    }
}