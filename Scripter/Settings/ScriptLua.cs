using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scripter.Settings
{
    class ScriptLua
    {
        public int ID { get; set; } // Indetificator
        public string Name { get; set; } //Name Script
        public string Path { get; set; } //Path to script
        public string Date { get; set; } //Date add script
        public bool Enable { get; set; } //Enable script
    }
}
