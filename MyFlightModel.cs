using System;
using System.Collections.Generic;
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
        public event PropertyChangedEvantHandler pChange;
        public double HEADING
        {
            get { return HEADING; }
            set { HEADING = value;
                Notify("HEADING");
            }
        }
        public double VERTICAL_SPEED
        {


            get { return VERTICAL_SPEED; }
            set {
                VERTICAL_SPEED = value;
                Notify("VERTICAL_SPEED"); ; }
        }
        public double GROUND_SPEED
        {
            get { return GROUND_SPEED; }
            set {
                GROUND_SPEED = value;
                Notify("GROUND_SPEED"); ; }
        }
        public double AIR_SPEED
        {
            get { return AIR_SPEED; }
            set {
                AIR_SPEED = value;
                Notify("AIR_SPEED"); ; }
        }
        public double ALTITUDE
        {
            get { return ALTITUDE; }
            set {
                ALTITUDE = value;
                Notify("ALTITUDE"); ; }
        }
        public double ROLL
        {
            get { return ROLL; }
            set {
                ROLL = value;
                Notify("ROLL"); ; }
        }
        public double PITCH
        {
            get { return PITCH; }
            set {
                PITCH = value;
                Notify("PITCH"); ; }
        }
        public double ALTIMETER
        {
            get { return ALTIMETER; }
            set {
                ALTIMETER = value;
                Notify("ALTIMETER"); ; }
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
                    cNet.write("get /instrumentation/heading-indicator/indicated-heading-deg");
                    HEADING = Double.Parse(cNet.read());

                    cNet.write("get /instrumentation/gps/indicated-vertical-speed");
                    VERTICAL_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/gps/indicated-ground-speed-kt");
                    GROUND_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/airspeed-indicator/indicated-speed-kt");
                    AIR_SPEED = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/gps/indicated-altitude-ft");
                    ALTITUDE = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/attitude-indicator/internal-roll-deg");
                    ROLL = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/attitude-indicator/internal-pitch-deg");
                    PITCH = Double.Parse(cNet.read());


                    cNet.write("get /instrumentation/altimeter/indicated-altitude-ft");
                    ALTIMETER = Double.Parse(cNet.read());
                    Thread.Sleep(250);


                }
            });

        }

        public void Notify(string propName)
        {
            if(this.pChange != null)
            {
                this.pChange(this, new PropertyChangedEvantArgs(propName));
            }
        }

    }
}
