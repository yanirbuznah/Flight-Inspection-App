using System;
using System.Collections.Generic;
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
                Console.WriteLine("Connect Error");
            }

        }
        public void Disconnect()
        {
            _ns.Close();
            _client.Close();
            isConnected = false;
        }
        public void Write(string path)
        {
            if (isConnected)
            {
                var file = new System.IO.StreamReader(path);
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    line += "\r\n";
                    Console.WriteLine(line);
                    _ns.Write(System.Text.Encoding.ASCII.GetBytes(line));
                    _ns.Flush();
                    Thread.Sleep(10);
                }
                file.Close();
            }
        }

        public bool isConnected
        {
            get; set;
        }


    }
}
