using System;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Exceptions;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für NewKunde.xaml
    /// </summary>
    public partial class NewKunde : Page
    {
        public NewKunde()
        {
            InitializeComponent();
        }

        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                try
                {
                    if (VornameTextBox.Text == "" || NachnameTextBox.Text == "" || AdresseTextBox.Text == "" ||
                        PlzTextBox.Text == "" || OrtTextBox.Text == "" || DatenschutzCheckbox.IsChecked == false)
                    {
                        throw new IsEmptyException();
                    }

                    var plz = Convert.ToInt32(PlzTextBox.Text);
                    var datenschutz = DatenschutzCheckbox.IsChecked == true;
                    db.Add(new Kunde.Kunde(VornameTextBox.Text, NachnameTextBox.Text, AdresseTextBox.Text, plz,
                        OrtTextBox.Text, TelefonNummerTextBox.Text,
                        datenschutz)); //TODO:Ueberpruefung ob bereits ein Kunde mit den Werten vorhanden ist
                    db.SaveChanges();
                    MessageBox.Show("Kunde angelegt!");
                }
                catch (IsEmptyException)
                {
                    MessageBox.Show("Alle Felder müssen ausgefüllt sein!");
                }
                finally
                {
                    db.Dispose();
                }
            }
        }

        private void PlzTextBox_OnPreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.,]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}