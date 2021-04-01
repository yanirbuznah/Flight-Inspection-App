using CsvHelper;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
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
        List<Feature> _features;
        private int _port = 5400;
        private float videoSpeed = 1;
        private float sleepTime = 100;
        private string _ip = "127.0.0.1";
        bool isStopped = false;
        int currentLineIndex;
        string currentFlightTime;
        string flightTime;
        int numOfRows=0;
        private ManualResetEvent wh = new ManualResetEvent(true);
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
        public FGM(Client client)
        {
            _telnetClient = client;
            var xmlPlaybackFilepath = Path.Combine("../../../", "playback_small.xml");
            XElement playbackXml = XElement.Load(xmlPlaybackFilepath);
            IEnumerable<string> chunkNames = playbackXml.Descendants("name").Select(x => (string)x);
            List<string> featursNames = chunkNames.ToList();
            _features = featursNames.Select(s => new Feature() { Name = s }).ToList();
        }


        public void Connect()
        {
            _telnetClient.Connect(_ip, _port);
        }

        public void Disconnect()
        {
            _telnetClient.Disconnect();
        }
        public KeyValuePair<string, string> ThisFile
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

        private void UpdateFeaturesValues(string[] arrCsv)
        {


            for (int i = 0; i < arrCsv.Length; i++)
            {
                List<String> listStrLineElements = arrCsv[i].Split(',').ToList();
                for (int j = 0; j < 42; ++j)
                {
                    _features[j].AddValue(listStrLineElements[j]);
                }

            }
        }

        public void Start()
        {
            string[] arrCsv;
            arrCsv = File.ReadAllLines(_file.Key);
            numOfRows = arrCsv.Length;
            FlightTime = numOfRows.ToString();
            UpdateFeaturesValues(arrCsv);
            new Thread(() =>
            {
                isStopped = false;
                if (_telnetClient.isConnected)
                {
                    string line;
                    currentLineIndex = 0;
                    for (; currentLineIndex < arrCsv.Length && !isStopped; ++currentLineIndex)
                    {
                        Altitude = _features[16].Values[currentLineIndex];
                        RollDegrees = _features[17].Values[currentLineIndex];
                        PitchDegrees = _features[18].Values[currentLineIndex];
                        HeadingDegrees = _features[19].Values[currentLineIndex];
                        AirSpeed = _features[21].Values[currentLineIndex];
                        FlightDirection = _features[37].Values[currentLineIndex];
                        line = arrCsv[currentLineIndex];
                        CurrentFlightTime = TimeSpan.FromSeconds(((arrCsv.Length - currentLineIndex) / 10)).ToString(@"hh\:mm\:ss"); 
                        wh.WaitOne(Timeout.Infinite);
                        //UpdateFeaturesValues(line);
                        line += "\r\n";
                        Console.WriteLine(line);
                        _telnetClient.getNs().Write(System.Text.Encoding.ASCII.GetBytes(line));
                        _telnetClient.getNs().Flush();
                        CurrentLineIndex = currentLineIndex;
                        Thread.Sleep((int)sleepTime);
                    }

                }
            }).Start();
        }


        public float VideoSpeed
        {
            get { return videoSpeed; }
            set
            {
                if (videoSpeed != value)
                {
                    videoSpeed = value;
                    sleepTime = 100 / videoSpeed;
                    OnPropertyChanged();
                }
            }
        }
        public string FlightTime
        {
            get { return flightTime; }
            set
            {
                if(flightTime != value)
                {
                    flightTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public int CurrentLineIndex
        {
            get { return currentLineIndex; }
            set
            {
                    currentLineIndex = value;
                    OnPropertyChanged();
            }
        }
        public int GetNumOfRows()
        {
            return numOfRows;
        }
       

        public string CurrentFlightTime
        {
            get { return currentFlightTime; }
            set
            {
                if (currentFlightTime != value)
                {
                    currentFlightTime = value;
                    OnPropertyChanged();
                }
            }
        }
        string altitude = "0";
        public string Altitude
        {
            get { return altitude; }
            private set
            {
                altitude = value;
                OnPropertyChanged();
            }
        }
        string _airSpeed = "0";
        public string AirSpeed
        {
            get { return _airSpeed; }
            private set
            {
                _airSpeed = value;
                OnPropertyChanged();
            }
        }
        string _flightDirection = "0";
        public string FlightDirection
        {
            get { return _flightDirection; }
            private set
            {
                _flightDirection = value;
                OnPropertyChanged();
            }
        }
        string _headingDegrees = "0";
        public string HeadingDegrees
        {
            get { return _headingDegrees; }
            private set
            {
                _headingDegrees = value;
                OnPropertyChanged();
            }
        }
        string _rollDegrees = "0";
        public string RollDegrees
        {
            get { return _rollDegrees; }
            private set
            {
                _rollDegrees = value;
                OnPropertyChanged();
            }
        }
        string _pitchDegrees = "0";
        public string PitchDegrees
        {
            get { return _pitchDegrees; }
            private set
            {
                _pitchDegrees = value;
                OnPropertyChanged();
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
        public List<Feature> Features { get { return _features; } }

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
            VideoSpeed += (float)0.1;
        }


        public void decreaseSpeed()
        {
            if ((videoSpeed - (float)0.1) > 0)
                VideoSpeed -= (float)0.1;
        }
    }
}