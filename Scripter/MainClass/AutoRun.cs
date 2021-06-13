using Microsoft.Win32;

namespace Scripter.MainClass
{
    internal class AutoRun
    {
        public bool IsStartupItem(string NameReg)
        {
            // The path to the key where Windows looks for startup applications
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);

            if (rkApp.GetValue(NameReg) == null)
                // The value doesn't exist, the application is not set to run at startup
                return false;
            else
                // The value exists, the application is set to run at startup
                return true;
        }

        public void AddAutoRun(string NameReg, string PathProgramm)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (!IsStartupItem(NameReg))
                rkApp.SetValue(NameReg, PathProgramm);
        }

        public void RemoveAutoRun(string NameReg)
        {
            RegistryKey rkApp = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (IsStartupItem(NameReg))
                // Remove the value from the registry so that the application doesn't start
                rkApp.DeleteValue(NameReg, false);
        }
    }
}