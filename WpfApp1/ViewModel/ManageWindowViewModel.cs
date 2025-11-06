using System.Collections.ObjectModel;
using System.Diagnostics;
using WpfApp1.MVVM;

namespace WpfApp1.ViewModel
{
    internal class ManageWindowViewModel : ViewModelBase
    {
        public ObservableCollection<Folder> Folders { get; set; } = new ObservableCollection<Folder>();

        private Folder selectedFolder;

        public ManageWindowViewModel()
        {
            Folders.Add(new Folder { Id = 1, Name = "Documents", Path = @"C:\Users\Dell\Desktop\Organized Downloads\MS Office Files", Types = ["docx", "pdf"] });
            Folders.Add(new Folder { Id = 2, Name = "Pictures", Path = @"C:\Users\Dell\Desktop\Organized Downloads\Images", Types = ["jpg", "png"] });
            Debug.WriteLine("AAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
        }

        public Folder SelectedFolder
        {
            get { return selectedFolder; }
            set
            {
                selectedFolder = value;
                OnPropertyChanged();

            }
        }


    }
}
