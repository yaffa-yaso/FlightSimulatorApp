using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
namespace FlightSimulatorApp


{
    class MyFlightModel : FlightModel
    {
        TelNetClient cNet;
        bool stop;
        bool isInitialized;
        bool outOfBorder ;

        public MyFlightModel(TelNetClient client)
        {
            this.cNet = client;
            stop = false;
            isInitialized = false;
            outOfBorder = false;
            location = new Location(latitude_deg, longitude_deg);

        }
        public event PropertyChangedEventHandler PropertyChanged;


        private double _HEADING = 0;
        public double HEADING
        {
            get { return _HEADING; }
            set
            {
                _HEADING = value;
                NotifyPropertyChangedtify("HEADING");
            }
        }
        private double _VERTICAL_SPEEDg = 0;
        public double VERTICAL_SPEED
        {


            get { return _VERTICAL_SPEEDg; }
            set
            {
                _VERTICAL_SPEEDg = value;
                NotifyPropertyChangedtify("VERTICAL_SPEED"); ;
            }
        }
        private double _GROUND_SPEED = 0;
        public double GROUND_SPEED
        {
            get { return _GROUND_SPEED; }
            set
            {
                _GROUND_SPEED = value;
                NotifyPropertyChangedtify("GROUND_SPEED"); ;
            }
        }
        private double _AIR_SPEED = 0;
        public double AIR_SPEED
        {
            get { return _AIR_SPEED; }
            set
            {
                _AIR_SPEED = value;
                NotifyPropertyChangedtify("AIR_SPEED"); ;
            }
        }
        private double _ALTITUDE = 0;
        public double ALTITUDE
        {
            get { return _ALTITUDE; }
            set
            {
                _ALTITUDE = value;
                NotifyPropertyChangedtify("ALTITUDE"); ;
            }
        }
        private double _ROLL = 0;
        public double ROLL
        {
            get { return _ROLL; }
            set
            {
                _ROLL = value;
                NotifyPropertyChangedtify("ROLL"); ;
            }
        }
        private double _PITCH = 0;
        public double PITCH
        {
            get { return _PITCH; }
            set
            {
                _PITCH = value;
                NotifyPropertyChangedtify("PITCH"); ;
            }
        }
        private double _ALTIMETER = 0;
        public double ALTIMETER
        {
            get { return _ALTIMETER; }
            set
            {
                _ALTIMETER = value;
                NotifyPropertyChangedtify("ALTIMETER"); ;
            }
        }

        // location update
        private double _longitude_deg= 34.8854;

        public double longitude_deg
        {
            get { return _longitude_deg; }
            set
            {
                if(value>90 || value < -90)
                {
                    this.outOfBorder = true;
                }
                _longitude_deg = value;
                NotifyPropertyChangedtify("longitude_deg"); ;
            }
        }

        private double _latitude_deg= 32.0055;
        public double latitude_deg
        {
            get { return _latitude_deg; }
            set
            {
                if (value > 180 || value < -180)
                {
                    this.outOfBorder = true;
                }
                _latitude_deg = value;
                NotifyPropertyChangedtify("latitude_deg"); ;
            


        }
    }

        private Location _location;
        public Location location
        {
            get { return _location; }
            set
            {
                _location = value;
            NotifyPropertyChangedtify("Location"); ;
            }
        }
        public double rudder { set => throw new NotImplementedException(); }
        public double elevator { set => throw new NotImplementedException(); }

        public void move(double rudder, double elevator)
        {
            cNet.write("set /controls/flight/rudder " + rudder + "\n");
            cNet.write("get /controls/flight/rudder " + "\n");
            string rudderTest = cNet.read();
            cNet.write("set /controls/flight/elevator " + elevator + "\n");
            cNet.write("get /controls/flight/elevator " + "\n");
            string elevatorTest = cNet.read();
        }

        public void changeSpeed(double throttle)
        {
            cNet.write("set /controls/engines/current-engine/throttle " + throttle + "\n");
            cNet.write("get /controls/engines/current-engine/throttle " + "\n");
            string throttleTest = cNet.read();
        }

        public void changeAileron(double aileron)
        {
            cNet.write("set /controls/flight/aileron " + aileron + "\n");
            cNet.write("get /controls/flight/aileron " + "\n");
            string aileronTest = cNet.read();
        }

        public void connect(string ip, int port)
        {
            cNet.connect(ip, port);


        }
        public void disConnect()
        {
            cNet.disconnect();

        }
        public void start()
        {


            new Thread(delegate ()
            {
                Console.WriteLine(stop);

                while (!stop)
                {



                    string answer;
                    cNet.write("get /instrumentation/heading-indicator/indicated-heading-de\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        HEADING = Double.Parse(answer);
                        Console.WriteLine(HEADING);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }
                    cNet.write("get /instrumentation/gps/indicated-vertical-speed\n");

                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        VERTICAL_SPEED = Double.Parse(answer);
                        Console.WriteLine(VERTICAL_SPEED.ToString());
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }

                    cNet.write("get /instrumentation/gps/indicated-ground-speed-kt\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        GROUND_SPEED = Double.Parse(answer);
                        Console.WriteLine(GROUND_SPEED);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }
                    cNet.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        AIR_SPEED = Double.Parse(answer);
                        Console.WriteLine(AIR_SPEED);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }

                    cNet.write("get /instrumentation/gps/indicated-altitude-ft\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        ALTITUDE = Double.Parse(answer);
                        Console.WriteLine(ALTITUDE);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }
                    cNet.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        ROLL = Double.Parse(answer);
                        Console.WriteLine(ROLL);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }
                    cNet.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        PITCH = Double.Parse(answer);
                        Console.WriteLine(PITCH);
                    }
                    else
                    {
                        Console.WriteLine(answer);

                    }
                    cNet.write("get /instrumentation/altimeter/indicated-altitude-ft\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        ALTIMETER = Double.Parse(answer);
                        Console.WriteLine(ALTIMETER);
                    }
                    //location

                    cNet.write("get /position/longitude-deg\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        longitude_deg = Double.Parse(answer);
                        Console.WriteLine("longitude_deg" + longitude_deg);
                    }
                    cNet.write("get /position/latitude-deg\n");
                    answer = cNet.read();
                    if (IsNumber(answer))
                    {
                        latitude_deg = Double.Parse(answer);
                        Console.WriteLine("latitude_deg" + latitude_deg);
                    }
                    //update location
                    location = new Location(latitude_deg, longitude_deg);
                    
                
                    isInitialized = true;
                    Thread.Sleep(250);


                }
            }).Start();
        }
        public void NotifyPropertyChangedtify(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }
        public bool isConnected()
        {
            return this.cNet.isConnected();
        }

        // check if the answer's string is a number(double)
        public bool IsNumber(string text)
        {
            Double num = 0;
            bool IsNumber = false;

            // Check for empty string.
            if (string.IsNullOrEmpty(text))
            {
                return false;
            }

            IsNumber = Double.TryParse(text, out num);

            return IsNumber;
        }
        public bool firstUpdate()
        {
            return isInitialized;
        }
    }
}
