using C1.Chart;
using C1.Maui.Chart.Palettes;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class ExtendedPalettes : ContentPage
    {
        DataService dataService = DataService.GetService();
        IDictionary<string, IList<Brush>> brushes;

        public ExtendedPalettes()
        {
            this.InitializeComponent();
            pickerGroup.SelectedIndex = 0;
        }

        public string[] Groups => new[] { "Diverging", "Qualitative", "Sequential Single", "Sequential Multi" }; 


        private void SymbolRendering(object sender, C1.Maui.Chart.RenderSymbolEventArgs args)
        {
            if (flexChart.CustomPalette != null)
            {
                args.Engine.SetFill(flexChart.CustomPalette[args.Index]);
                args.Engine.SetStroke(null);
            }
        }

        #region Data

        List<Data.GdpData> data = null;

        public List<Data.GdpData> Data => data != null ? data : data = dataService.GetGdpData().Take(8).ToList();

        #endregion

        private void pickerGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            var group = pickerGroup.SelectedItem.ToString();

            switch (group)
            {
                case "Diverging":
                    brushes = Diverging.Brushes;
                    break;
                case "Qualitative":
                    brushes = Qualitative.Brushes;
                    break;
                case "Sequential Single":
                    brushes = SequentialSingle.Brushes;
                    break;
                case "Sequential Multi":
                    brushes = SequentialMulti.Brushes;
                    break;
                default:
                    break;
            }

            pickerPalette.ItemsSource = brushes.Keys.ToList();
            pickerPalette.SelectedIndex = 0;
        }

        private void pickerPalette_SelectedIndexChanged(object sender, EventArgs e)
        {
            var palette = pickerPalette.SelectedItem?.ToString();

            if (brushes != null && !string.IsNullOrEmpty(palette))
            {
                flexPie.Palette = flexChart.Palette = Palette.Custom;
                flexPie.CustomPalette = flexChart.CustomPalette = brushes[palette];
            }
        }
    }
}