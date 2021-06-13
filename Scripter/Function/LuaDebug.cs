using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Scripter.Function
{
    class LuaDebug
    {
        // Запись любого текста с указанным цветом
        private void Print(string message, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }

        public void Log(string message)
        {
            Print("Log: " + message, ConsoleColor.White);
        }

        public void Warning(string message)
        {
            Print("Warning: " + message, ConsoleColor.Yellow);
        }

        public void Error(string message)
        {
            Print("Error: " + message, ConsoleColor.Red);
        }

        public string ConsoleRead()
        {
            return Console.ReadLine();
        }

        public void MessageDialog(string messgae)
        {
            MessageBox.Show(messgae);
        }
    }
}
