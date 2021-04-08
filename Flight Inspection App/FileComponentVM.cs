using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class FileComponentVM : FGVM
    {
        public FileComponentVM(FGM m) : base(m)
        {

        }

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
        }
    }
}
