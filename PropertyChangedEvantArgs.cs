using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class propertyChangedEvantArgs
    {
        private string propName
        {
            get { return propName;}
            set { propName = value;}
        }
        public propertyChangedEvantArgs(string name)
        {
            this.propName = name;
        }
    }
}
