using Android.Content.PM;
using Android.Util;
using Android.Views;
using C1.Android.Core;
using C1.Android.Grid;
using C1.DataCollection;

namespace FlexGridExplorer
{
    [Activity(Label = "@string/CustomCellsTitle", ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize)]
    public class CustomCellsActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.GettingStarted);

            ActionBar.Title = GetString(Resource.String.CustomCellsTitle);
            ActionBar.SetDisplayHomeAsUpEnabled(true);
            ActionBar.SetHomeButtonEnabled(true);

            var grid = FindViewById<FlexGrid>(Resource.Id.Grid);

            var metrics = BaseContext.Resources.DisplayMetrics;
            grid.AutoGenerateColumns = false;
            grid.Columns.Add(new GridColumn { Binding = "Name", Width = TypedValue.ApplyDimension(ComplexUnitType.Dip, 350, metrics) });
            grid.Columns.Add(new GridColumn { Binding = "Duration", Width = TypedValue.ApplyDimension(ComplexUnitType.Dip, 100, metrics) });
            grid.Columns.Add(new GridColumn { Binding = "Size", Width = TypedValue.ApplyDimension(ComplexUnitType.Dip, 100, metrics) });
            grid.Columns.Add(new GridColumn { Binding = "Rating", Width = TypedValue.ApplyDimension(ComplexUnitType.Dip, 100, metrics) });

            grid.CellFactory = new MusicCellFactory
            {
                ArtistIcon = C1IconTemplate.AlertCircle,// new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///artist") }),
                AlbumIcon = C1IconTemplate.Asterisk,//new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///album") }),
                SongIcon = C1IconTemplate.Star5,//new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///song") }),
            };

            BindITunesGrid(grid);
        }

        async void BindITunesGrid(FlexGrid grid)
        {
            var songs = await MediaLibrary.LoadAsync();
            var dataCollection = new C1DataCollection<Song>(songs);
            await dataCollection.GroupAsync(nameof(Song.Artist), nameof(Song.Album));
            grid.Columns["Duration"].ValueConverter = new SongDurationConverter();
            grid.Columns["Size"].ValueConverter = new SongSizeConverter();
            grid.ItemsSource = dataCollection;

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
    /// <summary>
    /// Cell factory used to create iTunes cells.
    /// </summary>
    public class MusicCellFactory : GridCellFactory
    {
        public MusicCellFactory()
        {
            AllowCustomCells = true;
        }

        public C1IconTemplate ArtistIcon { get; set; }
        public C1IconTemplate AlbumIcon { get; set; }
        public C1IconTemplate SongIcon { get; set; }

        public override object GetCellKind(GridCellType cellType, GridCellRange range)
        {
            var cellKind = base.GetCellKind(cellType, range);
            if (cellType == GridCellType.Cell)
            {
                var row = Grid.Rows[range.Row];
                var col = Grid.Columns[range.Column];

                if (row is GridGroupRow groupRow && range.Column == 0)
                {
                    return groupRow.Level == 0 ? (ArtistIcon, cellKind) : (AlbumIcon, cellKind);
                }

                var colName = col.ColumnName;
                if (colName == "Name")
                {
                    return (SongIcon, cellKind);
                }
            }
            return cellKind;
        }

        public override GridCellView CreateCell(GridCellType cellType, GridCellRange range, object cellKind)
        {
            if (cellKind is ValueTuple<C1IconTemplate, object> tuple)
            {
                var cell = base.CreateCell(cellType, range, tuple.Item2);
                cell.IconTemplate = tuple.Item1;
                return cell;
            }
            else
            {
                return base.CreateCell(cellType, range, cellKind);
            }
        }

        //public override object GetCellContentType(GridCellType cellType, GridCellRange range)
        //{
        //    if (cellType == GridCellType.Cell)
        //    {
        //        var col = Grid.Columns[range.Column];
        //        var colName = col.ColumnName;
        //        if (colName == "Rating")
        //        {
        //            return typeof(RatingCell);
        //        }
        //    }
        //    return base.GetCellContentType(cellType, range);
        //}

        //public override FrameworkElement CreateCellContent(GridCellType cellType, GridCellRange range, object cellContentType)
        //{
        //    if (cellContentType as Type == typeof(RatingCell))
        //        return new RatingCell();
        //    return base.CreateCellContent(cellType, range, cellContentType);
        //}

        //public override void BindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        //{
        //    if (cellContent is RatingCell ratingCell)
        //    {
        //        var col = Grid.Columns[range.Column];
        //        var row = Grid.Rows[range.Row];
        //        var cellValue = Grid.GetCellValue(range);
        //        ratingCell.Rating = Convert.ToInt32(cellValue);
        //        ratingCell.Range = range;
        //        if (row.DataItem is Song)
        //            ratingCell.PropertyChanged += RatingCellOnPropertyChanged;
        //    }
        //    else
        //    {
        //        base.BindCellContent(cellType, range, cellContent);
        //    }
        //}

        //public override void UnbindCellContent(GridCellType cellType, GridCellRange range, FrameworkElement cellContent)
        //{
        //    if (cellContent is RatingCell ratingCell)
        //    {
        //        var row = Grid.Rows[range.Row];
        //        if (row.DataItem is Song)
        //            ratingCell.PropertyChanged -= RatingCellOnPropertyChanged;
        //    }
        //    base.UnbindCellContent(cellType, range, cellContent);
        //}

        //private void RatingCellOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        //{
        //    if (e.PropertyName == "Rating")
        //    {
        //        var ratingCell = sender as RatingCell;

        //        if (ratingCell != null)
        //        {
        //            Grid.SetCellValue(ratingCell.Range, ratingCell.Rating);
        //            var task = Grid.StartEditingAsync(ratingCell.Range.Row, ratingCell.Range.Column);
        //            var res = task.Status == TaskStatus.RanToCompletion && task.Result;
        //            if (!res)
        //                return;
        //        }

        //        Grid.FinishEditing();
        //    }
        //}
    }

    class SongDurationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var ts = TimeSpan.FromMilliseconds((long)value);
            return ts.Hours == 0
                ? string.Format("{0:00}:{1:00}", ts.Minutes, ts.Seconds)
                : string.Format("{0:00}:{1:00}:{2:00}", ts.Hours, ts.Minutes, ts.Seconds);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    // converter for song sizes (return x.xx MB)
    class SongSizeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return string.Format("{0:n2} MB", (long)value / 1024.0 / 1024.0);
        }
        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

}