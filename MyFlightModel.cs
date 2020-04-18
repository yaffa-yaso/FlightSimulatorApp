using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Maps.MapControl.WPF;
using System.Diagnostics;
namespace FlightSimulatorApp


{
    class MyFlightModel : FlightModel
    {
        TelNetClient cNet;
        bool stop;
        bool isInitialized;
        private Stopwatch stopWatch = new Stopwatch();

        public MyFlightModel(TelNetClient client)
        {
            this.cNet = client;
            stop = false;
            isInitialized = false;
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
        private double _VERTICAL_SPEED = 0;
        public double VERTICAL_SPEED
        {


            get { return _VERTICAL_SPEED; }
            set
            {
                _VERTICAL_SPEED = value;
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


        // outOfborder error

        private bool _outOfBorder;

        public bool outOfBorder
        {
            get { return _outOfBorder; }
            set
            {

                _outOfBorder = value;
                NotifyPropertyChangedtify("outOfBorder"); ;
            }
        }

        // slowReaction error
        bool slowReaction = false;
        public bool SlowReaction
        {
            get { return slowReaction; }
            set
            {
                slowReaction = value;
                NotifyPropertyChangedtify("SlowReaction");
            }
        }

        int _Port = 5402;
        public int Port
        {
            get { return _Port; }
            set
            {
                _Port = value;
                NotifyPropertyChangedtify("Port");
            }
        }

        string _Address = "127.0.0.1";
        public string Address
        {
            get { return _Address; }
            set
            {
                _Address = value;
                NotifyPropertyChangedtify("Address");
            }
        }

        bool boardErr;
        public bool BoardErr
        {
            get { return boardErr; }
            set
            {
                boardErr = value;
                NotifyPropertyChangedtify("BoardErr");
            }
        }

        bool reconnecting;
        public bool Reconnecting
        {
            get { return reconnecting; }
            set
            {
                reconnecting = value;
                NotifyPropertyChangedtify("Reconnecting");
            }
        }

        // location update
        private double _longitude_deg = 34.8854;

        public double longitude_deg
        {
            get { return _longitude_deg; }
            set
            {
                if (value > 180 || value < -180)
                {
                    outOfBorder = true;
                }
                else
                {
                    _longitude_deg = value;
                    NotifyPropertyChangedtify("longitude_deg"); ;
                }
            }
        }

        private double _latitude_deg = 32.0055;
        public double latitude_deg
        {
            get { return _latitude_deg; }
            set
            {
                if (value > 77.5 || value < 77.5)
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
                NotifyPropertyChangedtify("location"); ;
            }
        }

        private bool _Connection;
        public bool Connection
        {
            get { return _Connection; }
            set
            {
                _Connection = value;
                NotifyPropertyChangedtify("Connection"); ;
            }
        }
        public bool _ServerErr;

        public bool ServerErr
        {
            get { return _ServerErr; }
            set
            {
                _ServerErr = value;
                NotifyPropertyChangedtify("ServerErr"); ;
            }
        }


        public double rudder { set => throw new NotImplementedException(); }
        public double elevator { set => throw new NotImplementedException(); }

        public void move(double rudder, double elevator)
        {
            if (_Connection)
            {
                cNet.write("set /controls/flight/rudder " + rudder + "\n");
                string rudderTest = cNet.read();
                cNet.write("get /controls/flight/rudder " + "\n");
                 rudderTest = cNet.read();
                cNet.write("set /controls/flight/elevator " + elevator + "\n");
                string elevatorTest = cNet.read();
                cNet.write("get /controls/flight/elevator " + "\n");
                 elevatorTest = cNet.read();
            }
        }

        public void changeSpeed(double throttle)
        {
            if (_Connection)
            {
                cNet.write("set /controls/engines/current-engine/throttle " + throttle + "\n");
                string throttleTest = cNet.read();
                cNet.write("get /controls/engines/current-engine/throttle " + "\n");
                 throttleTest = cNet.read();
            }
        }

        public void changeAileron(double aileron)
        {
            if (_Connection)
            {
                cNet.write("set /controls/flight/aileron " + aileron + "\n");
                string aileronTest = cNet.read();
                cNet.write("get /controls/flight/aileron " + "\n");
                 aileronTest = cNet.read();
            }
        }

        public void connect()
        {
            ServerErr = false;
            SlowReaction = false;
            stop = false;
            cNet.connect(this.Address, this.Port);
            if (isConnected() != true)
            {
                ServerErr = true;
            }
        }
        public void disConnect()
        {
            stop = true;
            cNet.disconnect();
            Connection = false;

        }
        public void start()
        {


            new Thread(delegate ()
            {
                Console.WriteLine(stop);

                while (!stop)
                {

                    try
                    {

                        string answer;
                        cNet.write("get /instrumentation/heading-indicator/indicated-heading-deg\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            HEADING = Double.Parse(answer);
                            Console.WriteLine("HEADING" + HEADING);
                        }
                        else
                        {
                            HEADING = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        cNet.write("get /instrumentation/gps/indicated-vertical-speed\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            VERTICAL_SPEED = Double.Parse(answer);
                            Console.WriteLine("VERTICAL_SPEED" + VERTICAL_SPEED);
                        }
                        else
                        {
                            VERTICAL_SPEED = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }

                        cNet.write("get /instrumentation/gps/indicated-ground-speed-kt\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            GROUND_SPEED = Double.Parse(answer);
                            Console.WriteLine("GROUND_SPEED" + GROUND_SPEED);
                        }
                        else
                        {
                            GROUND_SPEED = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        cNet.write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            AIR_SPEED = Double.Parse(answer);
                            Console.WriteLine("AIR_SPEED" + AIR_SPEED);
                        }
                        else
                        {
                            AIR_SPEED = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }

                        cNet.write("get /instrumentation/gps/indicated-altitude-ft\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            ALTITUDE = Double.Parse(answer);
                            Console.WriteLine("ALTITUDE" + ALTITUDE);
                        }
                        else
                        {
                            ALTITUDE = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        cNet.write("get /instrumentation/attitude-indicator/internal-roll-deg\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            ROLL = Double.Parse(answer);
                            Console.WriteLine("ROLL" + ROLL);
                        }
                        else
                        {
                            ROLL = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        cNet.write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            PITCH = Double.Parse(answer);
                            Console.WriteLine("PITCH" + PITCH);
                        }
                        else
                        {
                            PITCH = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        cNet.write("get /instrumentation/altimeter/indicated-altitude-ft\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            ALTIMETER = Double.Parse(answer);
                            Console.WriteLine("ALTIMETER" + ALTIMETER);
                        }
                        else
                        {
                            ALTIMETER = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        //location

                        cNet.write("get /position/longitude-deg\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            longitude_deg = Double.Parse(answer);
                            Console.WriteLine("longitude_deg" + longitude_deg);
                        }
                        cNet.write("get /position/latitude-deg\n");

                        stopWatch.Start();
                        answer = cNet.read();
                        stopWatch.Stop();
                        if (stopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        stopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            latitude_deg = Double.Parse(answer);
                            Console.WriteLine("latitude_deg" + latitude_deg);
                        }
                        //update location
                        location = new Location(latitude_deg, longitude_deg);


                        isInitialized = true;
                        Thread.Sleep(350);
                    }
                    catch(Exception E)
                    {
                        ServerErr = true;
                        Console.WriteLine("server error");
                        disConnect();
                        this.cNet.getMutex().ReleaseMutex();
                    }


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
        public void makeConnect()
        {
            Connection = true ;
        }
    }
}
