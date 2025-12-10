using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class TwoYAxesPage : ContentPage
    {
        List<DataItem> _data;
        Random rnd = new Random();
        int npts = 12;

        public TwoYAxesPage()
        {
            InitializeComponent();
            anglePicker.SelectedIndex = 2;
        }

        double labelAngle = 0;

        public List<double> LabelAngles => new List<double>() { -90, -45, 0, 45, 90 };

        public double LabelAngle
        {
            get => labelAngle;
            set { labelAngle = value; OnPropertyChanged(); }
        }

        ChartType chartType = ChartType.Line;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public ChartType[] ChartTypes => new[] { ChartType.Line, ChartType.LineSymbols, ChartType.Area        };

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<DataItem>();
                    var dt = DateTime.Today;
                    for (var i = 0; i < npts; i++)
                    {
                        _data.Add(new DataItem()
                        {
                            Time = dt.AddMonths(i),
                            Precipitation = rnd.Next(30, 100),
                            Temperature = rnd.Next(7, 20)
                        });
                    }
                }

                return _data;
            }
        }

        public class DataItem
        {
            public DateTime Time { get; set; }
            public int Precipitation { get; set; }
            public int Temperature { get; set; }
        }
    }
}