using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace WpfApp1.Model
{
    public static class FoldersConfig
    {
        static public ObservableCollection<Folder> Folders { get; set; } = [];

        static public string jsonPath = "folders.json";

        static public void SortFolders()
        {
            var sorted = Folders.OrderBy(x => x.Id).ToList();

            Folders.Clear();
            foreach (var item in sorted)
            {
                Folders.Add(item);
            }
        }
        public static void LoadFolders()
        {
            if (System.IO.File.Exists(jsonPath))
            {
                string json = System.IO.File.ReadAllText(jsonPath);

                Folders.Clear();
                ObservableCollection<Folder> folders = new ObservableCollection<Folder>();
                try
                {
                    folders = JsonSerializer.Deserialize<ObservableCollection<Folder>>(json)!;
                }
                catch (JsonException ex)
                {
                    Debug.WriteLine($"Invalid JSON format: {ex.Message}");
                }

                foreach (var folder in folders)
                {
                    Folders.Add(folder);
                }

                SortFolders();
            }
            else
            {
                Debug.WriteLine("JSON file not found.");

                File.WriteAllText(jsonPath, "[]");
            }
        }
        public static void WriteFolders()
        {
            if (System.IO.File.Exists(jsonPath))
            {
                SortFolders();
                string json = JsonSerializer.Serialize(Folders);

                File.WriteAllText(jsonPath, json);
            }
            else
            {
                Debug.WriteLine("JSON file not found.");

                File.WriteAllText(jsonPath, "[]");
            }
        }
        public static int NextId()
        {
            if (Folders.Count() == 0)
            {
                return 1;
            }
            int lastId = Folders.Last().Id;
            return lastId + 1;
        }

        public static string CheckFolderInputValididty(int id, string name, string types)
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

            foreach (var folder in Folders)
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
        public static string CheckFolderInputValididty(Folder checkedFolder)
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

            foreach (var folder in Folders)
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
