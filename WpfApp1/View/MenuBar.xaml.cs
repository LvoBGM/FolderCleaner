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

        private void SourceFolderClick(object sender, RoutedEventArgs e)
        {
            FindFolder(false);
        }

        private void DestinationFolderClick(object sender, RoutedEventArgs e)
        {
            FindFolder(true);      
        }

        private void FindFolder(bool searchForDestinationFolder)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = WinForms.Application.StartupPath;

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                string path = dialog.SelectedPath;
                if(searchForDestinationFolder)
                {
                    SourceDestinationFoldersClass.DestinationFolderPath = path;
                }
                else
                {
                    SourceDestinationFoldersClass.SourceFolderPath = path;
                }

                Debug.WriteLine(path);
            }
        }
    }
}
