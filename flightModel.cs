using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;


namespace FlightSimulatorApp
{
    interface FlightModel: INotifyPropertyChanged
    {
       double HEADING { set; get; }
       double VERTICAL_SPEED { set; get; }
       double GROUND_SPEED { set; get; }
       double AIR_SPEED { set; get; }
       double ALTITUDE { set; get; }
       double ROLL { set; get; }
       double PITCH { set; get; }
       double ALTIMETER { set; get; }
       double rudder { set; }
       double elevator { set; }
        bool Connection { set; get; }
        bool outOfBorder { set; get; }
        bool SlowReaction { set; get; }
        int Port { set; get; }
        string Address { set; get; }


        // location
        double latitude_deg { set; get; }
        double longitude_deg { set; get; }
        Location location { set; get; }





        void connect(string ip, int port);
        void disConnect();
        void start();
        bool isConnected();
        bool firstUpdate();

        void move(double rudder, double elevator);
        void changeSpeed(double throttle);
        void changeAileron(double aileron);
        
    }
}
