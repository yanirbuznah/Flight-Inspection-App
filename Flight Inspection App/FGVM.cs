using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Flight_Inspection_App.Commands;

namespace Flight_Inspection_App
{
    public class FGVM : IViewModel
    {
        FGM _fgm;
        public PauseCommand PauseTheFlight { get;private set; }
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
            IncreaseTheSpeed = new PlusCommand(increaseSpeed);
            DecreaseTheSpeed = new MinusCommand(decreaseSpeed);
            StopTheFlight = new StopCommand(StopFlight);

        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
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
        public float VM_VideoSpeed
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
                    _fgm.Start();
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
            _fgm.setStatus(true);
        }
        public void PauseThread()
        {
            _fgm.pauseThread();
        }
        public void ContinueRunning()
        {
            _fgm.continueThread();
        }
        public void StopFlight()
        {
            _fgm.stopSimulatorThread(); 
        }
        
        public void increaseSpeed()
        {
            _fgm.increaseSpeed();
        }

        public void decreaseSpeed()
        {
            _fgm.decreaseSpeed();
        }

        private bool SetProperty<T>(ref T field, T newValue, [CallerMemberName] string propertyName = null)
        {
            if (!Equals(field, newValue))
            {
                field = newValue;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
                return true;
            }
            return false;
        }


    }
}
