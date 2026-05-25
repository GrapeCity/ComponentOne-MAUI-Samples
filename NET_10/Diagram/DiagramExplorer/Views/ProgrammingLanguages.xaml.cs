using C1.Chart;
using System.ComponentModel;

namespace DiagramExplorer
{
#pragma warning disable 1591
    public partial class ProgrammingLanguages : ContentPage
    {
        public ProgrammingLanguages()
        {
            InitializeComponent();

            Common.Samples.LoadDiagramFromResource(diagram, "programming-language-tree.mermaid");
        }
    }
}