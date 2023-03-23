using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System;

namespace FlexGridExplorer
{
    public partial class Editing : ContentPage
    {
        GridCellRange selectedRange;
        public Editing()
        {
            InitializeComponent();

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.SelectionChanged += grid_SelectionChanged;
            grid.CellDoubleTapped += OnCellDoubleTapped;
            grid.IsReadOnly = true;
            grid.MinColumnWidth = 85;

            //toolbarItemEdit.Text = AppResources.EditRow;

            Title = AppResources.EditingTitle;
        }

        void grid_SelectionChanged(object sender, GridCellRangeEventArgs e)
        {
            selectedRange = e.CellRange;
        }

        async void OnCellDoubleTapped(object sender, GridCellRangeEventArgs e)
        {
            if(e.CellType == GridCellType.Cell)
            {
                Customer c = grid.Rows[e.CellRange.Row].DataItem as Customer;
                if (c != null)
                    await Navigation.PushModalAsync(new EditCustomerForm(c));
            }
        }

        private async void OnEditButtonClicked(object sender, EventArgs e)
        {
            if (selectedRange != null)
            {
                Customer c = grid.Rows[selectedRange.Row].DataItem as Customer;
                if(c != null)
                    await Navigation.PushModalAsync(new EditCustomerForm(c));
            }
            else
            {
                await DisplayAlert("", AppResources.SelectRowMessage, AppResources.OK);
            }
        }

    }
}
