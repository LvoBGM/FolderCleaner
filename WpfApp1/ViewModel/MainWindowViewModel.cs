using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    public static class FolderStore
    {
        static public ObservableCollection<Folder> Folders { get; set; } = [];
    }
    internal class MainWindowViewModel
    {
        public void LoadFolders()
        {

            string jsonPath = "folders.json";

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

                ObservableCollection<Folder> folders = new ObservableCollection<Folder> { new Folder(1,"MyFirstFolder") };

                string json = JsonSerializer.Serialize(folders);

                File.WriteAllText(jsonPath, json);
            }
        }
        public MainWindowViewModel()
        {
            LoadFolders();
        }
            
        private void btnOrganizeFiles(object sender, RoutedEventArgs e)
        { 
        
        // TODO
        }
    }
}
