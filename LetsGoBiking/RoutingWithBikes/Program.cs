using System;
using System.Linq;
using System.ServiceModel;
using RoutingWithBikes;

var service = new ServiceHost(typeof(Service1));
Console.WriteLine(string.Join(", ", service.BaseAddresses.Select(x => x.ToString())));
service.Open();
Console.ReadLine();