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
    ///     Interaktionslogik für Einzahlung.xaml
    /// </summary>
    public partial class Einzahlung : Page
    {
        public Einzahlung()
        {
            InitializeComponent();
        }

        private void EinzahlenButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                try
                {
                    if (EmpfaengerKonotTextbox.Text == "" || BetragTextbox.Text == "") throw new IsEmptyException();

                    var betrag = Convert.ToDouble(BetragTextbox.Text);
                    var g1 = db.KontoSet.Where(k => k.KontoNummer == EmpfaengerKonotTextbox.Text);
                    foreach (var giro in g1)
                    {
                        giro.TransactionsList.Add(new Transaktion(betrag, giro.KontoNummer, Types.Einzahlung,
                            "Einzahlung"));
                        giro.Betrag += betrag;
                    }

                    db.SaveChanges();
                    MessageBox.Show($"{betrag}€ wurde in Konto {EmpfaengerKonotTextbox.Text} einbezahlt!");
                    ResetForm();
                }
                catch (FormatException)
                {
                    MessageBox.Show("Kontonummer Existiert nicht!");
                }
                catch (IsEmptyException)
                {
                    MessageBox.Show("Fehlende Angaben!!");
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        private void KontoNummerTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BetragTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void ResetForm()
        {
            EmpfaengerKonotTextbox.Text = "";
            BetragTextbox.Text = "";
        }
    }
}