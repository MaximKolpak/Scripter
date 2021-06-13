using System;

namespace Scripter
{
    internal class AppPath
    {
        public static string FolderProgramm = AppDomain.CurrentDomain.BaseDirectory;
        public static string FolderScripts = AppDomain.CurrentDomain.BaseDirectory + @"Scripts\";
        public static string FolderSettings = AppDomain.CurrentDomain.BaseDirectory + @"Settings\";
    }
}