using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
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

        Stacking stacking = Stacking.None;

        public Stacking Stacking
        {
            get => stacking;
            set { stacking = value; OnPropertyChanged("Stacking"); }
        }

        public Stacking[] Stackings => new[] { Stacking.None, Stacking.Stacked, Stacking.Stacked100pc };

        Palette palette = Palette.Standard;

        public Palette Palette
        {
            get => palette;
            set { palette = value; OnPropertyChanged("Palette"); }
        }

        List<Palette> _palettes;

        public List<Palette> Palettes
        {
            get
            {
                if (_palettes == null)
                {
                    _palettes = Enum.GetValues(typeof(Palette)).OfType<Palette>().ToList();
                    _palettes.Remove(Palette.Custom);
                }

                return _palettes;
            }
        }

        public class DataItem
        {
            public string Country { get; set; }
            public double Q1 { get; set; }
            public double Q2 { get; set; }
            public double Q3 { get; set; }
            public double Q4 { get; set; }
        }

        List<DataItem> data = CreateData();

        public List<DataItem> Data => data;

        public static List<DataItem> CreateData()
        {
            var countries = new string[] { "US", "China", "UK", "Japan" };
            var count = countries.Length;
            var result = new List<DataItem>();
            var rnd = new Random();
            for (var i = 0; i < count; i++)
                result.Add(new DataItem()
                {
                    Country = countries[i],
                    Q1 = rnd.Next(20),
                    Q2 = rnd.Next(20),
                    Q3 = rnd.Next(20),
                    Q4 = rnd.Next(20)
                });
            return result;
        }
    }
}