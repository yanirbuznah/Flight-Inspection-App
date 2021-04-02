﻿using Flight_Inspection_App.Commands;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Flight_Inspection_App
{
    public class FGVM : IViewModel
    {
        private readonly FGM _fgm;
        public PauseCommand PauseTheFlight { get; private set; }
        public PlayCommand PlayTheFlight { get; private set; }
        public PlusCommand IncreaseTheSpeed { get; private set; }
        public MinusCommand DecreaseTheSpeed { get; private set; }
        public StopCommand StopTheFlight { get; private set; }

        public FGVM(FGM fgm)
        {
            _fgm = fgm;
            _fgm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            PauseTheFlight = new PauseCommand(PauseThread);
            PlayTheFlight = new PlayCommand(ContinueRunning);
            IncreaseTheSpeed = new PlusCommand(IncreaseSpeed);
            DecreaseTheSpeed = new MinusCommand(DecreaseSpeed);
            StopTheFlight = new StopCommand(StopFlight);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
        }



        public string VM_Ip
        {
            get { return _fgm.Ip; }
            set
            {
                if (_fgm.Ip != value)
                {
                    _fgm.Ip = value;
                    OnPropertyChanged();
                }
            }
        }
        public string VM_VideoSpeed
        {
            get { return _fgm.VideoSpeed; }
            set
            {
                if (_fgm.VideoSpeed != value)
                {
                    _fgm.VideoSpeed = value;
                    OnPropertyChanged();
                }
            }
        }


        public int VM_Port
        {
            get { return _fgm.Port; }
            set
            {
                if (_fgm.Port != value)
                {
                    _fgm.Port = value;
                    OnPropertyChanged();
                }
            }
        }

        public string VM_Altitude
        {
            get { return _fgm.Altitude; }
        }
        public string VM_AirSpeed
        {
            get { return _fgm.AirSpeed; }
        }
        public string VM_FlightDirection
        {
            get { return _fgm.FlightDirection; }
        }

        public string VM_FlightTime
        {
            get { return _fgm.FlightTime; }
        }


        public string VM_CurrentFlightTime
        {
            get { return _fgm.CurrentFlightTime; }
            set
            {
                if (_fgm.CurrentFlightTime != value)
                {
                    _fgm.CurrentFlightTime = value;
                    OnPropertyChanged();
                }
            }
        }

        public int VM_CurrentLineIndex
        {
            get { return _fgm.CurrentLineIndex; }
            set
            {
                _fgm.CurrentLineIndex = value;
                OnPropertyChanged();
            }
        }

        public float VM_Aileron
        {
            get { return _fgm.Aileron; }
        }
        public string VM_Throttle
        {
            get => _fgm.Throttle;
        }
        public string VM_Rudder
        {
            get => _fgm.Rudder;
        }
        public float VM_Elevator
        {
            get { return _fgm.Elevator; }
        }
        public string VM_HeadingDegrees
        {
            get { return _fgm.HeadingDegrees; }
        }
        public string VM_RollDegrees
        {
            get { return _fgm.RollDegrees; }
        }
        public string VM_PitchDegrees
        {
            get { return _fgm.PitchDegrees; }
        }
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        public List<Feature> VM_FeaturesNames { get { return _fgm.Features; } }


        public KeyValuePair<string, string> VM_File
        {
            get { return _fgm.ThisFile; }
            set
            {
                if (_fgm.ThisFile.Key != value.Key)
                {
                    _fgm.ThisFile = value;
                    OnPropertyChanged();
                }
            }
            //need to do the same for every component in Simulator.
        }
        public void Connect()
        {
            _fgm.Connect();
        }

        public void Disconnect()
        {
            _fgm.Disconnect();
        }

        public void Start()
        {
            _fgm.Start();
            _fgm.SetStatus(true);
        }
        public void PauseThread()
        {
            _fgm.PauseThread();
        }
        public void ContinueRunning()
        {
            _fgm.ContinueThread();
        }
        public void StopFlight()
        {
            _fgm.StopSimulatorThread();
        }

        public void IncreaseSpeed()
        {
            _fgm.IncreaseSpeed();
        }


        public void DecreaseSpeed()
        {
            _fgm.DecreaseSpeed();
        }

        /*        private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
                {
                    if (!Equals(field, newValue))
                    {
                        field = newValue;
                        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                        return true;
                    }
                    return false;
                }*/

        public string VM_Graph_Title
        {
            get
            {
                return _fgm.Graph_Title;
            }
        }
        public IList<DataPoint> VM_Graph_Points
        {
            get
            {
                return _fgm.Graph_Points;
            }
        }

        public Feature VM_IntresingFeature
        {
            get
            {
                return _fgm.IntresingFeature;
            }
            set
            {
                if (_fgm.IntresingFeature != value)
                {
                    _fgm.IntresingFeature = value;
                    OnPropertyChanged();

                }
            }
        }
    }
}
