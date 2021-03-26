using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App 
{
    
    public interface IViewModel : INotifyPropertyChanged
    {
        public void Connect();
        public void Disconnect();
        public void Start ();
        
    }
}
