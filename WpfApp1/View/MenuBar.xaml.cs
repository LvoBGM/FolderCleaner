using System.Diagnostics;
using System.Windows;
using WpfApp1.Model;
using UserControl = System.Windows.Controls.UserControl;
using WinForms = System.Windows.Forms;

namespace WpfApp1.View
{
    public partial class MenuBar : UserControl
    {
        public MenuBar()
        {
            InitializeComponent();
        }

        public void SourceFolderClick(object sender, RoutedEventArgs e)
        {
            FindFolder(false);
        }

        public void DestinationFolderClick(object sender, RoutedEventArgs e)
        {
            FindFolder(true);      
        }

        public static void FindFolder(bool searchForDestinationFolder)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = WinForms.Application.StartupPath;

            dialog.Description = searchForDestinationFolder ? "Set Destination Folder" : "Set Source Folder";
            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                if(searchForDestinationFolder)
                {
                    SourceDestinationFoldersClass.SetDestinationFolderPath(path);
                }
                else
                {
                    SourceDestinationFoldersClass.SetSourceFolderPath(path);
                }

                Debug.WriteLine(path);
            }
        }
    }
}
