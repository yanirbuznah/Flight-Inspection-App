using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{
    class FGM : IModel
    {
        public event PropertyChangedEventHandler PropertyChanged;
        Client _telnetClient;
        public FGM (Client client){
            _telnetClient = client;
        }
        public void connect(string ip, int port)
        {
            throw new NotImplementedException();
        }

        public void disconnect()
        {
            throw new NotImplementedException();
        }

        public void start()
        {
            throw new NotImplementedException();
        }
    }
}
