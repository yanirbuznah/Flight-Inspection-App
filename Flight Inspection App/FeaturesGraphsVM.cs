using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Flight_Inspection_App.Commands;
using OxyPlot;

namespace Flight_Inspection_App
{
    class FeaturesGraphsVM : FGVM
    {



        public DetectCommand Detect { get; private set; }
        public FeaturesGraphsVM(FGM m) : base(m)
        {
            Detect = new DetectCommand(GenerateGraph);
            
        }


        public double VM_MostCorreltiveFeatureMaxValue
        {
            get => _fgm.MostCorreltiveFeatureMaxValue;
        }

        public double VM_MostCorreltiveFeatureMinValue
        {
            get => _fgm.MostCorreltiveFeatureMinValue;
        }
        public double VM_FeatureMinValue
        {
            get => _fgm.FeatureMinValue;
        }
        public double VM_FeatureMaxValue
        {
            get => _fgm.FeatureMaxValue;
        }

        public float VM_Slope
        {
            get => _fgm.Slope;
        }
        public float VM_Intercept
        {
            get => _fgm.Intercept;
        }

        public List<DataPoint> VM_CorrelationPoints
        {
            get => _fgm.CorrelationPoints;
        }

        public List<DataPoint> VM_lastCorrelationPoints
        {
            get => _fgm.lastCorrelationPoints;
        }

        public UserControl VM_Graph
        {
                       
            get => _fgm.Graph;
        }
        public void GenerateGraph()
        {
            _fgm.GenerateGraph();
        }

/*        public UserControl VM_GenerateGraph()
        {
            return _fgm.GenerateGraph();
        }*/

        public KeyValuePair<string, string> VM_FileDll
        {
            get { return _fgm.ThisDllFile; }

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
