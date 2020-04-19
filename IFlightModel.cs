using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;


namespace FlightSimulatorApp
{
    public interface IFlightModel : INotifyPropertyChanged
    {
        double Heading { set; get; }
        double VerticalSpeed { set; get; }
        double GroundSpeed { set; get; }
        double AirSpeed { set; get; }
        double Altitude { set; get; }
        double Roll { set; get; }
        double Pitch { set; get; }
        double Altimeter { set; get; }
        double Rudder { set; }
        double Elevator { set; }
        bool Connection { set; get; }
        bool OutOfBorder { set; get; }
        bool SlowReaction { set; get; }
        bool BoardErr { set; get; }
        bool ServerErr { set; get; }
        bool Reconnecting { set; get; }
        int Port { set; get; }
        string Address { set; get; }




        // MyLocation
        double LatitudeDeg { set; get; }
        double LongitudeDeg { set; get; }
        Location MyLocation { set; get; }





        void Connect();
        void DisConnect();
        void Start();
        bool IsConnected();
        bool FirstUpdate();

        void Move(double rudder, double elevator);
        void ChangeSpeed(double throttle);
        void ChangeAileron(double aileron);
        void MakeConnect();


    }
}
