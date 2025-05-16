using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class LogAxesPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public LogAxesPage()
        {
            InitializeComponent();
        }

        void OnToggled(object sender, EventArgs e)
        {
            if (flexChart == null)
                return;
            
            flexChart.AxisX.LogBase = flexChart.AxisY.LogBase = ((Switch)sender).IsToggled ? 10 : double.NaN;
        }

        #region Data

        List<Data.GdpData>? data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData();

        #endregion

    }
}