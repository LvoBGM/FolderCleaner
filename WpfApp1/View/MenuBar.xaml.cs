using System.Windows;
using System.Windows.Controls;
using WinForms = System.Windows.Forms;

using UserControl = System.Windows.Controls.UserControl;
using System.Diagnostics;
using WpfApp1.Model;

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
            FindFolder();
        }

        private void DestinationFolderClick(object sender, RoutedEventArgs e)
        {
            FindFolder();
        }

        public string MainFolderPath;

        private void FindFolder()
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.InitialDirectory = WinForms.Application.StartupPath;

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                MainFolderPath = dialog.SelectedPath;
                Folder.MainFolderPath = MainFolderPath;

                Debug.WriteLine(MainFolderPath);
            }
        }
    }
}
