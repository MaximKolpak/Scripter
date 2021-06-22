using MahApps.Metro.Controls;
using Newtonsoft.Json;
using Scripter.DialogWindow;
using Scripter.MainClass;
using Scripter.Settings;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Drawing;
using System.Windows.Media;
using System.Threading;

namespace Scripter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        #region Settings

        private St_Mixer _settingsMixer; //Settings Mixer Behringer
        private St_Programm _settingsProgramm; //Setings Programm

        #endregion Settings

        private AutoRun autoRun = new AutoRun();

        private string Key = "Bhx32Scripter";

        private CheckConnect checkConnect;

        private List<ScriptLua> _scripts = new List<ScriptLua>();

        private Mixer mixerCore;

        private bool Connected = false;

        public MainWindow()
        {
            ConsoleManager.HideConsoleWindow();
            InitializeComponent();
            LoadSettings();
            LoadScripts();
            AddToListScripts();
            mixerCore = new Mixer(_settingsMixer.IpAdress, _settingsMixer.Port, _settingsMixer.Interval);
            StartCheckConnect(_settingsMixer.IpAdress, _settingsMixer.Port, _settingsMixer.CheckInterval, _settingsMixer.ThreadSleepConnection);
        }

        /// <summary>
        /// Помещает скрипты в список в UI
        /// </summary>
        private void AddToListScripts()
        {
            int removeScript = 0;
            foreach (ScriptLua script in _scripts.ToArray())
            {
                if (!File.Exists(script.Path))
                {
                    _scripts.Remove(script); removeScript++;
                }
                else
                {
                    if (script.State != StateScript.Container)
                        UiScripts.Items.Add(script);
                }
            }

            if (removeScript != 0)
                SaveListLuaScripts();
        }

        /// <summary>
        /// Загружает скрипты
        /// </summary>
        private void LoadScripts()
        {
            if (!Directory.Exists(AppPath.FolderScripts))
                Directory.CreateDirectory(AppPath.FolderScripts);

            if (File.Exists(AppPath.FolderSettings + "scripts.json"))
                _scripts = JsonConvert.DeserializeObject<List<ScriptLua>>(File.ReadAllText(AppPath.FolderSettings + "scripts.json"));

            string[] files = Directory.GetFiles(AppPath.FolderScripts, "*.lua");
            int newFile = 0;
            foreach (string file in files)
            {
                if (!_scripts.Exists(x => x.Path == file))
                {
                    NewScriptFound newScript = new NewScriptFound(file);
                    newScript.ShowDialog();

                    if (newScript.State == StateScript.Pase)
                        continue;

                    _scripts.Add(new ScriptLua { ID = _scripts.Count + 1, Name = newScript.NameScript, Path = file, Date = DateTime.Now.ToString("dd.MM.yyyy"), Enable = newScript.EnableScript, State = newScript.State, Description = newScript.Description });
                    newFile++;
                }
            }
            if (newFile != 0)
                SaveListLuaScripts();
        }

        /// <summary>
        /// Сохраняет скрипты
        /// </summary>
        private void SaveListLuaScripts()
        {
            using (StreamWriter file = File.CreateText(AppPath.FolderSettings + "scripts.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _scripts);
            }
        }

        /// <summary>
        /// Загрузка настроек
        /// </summary>
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

        /// <summary>
        /// Задает новые настройки программы
        /// </summary>
        private void NewSettingsProg()
        {
            _settingsProgramm = new St_Programm();
            _settingsProgramm.AutoRun = autoRun.IsStartupItem(Key);
            SaveParametrsProgramm();
        }

        /// <summary>
        /// Сохраняет настройки программы
        /// </summary>
        private void SaveParametrsProgramm()
        {
            using (StreamWriter file = File.CreateText(AppPath.FolderSettings + "program.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _settingsProgramm);
            }
        }

        /// <summary>
        /// Задает новые настройки микшера
        /// </summary>
        private void NewSettingsMixer()
        {
            _settingsMixer = new St_Mixer();
            ConnectionSettings connectionSettings = new ConnectionSettings(_settingsMixer);
            connectionSettings.ShowDialog();
            if (connectionSettings.AlreadySave)
            {
                _settingsMixer = connectionSettings._settings;
                SaveSettingsMixer();
                //И сохранить настройки микшера
            }
            else Environment.Exit(0);
        }

        /// <summary>
        /// Сохраняет параметры микшера
        /// </summary>
        private void SaveSettingsMixer()
        {
            using (StreamWriter file = File.CreateText(AppPath.FolderSettings + "mixer.json"))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, _settingsMixer);
            }
        }

        /// <summary>
        /// Запускает постоянныую проверку подключение к микшеру
        /// </summary>
        /// <param name="ipAdress">IpAdress Микшера</param>
        /// <param name="iPort">Port Микшера</param>
        /// <param name="iCheckInterval">Интервал проверки подключение</param>
        /// <param name="iSleepThread">Сон для потока</param>
        private void StartCheckConnect(string ipAdress, int iPort, int iCheckInterval, int iSleepThread)
        {
            checkConnect = new CheckConnect(ipAdress, iPort, iCheckInterval, iSleepThread);
            checkConnect.Connected += CheckConnect_Connected;
            checkConnect.NoConnected += CheckConnect_NoConnected;
        }

        private void CheckConnect_NoConnected(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                UIConnectMixer.IsChecked = false;
            });

            if (Connected)
            {
                Connected = false;
                mixerCore = new Mixer(_settingsMixer.IpAdress, _settingsMixer.Port, _settingsMixer.Interval);
                ShowStandardBalloon("Connection", "Потеря соединения с микшером", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Warning);
            }
        }

        private void CheckConnect_Connected(object sender, EventArgs e)
        {
            Dispatcher.Invoke(() =>
            {
                UIConnectMixer.IsChecked = true;
            });

            if (!Connected)
            {
                Connected = true;
                mixerCore.Connect();
                new Thread(() =>
                {
                    mixerCore.RunScripts(_scripts);
                }).Start();

                ShowStandardBalloon("Connection", "Подключен к микшеру", Hardcodet.Wpf.TaskbarNotification.BalloonIcon.Info);
            }
        }

        #region UIConrtoller

        private void Window_Closed(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void FolderScripts_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", AppPath.FolderScripts);
            Console.WriteLine("OpenFolderProject");
        }

        private void FolderSettings_Click(object sender, RoutedEventArgs e)
        {
            Process.Start("explorer.exe", AppPath.FolderSettings);
        }

        private void ToTray_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            this.ShowInTaskbar = false;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(0);
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Minimized) { Hide(); this.ShowInTaskbar = false; };
        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            this.ShowInTaskbar = true;
            Show();
        }

        private void ShowStandardBalloon(string title, string text, Hardcodet.Wpf.TaskbarNotification.BalloonIcon balloonIcon)
        {
            TascbrIcon.ShowBalloonTip(title, text, balloonIcon);
        }

        private void ConsoleCheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ConsoleManager.ShowConsoleWindow();
        }

        private void ConsoleCheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ConsoleManager.HideConsoleWindow();
        }

        private void ReloadScript_Click(object sender, RoutedEventArgs e)
        {
            RealoadScript();
        }

        #endregion UIConrtoller



        private void RealoadScript()
        {
            mixerCore.DestroyFunction();
            if (Connected)
                new Thread(() => {
                    mixerCore.RunScripts(_scripts);
                }).Start();
        }
    }
}