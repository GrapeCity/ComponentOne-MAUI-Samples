using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System;

namespace FlexGridExplorer
{
    public partial class SelectionModes : ContentPage
    {
        public SelectionModes()
        {
            InitializeComponent();

            Title = AppResources.SelectionModesTitle;
            selectionMode.Title = AppResources.SelectionModesTitle;
            lblMarquee.Text = AppResources.ShowMarquee;

            foreach (var value in Enum.GetValues(typeof(GridSelectionMode)))
            {
                selectionMode.Items.Add(value.ToString());
            }
            selectionMode.SelectedIndex = selectionMode.Items.IndexOf(GridSelectionMode.CellRange.ToString());

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.MinColumnWidth = 85;
        }

        private void SelectionModeChanged(object sender, EventArgs e)
        {
            grid.SelectionMode = (GridSelectionMode)Enum.Parse(typeof(GridSelectionMode), selectionMode.Items[selectionMode.SelectedIndex]);
        }

        private void OnSelectionChanging(object sender, GridCellRangeEventArgs e)
        {
            //e.Cancel = true;
        }

        private void OnSelectionChanged(object sender, GridCellRangeEventArgs e)
        {
            if (e.CellRange != null && e.CellRange.Row != -1)
            {
                int rowsSelected = Math.Abs(e.CellRange.Row2 - e.CellRange.Row) + 1;
                int colsSelected = Math.Abs(e.CellRange.Column2 - e.CellRange.Column) + 1;
                
                lblSelection.Text = (rowsSelected * colsSelected).ToString() + " "+AppResources.CellsSelectedText;
            }
        }
    }
}
