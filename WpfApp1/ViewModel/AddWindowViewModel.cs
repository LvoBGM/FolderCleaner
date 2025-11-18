using System.Diagnostics;
using WpfApp1.Model;
using WpfApp1.MVVM;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    internal class AddWindowViewModel : ViewModelBase
    {
        public RelayCommand AddFolder => new RelayCommand(execute => CreateFolder());
        private string errorText = string.Empty;
        public string ErrorText
        {
            get { return errorText; }
            set { errorText = value; OnPropertyChanged(); }
        }
        private int id = 0;
        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged(); }
        }

        private string name = "";

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged(); }
        }
        private string extentions = "";

        public string Extentions
        {
            get { return extentions; }
            set { extentions = value; OnPropertyChanged(); }
        }
        private void CreateFolder()
        {
            if (CheckInput())
            {
                if (SourceDestinationFoldersClass.DestinationFolderPath == string.Empty)
                {
                    return;
                }
                Folder folder = new Folder(Id, Name, ConvertToExtentionsList(Extentions));
                folder.LoadToJson();
                FolderStore.LoadFolders();
                Id = NextId();
                Name = "";
                Extentions = "";
                ErrorText = "";
                OnPropertyChanged();
                return;
            }
            Debug.WriteLine("Invalid Input!");
        }
        private List<string> ConvertToExtentionsList(string s)
        {
            return new List<string>(s.Split(" "));
        }

        private bool CheckInput()
        {
            if (Id < 1 || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Name))
            {
                ErrorText = "A field is empty";
                return false;
            }

            // Check Extentions formating
            var extentions = Extentions.Split(" ");

            foreach (var extention in extentions)
            {
                if (!extention.StartsWith('.'))
                {
                    ErrorText = "Extentions have to start with a dot!";
                    return false;
                }
                if (extention.Length < 2)
                {
                    ErrorText = "Extentions have to have letters after the dot!";
                    return false;
                }

                string extensionBody = extention.Substring(1);
                if (!extensionBody.All(char.IsLetterOrDigit))
                    ErrorText = "Input needs to be only letters and digits!";
                    return false;
            }

            // Check for duplicate name/Id
            foreach (var folder in FolderStore.Folders)
            {
                if (folder.Id == Id)
                {
                    ErrorText = "A folder with that id already exists!";
                    return false;
                }
                if (folder.Name == Name)
                {
                    ErrorText = "A folder with that name already exists!";
                    return false;
                }
            }
            return true;
        }
        private int NextId()
        {
            if(FolderStore.Folders.Count() == 0)
            {
                return 1;
            }
            int lastId = FolderStore.Folders.Last().Id;
            return lastId + 1;
        }

        public AddWindowViewModel()
        {
            Id = NextId();
        }

    }
}
