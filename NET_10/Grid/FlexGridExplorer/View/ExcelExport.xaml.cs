using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using GrapeCity.Documents.Excel;

namespace FlexGridExplorer
{
    public partial class ExcelExport : ContentPage
    {
        private string FILENAME = "ExportedGrid";
        public ExcelExport()
        {
            InitializeComponent();
            Title = AppResources.ExportTitle;
            btnSave.Text = AppResources.Save;
            grid.ItemsSource = Customer.GetCustomerList(100);
            grid.MinColumnWidth = 85;
            grid.AllowDragging = GridAllowDragging.Both;
            grid.AllowResizing = GridAllowResizing.Both;
        }
        private void OnAutoGeneratingColumn(object sender, GridAutoGeneratingColumnEventArgs e)
        {
            if (e.Property.Name == "Country" || e.Property.Name == "Name" || e.Property.Name == "OrderAverage")
            {
                //Avoids generating these columns
                e.Cancel = true;
            }
            else if (e.Property.Name == "Id")
            {
                e.Column.IsReadOnly = true;
            }
            else if (e.Property.Name == "CountryId")
            {
                //Sets the DataMap to the country column so a picker is used to select the country.
                e.Column.Header = "Country";
                e.Column.HorizontalAlignment = LayoutAlignment.Start;
                e.Column.DataMap = new GridDataMap() { ItemsSource = Customer.GetCountries(), DisplayMemberPath = "Value", SelectedValuePath = "Key" };
            }
            else if (e.Property.Name == "OrderTotal" || e.Property.Name == "OrderAverage")
            {
                //Sets currency format the these columns
                e.Column.Format = "C";
            }
            else if (e.Property.Name == "LastOrderDate")
            {
                //Replaces the column so that the editor is a date-picker instead of an entry.
                e.Column = new GridDateTimeColumn(e.Property);
            }
            else if (e.Property.Name == "Address")
            {
                e.Column.WordWrap = true;
            }
        }
        async void OnSave(object sender, EventArgs e)
        {
            var type = await DisplayActionSheet("Save As", "Cancel", null, "Excel");

            string filePath = "";
            switch (type)
            {
                case "Excel":
#if WINDOWS
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), FILENAME) + ".xlsx";
#else
                    filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), FILENAME) + ".xlsx";
#endif
                    try
                    {
                        await grid.SaveAsync(filePath, "FlexGrid Sheet", SaveFileFormat.Xlsx, headers: GridHeadersVisibility.Column);
#if WINDOWS
                        System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo { FileName = filePath, UseShellExecute = true });
#else
                        await DisplayAlert("Saved", "File has been saved to: " + filePath, "OK");
#endif
                    }
                    catch
                    {
                        await DisplayAlert("Error", "File has could not be saved", "OK");
                    }
                    break;
            }
        }
    }
}