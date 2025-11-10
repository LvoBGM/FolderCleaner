namespace WpfApp1.Model
{
    internal class FileClass
    {
        public FileClass(string name, string path)
        {
            Name = name;
            Path = path;
        }

        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
    }
}
