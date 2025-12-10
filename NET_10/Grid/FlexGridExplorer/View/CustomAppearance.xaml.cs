using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Graphics;

namespace FlexGridExplorer
{
    public partial class CustomAppearance : ContentPage
    {
        public CustomAppearance()
        {
            InitializeComponent();
            Title = AppResources.CustomAppearanceTitle;

            PopulateEditGrid();
        }

        private void PopulateEditGrid()
        {
            // create the data
            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;

            // provide auto-complete lists for first and last name columns
            var col = grid.Columns["FirstName"];
            col.DataMap = new GridDataMap() { ItemsSource = Customer.GetFirstNames() };
            col = grid.Columns["LastName"];
            col.DataMap = new GridDataMap() { ItemsSource = Customer.GetLastNames() };

            
            col = grid.Columns["Email"];
            col.InputType = Keyboard.Email;

            // make read-only columns look disabled
            var readOnlyBrush = new SolidColorBrush(Color.FromRgba(0xe0, 0xe0, 0xe0, 0xe0));
            foreach (var c in grid.Columns)
            {
                if (c.PropertyInfo != null && !c.PropertyInfo.CanWrite)
                {
                    c.Background = readOnlyBrush;
                }
            }

            grid.Columns.Move(grid.Columns["FirstName"].Index, 1);
        }
    }
}
