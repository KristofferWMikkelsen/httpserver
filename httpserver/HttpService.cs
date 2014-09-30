﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace httpserver
{
   public class HttpService
   {
      private TcpClient connectionSocket;
      private static readonly string RootCatalog = "c:/temp";

      string Roottext { get; set; }

      public HttpService(TcpClient connectionSocket)
      {
         this.connectionSocket = connectionSocket;
      }
      public void SocketHandler()
      {
         Stream ns = connectionSocket.GetStream();

         StreamReader sr = new StreamReader(ns);
         StreamWriter sw = new StreamWriter(ns);
         sw.AutoFlush = true; // enable automatic flushing

         string message = sr.ReadLine();
         Console.WriteLine(message);

         string uritext = "";
         string[] words = message.Split(' ');

         string get = words[0];

         uritext = words[1].Replace("/", "/");

         //sw.WriteLine("You requested " + uritext + "<br>");

         uritext = uritext + ".txt";
         Roottext = RootCatalog + uritext;



         //TESTGET
         string code = "200 OK";
         string illegalrequest = "400 Illegal request";
         string illegalmethod = "400 Illegal request";
         string illegalprotocol = "400 Illegal protocol";
         const string http10 = "HTTP/1.0";

         

         

         if (!File.Exists(Roottext))
         {
            code = "404 Not Found";
         }

         //Console.WriteLine(http10 + " " + illegalprotocol + " Slut");
         //TESTGET
         //TESTGETILLEGALMETHODNAME
         //TESTGETILLEGALMETHODNAME
         if (words[0] == "GET")
            if (words[2] == "HTTP/1.0")
            {
               sw.WriteLine(words[2] + " " + code);
            }
            else
            {
               sw.WriteLine(http10 + " " + illegalprotocol);
            }

         if (words[0] == "PLET")
            sw.WriteLine(words[2] + " " + illegalmethod);
            

         //string[] lines = File.ReadAllLines(Roottext);

         //foreach (string line in lines)
         //{
         //   sw.WriteLine("\t" + line);
         //}

         sr.Close();
         sw.Close();
         connectionSocket.Close();
      }
   }
}
