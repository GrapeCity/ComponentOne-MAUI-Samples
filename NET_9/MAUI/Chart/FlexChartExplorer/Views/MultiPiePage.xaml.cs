using C1.Chart;
using System.ComponentModel;

using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class MultiPiePage : ContentPage
    {
        List<SeasonSale> _data;
        string[] bindings = new string[] {
                "WinterOnline,WinterOffline,SummerOnline,SummerOffline",
                "OfflineTotal,OnlineTotal",
                "WinterTotal,SummerTotal",
                "TotalSales"
            };

        public MultiPiePage()
        {
            this.InitializeComponent();
            pickerShow.SelectedIndex = 0;
        }

        public string[] ShowOptions => new string[] { "Season & Channel", "Offline vs Online", "Winter vs Summer", "Total" };

        public List<SeasonSale> Data => SeasonSale.GetSeasonSales(5);

        string[] AdjustTitles(string[] titles)
        {
            if (titles.Length > 1)
            {
                for (var i = 0; i < titles.Length; i++)
                {
                    titles[i] = titles[i].Replace("Total", "");
                    // add space
                    titles[i] = string.Concat(titles[i].Select(x => Char.IsUpper(x) ? " " + x : x.ToString())).TrimStart(' ');
                }
            }
            else
                titles = null;

            return titles;
        }

        private void SelectedIndexChanged(object sender, EventArgs e)
        {
            if (pickerShow.SelectedItem != null)
            {
                flexPie.Binding = bindings[pickerShow.SelectedIndex];
                // adjust titles
                flexPie.Titles = AdjustTitles(flexPie.Binding.Split(','));
            }
        }

        public class SeasonSale
        {
            public string Name { get; set; }
            public double WinterOnline { get; set; }
            public double WinterOffline { get; set; }
            public double SummerOnline { get; set; }
            public double SummerOffline { get; set; }
            public double TotalSales => WinterOnline + WinterOffline + SummerOffline + SummerOnline;
            public double WinterTotal => WinterOnline + WinterOffline;
            public double SummerTotal => SummerOnline + SummerOffline;
            public double OnlineTotal => WinterOnline + SummerOnline;
            public double OfflineTotal => WinterOffline + SummerOffline;

            static string[] salesItems = "Computers|Software|Cell Phones|Video Games|Musical Instruments|Household Electronic Items|Sports & Fitness|Jewellery & Accessories|Furniture|Clothing".Split('|');

            public static List<SeasonSale> GetSeasonSales(int numOfCategories)
            {
                var rnd = new Random();
                var data = new List<SeasonSale>();
                for (int i = 0; i < numOfCategories; i++)
                {
                    var item = salesItems[i];
                    data.Add(new SeasonSale
                    {
                        Name = item,
                        WinterOnline = rnd.Next(10, 100),
                        SummerOnline = rnd.Next(10, 100),
                        WinterOffline = rnd.Next(10, 100),
                        SummerOffline = rnd.Next(10, 100),
                    });
                }
                return data;
            }
        }
    }
}