using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class MultiDimensionalModelVM :FGVM
    {
        public MultiDimensionalModelVM(FGM m) : base(m) { }
        public double VM_YawDegrees
        {
            get { return _fgm.YawDegrees; }
        }
        public double VM_RollDegrees
        {
            get { return _fgm.RollDegrees; }
        }
        public double VM_PitchDegrees
        {
            get { return _fgm.PitchDegrees; }
        }

        public double VM_XxAxis
        {
            get => _fgm.XxAxis;
        }
        public double VM_YxAxis
        {
            get => _fgm.YxAxis;
        }
        public double VM_ZxAxis
        {
            get => _fgm.ZxAxis;
        }
        public double VM_XyAxis
        {
            get => _fgm.XyAxis;
        }
        public double VM_YyAxis
        {
            get => _fgm.YyAxis;

        }
        public double VM_ZyAxis
        {
            get => _fgm.ZyAxis;
        }
        public double VM_XzAxis
        {
            get => _fgm.XzAxis;
        }
        public double VM_YzAxis
        {
            get => _fgm.YzAxis;


        }
        public double VM_ZzAxis
        {
            get => _fgm.ZzAxis;
        }
    }
}
