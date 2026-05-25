using C1.Chart;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class DecisionTree : ContentPage
    {
        public DecisionTree()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "decision-tree.mermaid");
        }
    }
}