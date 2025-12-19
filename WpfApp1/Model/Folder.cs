using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;

namespace WpfApp1.Model
{
    public class Folder
    {
        public int Id { get; set; }

        public string Name
        {
            get { return System.IO.Path.GetFileName(Path); }
            set { Path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{value}"; }
        }

        private string path;
        public string Path
        { 
            get
            {
                return path;
            }
            set
            {
                path = value;
            }
        }
        public List<string> Types { get; set; }

        public Folder(int id, string name, List<string>? types = null)
        {
            Id = id;
            Name = name ?? string.Empty;

            Path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{name}";

            Types = types ?? new List<string>();
        }

        public void LoadToJson()
        {
            string existingJson = File.ReadAllText(FoldersConfig.jsonPath);

            ObservableCollection<Folder> folders = new ObservableCollection<Folder>();

            folders = JsonSerializer.Deserialize<ObservableCollection<Folder>>(existingJson)!;
            folders.Add(this);

            string json = JsonSerializer.Serialize(folders);

            File.WriteAllText(FoldersConfig.jsonPath, json);
        }

        public Folder Copy()
        {
            return new Folder(this.Id, this.Name, this.Types);
        }
       
    }
}
