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
        void Connect(string ip, int port);
        void Write(string command);
        string Read();
        void Disconnect();
        bool IsConnected();
        Mutex getMutex();


    }
}
