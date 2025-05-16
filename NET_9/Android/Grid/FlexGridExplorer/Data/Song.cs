using System.IO.Compression;
using System.Text.Json;

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
        public static async Task<List<Song>> LoadAsync()
        {
            using (var rawZip = Application.Context.Resources.OpenRawResource(Resource.Raw.data))
            {
                var zip = new ZipArchive(rawZip);
                using (var stream = zip.Entries.First(e => e.Name == "songs.json").Open())
                {
                    return await JsonSerializer.DeserializeAsync<List<Song>>(stream);
                }
            }
        }
    }
}
