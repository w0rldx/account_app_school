using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Konto;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für DeleteKonto.xaml
    /// </summary>
    public partial class DeleteKonto : Page
    {
        public DeleteKonto()
        {
            InitializeComponent();
        }

        private void DeleteButton_OnClick(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Wollen Sie dieses Konto wirklich Löschen ?", "Löschen",
                MessageBoxButton.YesNo);
            var error = false;
            switch (result)
            {
                case MessageBoxResult.Yes:
                    using (var db = new BankingContext())
                    {
                        var kontoList = db.KontoSet.Where(s => s.KontoNummer == KontoNummerTextbox.Text);
                        foreach (var giro in kontoList)
                            if (Crypto.DecryptPin(giro.Pin) == PinTextbox.Password)
                            {
                                var transactionlist = db.TransaktionsSet
                                    .Where(s => s.Empfaenger == KontoNummerTextbox.Text)
                                    .ToList();
                                foreach (var transaction in transactionlist) db.TransaktionsSet.Remove(transaction);
                                db.Remove(giro);
                                error = false;
                            }
                            else
                            {
                                MessageBox.Show("Pin Falsch!");
                                error = true;
                                break;
                            }

                        //TODO: Fehler im Löschprotokoll Beheben FOREIGN-Key kann nicht gelöscht werden solange objekt noch besteht (Transaktionen werden deswegen nicht gelöscht sonderen besitzen keinen key mehr)
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Currently not working :D ! I work on a fix for this.");
                        }
                    }

                    if (error == false)
                    {
                        MessageBox.Show("Gelöscht!!");
                        ResetForm();
                    }

                    break;
                case MessageBoxResult.No:
                    ResetForm();
                    break;
            }
        }

        private void ResetForm()
        {
            KontoNummerTextbox.Text = "";
            PinTextbox.Password = "";
        }
    }
}