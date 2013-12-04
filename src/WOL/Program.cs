using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Sockets;

namespace WOL
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] macAddress = new byte[] { 0x00, 0x30, 0x05, 0xFC, 0x7F, 0x8E };

            Console.WriteLine("Taskte drücken, um Wake-On-Lan-signal zu senden.");
            Console.ReadKey();

            IPAddress.Broadcast.SendWOL(macAddress);
            
            Console.WriteLine("WOL gesendet.");
            Console.WriteLine("So machen wa das. (Taste drücken zum Beenden)");
            Console.ReadKey();
         }
    }
}