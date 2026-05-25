using C1.Chart;
using C1.Maui.Diagram;
using DiagramExplorer.Common;
using DiagramExplorer.Data;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class NodeTemplate : ContentPage
    {
        public NodeTemplate()
        {
            InitializeComponent();

            diagram.BeginUpdate();
            diagram.ItemsSource = DataService.Mammals;
            Samples.UpdateDirection(diagram);
            diagram.EndUpdate();
        }

        public FlexDiagram Diagram => diagram;

    }
}