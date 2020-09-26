using System.Windows;
using KontoVerwaltungV4.Pages;
using MahApps.Metro.Controls;

namespace KontoVerwaltungV4
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void NewKundeButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new NewKunde();
        }

        private void NewKontoButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new NewKonto();
        }

        private void KontoAuszugButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new KontoAuszug();
        }

        private void EinzahlungButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new Einzahlung();
        }

        private void AuszahlungButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new Auszahlung();
        }

        private void KontoLöschenButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new DeleteKonto();
        }

        private void ShowAllKontosButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new AllKontos();
        }

        private void ExitButton_OnClick(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void UeberweisungButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new Ueberweisung();
        }

        private void DevTestingButton_OnClick(object sender, RoutedEventArgs e)
        {
        }

        private void ZinsberechnungButton_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new ZinsBerechnungPage();
        }

        private void DevSettingsMenuItem_OnClick(object sender, RoutedEventArgs e)
        {
            PageView.Content = new DevOptionPage();
        }

        private void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            PageView.Content = new WelcomePage();
        }
    }
}