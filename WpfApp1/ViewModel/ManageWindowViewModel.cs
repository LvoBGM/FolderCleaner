using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfApp1.Model;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModel
{
    internal class ManageWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Folder> Folders { get; set; }
        public ObservableCollection<FileClass> Files { get; init; } = [];

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

        public ManageWindowViewModel()
        {
            Folders = FolderStore.Folders;
        }

    }
}
