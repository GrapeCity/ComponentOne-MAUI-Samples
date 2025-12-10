using C1.Maui.Grid;
using FlexGridExplorer.Strings;
using Microsoft.Maui.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace FlexGridExplorer
{
    public partial class ColumnLayout : ContentPage
    {
        private string FILENAME = "ColumnLayout.json";

        public ColumnLayout()
        {
            InitializeComponent();

            Title = AppResources.ColumnLayoutTitle;
            //btnEditColumnLayout.Text = AppResources.EditColumnLayout;
            //btnSerializeColumnLayout.Text = AppResources.Save;

            InitializeColumnLayout();
        }

        private async void OnEditColumnLayout(object sender, EventArgs e)
        {
            await ColumnLayoutForm.ShowAsync(Navigation, grid);
        }

        private async void OnSerializeColumnLayout(object sender, EventArgs e)
        {
            var serializedColumns = await SerializeLayout(grid);
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, FILENAME);
            if (!File.Exists(filePath))
                using (File.Create(filePath)) { }
            using var fileStream = File.Open(filePath, FileMode.Truncate, FileAccess.Write);
            using var textWriter = new StreamWriter(fileStream);
            await textWriter.WriteAsync(serializedColumns);
        }

        async void InitializeColumnLayout()
        {
            string data = null;
            var filePath = Path.Combine(FileSystem.Current.AppDataDirectory, FILENAME);
            if (File.Exists(filePath))
            {
                using var fileStream = File.OpenRead(filePath);
                using var streamReader = new StreamReader(fileStream);
                data = await streamReader.ReadToEndAsync();
            }

            var items = Customer.GetCustomerList(100);
            grid.ItemsSource = items;
            grid.MinColumnWidth = 85;

            if (!string.IsNullOrWhiteSpace(data))
                DeserializeLayout(grid, data);
        }

        private async Task<string> SerializeLayout(FlexGrid grid)
        {
            var columns = new List<ColumnInfo>();
            foreach (var col in grid.Columns)
            {
                var colInfo = new ColumnInfo { Name = col.Binding, IsVisible = col.IsVisible };
                columns.Add(colInfo);
            }
            var serializer = new DataContractJsonSerializer(typeof(ColumnInfo[]));
            var stream = new MemoryStream();
            serializer.WriteObject(stream, columns.ToArray());
            await stream.FlushAsync();
            stream.Seek(0, SeekOrigin.Begin);
            var serializedColumns = await new StreamReader(stream).ReadToEndAsync();
            return serializedColumns;
        }

        private void DeserializeLayout(FlexGrid grid, string layout)
        {
            var serializer = new DataContractJsonSerializer(typeof(ColumnInfo[]));
            var stream = new MemoryStream(System.Text.UTF8Encoding.UTF8.GetBytes(layout));
            var columns = serializer.ReadObject(stream) as ColumnInfo[];
            for (int i = 0; i < columns.Length; i++)
            {
                var colInfo = columns[i];
                var column = grid.Columns[colInfo.Name];
                var colIndex = grid.Columns.IndexOf(column);
                column.IsVisible = colInfo.IsVisible;
                if (colIndex != i)
                {
                    grid.Columns.Move(colIndex, i);
                }
            }
        }
    }

    [DataContract]
    public class ColumnInfo
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "isVisible")]
        public bool IsVisible { get; set; }
    }
}
