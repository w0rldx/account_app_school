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
        private readonly string _encryptionKey = "wy4LwvRZ55v3X6";

        private string _pin;
        public int ID { get; set; }

        [Column("pin_encrypt")]
        public string Pin
        {
            get => DecryptPin(_pin);
            set => _pin = EncryptPin(value);
        }

        [Required] public string KontoNummer { get; set; }

        public double Betrag { get; set; }

        [ForeignKey("KontoID")] public ICollection<Transaktion> TransactionsList { get; } = new List<Transaktion>();

        //KUNDE
        [Required] public Kunde.Kunde Inhaber { get; set; }

        /// <summary>
        ///     Verschlüsselungs Funktion da kein Klartext
        /// </summary>
        /// <param name="pinKlartext"></param>
        /// <returns></returns>
        public string EncryptPin(string pinKlartext)
        {
            string pinVerschluesselt;
            var clearBytes = Encoding.Unicode.GetBytes(pinKlartext);
            using (var a = Aes.Create())
            {
                var x = new Rfc2898DeriveBytes(_encryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                a.Key = x.GetBytes(32); //TODO:Moegliches null beachten
                a.IV = x.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, a.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }

                    pinVerschluesselt = Convert.ToBase64String(ms.ToArray());
                }
            }

            return pinVerschluesselt;
        }

        /// <summary>
        ///     Entschlüsselungs Funktion
        /// </summary>
        /// <param name="pinVerschluesselt"></param>
        /// <returns></returns>
        public string DecryptPin(string pinVerschluesselt)
        {
            string pinUnverschluesselt;
            var cipherBytes = Convert.FromBase64String(pinVerschluesselt);
            using (var a = Aes.Create())
            {
                var x = new Rfc2898DeriveBytes(_encryptionKey,
                    new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                a.Key = x.GetBytes(32); //TODO:Moegliches null beachten
                a.IV = x.GetBytes(16);
                using (var ms = new MemoryStream())
                {
                    using (var cs = new CryptoStream(ms, a.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }

                    pinUnverschluesselt = Encoding.Unicode.GetString(ms.ToArray());
                }
            }

            return pinUnverschluesselt;
        }

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