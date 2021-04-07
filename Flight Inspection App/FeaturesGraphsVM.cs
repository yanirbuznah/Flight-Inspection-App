using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OxyPlot;

namespace Flight_Inspection_App
{
    class FeaturesGraphsVM : FGVM
    {
        public FeaturesGraphsVM(FGM m) : base(m)
        {

        }
        public string VM_FeatureTitle
        {
            get
            {
                return _fgm.FeatureTitle;
            }
        }

        public string VM_MostCorreltiveFeatureTitle
        {
            get
            {
                return _fgm.MostCorreltiveFeatureTitle;
            }
        }

        public IList<DataPoint> VM_FeaturePoints
        {
            get
            {
                return _fgm.FeaturePoints;
            }
        }

        public IList<DataPoint> VM_MostCorreltiveFeaturePoints
        {
            get
            {
                return _fgm.MostCorreltiveFeaturePoints;
            }
        }
    }
}
