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

namespace Scripter
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        CheckConnect checkConnect;
        public MainWindow()
        {
            InitializeComponent();

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
    }
}
