using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfApp1.Model;
using WpfApp1.MVVM;
using static System.Net.WebRequestMethods;

namespace WpfApp1.ViewModel
{
    internal class ManageWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Folder> Folders { get; init; } = new ObservableCollection<Folder>();
        public ObservableCollection<FileClass> Files { get; init; } = new ObservableCollection<FileClass>();

        public RelayCommand DoubleClickFileClass => new RelayCommand(execute => OpenFile(execute), canExecute => { return true; });

        private void OpenFile(object parameter)
        {
            if (parameter != null && parameter is FileClass file)
            {
                Process.Start(new ProcessStartInfo(file.Path)
                {
                    UseShellExecute = true
                });
            }
        }


        public ManageWindowViewModel()
        {
            string jsonPath = "folders.json";

            if (System.IO.File.Exists(jsonPath))
            {
                string json = System.IO.File.ReadAllText(jsonPath);

                Debug.WriteLine(json);

                Folders = JsonSerializer.Deserialize<ObservableCollection<Folder>>(json)!;

                Debug.WriteLine(Folders[0].Id);
            }
            else
            {
                Debug.WriteLine("JSON file not found.");
            }
        }

        private Folder selectedFolder;
        public Folder SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                selectedFolder = value;

                string[] filePaths = Directory.GetFiles(selectedFolder.Path, "*");

                Files.Clear();

                foreach (string filePath in filePaths)
                {
                    Files.Add(new FileClass(Path.GetFileName(filePath), filePath));
                }
                

                OnPropertyChanged();

            }
        }

        private FileClass selectedFile;

        public FileClass SelectedFile
        {
            get { return selectedFile; }
            set { selectedFile = value; }
        }

    }
}
