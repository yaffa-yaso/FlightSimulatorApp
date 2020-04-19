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
        Mutex Mut = new Mutex(false, "mutex");
        public  void Connect(string ip, int port)
        {
            Connection = false;
            ClientSocket = new TcpClient();
            //saving Ip and Port as a members class
            this.Ip = ip;
            this.Port = port;
            Mut = new Mutex(false, "mutex");

                MakeConnection();

           




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
            
            Byte[] buffer = new Byte[256];
            Int32 bytes = Stream.Read(buffer, 0, buffer.Length);
            string responseData = System.Text.Encoding.ASCII.GetString(buffer, 0, bytes);
          
            return responseData;
        }

        public void Write(string command)
        {
            
               
            // send spacific command
            Byte[] data = System.Text.Encoding.ASCII.GetBytes(command);
            stream.Write(data, 0, data.Length);
          
             
                
          
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
                Thread.Sleep(350);



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
