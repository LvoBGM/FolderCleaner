using System.Diagnostics;

namespace WpfApp1.Model
{
    internal class Folder
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Path { get; set; } = string.Empty;
        public string[] Types { get; set; }

        public string TypesString => string.Join(", ", Types);

    }
}
