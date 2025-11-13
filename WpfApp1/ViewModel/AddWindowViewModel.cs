using System.Diagnostics;
using WpfApp1.Model;
using WpfApp1.MVVM;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    internal class AddWindowViewModel : ViewModelBase
    {
        public RelayCommand AddFolder => new RelayCommand(execute => CreateFolder());

        private string id = "";

        public string Id
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
                MainWindowViewModel.LoadFolders();
                Id = "0";
                Name = " ";
                Extentions = " ";
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
            Debug.WriteLine(Extentions);
            if (string.IsNullOrEmpty(Id) || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Name))
            {
                Debug.WriteLine("Something is null or empty");
                return false;
            }

            // Check Id
            if (!int.TryParse(Id, out int number))
            {
                return false;
            }

            // Check Extentions formating
            var extentions = Extentions.Split(" ");

            foreach (var extention in extentions)
            {
                if (!extention.StartsWith('.'))
                {
                    Debug.WriteLine("Not all extentions start with a .");
                    return false;
                }
                if (extention.Length < 2)
                {
                    Debug.WriteLine("Extentions has to have letters after the dot");
                    return false;
                }

                string extensionBody = extention.Substring(1);
                if (!extensionBody.All(char.IsLetterOrDigit))
                    return false;
            }
            return true;
        }
        private int NextId()
        {
            return 0;
        }

    }
}
