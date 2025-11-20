using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using System.Xml.Linq;
using WpfApp1.ViewModel;

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

        public Folder Copy()
        {
            return new Folder(this.Id, this.Name, this.Types);
        }

        public static string CheckFolderInput(int id, string name, string types)
        {
            if (id < 1 || string.IsNullOrEmpty(name) || string.IsNullOrEmpty(types))
            {
                return "A field is empty";
            }

            // Check Extentions formating
            var extentions = types.Split(" ");

            foreach (var extention in extentions)
            {
                if (!extention.StartsWith('.'))
                {
                    return "Extentions have to start with a dot!";
                }
                if (extention.Length < 2)
                {
                    return "Extentions have to have letters after the dot!";
                }

                string extensionBody = extention.Substring(1);
                if (!extensionBody.All(char.IsLetterOrDigit))
                    return "Input needs to be only letters and digits!";
            }

            foreach (var folder in FolderStore.Folders)
            {
                if (folder.Id == id)
                {
                    return "A folder with that id already exists!";
                }
                if (folder.Name == name)
                {
                    return "A folder with that name already exists!";
                }
            }
            return string.Empty;
        }
        public static string CheckFolderInput(Folder checkedFolder)
        {
            if (checkedFolder.Id < 1 || string.IsNullOrEmpty(checkedFolder.Name) || checkedFolder.Types.Count() == 0)
            {
                return "A field is empty";
            }

            // Check Extentions formating
            var extentions = checkedFolder.Types;

            foreach (var extention in extentions)
            {
                if (!extention.StartsWith('.'))
                {
                    return "Extentions have to start with a dot!";
                }
                if (extention.Length < 2)
                {
                    return "Extentions have to have letters after the dot!";
                }

                string extensionBody = extention.Substring(1);
                if (!extensionBody.All(char.IsLetterOrDigit))
                    return "Input needs to be only letters and digits!";
            }

            foreach (var folder in FolderStore.Folders)
            {
                if (folder.Id == checkedFolder.Id)
                {
                    return "A folder with that id already exists!";
                }
                if (folder.Name == checkedFolder.Name)
                {
                    return "A folder with that name already exists!";
                }
            }
            return string.Empty;
        }
        
    }
}
