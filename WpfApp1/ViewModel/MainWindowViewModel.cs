using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
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
            SourceDestinationFoldersClass.LoadFromJson();
            FolderStore.LoadFolders();
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
                        string newFileLocation = $"{folder.Path}\\{Path.GetFileName(file)}";
                        try
                        {
                            File.Move(file, newFileLocation);
                            Debug.Print($"Moved {file} to {folder.Path}");
                        }
                        catch (System.IO.IOException)
                        {
                            string messageBoxText = $"There already is a file named: {Path.GetFileName(file)} in {folder.Name}. Do you wish to replace it?";
                            string caption = "Duplicate file error";
                            MessageBoxButton button = MessageBoxButton.YesNoCancel;
                            MessageBoxImage icon = MessageBoxImage.Warning;
                            MessageBoxResult result;

                            result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                            if (result == MessageBoxResult.Yes)
                            {
                                File.Delete(newFileLocation);
                                File.Move(file, newFileLocation);
                                Debug.Print($"Moved {file} to {folder.Path}");
                            }
                        }
                    }
                }
            }
        }
    }
}
