using System.IO;
using System.Text.Json;

namespace WpfApp1.Model
{
    public static class SourceDestinationFoldersClass
    {
        public static string JsonPath { get; set; } = "sourceAndDestinationFolder.json";

        private static string destinationFolderPath = string.Empty;
        public static string DestinationFolderPath
        {
            get { return destinationFolderPath; }
            set
            {
                destinationFolderPath = value;
                
                var json = JsonSerializer.Serialize(new
                {
                    SourceFolderPath,
                    DestinationFolderPath
                });

                File.WriteAllTextAsync(JsonPath, json);
            }
        }

        private static string sourceFolderPath = string.Empty;
        public static string SourceFolderPath
    {
            get { return sourceFolderPath; }
            set
            {
            sourceFolderPath = value;

                var json = JsonSerializer.Serialize(new
                {
                    SourceFolderPath,
                    DestinationFolderPath
                });

                File.WriteAllTextAsync(JsonPath, json);
            }
        }

        public static bool LoadFromJson()
        {
            if (!File.Exists(JsonPath))
            {
                return false;
            }
            string json = File.ReadAllText(JsonPath);

            var obj = JsonSerializer.Deserialize<FolderPaths>(json);
            if (obj != null && !string.IsNullOrEmpty(obj.SourceFolderPath) && !string.IsNullOrEmpty(obj.DestinationFolderPath)) // Ensure JSON is fomrated correctly
            {
                SourceFolderPath = obj.SourceFolderPath;
                DestinationFolderPath = obj.DestinationFolderPath;
                return true;
            }
            return false;
        }
    }

    internal class FolderPaths
    {
        public string SourceFolderPath { get; set; } = string.Empty;
        public string DestinationFolderPath { get; set; } = string.Empty;
    }
}
