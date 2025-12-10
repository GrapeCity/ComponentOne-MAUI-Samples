using C1.Chart;
using System.ComponentModel;

using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class FinancialPage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public FinancialPage()
        {
            InitializeComponent();
        }

        ChartType chartType = ChartType.Candlestick;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged("ChartType"); }
        }

        public ChartType[] ChartTypes => new[] { ChartType.Candlestick, ChartType.HighLowOpenClose };
        #region Data

        List<Quote> _data;

        public List<Quote> Data => _data != null ? _data : _data = dataService.CreateFinancialData(DateTime.Today, 30);

        #endregion
    }
}