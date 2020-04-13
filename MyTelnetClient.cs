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
        TcpClient clientSocket;
        NetworkStream stream;
        Mutex m;
        public void connect(string ip, int port)
        {
            clientSocket = new TcpClient();
            //saving ip and port as a members class
            this.ip = ip;
            this.port = port;
            Thread cThread = new Thread(makeConnection);
            cThread.Start();
            while(connection == false){}
            stream = clientSocket.GetStream();
            m = new Mutex(false, "mutex");



        }


        public void disconnect()
        {
            connection = false;
            stream.Close();
            clientSocket.Close();
            
            //m.Close();


            //connection = false;
        }

        public string read()
        {
            m.WaitOne();
            Byte[] buffer = new Byte[256];
            Int32 bytes = stream.Read(buffer, 0, buffer.Length);
           string responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
            m.ReleaseMutex();
            return responseData;
        }

        public void write(string command)
        {
            m.WaitOne();
            // send spacific command
            Byte[] data =   System.Text.Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
            m.ReleaseMutex();
        }
        // self functions
        public void makeConnection()
            {
            try
            {
                Console.WriteLine("connecting...");

                clientSocket.Connect(ip, port);
                Console.WriteLine("connected");
                Thread.Sleep(150);
                connection = true;
                
                // thread loop - stopping when disconnection has been made
                while (connection != false)
                {
                    ///....
                }
                Console.WriteLine("disconnected");

            }

            catch (Exception e)
            {

                Console.WriteLine("Error.." + e.StackTrace);
            }
        }
        public bool isConnected()
        {
            return connection;
        }
    }
}
