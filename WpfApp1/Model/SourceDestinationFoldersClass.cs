using System.Diagnostics;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text.Json;
using WpfApp1.View;

namespace WpfApp1.Model
{
    public static class SourceDestinationFoldersClass
    {
        private static string JsonPath { get; set; } = "sourceAndDestinationFolder.json";

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
            if (obj == null)
            {
                return false;
            }
            else if (string.IsNullOrEmpty(obj.DestinationFolderPath) && !string.IsNullOrEmpty(obj.SourceFolderPath))
            {
                SourceFolderPath = obj.SourceFolderPath;
                DestinationFolderPath = string.Empty;
                return false;
            }
            else if (string.IsNullOrEmpty(obj.SourceFolderPath) && !string.IsNullOrEmpty(obj.DestinationFolderPath))
            {
                DestinationFolderPath = obj.DestinationFolderPath;
                SourceFolderPath = string.Empty;
                return false;
            }
            else if (string.IsNullOrEmpty(obj.SourceFolderPath) && string.IsNullOrEmpty(obj.DestinationFolderPath))
            {
                DestinationFolderPath = string.Empty;
                SourceFolderPath = string.Empty;
                return false;
            }
            else
            {
                SourceFolderPath = obj.SourceFolderPath;
                DestinationFolderPath = obj.DestinationFolderPath;
                return true;
            }
        }

        public static void CheckIfPathLoaded()
        {
            if (SourceFolderPath == string.Empty)
            {
                MenuBar.FindFolder(false);
            }
            if (DestinationFolderPath == string.Empty)
            {
                MenuBar.FindFolder(true);
            }
        }
    }

    internal class FolderPaths
    {
        public string SourceFolderPath { get; set; } = string.Empty;
        public string DestinationFolderPath { get; set; } = string.Empty;
    }
}
