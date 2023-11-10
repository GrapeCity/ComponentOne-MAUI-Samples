using C1.Maui.Grid;
using FlexGridExplorer.Strings;

namespace FlexGridExplorer
{
    public partial class ConditionalFormatting : ContentPage
    {
        public ConditionalFormatting()
        {
            InitializeComponent();
            Title = AppResources.ConditionalFormattingTitle;
            var data = Customer.GetCustomerList(100);
            grid.ItemsSource = data;
            grid.Columns[4].DataMap = new GridDataMap() { ItemsSource = Customer.GetCountries(), DisplayMemberPath = "Value", SelectedValuePath = "Key" };

            grid.CellFactory = new MyCellFactory();
            grid.MinColumnWidth = 85;
        }
    }

    public class MyCellFactory : GridCellFactory
    {
        static Color LightRedForeground = Color.FromArgb("#FFC42B1C");
        static SolidColorBrush LightRedBackground = new SolidColorBrush(Color.FromArgb("#FFFDE7E9"));
        static Color LightGreenForeground = Color.FromArgb("#FF0F7B0F");
        static SolidColorBrush LightGreenBackground = new SolidColorBrush(Color.FromArgb("#FFDFF6DD"));
        static Color DarkRedForeground = Color.FromArgb("#FFFF99A4");
        static SolidColorBrush DarkRedBackground = new SolidColorBrush(Color.FromArgb("#FF442726"));
        static Color DarkGreenForeground = Color.FromArgb("#FF6CCB5F");
        static SolidColorBrush DarkGreenBackground = new SolidColorBrush(Color.FromArgb("#FF393D1B"));

        private static bool IsLight()
        {
            return Application.Current.RequestedTheme == AppTheme.Light;
        }

        public Color RedForeground
        {
            get
            {
                return IsLight() ? LightRedForeground : DarkRedForeground;
            }
        }

        public Brush RedBackground
        {
            get
            {
                return IsLight() ? LightRedBackground : DarkRedBackground;
            }
        }

        public Color GreenForeground
        {
            get
            {
                return IsLight() ? LightGreenForeground : DarkGreenForeground;
            }
        }

        public Brush GreenBackground
        {
            get
            {
                return IsLight() ? LightGreenBackground : DarkGreenBackground;
            }
        }

        public override void PrepareCell(GridCellType cellType, GridCellRange range, GridCellView cell, Thickness internalBorders)
        {
            base.PrepareCell(cellType, range, cell, internalBorders);
            var orderCountColumn = Grid.Columns["OrderCount"];
            if (cellType == GridCellType.Cell && range.Column == orderCountColumn.Index)
            {
                var cellValue = Grid[range.Row, range.Column] as int?;
                if (cellValue.HasValue)
                {
                    cell.Background = cellValue < 50.0 ? RedBackground : GreenBackground;
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
            var orderCountColumn = Grid.Columns["OrderCount"];
            if (cellType == GridCellType.Cell && range.Column == orderTotalColumn.Index)
            {
                var label = cellContent as Label;
                if (label != null)
                {
                    var cellValue = Grid[range.Row, range.Column] as double?;
                    if (cellValue.HasValue)
                    {
                        label.TextColor = cellValue < 5000.0 ? RedForeground : GreenForeground;
                    }
                }
            }
            if (cellType == GridCellType.Cell && range.Column == orderCountColumn.Index)
            {
                var label = cellContent as Label;
                if (label != null)
                {
                    var cellValue = Grid[range.Row, range.Column] as int?;
                    if (cellValue.HasValue)
                    {
                        label.TextColor = Colors.Black;
                    }
                }
            }
        }

        public override void UnbindCellContent(GridCellType cellType, GridCellRange range, View cellContent)
        {
            base.UnbindCellContent(cellType, range, cellContent);
            var label = cellContent as Label;
            if (label != null)
            {
                label.TextColor = null;
            }
        }
    }
}
