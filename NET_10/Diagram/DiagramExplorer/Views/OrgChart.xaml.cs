using C1.Chart;
using C1.Maui.Chart;
using C1.Maui.Diagram;
using DiagramExplorer.Common;
using DiagramExplorer.Data;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class OrgChart : ContentPage
    {
        public OrgChart()
        {
            InitializeComponent();

            var data = DataService.GetOrgChartData();

            var nodeStyle = new ChartStyle() { 
                //FontFamily = fontFamily, 
                FontSize = 12 };
            var nodeContentStyle = new ChartStyle()
            {
                //FontFamily = fontFamily,
                FontSize = 10,
                //FontStyle = FontStyles.Italic,
                StrokeThickness = 0f,
            };

            var headerStyle = new ChartStyle()
            {
                //FontFamily = fontFamily,
                FontSize = 24, 
                FontAttributes = FontAttributes.Bold,
                StrokeThickness = 0,
            };

            diagram.BeginUpdate();

            diagram.NodeCreated += (s, a) =>
            {
                var orgNode = a.Data as OrgNode;
                var node = a.Node as C1.Maui.Diagram.Node;

                if (orgNode == null || node == null)
                    return;

                if (!string.IsNullOrEmpty(orgNode.FirstName))
                {
                    node.Title = $"{orgNode.FirstName}\n{orgNode.LastName}";
                    node.Content = orgNode.JobTitle;

                    node.TitleImage = orgNode.Image;
                    node.TitleImageSize = new Size(60, 80);
                    node.TitleStyle = nodeStyle;
                    node.NodeStyle = nodeContentStyle;
                }
                else
                {
                    node.Title = orgNode.Name;
                    node.TitleStyle = node.NodeStyle = headerStyle;
                }

                node.LegendItem = orgNode.Department;
                node.Shape = C1.Diagram.Shape.RoundedRectangle;
            };

            diagram.ItemsSource = data;

            Samples.UpdateDirection(diagram);
            diagram.EndUpdate();
        }

        public FlexDiagram Diagram => diagram;
    }
}