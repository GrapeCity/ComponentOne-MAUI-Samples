using FlexGridExplorer.Strings;
using Microsoft.Maui;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class TransposedGrid : ContentPage
    {
        public TransposedGrid()
        {
            InitializeComponent();

            Title = AppResources.TransposedGridTitle;
            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
        }
    }
}
