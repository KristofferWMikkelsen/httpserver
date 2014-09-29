﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace httpserver
{
    public class HttpServer
    {
        //public static readonly int DefaultPort = 8888;
        public void StartServer()
        {
            TcpListener serverSocket = new TcpListener(6789);
            serverSocket.Start();

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server activated");

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true; // enable automatic flushing

            string message = sr.ReadLine();
            string answer = "";
            while (message != null && message != "")
            {

                Console.WriteLine("Client: " + message);
                answer = message.ToUpper();
                sw.WriteLine(answer);
                message = sr.ReadLine();

            }

            ns.Close();
            connectionSocket.Close();
            serverSocket.Stop();

        }
    }
}
