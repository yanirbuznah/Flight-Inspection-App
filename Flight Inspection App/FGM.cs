using OxyPlot;
using OxyPlot.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Windows.Controls;
using System.Xml.Linq;


namespace Flight_Inspection_App
{
    public class FGM : IModel
    {
        private KeyValuePair<string, string> _csvFile;
        private KeyValuePair<string, string> _dllFile;
        private readonly List<Feature> _features;
        private int _port = 5400;
        private float _videoSpeed = 1;
        private float _sleepTime = 100;
        private string _ip = "127.0.0.1";
        bool _isStopped = false;
        private int _currentLineIndex;
        private string _currentFlightTime;
        private string _flightTime;
        private int _numOfCols = 0;
        private readonly ManualResetEvent wh = new(true);
        public event PropertyChangedEventHandler PropertyChanged;
        private readonly Client _telnetClient;
/*        private Annotation _detectorAnnotation;*/
        MethodInfo Detect, LearnNormal, GetAnnotation, GetAnomalies;
        object Detector;


        private UserControl _graph;
        public FGM(Client client)
        {
            _telnetClient = client;
            var xmlPlaybackFilepath = Path.Combine("../../../", "playback_small.xml");
            XElement playbackXml = XElement.Load(xmlPlaybackFilepath);
            IEnumerable<string> chunkNames = playbackXml.Descendants("output").Descendants("name").Select(name => (string)name);
            List<string> featursNames = chunkNames.ToList();
            int i = 0;
            _features = featursNames.Select(s => new Feature() { Name = s,Index = i++ }).ToList();
            _numOfCols = _features.Capacity;
            //IntresingFeature = _features[0];
        }


        public void Connect()
        {
            _telnetClient.Connect(_ip, _port);
        }

        public void Disconnect()
        {
            _telnetClient.Disconnect();
        }
        public KeyValuePair<string, string> ThisCsvFile
        {
            get
            { return _csvFile; }

            set
            {
                _csvFile = value;
                if (_dllFile.Key != null)
                {
                    Detect.Invoke(Detector, new object[] { _csvFile.Key });
                }
                OnPropertyChanged();
                _isStopped = true;
                Thread.Sleep((int)_sleepTime);
                Start();
            }
        }
        public KeyValuePair<string, string> ThisDllFile
        {
            get
            { return _dllFile; }

            set
            {
                _dllFile = value;
                
                    var a = Assembly.LoadFile(_dllFile.Key);
                    // Get the type to use.
                    Type myType = a.GetType("DetectorLibary.AnomalyDetector");
                    Detector = Activator.CreateInstance(myType);
                    // Get the method to call.

                    Detect = myType.GetMethod("Detect");
                    LearnNormal = myType.GetMethod("LearnNormal");
                    GetAnnotation = myType.GetMethod("GetAnnotation");
                    GetAnomalies = myType.GetMethod("GetAnomalies");


                    // Execute the method.
                    LearnNormal.Invoke(Detector, new object[] { @"..\..\..\Normal flight\reg_flight - Copy.csv" });
                if(_csvFile.Key != null)
                {
                    Detect.Invoke(Detector, new object[] { _csvFile.Key });

                }


                OnPropertyChanged();

            }
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void UpdateCorrelationFeatures()
        {
            for (int i = 0; i < _numOfCols; ++i)
            {

                double corrlation = 0;

                for (int j = 0; j < _numOfCols; ++j)
                {
                    if (i != j)
                    {
                        double newCorrlation = Math.Abs(
                                AnomalyDetectionUtil.Pearson(_features[i].Values, _features[j].Values,NumOfRows ));

                        if (corrlation <= newCorrlation)
                        {
                             corrlation = newCorrlation;
                            _features[i].MostCorrelativeFeature = _features[j];
                           

                        }
                    }
                }
               
            }
            _features.ForEach((x) => x.CalcCorrelationPoints());
            _features.ForEach((x) => x.CalcLineRegresion());
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
            UpdateCorrelationFeatures();
            
        }


        private void UpdateFeaturesValues()
        {
            Aileron = 100 * _features[0].Values[_currentLineIndex];
            Elevator = 100 * _features[1].Values[_currentLineIndex];
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
            arrCsv = File.ReadAllLines(_csvFile.Key);
            arrCsv = arrCsv.Skip(1).ToArray();
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
                        OnPropertyChanged("FeaturePoints");
                        OnPropertyChanged("MostCorreltiveFeaturePoints");
                        OnPropertyChanged("lastCorrelationPoints");

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
        
        public string FeatureTitle
        {
            get
            {
                if (IntresingFeature != null)
                    return IntresingFeature.Name;
                return "";
            }

        }

        public List<DataPoint> AnomalyPoints
        {
            get
            {
                if (IntresingFeature == null || Detector == null)
                    return new List<DataPoint>();
                return (List<DataPoint>)GetAnomalies.Invoke(Detector, new object[] { IntresingFeature.Index });
            }

        }


        public Annotation Annotation
        {
            get
            {
                if (IntresingFeature == null || Detector == null)
                    return null;
                return (Annotation)GetAnnotation.Invoke(Detector, new object[] { IntresingFeature.Index });
            }


        }
        public IList<DataPoint> FeaturePoints
        {
            get
            {
                if (IntresingFeature == null)
                    return new List<DataPoint>();
                return new List<DataPoint>(IntresingFeature.Points.Take(CurrentLineIndex));
            }

        }
        public string MostCorreltiveFeatureTitle
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.MostCorrelativeFeature == null)
                    return "";
                return IntresingFeature.MostCorrelativeFeature.Name;
            }

        }


        public IList<DataPoint> MostCorreltiveFeaturePoints
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.MostCorrelativeFeature == null)
                    return new List<DataPoint>();
                return new List<DataPoint>(IntresingFeature.MostCorrelativeFeature.Points.Take(CurrentLineIndex));
            }

        }
        public double MostCorreltiveFeatureMaxValue
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.MostCorrelativeFeature == null)
                    return 0;
                return IntresingFeature.MostCorrelativeFeature.MaxValue;
            }
        }

        public double MostCorreltiveFeatureMinValue
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.MostCorrelativeFeature == null)
                    return 0;
                return IntresingFeature.MostCorrelativeFeature.MinValue;
            }
        }
        public double FeatureMinValue
        {
            get
            {
                if (IntresingFeature != null)
                    return IntresingFeature.MinValue;
                return 0;
            }
        }
        public float Slope
        {
            get
            {
                if (IntresingFeature== null )
                    return 0;
                return IntresingFeature.Slope;

            }
        }
        public float Intercept
        {
            get
            {
                if (IntresingFeature == null )
                    return 0;
                return IntresingFeature.Intercept;

            }
        }
        public double FeatureMaxValue
        {
            get
            {
                if (IntresingFeature != null)
                    return IntresingFeature.MaxValue;
                return 0;
            }
        }
        public List<DataPoint> CorrelationPoints
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.CorrelationPoints == null)
                    return new List<DataPoint>();
                return new List<DataPoint>(IntresingFeature.CorrelationPoints);
            }
        }

        public List<DataPoint> lastCorrelationPoints
        {
            get
            {
                if (IntresingFeature == null || IntresingFeature.CorrelationPoints == null)
                    return new List<DataPoint>();
                return new List<DataPoint>(IntresingFeature.CorrelationPoints.Skip(CurrentLineIndex-300).Take(CurrentLineIndex));
            }
        }
        public void GenerateGraph()
        {
            Assembly a = Assembly.LoadFile(_dllFile.Key);
            // Get the type to use.
            Type myType = a.GetType("WpfLibrary.Graph");
            UserControl Graph = Activator.CreateInstance(myType) as UserControl;
            _graph =  Graph;
            // Get the method to call.
            /*
                        MethodInfo detect = myType.GetMethod("Detect");
                        MethodInfo setIntres = myType.GetMethod("setIntrestingFeatureIndex");
                        MethodInfo currentTime = myType.GetMethod("CurrentTime");*/


            // Create an instance.

            // Execute the method.
            /*            detect.Invoke(Graph, new object[] { @"C:\Users\yanir\Desktop\flightgearProject\anomaly_flight.csv" });
                        setIntres.Invoke(Graph, new object[] { 5 });
                        currentTime.Invoke(Graph, new object[] { 300 });*/
            //Detector.Children.Add(Graph);
            /*            setIntres.Invoke(Graph, new object[] { 0 });*/

        }
        public UserControl Graph
        {
            get => _graph;
            set
            {
                if (value != _graph)
                    _graph = value;
                OnPropertyChanged();
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
            get; set;
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
        double altitude = 0;
        public double Altitude
        {
            get => altitude;
            private set
            {
                altitude = value;
                OnPropertyChanged();
            }

        }
        double _airSpeed = 0;
        public double AirSpeed
        {
            get => _airSpeed;
            private set
            {
                _airSpeed = value;
                OnPropertyChanged();
            }
        }
        double _flightDirection = 0;
        public double FlightDirection
        {
            get => _flightDirection;
            private set
            {
                _flightDirection = value;
                OnPropertyChanged();
            }
        }
        double _headingDegrees = 0;
        public double HeadingDegrees
        {
            get => _headingDegrees;
            private set
            {
                _headingDegrees = value;
                OnPropertyChanged();
            }
        }
        double _rollDegrees = 0;
        public double RollDegrees
        {
            get => _rollDegrees;
            private set
            {
                _rollDegrees = value;
                OnPropertyChanged();
            }
        }
        double _pitchDegrees = 0;
        public double PitchDegrees
        {
            get => _pitchDegrees;
            private set
            {
                _pitchDegrees = value;
                OnPropertyChanged();
            }
        }
        double _aileron = 0;
        public double Aileron
        {
            get => _aileron;
            private set
            {
                _aileron = value;
                OnPropertyChanged();
            }
        }
        double _elevator = 0;
        public double Elevator
        {
            get => _elevator;
            private set
            {
                _elevator = value;
                OnPropertyChanged();
            }
        }
        double _throttle = 0;
        public double Throttle
        {
            get => _throttle;
            set
            {
                _throttle = value;
                OnPropertyChanged();
            }
        }
       double _rudder = 0;
        public double Rudder
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

        void ChangedIntrestingFeatureStates()
        {
            OnPropertyChanged("MostCorreltiveFeatureMinValue");
            OnPropertyChanged("MostCorreltiveFeatureMaxValue");
            OnPropertyChanged("FeatureMaxValue");
            OnPropertyChanged("FeatureMinValue");
            OnPropertyChanged("FeatureTitle");
            OnPropertyChanged("MostCorreltiveFeatureTitle");
            OnPropertyChanged("Slope");
            OnPropertyChanged("Intercept");
            OnPropertyChanged("CorrelationPoints");
            OnPropertyChanged("AnomalyPoints");
            OnPropertyChanged("Annotation");

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
                ChangedIntrestingFeatureStates();


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