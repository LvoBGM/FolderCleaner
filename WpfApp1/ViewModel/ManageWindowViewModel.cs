using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using WpfApp1.Model;
using WpfApp1.MVVM;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    internal class ManageWindowViewModel : ViewModelBase
    {
        public static ObservableCollection<Folder> Folders { get; set; } = [];

        public RelayCommand DeleteSelectedFolder => new RelayCommand(execute => DeleteFolder());
        public RelayCommand EditSelectedFolder => new RelayCommand(execute => EditFolder());

        public void DeleteFolder() // TODO: Doesn't work if user has selected multible folder AND ask if user wants to delete the actual directory as will
        {
            Folders.Remove(SelectedFolder);

            FolderStore.WriteFolders();
        }
        private void EditFolder()
        {
            ErrorText = Folder.CheckFolderInput(editedFolder, true);
            if (string.IsNullOrEmpty(ErrorText))
            {
                // This is here because the path property is really persistent and always wants to exist, so we need to delete it after we get it
                Folder newFolder = EditedFolder.Copy();
                string newFolderPath = newFolder.Path;
                string oldFolderPath = SelectedFolder.Path;

                // This sucks
                Directory.Delete(newFolder.Path);

                Directory.Move(oldFolderPath, newFolderPath);

                SelectedFolder.Id = newFolder.Id;
                SelectedFolder.Name = newFolder.Name;
                SelectedFolder.Types = newFolder.Types;
                SelectedFolder.Path = newFolder.Path;

                Directory.Delete(oldFolderPath);

                OnPropertyChanged();
            }
        }

        private Folder? selectedFolder = new Folder(FolderStore.NextId(), "", new List<string>());
        public Folder SelectedFolder
        {
            get
            {
                EditedFolder = selectedFolder.Copy();
                return selectedFolder;
            }
            set
            {
                selectedFolder = value;

                if (selectedFolder == null)
                {
                    return;
                }

                OnPropertyChanged();
            }
        }

        private Folder? editedFolder;
        public Folder EditedFolder
        {
            get { return editedFolder; }
            set
            {
                editedFolder = value;
                OnPropertyChanged();
            }
        }

        private string errorText = string.Empty;
        public string ErrorText
        {
            get { return errorText; }
            set { errorText = value; OnPropertyChanged(); }
        }

        public static void SyncFoldersCollection()
        {
            Folders = FolderStore.Folders;
        }

    }
}
