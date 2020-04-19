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
        string Ip;
        int Port;
        bool Connection = false;
        TcpClient ClientSocket;
        NetworkStream Stream=null;
        Mutex Mut;
        public async void Connect(string ip, int port)
        {
            Connection = false;
            ClientSocket = new TcpClient();
            //saving Ip and Port as a members class
            this.Ip = ip;
            this.Port = port;
            Mut = new Mutex(false, "mutex");

            await Task.Run(() =>
            {
                MakeConnection();

            });




        }


        public void Disconnect()
        {
            Connection = false;
            if (Stream!= null)
            {
                Stream.Close();
                Stream = null;
            }
            ClientSocket.Close();


            //Mut.Close();


            //Connection = false;
        }

        public string Read()
        {
            Mut.WaitOne();
            Byte[] buffer = new Byte[256];
            Int32 bytes = Stream.Read(buffer, 0, buffer.Length);
            string responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
            Mut.ReleaseMutex();
            return responseData;
        }

        public void Write(string command)
        {
            Mut.WaitOne();
            // send spacific command
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            Stream.Write(data, 0, data.Length);
            Mut.ReleaseMutex();
        }
        // self functions
        public void MakeConnection()
        {
            try
            {
                Console.WriteLine("connecting...");

                ClientSocket.Connect(Ip, Port);
                Console.WriteLine("connected");
                Stream = ClientSocket.GetStream();
                Connection = true;
                Thread.Sleep(150);



            }

            catch (Exception e)
            {
                Connection = false;
                Console.WriteLine("Error.." + e.StackTrace);
                Disconnect();
            }
        }
        public bool IsConnected()
        {
            return Connection;
        }
        public Mutex getMutex()
        {
            return this.Mut;
        }
    }
}
