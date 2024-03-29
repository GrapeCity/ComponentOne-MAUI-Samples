using Android.App;
using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.OS;
using Android.Views;
using Android.Widget;
using C1.Android.Grid;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/ConditionalFormattingTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class ConditionalFormattingActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.ConditionalFormattingTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);

            grid.AutoGenerateColumns = false;
            grid.Columns.Add(new GridColumn { Binding = "Name" });
            grid.Columns.Add(new GridColumn { Binding = "OrderTotal", Format = "C2" });
            grid.Columns.Add(new GridColumn { Binding = "OrderCount", Format = "N1" });
            grid.Columns.Add(new GridColumn { Binding = "CountryId", Header = "Country" });
            grid.Columns.Add(new GridDateTimeColumn { Binding = "LastOrderDate", Format = "d", Mode = GridDateTimeColumnMode.Date });
            grid.Columns.Add(new GridDateTimeColumn { Binding = "LastOrderDate", Header = "Last Order Time", Format = "t", Mode = GridDateTimeColumnMode.Time });

            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.Columns["CountryId"].DataMap = new GridDataMap() { ItemsSource = Customer.GetCountries(), DisplayMemberPath = "Value", SelectedValuePath = "Key" };

            grid.CellFactory = new MyCellFactory();
        }
        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            if (item.ItemId == global::Android.Resource.Id.Home)
            {
                Finish();
                return true;
            }
            else
            {
                return base.OnOptionsItemSelected(item);
            }
        }
    }

    public class MyCellFactory : GridCellFactory
    {
        private Color Red = Color.Argb(255, 255, 112, 112);
        private Color Green = Color.Argb(255, 142, 233, 142);

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, C1.Android.Core.C1Thickness internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            if (cellType == GridCellType.Cell && range.Column == 2)
            {
                var cellValue = Grid[range.Row, range.Column] as int?;
                if (cellValue.HasValue)
                {
                    cell.Background = new ColorDrawable(cellValue < 50.0 ? Red : Green);
                }
            }
        }

        /// <summary>
        /// Binds the content of the cell.
        /// </summary>
        /// <param name="cellType">Type of the cell.</param>
        /// <param name="range">The range.</param>
        /// <param name="cellContent">Content of the cell.</param>
        public override void BindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
        {
            base.BindCellContent(cellType, range, cellContent);
            if (cellType == GridCellType.Cell && range.Column == 1)
            {
                var label = cellContent as TextView;
                if (label != null)
                {
                    var cellValue = Grid[range.Row, range.Column] as double?;
                    if (cellValue.HasValue)
                    {
                        label.SetTextColor(cellValue < 5000.0 ? Red : Green);
                    }
                }
            }
        }

        public override void UnbindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
        {
            base.UnbindCellContent(cellType, range, cellContent);
            var label = cellContent as TextView;
            if (label != null)
            {
                label.SetBackgroundColor(Color.Transparent);
            }
        }
    }
}