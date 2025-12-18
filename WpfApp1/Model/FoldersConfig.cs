using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;

namespace WpfApp1.Model
{
    public static class FoldersConfig
    {
        static public ObservableCollection<Folder> Folders { get; set; } = [];

        static private string jsonPath = "folders.json";

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
    }
}
