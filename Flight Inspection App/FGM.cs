using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    public class FGM : IModel
    {
        private KeyValuePair<string,string> _file = new (@"C:\Users\yanir\Desktop\flightgearProject\reg_flight.csv", "reg_flight.csv");
        private int _port = 5400;
        private string _ip = "127.0.0.1";
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
        public FGM (Client client){
            _telnetClient = client;
        }
        public void Connect()
        {
            _telnetClient.Connect(_ip, _port);
        }

        public void Disconnect()
        {
            _telnetClient.Disconnect();
        }
         public KeyValuePair<string,string> File
        {
            get
            {return _file; }

            set
            {
                _file = value;
                OnPropertyChanged();
                Start();
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void Start()
        {
            _telnetClient.Write(_file.Key);
        }

        public string Ip
        {
            get { return _ip; }
            set
            {
                if (_ip != value)
                {
                    _ip = value;
                    OnPropertyChanged();
                }
            }
        }

        public int Port
        {
            get { return _port; }
            set
            {
                if (_port != value)
                {
                    _port = value;
                    OnPropertyChanged();
                }
            }
        }

    }
}
