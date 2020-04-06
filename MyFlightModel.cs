using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace FlightSimulatorApp
{
    class MyFlightModel : FlightModel
    {
        TelNetClient cNet;
        bool stop;
        public MyFlightModel(TelNetClient client)
        {
            this.cNet = client;
            stop = false;
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public double HEADING
        {
            get { return 0; }
            set { HEADING = value;
                NotifyPropertyChangedtify("HEADING");
            }
        }
        public double VERTICAL_SPEED
        {


            get { return 1; }
            set {
                VERTICAL_SPEED = value;
                NotifyPropertyChangedtify("VERTICAL_SPEED"); ; }
        }
        public double GROUND_SPEED
        {
            get { return 2; }
            set {
                GROUND_SPEED = value;
                NotifyPropertyChangedtify("GROUND_SPEED"); ; }
        }
        public double AIR_SPEED
        {
            get { return 3; }
            set {
                AIR_SPEED = value;
                NotifyPropertyChangedtify("AIR_SPEED"); ; }
        }
        public double ALTITUDE
        {
            get { return 4; }
            set {
                ALTITUDE = value;
                NotifyPropertyChangedtify("ALTITUDE"); ; }
        }
        public double ROLL
        {
            get { return 5; }
            set {
                ROLL = value;
                NotifyPropertyChangedtify("ROLL"); ; }
        }
        public double PITCH
        {
            get { return 6; }
            set {
                PITCH = value;
                NotifyPropertyChangedtify("PITCH"); ; }
        }
        public double ALTIMETER
        {
            get { return 7; }
            set {
                ALTIMETER = value;
                NotifyPropertyChangedtify("ALTIMETER"); ; }
        }

        // location update
        public double longitude_deg
        {
            get { return 0; }
            set
            {
                longitude_deg = value;
                NotifyPropertyChangedtify("longitude_deg"); ;
            }
        }

        public double latitude_deg
        {
            get { return 250; }
            set
            {
         latitude_deg = value;
                NotifyPropertyChangedtify("latitude_deg"); ;
            }
        }

        public double rudder { set => throw new NotImplementedException(); }
        public double elevator { set => throw new NotImplementedException(); }

        public void move(double rudder, double elevator)
        {
            cNet.write("set /controls/flight/rudder" + rudder + "\n");
            cNet.write("get /controls/flight/rudder" + "\n");
            string rudderTest = cNet.read();
            cNet.write("set /controls/flight/elevator " + elevator + "\n");
            cNet.write("get /controls/flight/elevator" + "\n");
            string elevatorTest = cNet.read();
        }

        public void changeSpeed(double throttle) {
            cNet.write("set /controls/engines/current-engine/throttle" + throttle + "\n");
            cNet.write("get /controls/engines/current-engine/throttle" + "\n");
            string throttleTest = cNet.read();
        }
        
        public void changeAileron(double aileron) {
            cNet.write("set /controls/flight/aileron " + aileron + "\n");
            cNet.write("get /controls/flight/aileron" + "\n");
            string aileronTest = cNet.read();
        }
        public void connect(string ip , int port)
        {
            cNet.connect(ip, port);
        }
        public void disConnect()
        {
            cNet.disconnect();

        }
        public  void start()
        {
            new Thread(delegate ()
            {
                while (!stop)
                {
                    cNet.write("get /instrumentation/heading-indicator/indicated-heading-de\n");
                    HEADING = Double.Parse(cNet.read());

                    cNet.write("get /instrumentation/gps/indicated-vertical-speed\n");
                    VERTICAL_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    GROUND_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    AIR_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    ALTITUDE = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    ROLL = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    PITCH = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    ALTIMETER = Double.Parse(cNet.read());

                    //location
                    cNet.write("get /position/longitude-deg\n");
                    longitude_deg = Double.Parse(cNet.read());

                    cNet.write("get /position/latitude-deg\n");
                    latitude_deg = Double.Parse(cNet.read());

                    Thread.Sleep(250);
                }
            });
        }
        public void NotifyPropertyChangedtify(string propName)
        {
            if(this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
