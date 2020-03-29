using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp: 
{
    public delegate void PropertyChangedEvantHandler(Object sender, propertyChangedEvantArgs e);
    interface notifyPropertyChanged
    {
        event PropertyChangedEvantHandler pChange;
         void Notify(string propName);
    }
}
