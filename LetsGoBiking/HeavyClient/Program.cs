using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HeavyClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            RoutingService.Service1Client client = new RoutingService.Service1Client();
            //Console.WriteLine(client.GetAllStations());
            //Console.WriteLine(client.GetRoute(new Tuple<double, double>(45.774200, 4.867518), new Tuple<double, double>(45.75190, 4.821669), "Lyon"));
            //45.774204,4.867512
            //45.75197,4.821662
            //Console.WriteLine(client.Test());
            //var res = client.GetTwoClosestStations(new Tuple<Tuple<double, double>, Tuple<double, double>> (new Tuple<double, double>(4, 5), new Tuple<double, double>(4, 9)));
            //Console.WriteLine($"start station: {res.Item1} | end station: {res.Item2}");
            Console.ReadLine();

        }
    }
}
