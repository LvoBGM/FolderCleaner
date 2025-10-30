using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Shapes;

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
            string folderPath = @"C:\Users\Dell\Downloads";

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

        private void EditFoldersBox_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}