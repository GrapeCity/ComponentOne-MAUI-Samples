using C1.Chart;
using C1.Maui.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class BoxWhiskerDemo : ContentPage
    {
        List<Point> _data1, _data2;
        List<MonthData> _months;

        public BoxWhiskerDemo()
        {
            this.InitializeComponent();

            flexChart.AxisX.ItemsSource = Months;
            flexChart.ToolTipContent = "{seriesName}\nMin: {Min}\nFirst Quartile: {FirstQuartile}\nMedian: {Median}\nMean: {Mean:n2}\nThird Quartile: {ThirdQuartile}\nMax: {Max}";
        }

        void UpdateSeries(object sender, EventArgs args)
        {
            foreach (BoxWhisker ser in new BoxWhisker[] { series1, series2 })
            {
                if (ser != null)
                {
                    ser.QuartileCalculation = QuartileCalculation;
                    ser.ShowOutliers = cbShowOutliers.IsChecked;
                    ser.ShowInnerPoints = cbShowInnerPoints.IsChecked;
                    ser.ShowMeanLine = cbShowMeanLine.IsChecked;
                    ser.ShowMeanMarks = cbShowMeanMarks.IsChecked;
                }
            }
        }

        #region Data

        public List<Point> Data1 => _data1 == null ? _data1 = GetTemperatureData() : _data1;
        public List<Point> Data2 => _data2 == null ? _data2 = GetTemperatureData() : _data2;

        public List<MonthData> Months => _months == null ? _months = GetMonths() : _months;

        public QuartileCalculation[] QuartileCalculations => new[] { QuartileCalculation.InclusiveMedian, QuartileCalculation.ExclusiveMedian };

        public QuartileCalculation QuartileCalculation { get; set; } = QuartileCalculation.InclusiveMedian;

        public class MonthData
        {
            public string Name { get; set; }
            public double Value { get; set; }
        }

        public List<MonthData> GetMonths()
        {
            var list = new List<MonthData>();
            var names = System.Globalization.DateTimeFormatInfo.CurrentInfo.AbbreviatedMonthNames;
            for (var i = 0; i < names.Length; i++)
                list.Add(new MonthData { Value = i, Name = names[i] });

            return list;
        }

        public class TemperatureData
        {
            public DateTime Date { get; set; }
            public double Temperature { get; set; }
        }

        public static List<Point> GetTemperatureData(int count = 365)
        {
            var rnd = new Random();
            var data = new List<TemperatureData>();
            var startDate = new DateTime(2017, 1, 1);
            for (int i = 0; i < count; i++)
            {
                var temp = new TemperatureData();
                DateTime date;
                date = startDate.AddDays(i);
                temp.Date = date;
                if (date.Month <= 8)
                    temp.Temperature = rnd.Next(2 * date.Month, 8 * date.Month);
                else
                    temp.Temperature = rnd.Next((13 - date.Month - 2) * date.Month, (13 - date.Month) * date.Month);
                data.Add(temp);
            }
            return data.GroupBy(x => x.Date.Month).SelectMany(grp => grp).OrderBy(x => x.Date.Day).Select(x => new Point { X = x.Date.Month - 1, Y = x.Temperature }).ToList(); ;
        }

        #endregion
    }
}