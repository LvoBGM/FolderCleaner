using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public static class FolderStore
    {
        static public ObservableCollection<Folder> Folders { get; set; } = [];

        static private string jsonPath = "folders.json";
        static public void LoadFolders()
        {
            if (System.IO.File.Exists(jsonPath))
            {
                string json = System.IO.File.ReadAllText(jsonPath);

                FolderStore.Folders.Clear();
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
                    FolderStore.Folders.Add(folder);
                }
            }
            else
            {
                Debug.WriteLine("JSON file not found.");

                File.WriteAllText(jsonPath, "[]");
            }
        }
        static public void WriteFolders()
        {
            if (System.IO.File.Exists(jsonPath))
            {
                string json = JsonSerializer.Serialize(FolderStore.Folders);

                File.WriteAllText(jsonPath, json);
            }
            else
            {
                Debug.WriteLine("JSON file not found.");

                File.WriteAllText(jsonPath, "[]");
            }
        }
    }
    internal class MainWindowViewModel
    {
        public RelayCommand OrganizeFiles => new RelayCommand(execute => btnOrganizeFiles());

        

        
        public MainWindowViewModel()
        {
            FolderStore.LoadFolders();
            SourceDestinationFoldersClass.LoadFromJson();
            ManageWindowViewModel.SyncFoldersCollection();
        }

        private void btnOrganizeFiles()
        {
            string[] files = Directory.GetFiles(SourceDestinationFoldersClass.SourceFolderPath);

            foreach (string file in files)
            {
                foreach(Folder folder in FolderStore.Folders)
                {
                    if (folder.Types.Contains(Path.GetExtension(file)))
                    {
                        File.Move(file, $"{folder.Path}\\{Path.GetFileName(file)}");
                        Debug.Print($"Moved {file} to {folder.Path}");
                    }
                }
            }
        }
    }
}
