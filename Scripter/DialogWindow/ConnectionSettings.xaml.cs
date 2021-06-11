﻿using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
using Scripter.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Scripter.DialogWindow
{
    /// <summary>
    /// Логика взаимодействия для ConnectionSettings.xaml
    /// </summary>
    public partial class ConnectionSettings : MetroWindow
    {
        public St_Mixer _settings;

        public bool AlreadySave { private set; get; }

        public ConnectionSettings(St_Mixer settMixer, bool enCancel = false)
        {
            InitializeComponent();
            Cancel.IsEnabled = enCancel;
            _settings = settMixer;

            UIIpAdress.Text = _settings.IpAdress;
            UIEPort.IsChecked = _settings.ePort;
            UIEInterval.IsChecked = _settings.eInterval;
            UIPort.Text = _settings.Port.ToString();
            UIInterval.Text = _settings.Interval.ToString();
            if (_settings.CheckInterval < 0)
                IntrConnCheck.Text = _settings.CheckInterval.ToString();
            CheckChecked();
        }

        /// <summary>
        /// Проверяет нужно ли включать поля Интервала и Порта
        /// </summary>
        private void CheckChecked()
        {
            if (UIEInterval.IsChecked == true)
                UIInterval.IsEnabled = true;
            if (UIEPort.IsChecked == true)
                UIPort.IsEnabled = true;
        }

        private void Unchecked()
        {
            if (UIEInterval.IsChecked == false)
                UIInterval.IsEnabled = false;
            if (UIEPort.IsChecked == false)
                UIPort.IsEnabled = false;
        }

        private void UIEPort_Checked(object sender, RoutedEventArgs e)
        {
            CheckChecked();
        }

        private void UIEPort_Unchecked(object sender, RoutedEventArgs e)
        {
            Unchecked();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            AlreadySave = false;
            this.Close();
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (IPAddress.TryParse(UIIpAdress.Text, out var address))
            {
                _settings.IpAdress = address.ToString();
            }
            else
            {
                
            }
        }
    }
}
