using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    public class PropertyChangedEvantArgs
    {
        private string name;
        private double newValue;
        public PropertyChangedEvantArgs(string name)
        {
            this.name = name;
        }
        public string getName()
        {
            return this.name;
        }
        public double getNewValue() 
        {
            return this.newValue;
        }

    }
}
