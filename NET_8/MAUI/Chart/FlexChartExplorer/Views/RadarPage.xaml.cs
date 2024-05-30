using C1.Chart;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class RadarPage : ContentPage
    {
        List<CountryData> _data;

        public RadarPage()
        {
            InitializeComponent();
        }

        RadarChartType chartType = RadarChartType.Line;

        public RadarChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public RadarChartType[] ChartTypes => new [] { RadarChartType.Line,
            RadarChartType.LineSymbols, RadarChartType.Area, RadarChartType.Scatter};

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

        #region Data

        public class CountryData
        {
            public string Name { get; set; }
            public double Sales { get; set; }
            public double Expenses { get; set; }
        }

        public static List<CountryData> GetCountrySales(int rangeMin = 100, int rangeMax = 1000)
        {
            var rnd = new Random();
            var data = new List<CountryData>();
            var countries = "US,China,Japan,Germany,UK,France".Split(',');
            for (int i = 0; i < countries.Length; i++)
            {
                var country = new CountryData
                {
                    Name = countries[i],
                    Sales = rnd.Next(rangeMin, rangeMax),
                    Expenses = rnd.Next(rangeMin, rangeMax)
                };
                data.Add(country);
            }
            return data;
        }

        public List<CountryData> Data => _data == null ? _data = GetCountrySales() : _data;

        #endregion
    }
}