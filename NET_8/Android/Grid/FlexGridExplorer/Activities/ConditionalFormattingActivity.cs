using Android.Content.PM;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Views;
using C1.Android.Core;
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
            grid.Columns.Add(new GridColumn { Binding = "FirstName" });
            grid.Columns.Add(new GridColumn { Binding = "LastName" });
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
        static Color LightRedForeground = Color.Argb(0xFF, 0xC4, 0x2B, 0x1C);
        static Color LightRedBackground = Color.Argb(0xFF, 0xFD, 0xE7, 0xE9);
        static Color LightGreenForeground = Color.Argb(0xFF, 0x0F, 0x7B, 0x0F);
        static Color LightGreenBackground = Color.Argb(0xFF, 0xDF, 0xF6, 0xDD);
        static Color DarkRedForeground = Color.Argb(0xFF, 0xFF, 0x99, 0xA4);
        static Color DarkRedBackground = Color.Argb(0xFF, 0x44, 0x27, 0x26);
        static Color DarkGreenForeground = Color.Argb(0xFF, 0x6C, 0xCB, 0x5F);
        static Color DarkGreenBackground = Color.Argb(0xFF, 0x39, 0x3D, 0x1B);

        public Color RedForeground
        {
            get
            {
                return !C1ThemeInfo.ForContext(Grid.Context).IsDark ? LightRedForeground : DarkRedForeground;
            }
        }

        public Color RedBackground
        {
            get
            {
                return !C1ThemeInfo.ForContext(Grid.Context).IsDark ? LightRedBackground : DarkRedBackground;
            }
        }

        public Color GreenForeground
        {
            get
            {
                return !C1ThemeInfo.ForContext(Grid.Context).IsDark ? LightGreenForeground : DarkGreenForeground;
            }
        }

        public Color GreenBackground
        {
            get
            {
                return !C1ThemeInfo.ForContext(Grid.Context).IsDark ? LightGreenBackground : DarkGreenBackground;
            }
        }

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, C1.Android.Core.C1Thickness internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            var orderCountColumn = Grid.Columns["OrderCount"];
            if (cellType == GridCellType.Cell && range.Column == orderCountColumn.Index)
            {
                var cellValue = Grid[range.Row, range.Column] as int?;
                if (cellValue.HasValue)
                {
                    cell.Background = new ColorDrawable(cellValue < 50.0 ? RedBackground : GreenBackground);
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
            var orderTotalColumn = Grid.Columns["OrderTotal"];
            if (cellType == GridCellType.Cell && range.Column == orderTotalColumn.Index)
            {
                var label = cellContent as TextView;
                if (label != null)
                {
                    var cellValue = Grid[range.Row, range.Column] as double?;
                    if (cellValue.HasValue)
                    {
                        label.SetTextColor(cellValue < 5000.0 ? RedForeground : GreenForeground);
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