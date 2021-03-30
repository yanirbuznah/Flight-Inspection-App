using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{

    public class Client
    {
        private TcpClient _client;
        private NetworkStream _ns;
        bool isRunning = false;
        public Client()
        {
            _client = new TcpClient(AddressFamily.InterNetwork);
        }

        public void Connect(string ip, int port)
        {
            try
            {
                _client.Connect(IPAddress.Parse(ip), port);
                _ns = _client.GetStream();
                isConnected = true;
            }
            catch
            {
                Debug.WriteLine("Connect Error");
            }

        }
        public NetworkStream getNs()
        {
            return _ns;
        }
       
        public void Disconnect()
        {
            _ns.Close();
            _client.Close();
            isConnected = false;
        }
        

        public void setStatus (bool val)
        {
            isRunning = val;
        }
        public bool getStatus()
        {
            return isRunning;
        }
        public bool isConnected
        {
            get; set;
        }


    }
}
