using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class FeaturesPanelVM : FGVM
    {
        public FeaturesPanelVM(FGM m) : base(m)
        {

        }

        public List<Feature> VM_FeaturesNames { get { return _fgm.Features; } }

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
