using System;
using System.Collections.Generic;
namespace ShipWar3
{
    class Program
    {
        public static List<Ship> test = new List<Ship>();
        static void Main(string[] args)
        {
            var lobby = new Lobby();
            lobby.Start();
        }
    }
}