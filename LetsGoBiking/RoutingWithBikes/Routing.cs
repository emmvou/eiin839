using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Net.Http;
using System.Runtime.Serialization;
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
		//TODO call once with proxy route
		

		public static async Task<Dictionary<string, Station>> InitStationList()
        {
			var client = new HttpClient();
			var response = await client.GetAsync("http://localhost:8733/Design_Time_Addresses/WebProxyService/Service1/Stations");
			if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
			{
				Console.WriteLine("error");
				throw new Exception("Couldn't get list of stations");
			}
			var res = await response.Content.ReadAsStringAsync();
			List<Station> r;
            try
            {

			r = JsonSerializer.Deserialize<List<Station>>(res);
            }
			catch (Exception ex)
            {
				Console.WriteLine(ex.Message);
				throw new Exception("couldn't read list of stations");
            }
			return r.ToDictionary(keySelector: s => s.name);
		}	

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
		public DateTime lastUpdate { get; set; }
		public bool connected { get; set; }
		public bool overflow { get; set; }
		public object shape { get; set; }
		public Stands totalStands { get; set; }
		public Stands mainStands { get; set; }
		public object overflowStands { get; set; }

		[JsonConstructor]
		public Station(int number, string contractName, string name, string address, Dictionary<string, double> position,
			bool banking, bool bonus, string status, DateTime lastUpdate, bool connected,
			bool overflow, object shape, Stands totalStands, Stands mainStands, object overflowStands)
		{
			this.number = number;
			this.contractName = contractName;
			this.name = name;
			this.address = address;
			this.position = position;
			this.position2 = new GeoCoordinate(position["latitude"], position["longitude"]);
			this.banking = banking;
			this.bonus = bonus;
			this.status = status;
			this.lastUpdate = lastUpdate;
			this.connected = connected;
			this.overflow = overflow;
			this.shape = shape;
			this.totalStands = totalStands;
			this.mainStands = mainStands;
			this.overflowStands = overflowStands;
		}

		public override string ToString()
		{
			//return $"{name} ({address})\n\t" + position2; //String.Join(" ", position);
			return $"{name}/{number} ({address})\n\t" + position2 + $"\n\t{contractName}, {banking}, {bonus}, " +
				$"{status}, {lastUpdate}, {connected}, {overflow}\n\t{shape}, {totalStands}, {mainStands}, {overflowStands}"; 
		}
	}

	public class Stands
	{
		public Availabilities availabilities { get; set; }
		public int capacity { get; set; }

        public override string ToString()
        {
            return $"stands: {availabilities} {capacity}";
        }
    }

	public class Availabilities
	{
		public int bikes { get; set; }
		public int stands { get; set; }
		public int mechanicalBikes { get; set; }
		public int electricalBikes { get; set; }
		public int electricalInternalBatteryBikes { get; set; }
		public int electricalRemovableBatteryBikes { get; set; }

        public override string ToString()
        {
            return $"{bikes}, {stands}, {mechanicalBikes}, {electricalBikes}, {electricalInternalBatteryBikes}, {electricalRemovableBatteryBikes}";
        }
    }

	public class Position
	{
		public double latitude { get; set; }
		public double longitude { get; set; }

        public override string ToString()
        {
			return $"lat:{latitude} | lng:{longitude}";
        }
    }

	[DataContract]
	public class Contract
    {
		[DataMember] public string name { get; set; }
		[DataMember] public string commercial_name { get; set; }
		[DataMember] public List<string> cities { get; set; }
		[DataMember] public string country_code { get; set; }

	
    }
}
