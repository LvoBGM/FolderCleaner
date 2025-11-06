using System.Windows;
using WpfApp1.ViewModel;

namespace WpfApp1
{
    public partial class ManageWindow : Window
    {
        public ManageWindow(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
            ManageWindowViewModel vm = new ManageWindowViewModel();
            DataContext = vm;
        }
    }
}
