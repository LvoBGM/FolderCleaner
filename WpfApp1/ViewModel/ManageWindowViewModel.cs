using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Shapes;
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
            // We need to remove selected folder when checking here, since CheckFolderInput will flag it as a duplicate to our edited one

            Folder editedFolderCopy = EditedFolder.Copy();
            Folder selectedFolderCopy = SelectedFolder.Copy();

            Folders.Remove(SelectedFolder);
            ErrorText = Folder.CheckFolderInput(editedFolderCopy.Id, editedFolderCopy.Name, SelectedFolderTypes);

            // Replacing the selected folder
            SelectedFolder = selectedFolderCopy;
            Folders.Add(SelectedFolder);
            

            if (string.IsNullOrEmpty(ErrorText))
            {
                // This is here because the path property is really persistent and always wants to exist, so we need to delete it after we get it
                Folder newFolder = editedFolderCopy.Copy();
                string newFolderPath = newFolder.Path;
                string oldFolderPath = SelectedFolder.Path;

                // This sucks
                var test = Directory.GetFiles(newFolderPath);
                if (!Directory.EnumerateFileSystemEntries(newFolderPath).Any())
                {
                    Directory.Delete(newFolderPath);
                }
                else
                {
                    ErrorText = "Folder with that name already exist in source folder!";
                    return;
                }
                Directory.Move(oldFolderPath, newFolderPath);

                SelectedFolder.Id = newFolder.Id;
                SelectedFolder.Name = newFolder.Name;
                SelectedFolder.Types = newFolder.Types;
                SelectedFolder.Path = newFolder.Path;

                Directory.Delete(oldFolderPath);

                FolderStore.WriteFolders();
                OnPropertyChanged();
            }
        }

        private Folder? selectedFolder = new Folder(0, "", new List<string>());
        public Folder SelectedFolder
        {
            get
            {
                if(selectedFolder != null)
                {
                    EditedFolder = selectedFolder.Copy();
                    SelectedFolderTypes = string.Join(" ", selectedFolder.Types);
                }
                return selectedFolder;
            }
            set
            {
                if (value == null)
                {
                    return;
                }
                selectedFolder = value;

                OnPropertyChanged();
            }
        }

        private string selectedFolderTypes = string.Empty;

        public string SelectedFolderTypes
        {
            get { return selectedFolderTypes; }
            set { selectedFolderTypes = value; OnPropertyChanged(); }
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
