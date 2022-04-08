using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RoutingWithBikes
{
	internal class Routing
		//TODO calculer plus intelligemment que juste à vol d'oiseau
	{
		public static async Task<string> fct()
		{
			var contract = "lyon";
			GeoCoordinate gps = new GeoCoordinate(4, 5);
			var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
			var baseUri = "https://api.jcdecaux.com/vls/v1/";
			var resContract = "contracts";
			var resStation = "stations";
			var client = new HttpClient();
			var response = await client.GetAsync(baseUri + "/" + resStation + "?" + "contract=" + contract + "&" + key);
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				return "Bad request";
			}

			var res = await response.Content.ReadAsStringAsync();
			var r = JsonSerializer.Deserialize<List<Station>>(res);

			//r.ForEach(Console.WriteLine);
			var closest = r.First(pos => gps.GetDistanceTo(pos.position2) == r.Min(min => gps.GetDistanceTo(min.position2)));
			return "closest station is " + closest.name;
		}

		public static async Task<Tuple<GeoCoordinate, GeoCoordinate>> ClosestStations(Tuple<double, double> startC, Tuple<double,double> endC)
		{
			// TODO trouver la ville du contrat
			var contract = "lyon";
			GeoCoordinate start = new GeoCoordinate(startC.Item1, startC.Item2);
			GeoCoordinate end = new GeoCoordinate(endC.Item1, endC.Item2);

			var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
			var baseUri = "https://api.jcdecaux.com/vls/v1/";
			var resContract = "contracts";
			var resStation = "stations";
			var client = new HttpClient();
			var response = await client.GetAsync(baseUri + "/" + resStation + "?" + "contract=" + contract + "&" + key);
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				throw new Exception("JDCAUX error");
			}

			var res = await response.Content.ReadAsStringAsync();
			var r = JsonSerializer.Deserialize<List<Station>>(res);

			var closestStart = r.First(pos => start.GetDistanceTo(pos.position2) == r.Min(min => start.GetDistanceTo(min.position2)));
			var closestEnd = r.First(pos => end.GetDistanceTo(pos.position2) == r.Min(min => end.GetDistanceTo(min.position2)));
			return new Tuple<GeoCoordinate, GeoCoordinate>(closestStart.position2, closestEnd.position2);
		}
    }

	public class Station
	{
		public int number { get; set; }
		public string contractName { get; set; }
		public string name { get; set; }
		public string address { get; set; }
		public Dictionary<string, double> position { get; set; }
		public GeoCoordinate position2 { get; set; }
		public bool banking { get; set; }
		public bool bonus { get; set; }
		public string status { get; set; }
		//public string lastUpdate { get; set; }
		public bool connected { get; set; }
		//public bool overflow { get; set; }
		//public object shape { get; set; }
		//public Stands totalStands { get; set; }
		//public Stands mainStands { get; set; }
		//public object overflows { get; set; }
		public int bike_stands { get; set; }
		public int available_bike_stands { get; set; }
		public int available_bikes { get; set; }
		public long lastUpdate { get; set; }

		[JsonConstructor]
		public Station(int number, string contractName, string name, string address, Dictionary<string, double> position,
			bool banking, bool bonus, string status, long lastUpdate, bool connected, int bike_stands, int available_bike_stands, int available_bikes)
		{
			this.number = number;
			this.contractName = contractName;
			this.name = name;
			this.address = address;
			this.position = position;
			this.position2 = new GeoCoordinate(position["lat"], position["lng"]);
			this.banking = banking;
			this.bonus = bonus;
			this.status = status;
			this.lastUpdate = lastUpdate;
			this.connected = connected;
			//this.overflow = overflow;
			//this.shape = shape;
			//this.totalStands = totalStands;
			//this.mainStands = mainStands;
			//this.overflows = overflows;
			this.bike_stands = bike_stands;
			this.available_bikes = available_bikes;
			this.available_bike_stands = available_bike_stands;
		}

		public override string ToString()
		{
			return $"{name} ({address})\n\t" + position2; //String.Join(" ", position);
		}
	}

	public class Stands
	{
		public Availabilities availabilities { get; set; }
		public int capacity { get; set; }
	}

	public class Availabilities
	{
		public int bikes { get; set; }
		public int stands { get; set; }
		public int mechanicalBikes { get; set; }
		public int electricalBikes { get; set; }
		public int electricalInternalBatteryBikes { get; set; }
		public int electricalRemovableBatteryBikes { get; set; }
	}

	public class Position
	{
		public double latitude { get; set; }
		public double longitude { get; set; }
	}
}
