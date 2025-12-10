using C1.Chart;
using C1.Maui.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class ErrorBars : ContentPage
    {
        public ErrorBars()
        {
            this.InitializeComponent();
        }

        public ChartType[] ChartTypes => new[] {
            ChartType.Column, ChartType.Bar, ChartType.Line, ChartType.LineSymbols,
        };

        ChartType chartType = ChartType.Column;

        public ChartType ChartType
        {
            get => chartType;
            set { chartType = value; OnPropertyChanged(); }
        }


        public ErrorBarDirection[] Directions => new[] { ErrorBarDirection.Both, ErrorBarDirection.Minus, ErrorBarDirection.Plus };

        ErrorBarDirection direction = ErrorBarDirection.Both;

        public ErrorBarDirection Direction
        {
            get => direction;
            set { direction = value; OnPropertyChanged(); }
        }


        public ErrorBarEndStyle[] EndStyles => new[] { ErrorBarEndStyle.Cap, ErrorBarEndStyle.NoCap };

        ErrorBarEndStyle endStyle = ErrorBarEndStyle.Cap;

        public ErrorBarEndStyle EndStyle
        {
            get => endStyle;
            set { endStyle = value; OnPropertyChanged(); }
        }

        public ErrorAmount[] ErrorAmounts => new[] { ErrorAmount.FixedValue, ErrorAmount.Percentage };

        ErrorAmount errorAmount = ErrorAmount.FixedValue;

        public ErrorAmount ErrorAmount
        {
            get => errorAmount;
            set { errorAmount = value; OnPropertyChanged(); }
        }


        private void ErrorAmount_SelectionChanged(object sender, EventArgs e)
        {
            if (errorBar == null)
                return;

            switch (ErrorAmount)
            {
                case ErrorAmount.FixedValue:
                    errorBar.ErrorValue = 50;
                    break;
                case ErrorAmount.Percentage:
                    errorBar.ErrorValue = 0.10; // 10%
                    break;
            }
        }

        #region Data
        DataService dataService = DataService.GetService();

        List<CountrySalesData> _data;

        public List<CountrySalesData> Data => _data != null ? _data : _data = dataService.GetCountrySales();

        #endregion

    }
}