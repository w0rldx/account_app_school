using System;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Exceptions;
using KontoVerwaltungV4.Konto;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für NewKonto.xaml
    /// </summary>
    public partial class NewKonto : Page
    {
        private readonly BankingContext _context = new BankingContext();

        public NewKonto()
        {
            InitializeComponent();
        }

        ~NewKonto() // finalizer
        {
            _context.Dispose();
        }

        /// <summary>
        ///     Textboxevent um nur Zahlen zu erlauben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTextBoxInt(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        /// <summary>
        ///     Textboxevent um nur Zahlen zu erlauben
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckTextBoxDouble(object sender, TextCompositionEventArgs e)
        {
            var regex = new Regex("[^0-9.]+");
            e.Handled = regex.IsMatch(e.Text);
        }


        private void AddButton_OnClick(object sender, RoutedEventArgs e)
        {
            if (GiroItem.IsSelected)
                CreateGiroKonto();
            else if (TagesgeldItem.IsSelected)
                CreateTagesgeldKonto();
            else if (FestgeldItem.IsSelected)
                CreateFestgeldKonto();
            else if (SpargeldItem.IsSelected) CreateSparKonto();
        }

        private void CreateGiroKonto()
        {
            try
            {
                if (PinNummerTexbox.Password == "" || StartbetragTexbox.Text == "" || ZinssSatzTexbox.Text == "")
                    throw new NoTextException();

                var kunde = DataGrid.SelectedItem as Kunde.Kunde;
                if (kunde == null) throw new IsEmptyException();
                var betrag = Convert.ToDouble(StartbetragTexbox.Text);
                var zins = Convert.ToDouble(ZinssSatzTexbox.Text);
                var konto = new GiroKonto(KontoNummerTextBox.Text, PinNummerTexbox.Password, betrag, zins, kunde);
                _context.Add(konto);
                _context.SaveChanges();
                MessageBox.Show("Girokonto angelegt!");
                ResetForms();
            }
            catch (NoTextException)
            {
                MessageBox.Show("Es wurde nicht alle Angaben ausgefüllt!");
            }
            catch (IsEmptyException)
            {
                MessageBox.Show("Es wurde kein Kunden ausgewählt!");
            }
        }

        private void CreateTagesgeldKonto()
        {
            try
            {
                if (PinNummerTexbox.Password == "" || StartbetragTexbox.Text == "" || ZinssSatzTexbox.Text == "")
                    throw new NoTextException();

                var kunde = DataGrid.SelectedItem as Kunde.Kunde;
                if (kunde == null) throw new IsEmptyException();
                var betrag = Convert.ToDouble(StartbetragTexbox.Text);
                var zins = Convert.ToDouble(ZinssSatzTexbox.Text);
                var konto = new TagesgeldKonto(KontoNummerTextBox.Text, PinNummerTexbox.Password, betrag, zins, kunde);
                _context.Add(konto);
                _context.SaveChanges();
                MessageBox.Show("TagesgeldKonto angelegt!");
                ResetForms();
            }
            catch (NoTextException)
            {
                MessageBox.Show("Es wurde nicht alle Angaben ausgefüllt!");
            }
            catch (IsEmptyException)
            {
                MessageBox.Show("Es wurde kein Kunden ausgewählt!");
            }
        }

        private void CreateFestgeldKonto()
        {
            try
            {
                if (PinNummerTexbox.Password == "" || StartbetragTexbox.Text == "" || ZinssSatzTexbox.Text == "")
                    throw new NoTextException();

                var kunde = DataGrid.SelectedItem as Kunde.Kunde;
                if (kunde == null) throw new IsEmptyException();
                var betrag = Convert.ToDouble(StartbetragTexbox.Text);
                var zins = Convert.ToDouble(ZinssSatzTexbox.Text);
                var konto = new FestgeldKonto(KontoNummerTextBox.Text, PinNummerTexbox.Password, betrag, zins, kunde);
                _context.Add(konto);
                _context.SaveChanges();
                MessageBox.Show("FestgeldKonto angelegt!");
                ResetForms();
            }
            catch (NoTextException)
            {
                MessageBox.Show("Es wurde nicht alle Angaben ausgefüllt!");
            }
            catch (IsEmptyException)
            {
                MessageBox.Show("Es wurde kein Kunden ausgewählt!");
            }
        }

        private void CreateSparKonto()
        {
            try
            {
                if (PinNummerTexbox.Password == "" || StartbetragTexbox.Text == "" || ZinssSatzTexbox.Text == "")
                    throw new NoTextException();

                var kunde = DataGrid.SelectedItem as Kunde.Kunde;
                if (kunde == null) throw new IsEmptyException();
                var betrag = Convert.ToDouble(StartbetragTexbox.Text);
                var zins = Convert.ToDouble(ZinssSatzTexbox.Text);
                var konto = new SparKonto(KontoNummerTextBox.Text, PinNummerTexbox.Password, betrag, zins, kunde);
                _context.Add(konto);
                _context.SaveChanges();
                MessageBox.Show("SparKonto angelegt!");
                ResetForms();
            }
            catch (NoTextException)
            {
                MessageBox.Show("Es wurde nicht alle Angaben ausgefüllt!");
            }
            catch (IsEmptyException)
            {
                MessageBox.Show("Es wurde kein Kunden ausgewählt!");
            }
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            var objects = _context.KundenSet.ToList();
            DataGrid.ItemsSource = objects;
        }

        protected string GenerateKontoNummer()
        {
            var random = new Random();
            var num = "";
            int i;
            bool isEmpty;
            do
            {
                for (i = 1; i < 11; i++) num += random.Next(0, 9).ToString();
                var kontonummers = _context.KontoSet.Where(n => n.KontoNummer.Contains(num)).ToList();
                isEmpty = !kontonummers.Any();
            } while (!isEmpty);

            return num;
        }

        private void NewKonto_OnLoaded(object sender, RoutedEventArgs e)
        {
            KontoNummerTextBox.Text = GenerateKontoNummer(); //TODO:Auslagerung in anderen Thread bzw Backgroundworker
            var objects = _context.KundenSet.ToList();
            DataGrid.ItemsSource = objects;
        }

        private void ResetForms()
        {
            KontoNummerTextBox.Text = GenerateKontoNummer();//TODO:Auslagerung in anderen Thread bzw Backgroundworker
            PinNummerTexbox.Password = "";
            StartbetragTexbox.Text = "";
            ZinssSatzTexbox.Text = "";
            DataGrid.SelectedItem = null;
        }

        private void BackgroundWorker_RunworkerComplette(object sender, RunWorkerCompletedEventArgs e)
        {
            
        }
    }
}