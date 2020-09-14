using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Exceptions;
using KontoVerwaltungV4.Transaktionen;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für Auszahlung.xaml
    /// </summary>
    public partial class Auszahlung : Page
    {
        public Auszahlung()
        {
            InitializeComponent();
        }

        private void AuszahlenButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                try
                {
                    if (BetragTextbox.Text == "" || KonotonummerTextbox.Text == "" || PinTextbox.Password == "")
                        throw new IsEmptyException();

                    var correct = false;
                    var betrag = Convert.ToDouble(BetragTextbox.Text) * -1;
                    var g1 = db.KontoSet.Where(k => k.KontoNummer == KonotonummerTextbox.Text).ToList();
                    if (!g1.Any())
                        throw new IsEmptyException();
                    foreach (var k in g1)
                        if (k.DecryptPin(k.Pin) == PinTextbox.Password)
                        {
                            k.TransactionsList.Add(new Transaktion(betrag, k.KontoNummer, Types.Auszahlung,
                                "Auszahlung"));
                            k.Betrag += betrag;
                            correct = true;
                        }
                        else
                        {
                            throw new FalsePinException();
                        }


                    if (correct)
                    {
                        db.SaveChanges();

                        MessageBox.Show($"{betrag * -1}€ wurde von Konto {KonotonummerTextbox.Text} ausbezahlt!");
                        ResetForm();
                    }
                }
                catch (IsEmptyException)
                {
                    MessageBox.Show("Fehlende Informationen");
                }
                catch (FalsePinException)
                {
                    MessageBox.Show("Pin Falsch!!");
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        private void ResetForm()
        {
            KonotonummerTextbox.Text = "";
            BetragTextbox.Text = "";
            PinTextbox.Password = "";
        }

        private void KonotonummerTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BetragTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void PinTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}