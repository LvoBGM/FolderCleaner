using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;

namespace WpfApp1.Model
{
    public class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private string path;
        public string Path
        { 
            get
            {
                if (Directory.Exists(path)) { return  path; }
                else
                {
                    Directory.CreateDirectory(path);
                    return path;
                }
            }
            set
            {
                path = value;
            }
        }
        public List<string> Types { get; set; }

        private static string JsonPath { get; set; } = "folders.json";

        public Folder(int id, string name, List<string>? types = null)
        {
            Id = id;
            Name = name ?? string.Empty;

            Path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{name}";

            if (!Directory.Exists(Path))
            {
                Directory.CreateDirectory(Path);
            }

                Types = types ?? new List<string>();
        }

        public void LoadToJson()
        {
            string existingJson = File.ReadAllText(JsonPath);

            ObservableCollection<Folder> folders = new ObservableCollection<Folder>();

            folders = JsonSerializer.Deserialize<ObservableCollection<Folder>>(existingJson)!;
            folders.Add(this);

            string json = JsonSerializer.Serialize(folders);

            File.WriteAllText(JsonPath, json);
        }
    }
}
