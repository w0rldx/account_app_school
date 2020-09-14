using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KontoVerwaltungV4.Database;
using KontoVerwaltungV4.Konto;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für AllKontos.xaml
    /// </summary>
    public partial class ZinsBerechnungPage : Page
    {
        public ZinsBerechnungPage()
        {
            InitializeComponent();
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                if (GiroItem.IsSelected)
                {
                    var objects = db.KontoSet.ToList();
                    var list = objects.Where(b => b.GetType() == typeof(GiroKonto))
                        .ToList(); //Muss gemacht werden da EFC aktuell kein durchsuchen nach Type erlaubt
                    DataGrid.ItemsSource = list;
                }
                else if (FestgeldItem.IsSelected)
                {
                    var objects = db.KontoSet.ToList();
                    var list = objects.Where(b => b.GetType() == typeof(FestgeldKonto))
                        .ToList(); //Muss gemacht werden da EFC aktuell kein durchsuchen nach Type erlaubt
                    DataGrid.ItemsSource = list;
                }
                else if (TagesgeldItem.IsSelected)
                {
                    var objects = db.KontoSet.ToList();
                    var list = objects.Where(b => b.GetType() == typeof(TagesgeldKonto))
                        .ToList(); //Muss gemacht werden da EFC aktuell kein durchsuchen nach Type erlaubt
                    DataGrid.ItemsSource = list;
                }
                else if (SpargeldItem.IsSelected)
                {
                    var objects = db.KontoSet.ToList();
                    var list = objects.Where(b => b.GetType() == typeof(SparKonto))
                        .ToList(); //Muss gemacht werden da EFC aktuell kein durchsuchen nach Type erlaubt
                    DataGrid.ItemsSource = list;
                }

                db.Dispose();
            }
        }

        private void AllKontos_OnLoaded(object sender, RoutedEventArgs e)
        {
        }

        //TODO:Savebutton beheben
        private void SaveButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                db.SaveChanges();
                db.Dispose();
            }
        }

        private void ZinsBerechnenButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                try
                {
                    var selectedItem = (Konto.Konto) DataGrid.SelectedItem;
                    var zins = selectedItem.BerechneZins();

                    var result = MessageBox.Show($"Der Zinssatz Beträgt {zins} €, möchten sie ihn anwenden?", "Löschen",
                        MessageBoxButton.YesNo);

                    switch (result)
                    {
                        case MessageBoxResult.Yes:
                            selectedItem.VerrechneZins(zins);
                            var konto = db.KontoSet.Where(s => s == selectedItem).ToList();
                            foreach (var k in konto) k.VerrechneZins(zins);

                            db.SaveChanges();
                            MessageBox.Show("Angewendet!!");
                            break;
                        case MessageBoxResult.No:
                            MessageBox.Show("Abgebrochen!!");
                            break;
                    }
                }
                catch (NullReferenceException exception)
                {
                    MessageBox.Show("Es wurde keine Item ausgewählt!");
                }
                finally
                {
                    db.Dispose();
                }
            }
        }
    }
}