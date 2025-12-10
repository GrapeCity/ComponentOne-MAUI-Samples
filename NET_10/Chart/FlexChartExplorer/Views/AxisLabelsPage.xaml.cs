using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class AxisLabelsPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public AxisLabelsPage()
        {
            InitializeComponent();
        }

        #region Data

        int staggeredLine = 2;

        public int StaggeredLine
        {
            get => staggeredLine;
            set { staggeredLine = value; OnPropertyChanged(); }
        }

        public int[] StaggeredLines => new[] { 1, 2, 3, 4 };

        OverlappingLabels overlappingLabels = OverlappingLabels.Trim;

        public OverlappingLabels OverlappingLabels
        {
            get => overlappingLabels;
            set { overlappingLabels = value; OnPropertyChanged(); }
        }

        public OverlappingLabels[] OverlappingLabelsValues => new[] { OverlappingLabels.Trim, OverlappingLabels.WordWrap }; 

        public object[] Data => GdpData.GetData();

        class GdpData
        {
            public static object[] GetData()
            {
                return new object[]
                {
                new { Country = "United States", Gdp = 25462.700 },
                new { Country = "China", Gdp = 17963.171 },
                new { Country = "European Union", Gdp = 16600.000 },
                new { Country = "Japan", Gdp = 4231.141 },
                new { Country = "Germany", Gdp = 4072.192 },
                new { Country = "India", Gdp = 3385.090 },
                new { Country = "United Kingdom", Gdp = 3070.668 },
                new { Country = "France", Gdp = 2.782905 },
                //new { Country = "Russia", Gdp = 2240.422 },
                //new { Country = "Canada", Gdp = 2139840 },
                //new { Country = "Italy", Gdp = 2010432 },
                //new { Country = "Brazil", Gdp = 1920096 },
                //new { Country = "South Korea", Gdp = 1665246 },
                };
            }
        }
        #endregion
    }
}