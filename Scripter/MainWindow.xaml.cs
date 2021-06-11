using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Scripter.MainClass;
using Scripter.Settings;
using Scripter;
using System.IO;
using Newtonsoft.Json;
using Scripter.DialogWindow;

namespace Scripter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Settings

        St_Mixer _settingsMixer; //Settings Mixer Behringer
        St_Programm _settingsProgramm; //Setings Programm

        #endregion

        CheckConnect checkConnect;
        public MainWindow()
        {
            InitializeComponent();
            LoadSettings();
            StartCheckConnect();
        }

        private void LoadSettings()
        {
            if (!Directory.Exists(AppPath.FolderSettings))
                Directory.CreateDirectory(AppPath.FolderSettings);

            if (File.Exists(AppPath.FolderSettings + "mixer.json"))
                _settingsMixer = JsonConvert.DeserializeObject<St_Mixer>(File.ReadAllText(AppPath.FolderSettings + "mixer.json"));
            else NewSettingsMixer();

            if (File.Exists(AppPath.FolderSettings + "program.json"))
                _settingsProgramm = JsonConvert.DeserializeObject<St_Programm>(File.ReadAllText(AppPath.FolderSettings + "program.json"));
            else NewSettingsProg();
        }

        private void NewSettingsProg()
        {
            
        }

        private void NewSettingsMixer()
        {
            _settingsMixer = new St_Mixer();
            ConnectionSettings connectionSettings = new ConnectionSettings(_settingsMixer);
            connectionSettings.ShowDialog();
        }

        private void StartCheckConnect()
        {
            checkConnect = new CheckConnect("192.168.31.54", 10023, 1000);
            checkConnect.Connected += CheckConnect_Connected;
            checkConnect.NoConnected += CheckConnect_NoConnected;
        }
        private void CheckConnect_NoConnected(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Conn.Text = "false";

            });
        }

        private void CheckConnect_Connected(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                Conn.Text = "true";

            });
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }
    }
}
