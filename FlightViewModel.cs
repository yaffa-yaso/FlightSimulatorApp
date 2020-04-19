using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    public class FlightViewModel : INotifyPropertyChanged
    {
        private IFlightModel model;

        public FlightViewModel(IFlightModel model)
        {
            this.model = model;
            model.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e) {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        public void movePlain(double rudder, double elevator)
        {
            model.Move(rudder, elevator);
        }
        public double VM_Heading
        {
            get { return model.Heading; }
        }
        public double VM_VerticalSpeed
        {
            get { return model.VerticalSpeed; }
        }
        public double VM_GroundSpeed
        {
            get { return model.GroundSpeed; }
        }
        public double VM_AirSpeed
        {
            get { return model.AirSpeed; }
        }
        public double VM_Altitude
        {
            get { return model.Altitude; }
        }
        public double VM_Roll
        {
            get { return model.Roll; }
        }
        public double VM_Pitch
        {
            get { return model.Pitch; }
        }
        public double VM_Altimeter
        {
            get { return model.Altimeter; }
        }

        // Location
        public double VM_LatitudeDeg
        {
            get { return model.LatitudeDeg; }
        }
        public double VM_LongitudeDeg
        {
            get { return model.LongitudeDeg; }
        }

        public double VM_Throttle
        {
            set
            {
                model.ChangeSpeed(value);
            }
        }

        public double VM_Aileron
        {
            set { model.ChangeAileron(value); }
        }

        //return the Location 
        public Location VM_Location
        {
            get { return model.Location; }
        }


        public bool VM_Connection
        {
            get { return model.Connection; }

        }

        public bool VM_OutOfBorder
        {
            get { return model.OutOfBorder; }

        }
        public bool VM_SlowReaction
        {
            get { return model.SlowReaction; }

        }
        public int VM_Port
        {
            get { return model.Port; }

            set { model.Port = value; }

        }
        public bool VM_BoardErr
        {
            get { return model.BoardErr; }

        }
        public bool VM_ServerErr
        {
            get { return model.ServerErr; }

        }

        public string VM_Address
        {
            get { return model.Address; }
            set { model.Address = value; }

        }

        // simple getter
        public IFlightModel GetModel()
        {
            return model;
        }

        public void VM_Connect()
        {
            this.model.Connect();
        }
        public void VM_MakeConnect()
        {
            this.model.MakeConnect();
        }
        public void VM_Start()
        {
            this.model.Start();
        }

    }
}
