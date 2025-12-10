using C1.Chart;
using System.ComponentModel;
using System.Globalization;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class SelectionPage : ContentPage
    {
        public SelectionPage()
        {
            InitializeComponent();
        }

        ChartType chartType = ChartType.Column;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public ChartType[] ChartTypes => new[] 
        { 
            ChartType.Column, ChartType.Bar, 
            ChartType.Line, ChartType.LineSymbols,
            ChartType.Scatter, ChartType.Area,
            ChartType.Spline, ChartType.SplineSymbols, ChartType.SplineArea,
        };

        ChartSelectionMode selectionMode = ChartSelectionMode.Point;

        public ChartSelectionMode SelectionMode
        {
            get => selectionMode;
            set { selectionMode = value; OnPropertyChanged("SelectionMode"); }
        }

        public ChartSelectionMode[] SelectionModes => new[] 
        { ChartSelectionMode.None, ChartSelectionMode.Point, ChartSelectionMode.Series };

        #region Data
        List<DataItem> _data;

        public class DataItem
        {
            public string Month { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
        }

        public static List<DataItem> CreateData()
        {
            var result = new List<DataItem>();
            var rnd = new Random();
            for (var i = 1; i <= 6; i++)
                result.Add(new DataItem()
                {
                    Month = DateTimeFormatInfo.CurrentInfo.GetAbbreviatedMonthName(i),
                    Sales = rnd.Next(30),
                    Expenses = rnd.Next(20),
                });
            return result;
        }

        public List<DataItem> Data => (_data == null) ? _data = CreateData() : _data;

        #endregion
    }
}