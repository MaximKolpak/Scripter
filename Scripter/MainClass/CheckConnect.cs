using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Scripter.MainClass
{
    class CheckConnect
    {
        public bool Connect = false;

        public event EventHandler Connected;
        public event EventHandler NoConnected;

        private byte[] _buffer = new byte[256]; //Buffer
        private Byte[] _sendBytes;

        private Socket _socket;

        /// <summary>
        /// Проверяет подключение к микшеру
        /// </summary>
        /// <param name="IpAdress">IpAdress микшера</param>
        /// <param name="Port">Port микшера</param>
        /// <param name="ReceiveTimeout">Ожидание отклика микшера в (мс)</param>
        public CheckConnect(string IpAdress, int Port, int ReceiveTimeout)
        {
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.Udp);
            _socket.ReceiveTimeout = ReceiveTimeout;
            IPAddress mixerAddr = IPAddress.Parse(IpAdress);
            IPEndPoint endPoint = new IPEndPoint(mixerAddr, Port);
            _sendBytes = Encoding.ASCII.GetBytes("/status");
            ConnectMixer(endPoint);
        }

        private void ConnectMixer(IPEndPoint endPoint)
        {
            new Thread(() =>
            {
                while (true)
                {
                    _socket.SendTo(_sendBytes, endPoint);
                    try
                    {
                        _socket.Receive(_buffer);
                        Connect = true;
                        Connected?.Invoke(this, new EventArgs());
                        string text = Encoding.UTF8.GetString(_buffer);

                    }
                    catch
                    {
                        Connect = false;
                        NoConnected?.Invoke(this, new EventArgs());
                    }
                    Thread.Sleep(1000);
                }
            }).Start();
        }
    }
}
