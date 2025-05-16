using C1.Chart;
using System.ComponentModel;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class CustomDataLabels : ContentPage
    {
        public CustomDataLabels()
        {
            InitializeComponent();
        }

        #region Data

        int gap = 10;
        public int Gap
        {
            get => gap;
            set { gap = value; OnPropertyChanged(); }
        }

        #endregion
        public List<FinancialData.Quote> Data => FinancialData.GetData(150);

        public int[] Gaps => new[] { 5, 10, 20 };

        void LabelRendering(object sender, C1.Maui.Chart.RenderDataLabelEventArgs args)
        {
            // skip labels according to the gap
            var gap = Gaps[gapPicker.SelectedIndex];
            args.Cancel = args.Index % gap != 0;
        }

        #region Data
        public class FinancialData
        {
            static Random rnd = new Random();

            public class Quote
            {
                public DateTime Date { get; set; }
                public double Price { get; set; }
            }

            public static List<Quote> GetData(int npts = 100)
            {
                var data = new List<Quote>();
                var startDate = new DateTime(2017, 1, 1);
                var temp = 50;
                for (int i = 0; i < npts; i++)
                {
                    if (i % 7 == 0)
                        temp += i < 70 ? rnd.Next(5, 15) : -rnd.Next(1, 15);
                    var open = rnd.Next(temp - 15, temp + 15);
                    var close = rnd.Next(temp - 15, temp + 15);
                    var high = open > close ? rnd.Next(open + 1, open + 10) : rnd.Next(close + 1, close + 10);
                    data.Add(new Quote
                    {
                        Date = startDate.AddDays(i),
                        Price = high,
                    });
                }
                return data;
            }
        }
        #endregion

        private void gapPicker_SelectedIndexChanged(object sender, EventArgs e)
        {
            flexChart?.Invalidate();
        }
    }
}