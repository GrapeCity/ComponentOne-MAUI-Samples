using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class AxisGroupsPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public AxisGroupsPage()
        {
            this.InitializeComponent();
        }

        #region Data

        AxisGroupSeparator groupSeparator = AxisGroupSeparator.Vertical;

        public AxisGroupSeparator GroupSeparator
        {
            get => groupSeparator;
            set { groupSeparator = value; OnPropertyChanged(); }
        }
        
        public AxisGroupSeparator[] GroupSeparators => new[] {
            AxisGroupSeparator.None, AxisGroupSeparator.Horizontal,
            AxisGroupSeparator.Vertical, AxisGroupSeparator.Grid };

        List<Data.GdpData> data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData().Take(6).ToList();

        #endregion

    }
}