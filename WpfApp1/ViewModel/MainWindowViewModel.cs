using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows;
using WpfApp1.Model;

namespace WpfApp1.ViewModel
{
    internal class MainWindowViewModel
    {
        public RelayCommand OrganizeFiles => new RelayCommand(execute => btnOrganizeFiles());
        public MainWindowViewModel()
        {
            SourceDestinationFoldersClass.LoadFromJson();
            FoldersConfig.LoadFolders();
            ManageWindowViewModel.SyncFoldersCollection();
        }

        private void btnOrganizeFiles()
        {
            string[] files = Directory.GetFiles(SourceDestinationFoldersClass.SourceFolderPath);

            foreach (string file in files)
            {
                foreach(Folder folder in FoldersConfig.Folders)
                {
                    if (folder.Types.Contains(Path.GetExtension(file)))
                    {
                        string newFileLocation = $"{folder.Path}\\{Path.GetFileName(file)}";
                        try
                        {
                            File.Move(file, newFileLocation);
                            Debug.Print($"Moved {file} to {folder.Path}");
                        }
                        catch (System.IO.IOException)
                        {
                            string messageBoxText = $"There already is a file named: {Path.GetFileName(file)} in {folder.Name}. Do you wish to replace it?";
                            string caption = "Duplicate file error";
                            MessageBoxButton button = MessageBoxButton.YesNoCancel;
                            MessageBoxImage icon = MessageBoxImage.Warning;
                            MessageBoxResult result;

                            result = System.Windows.MessageBox.Show(messageBoxText, caption, button, icon, MessageBoxResult.Yes);

                            if (result == MessageBoxResult.Yes)
                            {
                                File.Delete(newFileLocation);
                                File.Move(file, newFileLocation);
                                Debug.Print($"Moved {file} to {folder.Path}");
                            }
                        }
                    }
                }
            }
        }
    }
}
