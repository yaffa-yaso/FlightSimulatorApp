using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    interface FlightModel: notifyPropertyChanged
    {
       double HEADING { set; get; }
       double VERTICAL_SPEED { set; get; }
       double GROUND_SPEED { set; get; }
       double AIR_SPEED { set; get; }
       double ALTITUDE { set; get; }
       double ROLL { set; get; }
       double PITCH { set; get; }
       double ALTIMETER { set; get; }


        void connect(string ip, int port);
        void disConnect();
        void start();

        // add properties
        
    }
}
