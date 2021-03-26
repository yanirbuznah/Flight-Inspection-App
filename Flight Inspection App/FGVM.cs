using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    public class FGVM : IViewModel
    {
        FGM _fgm;
        public FGVM(FGM fgm)
        {
            _fgm = fgm;
            _fgm.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                INotifyPropertyChanged("VM_" + e.PropertyName);
            };
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void INotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        public string VM_Path
        {
            get { return _fgm.Path; }//need to do the same for every component in Simulator.
        } // only need getter here !
        public void Connect()
        {
            _fgm.Connect("127.0.0.1",5400);
        }

        public void Disconnect()
        {
            _fgm.Disconnect();
        }

        public void Start()
        {
            _fgm.Start();
        }
    }
}
