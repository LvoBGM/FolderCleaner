using System.Diagnostics;
using WpfApp1.Model;
using WpfApp1.View;

namespace WpfApp1.ViewModel
{
    internal class AddWindowViewModel
    {
        public RelayCommand AddFolder => new RelayCommand(execute => CreateFolder());

        public int Id { get; set; } = 0;
        public string Name { get; set; } = "";
        public string Extentions { get; set; } = "";
        private void CreateFolder()
        {
            if (CheckInput())
            {

                Folder folder = new Folder(Id, Name, ConvertToExtentionsList(Extentions));

                folder.LoadToJson();
                return;
            }
            Debug.WriteLine("Invalid Input!");
        }
        private List<string> ConvertToExtentionsList(string s)
        {
            return new List<string>(); // TODO
        }

        private bool CheckInput()
        {
            Debug.WriteLine(Extentions);
            if (Id <= 0 || string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Name))
            {
                Debug.WriteLine("Something is null or empty");
                return false;
            }

            var extentions = Extentions.Split(" ");

            foreach (var extention in extentions)
            {
                if (!extention.StartsWith('.'))
                {
                    Debug.WriteLine("Not all extentions start with a .");
                    return false;
                }
                if (extention.Length < 2)
                {
                    Debug.WriteLine("Extentions has to have letters after the dot");
                    return false;
                }

                string extensionBody = extention.Substring(1);
                if (!extensionBody.All(char.IsLetterOrDigit))
                    return false;
            }
            return true;
        }
        private int NextId()
        {
            return 0;
        }

    }
}
