using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flight_Inspection_App
{

    public class Feature
    {
        public string Name { get; set; }
        private List<string> _Values = new List<string>();

        public List<string> Values {
            get { return _Values; }
        }
        public void AddValue(string value)
        {
            _Values.Add(value);
        }
    }

}
