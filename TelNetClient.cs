using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;


namespace FlightSimulatorApp
{
    interface TelNetClient
    {
        void connect(string ip, int port);
        void write(string command);
        string read();
        void disconnect();
        bool isConnected();
        Mutex getMutex();


    }
}
