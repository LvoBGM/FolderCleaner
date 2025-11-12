using System.IO;
using System.Text.Json;

namespace WpfApp1.Model
{
    public class Folder
    {
        //public int Id { get; set; }
        //public string Name { get; set; } = string.Empty;

        //private string path;

        //public string Path
        //{
        //    get { return path; }
        //    set 
        //    { 
        //        if(SourceDestinationFoldersClass.DestinationFolderPath != string.Empty)
        //        {
        //            path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{value}";
        //        }
        //    }
        //}

        //public List<string> Types { get; set; } = [];
        public int Id { get; set; }
        public string Name { get; set; }
        public string Path { get; private set; } = "";
        public List<string> Types { get; set; }

        private static string JsonPath { get; set; } = "folders.json";

        public Folder(int id, string name, List<string>? types = null)
        {
            Id = id;
            Name = name ?? string.Empty;

            if (SourceDestinationFoldersClass.DestinationFolderPath != string.Empty)
            {
                Path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{name}";
            }

            Types = types ?? new List<string>();
        }

        public void LoadToJson()
        {
            string json = JsonSerializer.Serialize(this);

            File.WriteAllTextAsync(JsonPath, json);
        }
    }
}
