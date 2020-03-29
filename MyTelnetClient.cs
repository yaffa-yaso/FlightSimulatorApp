using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.Threading;

namespace FlightSimulatorApp
{
    class MyTelnetClient : TelNetClient
    {
        string ip;
        int port;
        bool connection = false;
            TcpClient clientSocket = new TcpClient();
        public void connect(string ip, int port)
        {
            //saving ip and port as a members class
            this.ip = ip;
            this.port = port;
                Thread cThread = new Thread(makeConnection);
            
        }

        public void disconnect()
        {
            connection = false;
        }

        public string read()
        {
            StreamReader reader = new StreamReader(clientSocket.GetStream());
            return reader.ReadLine();
        }

        public void write(string command)
        {
            StreamWriter writer = new StreamWriter(clientSocket.GetStream());
            // send spacific command
            writer.WriteLine(command);

        }
        // self functions
        public void makeConnection()
            {
            try
            {
                clientSocket.Connect(ip, port);
                connection = true;
                // thread loop - stopping when disconnected has been made
                while (connection != false)
                {
                    ///....
                }
            }

            catch (Exception e)
            {
                Console.WriteLine("Error.." + e.StackTrace);
            }
        }
    }
}
