using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace FlexGridExplorer
{
    public class Song
    {
        public string Name { get; set; }
        public string Album { get; set; }
        public string Artist { get; set; }
        public long Duration { get; set; }  // in milliseconds
        public long Size { get; set; }      // in bytes
        public int Rating { get; set; }     // from 0 to 5
    }

    public class MediaLibrary
    {
        public static async Task<List<Song>> Load()
        {
            var fileStream = await FileSystem.OpenAppPackageFileAsync("data.zip");
            var zip = new ZipArchive(fileStream);
            using (var stream = zip.Entries.First(e => e.Name == "songs.xml").Open())
            {
                var xmls = new XmlSerializer(typeof(List<Song>));
                return (List<Song>)xmls.Deserialize(stream);
            }
        }
    }
}
