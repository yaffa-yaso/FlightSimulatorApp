using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using Microsoft.Maps.MapControl.WPF;

namespace FlightSimulatorApp
{
    class FlightViewModel : INotifyPropertyChanged
    {
        private FlightModel model;


        public FlightViewModel(FlightModel model)
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
            model.move(rudder, elevator);
        }
        public double VM_heading
        {
            get { return model.HEADING; }
        }
        public double VM_verticalSpeed
        {
            get { return model.VERTICAL_SPEED; }
        }
        public double VM_groundSpeed
        {
            get { return model.GROUND_SPEED; }
        }
        public double VM_airSpeed
        {
            get { return model.AIR_SPEED; }
        }
        public double VM_altitude
        {
            get { return model.ALTITUDE; }
        }
        public double VM_roll
        {
            get { return model.ROLL; }
        }
        public double VM_pitch
        {
            get { return model.PITCH; }
        }
        public double VM_altimeter
        {
            get { return model.ALTIMETER; }
        }

        // location
        public double VM_latitude
        {
            get { return model.latitude_deg; }
        }
        public double VM_longitude
        {
            get {return model.longitude_deg; }
        }

        //return the location 
        public Location VM_location
        {
            get { return new Location(VM_latitude, VM_longitude); }
        }
        public double VM_throttle
        {
            set { model.changeSpeed(value); }
        }
        public double VM_aileron
        {
            set { model.changeAileron(value); }
        }

        // simple getter
        public FlightModel getModel(){
            return model;
            }
    }
}
