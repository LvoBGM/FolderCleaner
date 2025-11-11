namespace WpfApp1.Model
{
    internal class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;

        private string path;

        public string Path
        {
            get { return path; }
            set 
            { 
                if(SourceDestinationFoldersClass.DestinationFolderPath != string.Empty)
                {
                    path = $"{SourceDestinationFoldersClass.DestinationFolderPath}\\{value}";
                }
            }
        }

        public string[] Types { get; set; } = Array.Empty<string>(); // Change to list

    }
}
