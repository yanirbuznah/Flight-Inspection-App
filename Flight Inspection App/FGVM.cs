using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class FGVM : IViewModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        FGM _fgm;
        public FGVM(FGM fgm)
        {
            _fgm = fgm;
        }
    }
}
