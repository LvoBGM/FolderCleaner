using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using WpfApp1.Model;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnOrganizeFiles(object sender, RoutedEventArgs e)
        {
            string folderPath = SourceDestinationFoldersClass.SourceFolderPath;
            try
            {
                string[] files = Directory.GetFiles(folderPath);
                foreach (string file in files)
                {
                    Debug.WriteLine($"{System.IO.Path.GetExtension(file)}");
                }
            } 
            catch (DirectoryNotFoundException)
            {
                Debug.WriteLine($"Error: The directory '{folderPath}' was not found.");
            }
        }
    }
}