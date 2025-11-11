using System.Windows;

using UserControl = System.Windows.Controls.UserControl;
using Application = System.Windows.Application;
using WpfApp1.Model;

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
            if (SourceDestinationFoldersClass.DestinationFolderPath == String.Empty && !SourceDestinationFoldersClass.LoadFromJson())
            {
                System.Windows.MessageBox.Show("Set Main Folder Path!");
                return;
            }
            ManageWindow manageWindow = new ManageWindow(Application.Current.MainWindow);
            manageWindow.Show();
        }

        private void btnNewFolder_Click(object sender, RoutedEventArgs e)
        {
            if (SourceDestinationFoldersClass.DestinationFolderPath == String.Empty && !SourceDestinationFoldersClass.LoadFromJson())
            {
                System.Windows.MessageBox.Show("Set Main Folder Path!");
                return;
            }
            AddFolderWindow addFolderWindow = new AddFolderWindow(Application.Current.MainWindow);
            addFolderWindow.Show();
        }
    }
}
