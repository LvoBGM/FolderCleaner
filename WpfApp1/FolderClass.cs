using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp1
{
    class Folder
    {
        public string Name { get; set; } = "";
        public string Path { get; set; } = "";
        public string[] Extentions { get; set; } = { };
        public string[] Types { get; internal set; }
        public int Id { get; internal set; }
        public object TypesString { get; internal set; }
    }
}
