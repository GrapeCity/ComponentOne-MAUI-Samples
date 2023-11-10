using Microsoft.Maui.Controls;
using System;

namespace CalendarExplorer
{
    public class Sample
    {
        public string Name { get; set; }

        public int SampleViewType { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public ImageSource ThumbnailImageSource
        {
            get
            {
                try
                {
                    return ImageSource.FromResource(@"Resources\Images\" + Thumbnail, typeof(App));
                }
                catch (Exception exc)
                {
                    return null;
                }
            }
        }
    }
}
