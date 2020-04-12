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
        private int flag = 0;


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
        public double VM_HEADING
        {
            get { return model.HEADING; }
        }
        public double VM_VERTICAL_SPEED
        {
            get { return model.VERTICAL_SPEED; }
        }
        public double VM_GROUND_SPEED
        {
            get { return model.GROUND_SPEED; }
        }
        public double VM_AIR_SPEED
        {
            get { return model.AIR_SPEED; }
        }
        public double VM_ALTITUDE
        {
            get { return model.ALTITUDE; }
        }
        public double VM_ROLL
        {
            get { return model.ROLL; }
        }
        public double VM_PITCH
        {
            get { return model.PITCH; }
        }
        public double VM_ALTIMETER
        {
            get { return model.ALTIMETER; }
        }

        // location
        public double VM_latitude_deg
        {
            get { return model.latitude_deg; }
        }
        public double VM_longitude_deg
        {
            get { return model.longitude_deg; }
        }

        public double VM_Throttle
        {
            set {
                if (flag == 1)
                {
                    flag = 1;
                }
                model.changeSpeed(value);
                flag = 1;
            }
        }

        public double VM_Aileron
        {
            set { model.changeAileron(value); }
        }

        //return the location 
        public Location VM_location
        {
            get { return model.location; }
        }


        public bool VM_Connection
        {
            get { return model.Connection; }

        }

        public bool VM_outOfBorder
        {
            get { return model.outOfBorder; }

        }

        
        // simple getter
        public FlightModel getModel(){
            return model;
            }
    }
}
