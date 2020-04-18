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
        public async void connect(string ip, int port)
        {
            connection = false;
            clientSocket = new TcpClient();
            //saving ip and port as a members class
            this.ip = ip;
            this.port = port;
            m = new Mutex(false, "mutex");

            await Task.Run(() =>
            {
                makeConnection();

            });




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
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
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
                stream = clientSocket.GetStream();
                connection = true;
                Thread.Sleep(350);



            }

            catch (Exception e)
            {
                connection = false;
                Console.WriteLine("Error.." + e.StackTrace);
                disconnect();
            }
        }
        public bool isConnected()
        {
            return connection;
        }
        public Mutex getMutex()
        {
            return this.m;
        }
    }
}
