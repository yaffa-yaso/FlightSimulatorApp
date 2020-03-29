using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightSimulatorApp
{
    class FlightViewModel: notifyPropertyChanged
    {
        private FlightModel model;
        public FlightViewModel(FlightModel model)
        {
            this.model = model;
            model.pChange;


        }

        public event PropertyChangedEvantHandler pChange;

        public void Notify(string propName)
        {
           
        }
        public double HEADING
        {
            get { return HEADING; }
            set
            {
                HEADING = value;
                Notify("HEADING");
            }
        }
        public double VERTICAL_SPEED
        {


            get { return VERTICAL_SPEED; }
            set
            {
                VERTICAL_SPEED = value;
                Notify("VERTICAL_SPEED"); ;
            }
        }
        public double GROUND_SPEED
        {
            get { return GROUND_SPEED; }
            set
            {
                GROUND_SPEED = value;
                Notify("GROUND_SPEED"); ;
            }
        }
        public double AIR_SPEED
        {
            get { return AIR_SPEED; }
            set
            {
                AIR_SPEED = value;
                Notify("AIR_SPEED"); ;
            }
        }
        public double ALTITUDE
        {
            get { return ALTITUDE; }
            set
            {
                ALTITUDE = value;
                Notify("ALTITUDE"); ;
            }
        }
        public double ROLL
        {
            get { return ROLL; }
            set
            {
                ROLL = value;
                Notify("ROLL"); ;
            }
        }
        public double PITCH
        {
            get { return PITCH; }
            set
            {
                PITCH = value;
                Notify("PITCH"); ;
            }
        }
        public double ALTIMETER
        {
            get { return ALTIMETER; }
            set
            {
                ALTIMETER = value;
                Notify("ALTIMETER"); ;
            }
        }
    }
}
