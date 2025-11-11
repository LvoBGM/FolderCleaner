using System.Windows;
using WpfApp1.ViewModel;

namespace WpfApp1.View
{
    public partial class AddFolderWindow : Window
    {
        public AddFolderWindow(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            ManageWindowViewModel vm = new ManageWindowViewModel();
            DataContext = vm;
        }
    }
}
