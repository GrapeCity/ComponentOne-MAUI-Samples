using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class StarSizing : ContentPage
    {
        public StarSizing()
        {
            InitializeComponent();

            Title = AppResources.StarSizingTitle;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
        }
    }
}
