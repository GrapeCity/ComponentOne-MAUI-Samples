using C1.DataCollection;
using C1.iOS.Core;
using C1.iOS.Grid;
using CoreAnimation;
using Foundation;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UIKit;

namespace FlexGridExplorer
{
    [Register("AppDelegate")]
    public class AppDelegate : UIApplicationDelegate
    {
        public override UIWindow Window
        {
            get;
            set;
        }

        public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
        {
            // create a new window instance based on the screen size
            Window = new UIWindow(UIScreen.MainScreen.Bounds);

            var vc = new UIViewController();
            var grid = new FlexGrid();
            //grid.Rows.Add(new GridFilterRow() { AutoComplete = true, Placeholder = "Enter text to filer" });
            //grid.ItemsSource = Customer.GetCustomerList(100);

            //#region Create tasks
            //var task1 = new ProjectTask() { WBS = "1", Name = "Requirements", Duration = new TimeSpan(50, 0, 0, 0), Start = new DateTime(2009, 12, 4) };
            //var task11 = new ProjectTask() { WBS = "1.1", Name = "Analysis", Duration = new TimeSpan(38, 3, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task1 };
            //var task111 = new ProjectTask() { WBS = "1.1.1", Name = "Analyze online reservations", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task11 };
            //var task112 = new ProjectTask() { WBS = "1.1.2", Name = "Analyze query processes", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2009, 12, 4), ParentTask = task11 };
            //var task113 = new ProjectTask() { WBS = "1.1.3", Name = "Analyze multimedia enhancements", Duration = new TimeSpan(12, 12, 0, 0), Start = new DateTime(2010, 1, 4), ParentTask = task11 };
            //var task114 = new ProjectTask() { WBS = "1.1.4", Name = "Draft Preliminary requirements", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            //var task115 = new ProjectTask() { WBS = "1.1.5", Name = "Review preliminary requirements", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            //var task116 = new ProjectTask() { WBS = "1.1.6", Name = "Incorporate feedback on requirements", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 1, 14), ParentTask = task11 };
            //var task117 = new ProjectTask() { WBS = "1.1.7", Name = "Obtain approval to proceed", Duration = new TimeSpan(3, 2, 0, 0), Start = new DateTime(2010, 2, 6), ParentTask = task11 };
            //task11.SubTasks = new List<ProjectTask> { task111, task112, task113, task114, task115, task116, task117 };
            //var task12 = new ProjectTask() { WBS = "1.2", Name = "Acceptance Test Plan", Duration = new TimeSpan(12, 3, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task1 };
            //var task121 = new ProjectTask() { WBS = "1.2.1", Name = "Write acceptance test plans", Duration = new TimeSpan(5, 2, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task12 };
            //var task122 = new ProjectTask() { WBS = "1.2.2", Name = "Draft acceptance test plan", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 6, 23), ParentTask = task12 };
            //var task123 = new ProjectTask() { WBS = "1.2.3", Name = "Review acceptance test plan", Duration = new TimeSpan(5, 6, 0, 0), Start = new DateTime(2010, 7, 4), ParentTask = task12 };
            //var task124 = new ProjectTask() { WBS = "1.2.4", Name = "Incorporate feedback on acceptance", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 7, 4), ParentTask = task12 };
            //task12.SubTasks = new List<ProjectTask> { task121, task122, task123, task124 };
            //task1.SubTasks = new List<ProjectTask> { task11, task12 };
            //var task2 = new ProjectTask() { WBS = "2", Name = "Design", Duration = new TimeSpan(55, 0, 0, 0), Start = new DateTime(2010, 8, 12) };
            //var task21 = new ProjectTask() { WBS = "2.1", Name = "Top-level Design", Duration = new TimeSpan(27, 12, 0, 0), Start = new DateTime(2010, 8, 12), ParentTask = task2 };
            //var task211 = new ProjectTask() { WBS = "2.1.1", Name = "Design online reservations", Duration = new TimeSpan(10, 0, 0, 0), Start = new DateTime(2010, 9, 7), ParentTask = task21 };
            //var task212 = new ProjectTask() { WBS = "2.1.2", Name = "Design query processes", Duration = new TimeSpan(10, 12, 0, 0), Start = new DateTime(2010, 9, 14), ParentTask = task21 };
            //var task213 = new ProjectTask() { WBS = "2.1.3", Name = "Design multimedia enhancements", Duration = new TimeSpan(10, 6, 0, 0), Start = new DateTime(2010, 10, 4), ParentTask = task21 };
            //var task214 = new ProjectTask() { WBS = "2.1.4", Name = "Review design specification", Duration = new TimeSpan(5, 12, 0, 0), Start = new DateTime(2010, 10, 9), ParentTask = task21 };
            //var task215 = new ProjectTask() { WBS = "2.1.5", Name = "Incorporate feedback into design", Duration = new TimeSpan(2, 2, 0, 0), Start = new DateTime(2010, 10, 9), ParentTask = task21 };
            //var task216 = new ProjectTask() { WBS = "2.1.6", Name = "Top-level design approved", Duration = new TimeSpan(1, 0, 0, 0), Start = new DateTime(2010, 12, 1), ParentTask = task21 };
            //task21.SubTasks = new List<ProjectTask> { task211, task212, task213, task214, task215, task216 };
            //var task22 = new ProjectTask() { WBS = "2.2", Name = "Detailed Design", Duration = new TimeSpan(23, 0, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task2 };
            //var task221 = new ProjectTask() { WBS = "2.2.1", Name = "Draft design specifications", Duration = new TimeSpan(17, 12, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task22 };
            //var task222 = new ProjectTask() { WBS = "2.2.2", Name = "Review design specifications", Duration = new TimeSpan(17, 0, 0, 0), Start = new DateTime(2010, 12, 8), ParentTask = task22 };
            //var task223 = new ProjectTask() { WBS = "2.2.3", Name = "Incorporate feedback on design specifications", Duration = new TimeSpan(17, 6, 0, 0), Start = new DateTime(2010, 12, 14), ParentTask = task22 };
            //var task224 = new ProjectTask() { WBS = "2.2.4", Name = "Obtain approval to proceed", Duration = new TimeSpan(5, 0, 0, 0), Start = new DateTime(2010, 12, 24), ParentTask = task22 };
            //var task225 = new ProjectTask() { WBS = "2.2.5", Name = "Detailed design approved", Duration = new TimeSpan(2, 12, 0, 0), Start = new DateTime(2010, 12, 28), ParentTask = task22 };
            //task22.SubTasks = new List<ProjectTask> { task221, task222, task223, task224, task225 };
            //task2.SubTasks = new List<ProjectTask> { task21, task22 };
            //var task3 = new ProjectTask() { WBS = "3", Name = "Code and Unit Test", Duration = new TimeSpan(32, 4, 0, 0), Start = new DateTime(2010, 12, 4) };
            //var task31 = new ProjectTask() { WBS = "3.1", Name = "Assign development staff", Duration = new TimeSpan(2, 3, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task3 };
            //var task32 = new ProjectTask() { WBS = "3.2", Name = "Develop Code", Duration = new TimeSpan(10, 3, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task3 };
            //var task321 = new ProjectTask() { WBS = "3.2.1", Name = "Develop online reservations", Duration = new TimeSpan(10, 2, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            //var task322 = new ProjectTask() { WBS = "3.2.2", Name = "Test online reservations", Duration = new TimeSpan(1, 11, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            //var task323 = new ProjectTask() { WBS = "3.2.3", Name = "Develop query processes", Duration = new TimeSpan(10, 0, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            //var task324 = new ProjectTask() { WBS = "3.2.4", Name = "Test query processes", Duration = new TimeSpan(1, 4, 0, 0), Start = new DateTime(2010, 12, 4), ParentTask = task32 };
            //task32.SubTasks = new List<ProjectTask> { task321, task322, task323, task324 };
            //task3.SubTasks = new List<ProjectTask> { task31, task32 };
            //var tasks = new List<ProjectTask> { task1, task2, task3 };
            //#endregion

            //grid.ChildItemsPath = nameof(ProjectTask.SubTasks);
            //grid.AutoGeneratingColumn += OnAutoGeneratingColumn;
            //grid.TreeColumnIndex = 1;
            //grid.TreeLinesMode = GridTreeLinesMode.Connected;
            //grid.TreeExpandMode = GridTreeExpandMode.Expanded;
            //grid.TreeLinesThickness = 4;
            //grid.TreeLinesBrush = new CALayer { BackgroundColor = UIColor.FromRGBA(0x00, 0x00, 0x00, 0x10).CGColor };
            //grid.TreeLinesRadiusX = 4;
            //grid.TreeLinesRadiusY = 4;
            //grid.GridLinesVisibility = GridLinesVisibility.None;
            //grid.ItemsSource = tasks;


            grid.AutoGenerateColumns = false;
            grid.Columns.Add(new GridColumn { Binding = "Name", Width = 350 });
            grid.Columns.Add(new GridColumn { Binding = "Duration", Width = 100 });
            grid.Columns.Add(new GridColumn { Binding = "Size", Width = 100 });
            grid.Columns.Add(new GridColumn { Binding = "Rating", Width = 100 });
            grid.CellFactory = new MusicCellFactory
            {
                ArtistIcon = C1IconTemplate.AlertCircle,// new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///artist") }),
                AlbumIcon = C1IconTemplate.Asterisk,//new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///album") }),
                SongIcon = C1IconTemplate.Star5,//new C1IconTemplate(context => new C1SVGIcon(context) { Source = new Uri("raw:///song") }),
            };
            BindITunesGrid(grid);


            //grid.ColumnFooterRows.Add(new GridSummaryRow());
            grid.TranslatesAutoresizingMaskIntoConstraints = false;
            vc.View.BackgroundColor = UIColor.White;
            vc.View.AddSubview(grid);

            Window.RootViewController = vc;
            var constraints = new NSLayoutConstraint[]
            {
                grid.TopAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.TopAnchor),
                grid.BottomAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.BottomAnchor),
                grid.LeftAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.LeftAnchor),
                grid.RightAnchor.ConstraintEqualTo(vc.View.SafeAreaLayoutGuide.RightAnchor)
            };
            NSLayoutConstraint.ActivateConstraints(constraints);

            // make the window visible
            Window.MakeKeyAndVisible();

            return true;
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

        private void OnAutoGeneratingColumn(object sender, C1.iOS.Grid.GridAutoGeneratingColumnEventArgs e)
        {
            if (e.Property.Name == "Name")
            {
                e.Column.AllowResizing = false;
                e.Column.MinWidth = 300;
                e.Column.Width = new GridLength(1, GridUnitType.Star);
            }
            if (e.Property.Name == "WBS")
                e.Column.Width = GridLength.Auto;
            if (e.Property.Name == "Duration")
                e.Column.ValueConverter = DelegateConverter.Create(
                    (value, type, parameter, culture) => string.Format("{0:N1} days?", ((TimeSpan)value).TotalDays),
                    (value, type, parameter, culture) =>
                    {
                        var str = value?.ToString() ?? "";
                        TimeSpan timeSpan;
                        if (TimeSpan.TryParse(str, out timeSpan))
                            return timeSpan;
                        if (str.EndsWith(" days?"))
                            str = str.Substring(0, str.Length - " days?".Length);
                        double totalDays;
                        if (double.TryParse(str, out totalDays))
                            return TimeSpan.FromDays(totalDays);
                        return TimeSpan.Zero;
                    });
            if (e.Column is GridDateTimeColumn)
                e.Column.Format = "ddd d/M/yyyy";
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
