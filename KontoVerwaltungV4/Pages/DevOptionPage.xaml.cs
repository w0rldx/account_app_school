using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaction logic for DevOptionPage.xaml
    /// </summary>
    public partial class DevOptionPage : Page
    {
        public DevOptionPage()
        {
            InitializeComponent();
        }

        private void EncryptButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                DecryptTextbox.Text = EncryptPin(EncryptTextbox.Text, EncryptKeyTextbox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        private void DecryptButton_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                EncryptTextbox.Text = DecryptPin(DecryptTextbox.Text, EncryptKeyTextbox.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        ///     Verschlüsselungs Funktion da kein Klartext
        /// </summary>
        /// <param name="pinKlartext"></param>
        /// <returns></returns>
        public string EncryptPin(string pinKlartext, string encryptionKey)
        {
            string pinVerschluesselt;
            var clearBytes = Encoding.Unicode.GetBytes(pinKlartext);
            using (var a = Aes.Create())
            {
                var x = new Rfc2898DeriveBytes(encryptionKey,
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
        public string DecryptPin(string pinVerschluesselt, string encryptionKey)
        {
            string pinUnverschluesselt;
            var cipherBytes = Convert.FromBase64String(pinVerschluesselt);
            using (var a = Aes.Create())
            {
                var x = new Rfc2898DeriveBytes(encryptionKey,
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
    }
}