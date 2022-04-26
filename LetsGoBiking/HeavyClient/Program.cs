﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace HeavyClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Thread.Sleep(1000);
            RoutingService.Service1Client client = new RoutingService.Service1Client();
            DateTime t = DateTime.Now;
            Console.WriteLine($"{client.GetAllStations()} took {DateTime.Now.Subtract(t)} to compute");
            t = DateTime.Now;
            Console.WriteLine($"{client.GetAllStations()} took {DateTime.Now.Subtract(t)} to compute");
            t = DateTime.Now;
            Console.WriteLine($"{client.GetRoute(4.835571, 45.732398, 4.748162441997923, 45.85909499964853, "lyon")} took {DateTime.Now.Subtract(t)} to compute");
            t = DateTime.Now;
            Console.WriteLine($"{client.GetRoute(4.835571, 45.732398, 4.748162441997923, 45.85909499964853, "lyon")} took {DateTime.Now.Subtract(t)} to compute");

            //45.774204,4.867512
            //45.75197,4.821662
            //Console.WriteLine(client.Test());
            //var res = client.GetTwoClosestStations(new Tuple<Tuple<double, double>, Tuple<double, double>> (new Tuple<double, double>(4, 5), new Tuple<double, double>(4, 9)));
            //Console.WriteLine($"start station: {res.Item1} | end station: {res.Item2}");
            Console.ReadLine();

        }
    }
}
