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
            get { return HEADING; }
            set { HEADING = value;
                NotifyPropertyChangedtify("HEADING");
            }
        }
        public double VERTICAL_SPEED
        {


            get { return VERTICAL_SPEED; }
            set {
                VERTICAL_SPEED = value;
                NotifyPropertyChangedtify("VERTICAL_SPEED"); ; }
        }
        public double GROUND_SPEED
        {
            get { return GROUND_SPEED; }
            set {
                GROUND_SPEED = value;
                NotifyPropertyChangedtify("GROUND_SPEED"); ; }
        }
        public double AIR_SPEED
        {
            get { return AIR_SPEED; }
            set {
                AIR_SPEED = value;
                NotifyPropertyChangedtify("AIR_SPEED"); ; }
        }
        public double ALTITUDE
        {
            get { return ALTITUDE; }
            set {
                ALTITUDE = value;
                NotifyPropertyChangedtify("ALTITUDE"); ; }
        }
        public double ROLL
        {
            get { return ROLL; }
            set {
                ROLL = value;
                NotifyPropertyChangedtify("ROLL"); ; }
        }
        public double PITCH
        {
            get { return PITCH; }
            set {
                PITCH = value;
                NotifyPropertyChangedtify("PITCH"); ; }
        }
        public double ALTIMETER
        {
            get { return ALTIMETER; }
            set {
                ALTIMETER = value;
                NotifyPropertyChangedtify("ALTIMETER"); ; }
        }

        public double rudder { set => throw new NotImplementedException(); }
        public double elevator { set => throw new NotImplementedException(); }

        void move(double rudder, double elevator, double throttle, double aileron)
        {
            cNet.write("set /controls/flight/rudder " + rudder + "\n");
            cNet.write("get /controls/flight/rudder" + "\n");
            string rudderTest = cNet.read();
            cNet.write("set /controls/flight/elevator " + elevator + "\n");
            cNet.write("get /controls/flight/elevator" + "\n");
            string elevatorTest = cNet.read();
            cNet.write("set /controls/engines/current-engine/throttle " + throttle + "\n");
            cNet.write("get /controls/engines/current-engine/throttle" + "\n");
            string throttleTest = cNet.read();
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

        void FlightModel.move(double rudder, double elevator, double throttle, double aileron)
        {
            throw new NotImplementedException();
        }
    }
}
