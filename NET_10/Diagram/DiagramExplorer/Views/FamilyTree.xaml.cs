using C1.Chart;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class FamilyTree : ContentPage
    {
        public FamilyTree()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "family-tree.mermaid");
        }
    }
}