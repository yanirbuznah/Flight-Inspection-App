﻿using System;
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
        public PauseCommand StopTheFlight { get;private set; }
        public PlayCommand PlayTheFlight { get; private set; }
        public FGVM(FGM fgm)
        {
            _fgm = fgm;
            _fgm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_" + e.PropertyName);
            };
            StopTheFlight = new PauseCommand(PauseThread);
            PlayTheFlight = new PlayCommand(ContinueRunning);
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
        public int VM_SleepTime
        {
            get { return _fgm.SleepTime; }
            set
            {
                if (_fgm.SleepTime != value)
                {
                    _fgm.SleepTime = value;
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
        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }



        public KeyValuePair<string, string> VM_File
        {
            get { return _fgm.File; }
            set
            {
                if (_fgm.File.Key != value.Key)
                {
                    _fgm.File = value;
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
        
    }
}
