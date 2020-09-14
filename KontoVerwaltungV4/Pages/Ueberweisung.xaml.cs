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
    ///     Interaktionslogik für Ueberweisung.xaml
    /// </summary>
    public partial class Ueberweisung : Page
    {
        public Ueberweisung()
        {
            InitializeComponent();
        }

        private void UeberweisenButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                try
                {
                    if (EmpfaengerKonotTextbox.Text == "" || SenderKontoTextbox.Text == "" || BetragTextbox.Text == "")
                        throw new NoTextException();

                    var betrag = Convert.ToDouble(BetragTextbox.Text);
                    var g2 = db.KontoSet.Where(k => k.KontoNummer == EmpfaengerKonotTextbox.Text);
                    var g1 = db.KontoSet.Where(k => k.KontoNummer == SenderKontoTextbox.Text);
                    if (!g2.Any() || !g1.Any())
                    {
                        throw new IsEmptyException();
                    }

                    if (BeschreibungTextbox.Text == string.Empty)
                        foreach (var giro in g2)
                        {
                            giro.TransactionsList.Add(new Transaktion(betrag, EmpfaengerKonotTextbox.Text,
                                Types.Ueberweisung, "Überweisung von: " + SenderKontoTextbox.Text));
                            giro.Betrag = giro.Betrag + betrag;
                        }
                    else
                        foreach (var giro in g2)
                        {
                            giro.TransactionsList.Add(new Transaktion(betrag, EmpfaengerKonotTextbox.Text,
                                Types.Ueberweisung,
                                "Überweisung von: " + SenderKontoTextbox.Text + " Beschreibung: " +
                                BeschreibungTextbox.Text));
                            giro.Betrag = giro.Betrag + betrag;
                        }


                    betrag = betrag * -1;
                    if (BeschreibungTextbox.Text == string.Empty)
                        foreach (var giro in g1)
                        {
                            giro.TransactionsList.Add(new Transaktion(betrag, SenderKontoTextbox.Text,
                                Types.Ueberweisung, ""));
                            giro.Betrag = giro.Betrag + betrag;
                        }
                    else
                        foreach (var giro in g1)
                        {
                            giro.TransactionsList.Add(new Transaktion(betrag, SenderKontoTextbox.Text,
                                Types.Ueberweisung, "Überweisung: " + BeschreibungTextbox.Text));
                            giro.Betrag = giro.Betrag + betrag;
                        }


                    db.SaveChanges();
                    betrag = betrag * -1;
                    MessageBox.Show(
                        $"{betrag}€ wurde von Konto {SenderKontoTextbox.Text} nach {EmpfaengerKonotTextbox.Text} Konto überwiesen!");
                    ResetForm();
                }
                catch (NoTextException)
                {
                    MessageBox.Show("Fehlende Angaben!");
                }
                catch (IsEmptyException)
                {
                    MessageBox.Show("Empfänger- oder Senderkonto existieren nicht!");
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        private void ResetForm()
        {
            SenderKontoTextbox.Text = "";
            EmpfaengerKonotTextbox.Text = "";
            BetragTextbox.Text = "";
            BeschreibungTextbox.Text = "";
        }

        private void EmpfaengerKonotTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SenderKontoTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void BetragTextbox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}