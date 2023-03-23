using FlexGridExplorer.Strings;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class CheckList : ContentPage
    {
        public CheckList ()
        {
            InitializeComponent ();
            Title = AppResources.CheckListTitle;
            grid.ItemsSource = Customer.GetCities();
        }

        private void OnAutoGeneratingColumn(object sender, C1.Maui.Grid.GridAutoGeneratingColumnEventArgs e)
        {
            e.Column.Width = GridLength.Star;
        }
    }
}