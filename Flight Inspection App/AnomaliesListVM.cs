using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class AnomaliesListVM : FGVM
    {
        public AnomaliesListVM(FGM m) : base(m)
        {

        }

        public List<KeyValuePair<int, string>> VM_AnomaliesDescriptions { get { return _fgm.AnomaliesDescriptions; } }

        public KeyValuePair<int, string> VM_AnomalyDescription 
        {
            get
            {
                return _fgm.AnomaliesDescription;
            }

            set
            {
                _fgm.AnomaliesDescription = value;
                OnPropertyChanged();
            }
        }
    }
}
