using Android.Content.PM;
using Android.Content.Res;
using Android.Graphics;
using Android.Util;
using Android.Views;
using C1.Android.Core;
using C1.Android.Grid;
using C1.DataCollection;
using System.Collections.ObjectModel;
using Orientation = Android.Content.Res.Orientation;

namespace Flights
{
    [Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@style/ApplicationTheme", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.SmallestScreenSize | ConfigChanges.ScreenLayout)]
    public class MainActivity : Activity
    {
        private Random _rand = new Random();
        private FlexGrid _grid;
        private ObservableCollection<Flight> _flights;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            _grid = FindViewById<FlexGrid>(Resource.Id.Grid);

            var border = TypedValue.ApplyDimension(ComplexUnitType.Dip, 6, Resources.DisplayMetrics);
            var cornerRadius = TypedValue.ApplyDimension(ComplexUnitType.Dip, 12, Resources.DisplayMetrics);
            var elevation = TypedValue.ApplyDimension(ComplexUnitType.Dip, 2, Resources.DisplayMetrics);
            _grid.CellFactory = new ListViewCellFactoryStyle(border, cornerRadius, elevation);
            _grid.AutoGeneratingColumn += OnAutoGeneratingColumn;

            _ = Load();
        }

        public override void OnConfigurationChanged(Configuration newConfig)
        {
            base.OnConfigurationChanged(newConfig);
            UpdateOrientation();
        }

        private void UpdateOrientation()
        {
            _grid.Columns[nameof(Flight.Remark)].IsVisible = Resources.Configuration.Orientation.HasFlag(Orientation.Landscape);
            _grid.Refresh();
        }

        private async Task Load()
        {
            _flights = new ObservableCollection<Flight>(Flight.CreateRandomList(100));
            var collection = new C1DataCollection<Flight>(_flights);
            await collection.SortAsync(nameof(Flight.Time));
            _grid.ItemsSource = collection;
            _grid.AllowResizing = GridAllowResizing.None;
            UpdateOrientation();
            SimulateChanges();
        }

        private void OnAutoGeneratingColumn(object sender, GridAutoGeneratingColumnEventArgs e)
        {
            if (e.Property.Name == nameof(Flight.Time))
            {
                e.Column.Format = "HH:mm";
                e.Column.Width = GridLength.Auto;
                e.Column.AllowDragging = false;
            }
            if (e.Property.Name == nameof(Flight.Destination))
            {
                e.Column.Width = GridLength.Star;
            }
            if (e.Property.Name == nameof(Flight.FlightId))
            {
                e.Column.Width = GridLength.Auto;
            }
            if (e.Property.Name == nameof(Flight.Gate))
            {
                e.Column.Width = GridLength.Auto;
                e.Column.HorizontalAlignment = GravityFlags.Center;
                e.Column.HeaderHorizontalAlignment = GravityFlags.Center;
            }
            if (e.Property.Name == nameof(Flight.Remark))
            {
                e.Column.Width = new GridLength(0.5, GridUnitType.Star);
                e.Column.AllowDragging = false;
                e.Column.HorizontalAlignment = GravityFlags.Center;
                e.Column.HeaderHorizontalAlignment = GravityFlags.Center;
            }
        }

        private async void SimulateChanges()
        {
            await Task.Delay(TimeSpan.FromMilliseconds(800));
            GenerateRandomChange();
            SimulateChanges();
        }

        private void GenerateRandomChange()
        {
            switch (_rand.Next(4))
            {
                case 0:
                    _flights.Insert(_rand.Next(_flights.Count + 1), new Flight());
                    break;
                case 1:
                    if (_flights.Count > 0)
                        _flights.RemoveAt(_rand.Next(_flights.Count));
                    break;
                case 2:
                    if (_flights.Count > 0)
                        _flights.Move(_rand.Next(_flights.Count), _rand.Next(_flights.Count));
                    break;
                case 3:
                    if (_flights.Count > 0)
                        _flights[_rand.Next(_flights.Count)] = new Flight();
                    break;
            }
        }
    }

    public class ListViewCellFactoryStyle : GridCellFactory
    {
        double _border, _cornerRadius, _elevation;

        public ListViewCellFactoryStyle(double border, double cornerRadius, double elevation)
        {
            _border = border;
            _cornerRadius = cornerRadius;
            _elevation = elevation;
        }

        //public Style LeftCellStyle { get; set; }
        //public Style CenterCellStyle { get; set; }
        //public Style RightCellStyle { get; set; }

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, C1Thickness internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            //cell.ClearValue(Control.BorderThicknessProperty);
            if (cellType == GridCellType.Cell)
            {
                if (range.Column == 0)
                {
                    cell.CornerRadius = new C1CornerRadius(_cornerRadius, 0, 0, _cornerRadius);
                    cell.BorderThickness = new C1Thickness(_border, _border, 0, _border);
                    //cell.Style = LeftCellStyle ?? CenterCellStyle;
                }
                else if (range.Column == Grid.Columns.Count(c => c.IsVisible) - 1)
                {
                    cell.CornerRadius = new C1CornerRadius(0, _cornerRadius, _cornerRadius, 0);
                    cell.BorderThickness = new C1Thickness(0, _border, _border, _border);
                    //cell.Style = RightCellStyle ?? CenterCellStyle;
                }
                else
                {
                    cell.CornerRadius = new C1CornerRadius(0, 0, 0, 0);
                    cell.BorderThickness = new C1Thickness(0, _border, 0, _border);
                    //cell.Style = CenterCellStyle;
                }
            }
        }

        public override void BindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
        {
            base.BindCellContent(cellType, range, cellContent);
            if (cellType == GridCellType.Cell && cellContent is TextView label)
            {
                label.SetShadowLayer(4, 4, 4, Color.Black);
                if (range.Column == Grid.Columns[nameof(Flight.Remark)].Index)
                {
                    var flight = Grid.Rows[range.Row].DataItem as Flight;
                    switch (flight.Remark)
                    {
                        case "Delayed":
                            label.SetTextColor(Color.Red);
                            break;
                        case "Check-in":
                            label.SetTextColor(Color.Green);
                            break;
                    }
                }
            }
            else if (cellType == GridCellType.ColumnHeader && cellContent is GridColumnHeaderCell columnHeader)
            {
                var linearLayout = columnHeader.GetChildAt(0) as LinearLayout;
                var label2 = linearLayout.GetChildAt(2) as TextView;
                label2.SetShadowLayer((float)_elevation, (float)_elevation, (float)_elevation, Color.Black);
            }
        }
    }
}