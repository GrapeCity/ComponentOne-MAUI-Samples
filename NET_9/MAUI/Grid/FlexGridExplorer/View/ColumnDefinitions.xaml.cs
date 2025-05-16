using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;

namespace FlexGridExplorer
{
    public partial class ColumnDefinitions : ContentPage
    {
        public ColumnDefinitions()
        {
            InitializeComponent();

            Title = AppResources.ColumnDefinitionTitle;
            var data = Customer.GetCustomerList(1000);
            grid.ItemsSource = data;
            grid.Columns.RemoveAt(1);
            grid.Columns["CountryID"].DataMap = new GridDataMap() { ItemsSource = Customer.GetCountries(), DisplayMemberPath = "Value", SelectedValuePath = "Key" };
            grid.MinColumnWidth = 85;
        }
    }
}
