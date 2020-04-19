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
    class MyFlightModel : IFlightModel
    {
        static TelNetClient CNet;
        bool Stop;
        bool IsInitialized;
        private Stopwatch StopWatch = new Stopwatch();

        public MyFlightModel(TelNetClient client)
        {
            CNet = client;
            Stop = false;
            IsInitialized = false;
            Location = new Location(LatitudeDeg, LongitudeDeg);

        }
        public event PropertyChangedEventHandler PropertyChanged;


        private double heading = 0;
        public double Heading
        {
            get { return heading; }
            set
            {
                heading = value;
                NotifyPropertyChangedtify("Heading");
            }
        }
        private double verticalSpeed = 0;
        public double VerticalSpeed
        {


            get { return verticalSpeed; }
            set
            {
                verticalSpeed = value;
                NotifyPropertyChangedtify("VerticalSpeed"); ;
            }
        }
        private double groundSpeed = 0;
        public double GroundSpeed
        {
            get { return groundSpeed; }
            set
            {
                groundSpeed = value;
                NotifyPropertyChangedtify("GroundSpeed"); ;
            }
        }
        private double airSpeed = 0;
        public double AirSpeed
        {
            get { return airSpeed; }
            set
            {
                airSpeed = value;
                NotifyPropertyChangedtify("AirSpeed"); ;
            }
        }
        private double altitude = 0;
        public double Altitude
        {
            get { return altitude; }
            set
            {
                altitude = value;
                NotifyPropertyChangedtify("Altitude"); ;
            }
        }
        private double roll = 0;
        public double Roll
        {
            get { return roll; }
            set
            {
                roll = value;
                NotifyPropertyChangedtify("Roll"); ;
            }
        }
        private double pitch = 0;
        public double Pitch
        {
            get { return pitch; }
            set
            {
                pitch = value;
                NotifyPropertyChangedtify("Pitch"); ;
            }
        }
        private double altimeter = 0;
        public double Altimeter
        {
            get { return altimeter; }
            set
            {
                altimeter = value;
                NotifyPropertyChangedtify("Altimeter"); ;
            }
        }


        // outOfborder error

        private bool outOfBorder;

        public bool OutOfBorder
        {
            get { return outOfBorder; }
            set
            {

                outOfBorder = value;
                NotifyPropertyChangedtify("OutOfBorder"); ;
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

        int port = 5402;
        public int Port
        {
            get { return port; }
            set
            {
                port = value;
                NotifyPropertyChangedtify("Port");
            }
        }

        string address = "127.0.0.1";
        public string Address
        {
            get { return address; }
            set
            {
                address = value;
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

        // Location update
        private double longitudeDeg = 34.8854;

        public double LongitudeDeg
        {
            get { return longitudeDeg; }
            set
            {
                if (value > 180 || value < -180)
                {
                    OutOfBorder = true;
                }
                else
                {
                    longitudeDeg = value;
                    NotifyPropertyChangedtify("Longitude_deg"); ;
                }
            }
        }

        private double latitudeDeg = 32.0055;
        public double LatitudeDeg
        {
            get { return latitudeDeg; }
            set
            {
                if (value > 77.5 || value < -77.5)
                {
                    this.OutOfBorder = true;
                }
                latitudeDeg = value;
                NotifyPropertyChangedtify("Latitude_deg"); ;
            }
        }

        private Location location;
        public Location Location
        {
            get { return location; }
            set
            {
                location = value;
                NotifyPropertyChangedtify("Location"); ;
            }
        }

        private bool connection;
        public bool Connection
        {
            get { return connection; }
            set
            {
                connection = value;
                NotifyPropertyChangedtify("Connection"); ;
            }
        }
        public bool serverErr;

        public bool ServerErr
        {
            get { return serverErr; }
            set
            {
                serverErr = value;
                NotifyPropertyChangedtify("ServerErr"); ;
            }
        }


        public double Rudder { set => throw new NotImplementedException(); }
        public double Elevator { set => throw new NotImplementedException(); }

        public void Move(double rudder, double elevator)
        {
            if (Connection)
            {
                bool lockWasTaken = false;
                try
                {
                    Monitor.Enter(CNet, ref lockWasTaken);
                    CNet.Write("set /controls/flight/rudder " + rudder + "\n");
                    string rudderTest = CNet.Read();
                    CNet.Write("get /controls/flight/rudder " + "\n");
                    rudderTest = CNet.Read();
                    CNet.Write("set /controls/flight/elevator " + elevator + "\n");
                    string elevatorTest = CNet.Read();
                    CNet.Write("get /controls/flight/elevator " + "\n");
                    elevatorTest = CNet.Read();
                }
                finally
                {
                    if (lockWasTaken)
                    {
                        Monitor.Exit(CNet);
                    }
                }
            }
        }

        public void ChangeSpeed(double throttle)
        {
            if (Connection)
            {

                bool LockWasTaken = false;
                try
                {
                    Monitor.Enter(CNet, ref LockWasTaken);
                    CNet.Write("set /controls/engines/current-engine/throttle " + throttle + "\n");
                    string throttleTest = CNet.Read();
                    CNet.Write("get /controls/engines/current-engine/throttle " + "\n");
                    throttleTest = CNet.Read();
                }
                finally
                {
                    if (LockWasTaken)
                    {
                        Monitor.Exit(CNet);
                    }
                }
            }
        }

        public void ChangeAileron(double aileron)
        {
            if (Connection)
            {
                bool LockWasTaken = false;
                try
                {
                    Monitor.Enter(CNet, ref LockWasTaken);
                    CNet.Write("set /controls/flight/aileron " + aileron + "\n");
                    string aileronTest = CNet.Read();
                    CNet.Write("get /controls/flight/aileron " + "\n");
                    aileronTest = CNet.Read();
                }
                finally
                {
                    if (LockWasTaken)
                    {
                        Monitor.Exit(CNet);
                    }
                }
            }
        }

        public void Connect()
        {
            ServerErr = false;
            SlowReaction = false;
            Stop = false;
            CNet.Connect(this.Address, this.Port);
            Thread.Sleep(1000);
            Connection = IsConnected();
        }
        public void DisConnect()
        {
            Stop = true;
            CNet.Disconnect();
            Connection = false;

        }
        public void Start()
        {


            new Thread(delegate ()
            {
                Console.WriteLine(Stop);

                while (!Stop)
                {

                    try
                    {

                        string answer;
                        CNet.Write("get /instrumentation/heading-indicator/indicated-heading-deg\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            Heading = Double.Parse(answer);
                            Console.WriteLine("HEADING" + Heading);
                        }
                        else
                        {
                            Heading = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        CNet.Write("get /instrumentation/gps/indicated-vertical-speed\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            VerticalSpeed = Double.Parse(answer);
                            Console.WriteLine("VERTICAL_SPEED" + VerticalSpeed);
                        }
                        else
                        {
                            VerticalSpeed = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }

                        CNet.Write("get /instrumentation/gps/indicated-ground-speed-kt\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            GroundSpeed = Double.Parse(answer);
                            Console.WriteLine("GROUND_SPEED" + GroundSpeed);
                        }
                        else
                        {
                            GroundSpeed = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        CNet.Write("get /instrumentation/airspeed-indicator/indicated-speed-kt\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            AirSpeed = Double.Parse(answer);
                            Console.WriteLine("AIR_SPEED" + AirSpeed);
                        }
                        else
                        {
                            AirSpeed = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }

                        CNet.Write("get /instrumentation/gps/indicated-altitude-ft\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            Altitude = Double.Parse(answer);
                            Console.WriteLine("ALTITUDE" + Altitude);
                        }
                        else
                        {
                            Altitude = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        CNet.Write("get /instrumentation/attitude-indicator/internal-roll-deg\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            Roll = Double.Parse(answer);
                            Console.WriteLine("ROLL" + Roll);
                        }
                        else
                        {
                            Roll = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        CNet.Write("get /instrumentation/attitude-indicator/internal-pitch-deg\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            Pitch = Double.Parse(answer);
                            Console.WriteLine("PITCH" + Pitch);
                        }
                        else
                        {
                            Pitch = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        CNet.Write("get /instrumentation/altimeter/indicated-altitude-ft\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            Altimeter = Double.Parse(answer);
                            Console.WriteLine("ALTIMETER" + Altimeter);
                        }
                        else
                        {
                            Altimeter = Double.NaN;
                            BoardErr = true;
                            Console.WriteLine(answer);
                        }
                        //Location

                        CNet.Write("get /position/longitude-deg\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            LongitudeDeg = Double.Parse(answer);
                            Console.WriteLine("longitude_deg" + LongitudeDeg);
                        }
                        CNet.Write("get /position/latitude-deg\n");

                        StopWatch.Start();
                        answer = CNet.Read();
                        StopWatch.Stop();
                        if (StopWatch.Elapsed.TotalSeconds > 10.0)
                        {
                            SlowReaction = true;
                        }
                        StopWatch.Reset();

                        if (IsNumber(answer))
                        {
                            LatitudeDeg = Double.Parse(answer);
                            Console.WriteLine("latitude_deg" + LatitudeDeg);
                        }
                        //update Location
                        Location = new Location(LatitudeDeg, LongitudeDeg);


                        IsInitialized = true;
                        Thread.Sleep(350);
                    }
                    catch(Exception E)
                    {
                        ServerErr = true;
                        Console.WriteLine("server error");
                        DisConnect();
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
        public bool IsConnected()
        {
            return CNet.IsConnected();
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
        public bool FirstUpdate()
        {
            return IsInitialized;
        }
        public void MakeConnect()
        {
            Connection = true ;
        }
    }
}
