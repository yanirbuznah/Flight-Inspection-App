using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class JoystickVM : FGVM
    {
        public JoystickVM(FGM m) : base(m)
        {
            
        }
        public double VM_Elevator
        {
            get { return _fgm.Elevator; }
        }

        public double VM_Aileron
        {
            get { return _fgm.Aileron; }
        }
    }
}
