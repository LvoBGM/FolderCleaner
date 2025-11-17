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
        public static string DestinationFolderPath { get; private set; } = string.Empty;
        public static void SetDestinationFolderPath(string value)
        {
            DestinationFolderPath = value;

            var json = JsonSerializer.Serialize(new
            {
                SourceFolderPath,
                DestinationFolderPath
            });

            File.WriteAllText(JsonPath, json);
        }
        public static string SourceFolderPath { get; private set; } = string.Empty;
        public static void SetSourceFolderPath(string value)
        {
            SourceFolderPath = value;

            var json = JsonSerializer.Serialize(new
            {
                SourceFolderPath,
                DestinationFolderPath
            });

            File.WriteAllText(JsonPath, json);
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
