using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class AxisMarkers: ContentPage
    {
        int npts = 150;
        Random rnd = new Random();
        List<CpuDataItem> _data;

        public AxisMarkers()
        {
            this.InitializeComponent();
        }

        #region Data

        public List<CpuDataItem> Data
        {
            get
            {
                if (_data == null)
                {
                    _data = new List<CpuDataItem>();
                    var dateStep = 0;
                    for (var i = 0; i < npts; i++)
                    {
                        var date = DateTime.Today.AddSeconds(dateStep += 9);
                        _data.Add(new CpuDataItem()
                        {
                            Cpu1 = rnd.Next(10, 20),
                            Cpu2 = rnd.Next(40, 80),
                            Time = date
                        });
                    }
                }

                return _data;
            }
        }

        public class CpuDataItem
        {
            public int? Cpu1 { get; set; }
            public int? Cpu2 { get; set; }
            public DateTime Time { get; set; }
        }
        #endregion

    }
}