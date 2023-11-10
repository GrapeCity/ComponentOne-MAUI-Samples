using FlexGridExplorer.Strings;
using C1.DataCollection;
using C1.Maui.Grid;
using System.ComponentModel;

namespace FlexGridExplorer
{
    public partial class CustomCells : ContentPage
    {
        public CustomCells()
        {
            InitializeComponent();

            _flexiTunes.CellFactory = new MusicCellFactory();
            _flexiTunes.Columns["Duration"].ValueConverter = new SongDurationConverter();
            _flexiTunes.Columns["Size"].ValueConverter = new SongSizeConverter();

            _ = BindITunesGrid();
        }

        async Task BindITunesGrid()
        {
            var songs = await MediaLibrary.Load();
            var view = new C1DataCollection<Song>(songs);
            await view.GroupAsync(nameof(Song.Artist), nameof(Song.Album));
            _flexiTunes.ItemsSource = view;

        }

        // converter for artist/album groups
        public class GroupHeaderConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                // return group name only (no counts)
                var group = value as IDataCollectionGroup<object, object>;
                if (group != null && targetType == typeof(string))
                {
                    return group.Group;
                }

                // default
                return value;
            }
            public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }

        // converter for song durations (stored in milliseconds)
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
        // cell factory used to create iTunes cells
        public class MusicCellFactory : GridCellFactory
        {
            public override object GetCellContentType(GridCellType cellType, GridCellRange range)
            {
                if (cellType == GridCellType.Cell)
                {
                    var row = Grid.Rows[range.Row];
                    var col = Grid.Columns[range.Column];

                    if (row is GridGroupRow groupRow && range.Column == 0)
                    {
                        return typeof(NodeCell);
                    }

                    var colName = col.ColumnName;
                    if (colName == "Name")
                    {
                        return typeof(ImageCell);
                    }
                    if (colName == "Rating")
                    {
                        return typeof(RatingCell);
                    }
                }
                return base.GetCellContentType(cellType, range);
            }

            public override View CreateCellContent(GridCellType cellType, GridCellRange range, object cellContentType)
            {
                if (cellContentType as Type == typeof(NodeCell))
                    return new NodeCell();
                else if (cellContentType as Type == typeof(ImageCell))
                    return new ImageCell();
                else if (cellContentType as Type == typeof(RatingCell))
                    return new RatingCell();
                return base.CreateCellContent(cellType, range, cellContentType);
            }

            public override void BindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
            {
                if (cellContent is ImageCell imageCell)
                {
                    var col = Grid.Columns[range.Column];
                    imageCell.Text = Grid.GetCellText(range);
                    if (col.ColumnName == "Name")
                    {
                        imageCell.ImageSource = ImageSource.FromFile("song.png");
                    }
                }
                if (cellContent is NodeCell nodeCell)
                {
                    var groupRow = Grid.Rows[range.Row] as GridGroupRow;
                    nodeCell.Text = Grid.GetCellText(range);
                    nodeCell.ImageSource = groupRow.Level == 0 ? ImageSource.FromFile("artist.png") : ImageSource.FromFile("album.png");
                    nodeCell.Tag = groupRow;
                    nodeCell.IsCollapsed = groupRow?.IsCollapsed ?? false;
                    nodeCell.IsCollapsedChanged += OnIsCollapsedChanged;
                }
                else if (cellContent is RatingCell ratingCell)
                {
                    var col = Grid.Columns[range.Column];
                    var row = Grid.Rows[range.Row];
                    var cellValue = Grid.GetCellValue(range);
                    ratingCell.Rating = Convert.ToInt32(cellValue);
                    ratingCell.Range = range;
                    if (row.DataItem is Song)
                        ratingCell.PropertyChanged += RatingCellOnPropertyChanged;
                }
                else
                {
                    base.BindCellContent(cellType, range, cellContent);
                }
            }

            private void RatingCellOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "Rating")
                {
                    var ratingCell = sender as RatingCell;

                    if (ratingCell != null)
                    {
                        Grid.SetCellValue(ratingCell.Range, ratingCell.Rating);
                        var task = Grid.StartEditingAsync(ratingCell.Range.Row, ratingCell.Range.Column);
                        var res = task.Status == TaskStatus.RanToCompletion && task.Result;
                        if (!res)
                            return;
                    }

                    Grid.FinishEditing();
                }
            }

            public override void UnbindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
            {
                if (cellContent is NodeCell nodeCell)
                {
                    nodeCell.IsCollapsedChanged -= OnIsCollapsedChanged;
                }
                else
                {
                    if (cellContent is RatingCell ratingCell)
                    {
                        var row = Grid.Rows[range.Row];
                        if (row.DataItem is Song)
                            ratingCell.PropertyChanged -= RatingCellOnPropertyChanged;
                    }
                    base.UnbindCellContent(cellType, range, cellContent);
                }
            }
            private void OnIsCollapsedChanged(object sender, EventArgs e)
            {
                var nodeCell = sender as NodeCell;
                var groupRow = nodeCell.Tag as GridGroupRow;
                groupRow.IsCollapsed = nodeCell.IsCollapsed;
            }
        }
    }
}
