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
        private string path;
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
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
         public string Path
        {
            get
            {return path; }

            set
            {
                path = value;
                INotifyPropertyChanged("Path");
            }
        }

        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        public void Start()
        {
            _telnetClient.Write(path);
        }


    }
}
