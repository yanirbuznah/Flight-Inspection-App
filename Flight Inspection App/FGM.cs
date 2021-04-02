using CsvHelper;
using OxyPlot;
using OxyPlot.Series;
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
        private readonly List<Feature> _features;
        private int _port = 5400;
        private float _videoSpeed = 1;
        private float _sleepTime = 100;
        private string _ip = "127.0.0.1";
        bool _isStopped = false;
        private int _currentLineIndex;
        private  string _currentFlightTime;
        private  string _flightTime;
        private  int _numOfCols = 0;
        private readonly ManualResetEvent wh = new(true);
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Client _telnetClient;
        public FGM(Client client)
        {
            _telnetClient = client;
            var xmlPlaybackFilepath = Path.Combine("../../../", "playback_small.xml");
            XElement playbackXml = XElement.Load(xmlPlaybackFilepath);
            IEnumerable<string> chunkNames = playbackXml.Descendants("output").Descendants("name").Select(name => (string)name);
            List<string> featursNames = chunkNames.ToList();
            _features = featursNames.Select(s => new Feature() { Name = s }).ToList();
            _numOfCols = _features.Capacity;
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
                _isStopped= true;
                Thread.Sleep((int)_sleepTime);
                Start();
            }
        }

        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void CalcFeaturesValues(string[] arrCsv)
        {


            for (int i = 0; i < arrCsv.Length; i++)
            {
                List<string> listStrLineElements = arrCsv[i].Split(',').ToList();
                for (int j = 0; j < _numOfCols; ++j)
                {
                    _features[j].AddPoint(i, listStrLineElements[j]);
                }
            }
        }


        private void UpdateFeaturesValues()
        {
            Aileron = 100 * float.Parse(_features[0].Values[_currentLineIndex]);
            Elevator = 100 * float.Parse(_features[1].Values[_currentLineIndex]);
            Rudder = _features[2].Values[_currentLineIndex];
            Throttle = _features[6].Values[_currentLineIndex];
            Altitude = _features[16].Values[_currentLineIndex];
            RollDegrees = _features[17].Values[_currentLineIndex];
            PitchDegrees = _features[18].Values[_currentLineIndex];
            HeadingDegrees = _features[19].Values[_currentLineIndex];
            AirSpeed = _features[21].Values[_currentLineIndex];
            FlightDirection = _features[37].Values[_currentLineIndex];
        }

        public void Start()
        {
            string[] arrCsv;
            arrCsv = File.ReadAllLines(_file.Key);
            NumOfRows = arrCsv.Length;
            FlightTime = NumOfRows.ToString();
            CalcFeaturesValues(arrCsv);

                new Thread(() =>
                {
                     _isStopped = false;
                    string line;
                    CurrentLineIndex = 0;
                    for (; CurrentLineIndex < arrCsv.Length && !_isStopped; ++CurrentLineIndex)
                    {

                        UpdateFeaturesValues();
                        line = arrCsv[_currentLineIndex] +"\r\n";
                        CurrentFlightTime = TimeSpan.FromSeconds(((arrCsv.Length - CurrentLineIndex) / 10)).ToString(@"hh\:mm\:ss");
                        wh.WaitOne(Timeout.Infinite);
                        Console.WriteLine(line);
                        if (_telnetClient.IsConnected)
                        {
                            _telnetClient.GetNs().Write(System.Text.Encoding.ASCII.GetBytes(line));
                            _telnetClient.GetNs().Flush();
                        }
                        NotifyPropertyChanged("Graph_Points");
                        Thread.Sleep((int)_sleepTime);
                    }

                }).Start();
        }


        public string VideoSpeed
        {
            get { return _videoSpeed.ToString("F"); }
            set
            {
                if (_videoSpeed.ToString("F") != value)
                {
                    _videoSpeed = float.Parse(value);
                    _sleepTime = 100 / _videoSpeed;
                    OnPropertyChanged();
                }
            }
        }

        public void IncreaseSpeed()
        {
            float newSpeed = _videoSpeed + (float)0.1;
            VideoSpeed = newSpeed.ToString("F");
        }


        public void DecreaseSpeed()
        {
            float newSpeed = _videoSpeed - (float)0.1;
            if (newSpeed > 0)
                VideoSpeed = newSpeed.ToString("F");
        }
        
        public string Graph_Title {
            get
            {
                if (IntresingFeature != null)
                    return IntresingFeature.Name;
                return "";
            }

        }


        public IList<DataPoint> Graph_Points
        {
            get
            {
                if (IntresingFeature == null)
                    return new List<DataPoint>();
                return new List<DataPoint>(IntresingFeature.Points.Take(CurrentLineIndex));
            }

        }

        public string FlightTime
        {
            get => _flightTime;
            set
            {
                if (_flightTime != value)
                {
                    _flightTime = value;
                    OnPropertyChanged();
                }
            }
        }
        public int CurrentLineIndex
        {
            get => _currentLineIndex;
            set
            {
                _currentLineIndex = value;
                OnPropertyChanged();
            }
        }
        public int NumOfRows
        {
            get;set;
        }


        public string CurrentFlightTime
        {
            get => _currentFlightTime;
            set
            {
                if (_currentFlightTime != value)
                {
                    _currentFlightTime = value;
                    OnPropertyChanged();
                }
            }
        }
        string altitude = "0";
        public string Altitude
        {
            get => altitude;
            private set
            {
                altitude = value;
                OnPropertyChanged();
            }
            
        }
        string _airSpeed = "0";
        public string AirSpeed
        {
            get => _airSpeed;
            private set
            {
                _airSpeed = value;
                OnPropertyChanged();
            }
        }
        string _flightDirection = "0";
        public string FlightDirection
        {
            get => _flightDirection;
            private set
            {
                _flightDirection = value;
                OnPropertyChanged();
            }
        }
        string _headingDegrees = "0";
        public string HeadingDegrees
        {
            get => _headingDegrees;
            private set
            {
                _headingDegrees = value;
                OnPropertyChanged();
            }
        }
        string _rollDegrees = "0";
        public string RollDegrees
        {
            get => _rollDegrees;
            private set
            {
                _rollDegrees = value;
                OnPropertyChanged();
            }
        }
        string _pitchDegrees = "0";
        public string PitchDegrees
        {
            get => _pitchDegrees;
            private set
            {
                _pitchDegrees = value;
                OnPropertyChanged();
            }
        }
        float _aileron = 0;
        public float Aileron
        {
            get => _aileron;
            private set
            {
                _aileron = value;
                OnPropertyChanged();
            }
        }
        float _elevator = 0;
        public float Elevator
        {
            get => _elevator;
            private set
            {
                _elevator = value;
                OnPropertyChanged();
            }
        }
        string _throttle = "0";
        public string Throttle
        {
            get => _throttle;
            set
            {
                _throttle = value;
                OnPropertyChanged();
            }
        }
       string _rudder = "0";
        public string Rudder
        {
            get => _rudder;
            set
            {
                _rudder = value;
                OnPropertyChanged();
            }
        }
        public string Ip
        {
            get => _ip;
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
            get => _port;
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
        private Feature _intresingFeature; 
        public Feature IntresingFeature
        {
            get => _intresingFeature;
            
            set
            {
                _intresingFeature = value;
                OnPropertyChanged();
            }
        }
        public bool GetStatus()
        {
            return _telnetClient.GetStatus();
        }
        public void SetStatus(bool val)
        {
            _telnetClient.SetStatus(val);
        }
        public void PauseThread()
        {
            wh.Reset();
        }
        public void ContinueThread()
        {
            if (_isStopped)
            {
                Start();
            }
            else
            {
                wh.Set();
            }

        }
        public void StopSimulatorThread()
        {
            wh.Set();
            _isStopped = true;
        }
    }
}