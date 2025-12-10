using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class FilterRow : ContentPage
    {
        public FilterRow()
        {
            InitializeComponent();

            Title = AppResources.FilterRowTitle;

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
        }

    }
}
