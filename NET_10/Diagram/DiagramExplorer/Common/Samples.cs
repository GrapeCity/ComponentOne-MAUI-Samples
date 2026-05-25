using C1.Chart.Standard;
using C1.Diagram.Parser;
using C1.Maui.Diagram;
using DiagramExplorer.Data;
using Microsoft.Maui.Graphics.Platform;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace DiagramExplorer.Common
{
    /// <summary>
    /// Common stuff shared across samples.
    /// </summary>
    class Samples
    {
        static Stream? GetResourceStream(string name)
        {
            var asm = Assembly.GetExecutingAssembly();
            return asm.GetManifestResourceStream($"{asm.GetName().Name}.Resources.{name}");
        }

        public static void LoadDiagramFromResource(C1.Maui.Diagram.FlexDiagram diagram, string name)
        {
            using var stream = GetResourceStream(name);

            var text = "";
            if (stream != null)
            {
                using (var sr = new StreamReader(stream))
                    text = sr.ReadToEnd();
            }

            
            diagram.LoadMermaidGraph(text);
        }

        public static void UpdateDirection(FlexDiagram diagram)
        {
            diagram.Direction = DeviceDisplay.MainDisplayInfo.Orientation == DisplayOrientation.Portrait ?
            C1.Diagram.DiagramDirection.LeftRight : C1.Diagram.DiagramDirection.TopBottom;
        }

        static Random rnd = new Random();
    }
}
