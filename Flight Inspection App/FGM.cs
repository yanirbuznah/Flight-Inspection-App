using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Flight_Inspection_App
{
    public class FGM : IModel
    {
        private KeyValuePair<string, string> _file;
        List<string> _featuresNames;
        private int _port = 5400;

        private float speed = 1;
        private float sleepTime = 100;

        private System.IO.StreamReader streamreader;

        private string _ip = "127.0.0.1";
        bool isStopped = false;
        private ManualResetEvent wh = new ManualResetEvent(true);
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
        public FGM(Client client) {
            _telnetClient = client;
            var xmlPlaybackFilepath = Path.Combine("../../../", "playback_small.xml");
            XElement purchaseOrder = XElement.Load(xmlPlaybackFilepath);
            IEnumerable<string> partNos = purchaseOrder.Descendants("name").Select(x => (string)x);
            _featuresNames = partNos.Distinct().ToList();

        }


        public void Connect()
        {
            _telnetClient.Connect(_ip, _port);
        }

        public void Disconnect()
        {
            _telnetClient.Disconnect();
        }
        public KeyValuePair<string, string> File
        {
            get
            { return _file; }

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

            new Thread(() =>
            {
                isStopped = false;
                if (_telnetClient.isConnected)
                {
                    var file = new System.IO.StreamReader(_file.Key);
                    string line;
                    while (!isStopped && (line = file.ReadLine()) != null)
                    {
                        wh.WaitOne(Timeout.Infinite);
                        line += "\r\n";
                        Console.WriteLine(line);
                        _telnetClient.getNs().Write(System.Text.Encoding.ASCII.GetBytes(line));
                        _telnetClient.getNs().Flush();
                        Thread.Sleep((int)sleepTime);
                    }
                    file.Close();
                }
            }).Start();
        }


        public float Speed
        {
            get { return speed; }
            set
            {
                if (speed != value)
                {
                    speed = value;
                    sleepTime = 100 / speed;
                    OnPropertyChanged();
                }
            }
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
        public List<string> FeaturesNames { get { return _featuresNames; } }

        public bool getStatus()
        {
            return _telnetClient.getStatus();
        }
        public void setStatus(bool val)
        {
            _telnetClient.setStatus(val);
        }
        public void pauseThread()
        {
            wh.Reset();
        }
        public void continueThread()
        {
            if (isStopped)
            {
                Start();
            }
            else
            {
                wh.Set();
            }
         
        }
        public void stopSimulatorThread()
        {
            wh.Set();//Maybe improve that the client sees 10 pixels before the stop.
            isStopped = true;
        }

        public void increaseSpeed()
        {
            Speed = Speed + (float)0.1;
        }


        public void decreaseSpeed()
        {
            if((speed - (float)0.1) > 0)
                Speed = Speed - (float)0.1;
        }
    }
}
