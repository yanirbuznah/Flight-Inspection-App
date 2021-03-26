using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    interface IModel: INotifyPropertyChanged
    {
        void Connect();
        void Disconnect();
        void Start();
    }
}
