using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class BindingPage : ContentPage
    {
        int npts = 50;
        List<DataItem> _data;
        Random rnd = new();

        public BindingPage()
        {
            InitializeComponent();
        }

        ChartType chartType = ChartType.Line;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public ChartType[] ChartTypes => new[] { ChartType.Line, ChartType.LineSymbols, ChartType.Area        };

        public class DataItem
        {
            public int? Downloads { get; set; }
            public int? Sales { get; set; }
            public DateTime Date { get; set; }
        }

        public List<DataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<DataItem>();
                    var dateStep = 0;
                    for (var i = 0; i < npts; i++)
                    {
                        var date = DateTime.Today.AddDays(dateStep += 9);
                        _data.Add(new DataItem()
                        {
                            Downloads = date.Month == 4 || date.Month == 8 ? (int?)null : rnd.Next(10, 20),
                            Sales = date.Month == 4 || date.Month == 8 ? (int?)null : rnd.Next(0, 10),
                            Date = date
                        });
                    }
                }

                return _data;
            }
        }

    }
}