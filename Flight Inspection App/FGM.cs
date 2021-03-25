using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    public class FGM : IModel
    {
        
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
        string path = @"C:\Users\yanir\Desktop\flightgearProject\reg_flight.csv";
        public FGM (Client client){
            _telnetClient = client;
        }
        public void Connect(string ip, int port)
        {
            _telnetClient.Connect(ip, port);
        }

        public void Disconnect()
        {
            _telnetClient.Disconnect();
        }


        public void Start()
        {
            _telnetClient.Write(path);
        }


    }
}
