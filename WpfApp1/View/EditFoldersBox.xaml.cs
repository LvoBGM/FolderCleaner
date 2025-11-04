using System.Windows;

using UserControl = System.Windows.Controls.UserControl;
using Application = System.Windows.Application;

namespace WpfApp1.View
{
    public partial class EditFoldersBox : UserControl
    {
        public EditFoldersBox()
        {
            InitializeComponent();
        }

        private void btnFolders_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ManageWindow manageWindow = new ManageWindow(Application.Current.MainWindow);
            manageWindow.Show();
        }
    }
}
