using C1.Chart;
using System.ComponentModel;
using FlexChartExplorer.Data;

namespace FlexChartExplorer
{
#pragma warning disable 1591
    public partial class BubblePage : ContentPage
    {
        DataService dataService = DataService.GetService();

        public BubblePage()
        {
            this.InitializeComponent();
            flexChart.Palette = Palette.Custom;
            flexChart.CustomPalette = C1.Maui.Chart.Palettes.Qualitative.Set1;
        }

        void BindingChanged(object sender, EventArgs args)
        {
            if(flexChart == null)
                return;

            flexChart.BeginUpdate();

            flexChart.BindingX = XBinding;
            flexChart.Binding = $"{YBinding},{SizeBinding}";

            flexChart.AxisX.Title = XBinding;
            flexChart.AxisY.Title = YBinding;

            flexChart.EndUpdate();
        }

        #region Data

        public string XBinding { get; set; } = "MilesPerGallon";
        public string YBinding { get; set; } = "HorsePower";

        public string SizeBinding { get; set; } = "Cylinders";
        
        public string[] Fields => new[] { "MilesPerGallon", "HorsePower", "Cylinders", "Displacement" };

        List<Data.CarData> dataUSA = null;
        List<Data.CarData> dataJapan = null;
        List<Data.CarData> dataEurope = null;

        public List<Data.CarData> DataUSA => dataUSA != null ? dataUSA : dataUSA =
            dataService.GetCarData().Where(item => item.Origin == "USA").ToList();

        public List<Data.CarData> DataJapan => dataJapan != null ? dataJapan : dataJapan =
            dataService.GetCarData().Where(item => item.Origin == "Japan").ToList();

        public List<Data.CarData> DataEurope => dataEurope != null ? dataEurope : dataEurope =
            dataService.GetCarData().Where(item => item.Origin == "Europe").ToList();

        #endregion
    }
}