using C1.DataCollection.BindingList;
using FlexGridExplorer.Strings;
using System.Data;

namespace FlexGridExplorer
{
    public partial class DataTableSample : ContentPage
    {
        private int count = 7;
        private DataTable _dataTable = new DataTable();
        private Random _random = new Random();

        public DataTableSample()
        {
            InitializeComponent();

            Title = AppResources.DataTableSampleTitle;

            _dataTable.Columns.Add("Int", typeof(int));
            _dataTable.Columns.Add("Double", typeof(double));
            _dataTable.Columns.Add("Float", typeof(float));
            _dataTable.Columns.Add("String", typeof(string));
            _dataTable.Columns.Add("DateTime", typeof(DateTime));
            _dataTable.Columns.Add("Boolean", typeof(bool));

            for (int i = 0; i < count; i++)
            {
                _dataTable.Rows.Add(i, _random.NextDouble() * count, (float)_random.NextDouble() * count, "String " + _random.Next(count), DateTime.Now.AddDays(i - _random.Next(count) / 2), _random.Next(count) % 2 == 0);
                ValidateRow(_dataTable.Rows[i]);
            }

            _dataTable.AcceptChanges();
            grid.ItemsSource = new C1BindingListDataCollection(_dataTable.DefaultView);

            _dataTable.RowChanged += OnRowChanged;
        }

        private void OnRowChanged(object sender, DataRowChangeEventArgs e)
        {
            ValidateRow(e.Row);
        }

        private void ValidateRow(DataRow row)
        {
            if (!(row["Double"] is double doubleValue) || doubleValue < 3)
            {
                var error = "Column Double should be less than 3";
                row.SetColumnError("Double", error);
                row.RowError = error;
            }
            else
            {
                row.ClearErrors();
            }
        }

        private async void OnShowChangesClicked(object sender, EventArgs e)
        {
            var changes = _dataTable.GetChanges();
            if (changes == null)
            {
                await DisplayAlert(AppResources.DataTableSampleTitle, AppResources.NoChangesEditDataTableFirstMessage, AppResources.OK);
            }
            else
            {
                var added = changes.Rows.Cast<DataRow>().Count(r => r.RowState == DataRowState.Added);
                var modified = changes.Rows.Cast<DataRow>().Count(r => r.RowState == DataRowState.Modified);
                var deleted = changes.Rows.Cast<DataRow>().Count(r => r.RowState == DataRowState.Deleted);
                await DisplayAlert(AppResources.DataTableSampleTitle, string.Format(AppResources.ChangesInDataTableMessage, added, modified, deleted), AppResources.OK);
            }
        }
    }
}