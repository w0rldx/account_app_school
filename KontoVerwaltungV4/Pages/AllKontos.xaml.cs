using System.Linq;
using System.Windows;
using System.Windows.Controls;
using KontoVerwaltungV4.Database;

namespace KontoVerwaltungV4.Pages
{
    /// <summary>
    ///     Interaktionslogik für AllKontos.xaml
    /// </summary>
    public partial class AllKontos : Page
    {
        public AllKontos()
        {
            InitializeComponent();
        }

        private void UpdateButton_OnClick(object sender, RoutedEventArgs e)
        {
            using (var db = new BankingContext())
            {
                if (GiroItem.IsSelected)
                {
                    var listing = db.GiroKontoSet.ToList();
                    DataGrid.ItemsSource = listing;
                }
                else if (FestgeldItem.IsSelected)
                {
                    var listing = db.FestgeldKontoSet.ToList();
                    DataGrid.ItemsSource = listing;
                }
                else if (TagesgeldItem.IsSelected)
                {
                    var listing = db.TagesgeldKontoSet.ToList();
                    DataGrid.ItemsSource = listing;
                }
                else if (SpargeldItem.IsSelected)
                {
                    var listing = db.SparKontoSet.ToList();
                    DataGrid.ItemsSource = listing;
                }

                db.Dispose();
            }
        }
    }
}