using C1.Chart;
using C1.Maui.Diagram;
using DiagramExplorer.Common;
using DiagramExplorer.Data;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class Animals : ContentPage
    {
        public Animals()
        {
            InitializeComponent();

            diagram.BeginUpdate();
            diagram.NodeCreated += (s, args) =>
            {
                var node = (Node)args.Node;
                var animal = args.Data as Animal;

                node.Title = animal != null ? animal.Name : args.Data.ToString();

                if (animal != null && animal.Image != null)
                {
                    node.TitleImage = animal.Image;
                    node.TitleImageSize = new Size(64, 64);
                    node.TitleDirection = Direction.Vertical;
                }

                node.Shape = C1.Diagram.Shape.RoundedRectangle;
                node.LegendItem = args.ParentNode == null ? args.Data?.ToString() : args.ParentNode.LegendItem;
            };

            diagram.ItemsSource = DataService.Animals;
            Samples.UpdateDirection(diagram);
            diagram.EndUpdate();
        }

        public FlexDiagram Diagram => diagram;

    }
}