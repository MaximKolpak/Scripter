using System.Drawing;

namespace Scripter.Settings
{
    internal class ScriptLua
    {
        public int ID { get; set; } // Indetificator
        public string Name { get; set; } //Name Script
        public string Description { get; set; } //Description Script
        public string Path { get; set; } //Path to script
        public string Date { get; set; } //Date add script
        public bool Enable { get; set; } //Enable script
        public StateScript State { get; set; } //Status script
    }
}