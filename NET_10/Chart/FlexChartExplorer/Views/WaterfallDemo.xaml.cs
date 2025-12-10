using C1.Chart;
using C1.Maui.Chart;
using System.ComponentModel;
using System.Diagnostics.Metrics;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class WaterfallDemo : ContentPage
    {
        List<object> _data;

        public WaterfallDemo()
        {
            this.InitializeComponent();
            wf.IntermediateTotalPositions = new List<int> { 3, 6, 9, 12 };
        }

        #region Data

        public List<object> Data => _data != null ? _data : _data = CreateData();

        public static List<object> CreateData()
        {
            var rnd = new Random();
            var items = new List<object>();
            string[] names = new[] { "Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec" };
            foreach (var name in names)
            {
                items.Add(new
                {
                    Name = name,
                    Value = Math.Round((0.5 - rnd.NextDouble()) * 1000)
                });
            }
            return items;
        }

        #endregion

    }
}