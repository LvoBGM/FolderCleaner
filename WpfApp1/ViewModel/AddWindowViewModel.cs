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
            ErrorText = FoldersConfig.CheckFolderInputValididty(Id, Name, Extentions);
            if (string.IsNullOrEmpty(ErrorText))
            {
                if (SourceDestinationFoldersClass.DestinationFolderPath == string.Empty)
                {
                    return;
                }
                Folder folder = new Folder(Id, Name, ConvertToExtentionsList(Extentions));
                folder.LoadToJson();
                FoldersConfig.LoadFolders();
                Id = FoldersConfig.NextId();
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

        public AddWindowViewModel()
        {
            Id = FoldersConfig.NextId();
        }

    }
}
