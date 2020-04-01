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

        }

        public void movePlain(double rudder, double elevator, double throttle, double aileron)
        {
            model.move(rudder, elevator, throttle, aileron);
        }
        public double NM_heading
        {
            get { return model.HEADING; }
        }
        public double VM_verticalSpeed
        {
            get { return model.VERTICAL_SPEED; }
        }
        public double VM_groudSpeed
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
        public double VM_alitimeter
        {
            get { return model.ALTIMETER; }
        }

        // location
        public double VM_latitude
        {
            get { return 0; }
        }
        public double VM_longitude
        {
            get { return 250; }
        }

        //return the location 
        public Location VM_location
        {
            get { return new Location(VM_latitude, VM_longitude); }
        }

        // simple getter
        public FlightModel getModel(){
            return model;
            }
    }
}
