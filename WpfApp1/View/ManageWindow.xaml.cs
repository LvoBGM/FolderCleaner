using System.Windows;

namespace WpfApp1
{
    public partial class ManageWindow : Window
    {
        public ManageWindow(Window parentWindow)
        {
            Owner = parentWindow;
            InitializeComponent();
        }
    }
}
