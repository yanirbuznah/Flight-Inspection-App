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
        
        public event PropertyChangedEventHandler PropertyChanged;
        FGM _fgm;
        public FGVM(FGM fgm)
        {
            _fgm = fgm;
        }

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
