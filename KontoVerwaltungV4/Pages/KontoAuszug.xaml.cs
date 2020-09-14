using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Exceptions;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für KontoAuszug.xaml
    /// </summary>
    public partial class KontoAuszug : Page
    {
        public KontoAuszug()
        {
            InitializeComponent();
        }

        private void GenerateButton_OnClick(object sender, RoutedEventArgs e)
        {
            KontoauszugTextbox.Text = "";
            using (var db = new BankingContext())
            {
                try
                {
                    if (KontoNummerTextbox.Text == "") throw new NoTextException();

                    var kontoList = db.TransaktionsSet.Where(s => s.Empfaenger == KontoNummerTextbox.Text);
                    if (!kontoList.Any())
                        throw new IsEmptyException();
                    foreach (var t in kontoList) KontoauszugTextbox.Text += t.ToString();
                }
                catch (IsEmptyException)
                {
                    MessageBox.Show("Kontonummer Falsch!");
                }
                catch (NoTextException)
                {
                    MessageBox.Show("Es wurde keine Kontonummer eingegeben!");
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
    }
}