
using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace RoutingWithBikes
{
	internal class Routing
	{

        static List<Station> Stations = new List<Station>();
        static List<Contract> Contracts = new List<Contract>();


		public static async Task<List<Station>> InitStationList()
        {
            if(Stations.Count == 0)
                await internalInit();
            return Stations;
        }

        public static async Task<List<Contract>> InitContractList()
        {
            if (Contracts.Count == 0)
                await internalInit();
            return Contracts;
        }

        private static async Task internalInit()
        {
            Stations = await CallStations();
            Contracts = await CallContracts();
        }

        private static async Task<List<Station>> CallStations()
        {
            return await CallProxy<List<Station>>("Stations", "");
        }

        private static async Task<List<Contract>> CallContracts()
        {
            return await CallProxy<List<Contract>>("Contracts", "");
        }


        public static async Task<ComputedRoute> GetComputedRoute(Tuple<double, double> start, Tuple<double, double> end, string contract)
        {
            var startPos = new GeoCoordinate(start.Item1, start.Item2);
            var endPos = new GeoCoordinate(end.Item1, end.Item2);
            return await GetRoute(startPos, endPos, contract);
        }

        public static async Task<ComputedRoute> GetRoute(GeoCoordinate start, GeoCoordinate end, string contract)
        {
            if (Stations.Count == 0)
            {
                await internalInit();
            }

            //on prend toutes les stations du contrat
            var stations = Stations.FindAll(s => s.contractName == contract);
            if (stations.Count == 0)
            {
                throw new Exception("cannot find any station for given contract");
            }
            //on prend dans l'ordre jusqu'à la première qui a du stock de vélo
            Station checkPoint1 = await getClosestStationWithBikes(stations, start);
            Station checkPoint2 = await getClosestStationWithStands(stations, end);

            //on appelle l'API à partir des arguments
            var stb = await CallOpenRouteService(start, checkPoint1.position2, "foot-walking");
            var btb = await CallOpenRouteService(checkPoint1.position2, checkPoint2.position2, "cycling-regular");
            var bte = await CallOpenRouteService(checkPoint2.position2, end, "foot-walking");

            return new ComputedRoute() { StartToBike = stb, BikeToBike = btb, BikeToEnd = bte };
        }

        
        private static async Task<string> CallOpenRouteService(GeoCoordinate start, GeoCoordinate end, string profile)
        {
            return await RESTRoute("https://api.openrouteservice.org/v2/directions/", profile + "?"+
                "api_key=5b3ce3597851110001cf6248e622e9fdbb574103bd36807aaff5b7b2" +
                "&start="+ start.Longitude.ToString(CultureInfo.InvariantCulture) + ","+start.Latitude.ToString(CultureInfo.InvariantCulture) +
                "&end=" + end.Longitude.ToString(CultureInfo.InvariantCulture) + ","+ end.Latitude.ToString(CultureInfo.InvariantCulture));
        }

        public static async Task<string> CallGeoCodeSearch(string pos)
        {
            return await RESTRoute("https://api.openrouteservice.org/geocode/search?",
                "api_key=5b3ce3597851110001cf6248e622e9fdbb574103bd36807aaff5b7b2&text=" + pos);
        }

        private static async Task<Station> getClosestStationWithBikes(List<Station> stations, GeoCoordinate position)
        {
            foreach (var s in stations.OrderBy( pos => position.GetDistanceTo(pos.position2)))
            {
                var st = await GetStation(s.number, s.contractName);
                if (st.totalStands.availabilities.bikes > 0)
                {
                    return st;
                }
            }
            throw new Exception("no bikes available anywhere");
		}

        private static async Task<Station> getClosestStationWithStands(List<Station> stations, GeoCoordinate position)
        {
            foreach (var s in stations.OrderBy(pos => position.GetDistanceTo(pos.position2)))
            {
                var st = await GetStation(s.number, s.contractName);
                if (st.totalStands.capacity - st.totalStands.availabilities.bikes > 0)
                {
                    return st;
                }
            }
            throw new Exception("no bikes available anywhere");
        }

        public static async Task<Station> GetStation(int id, string contract)
        {
            return await CallProxy<Station>("Station","?x=" + id.ToString(CultureInfo.InvariantCulture) + "&contract=" + contract);
        }

        private static async Task<T> CallProxy<T>(string uri, string parameters)
        {
            return DeserializeResponse<T>(await RESTRoute("http://localhost:8733/Design_Time_Addresses/WebProxyService/Service1/" + uri,
                parameters));
        }

        private static async Task<string> RESTRoute(string baseUri, string parameters)
        {
            var client = new HttpClient();
            var response = await client.GetAsync(baseUri + parameters);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                throw new Exception("wrong external route\nuri: "+baseUri+parameters + await response.Content.ReadAsStringAsync());
            }
            return await response.Content.ReadAsStringAsync();
        }

        private static T DeserializeResponse<T>(string response)
        {
            T s;
            try
            {
                s = JsonSerializer.Deserialize<T>(JsonSerializer.Deserialize<string>(response));
            }
            catch (Exception ex)
            {
                throw new Exception("couldn't deserialize");
            }

            return s;
        }

    }

    [DataContract]
	public class Station : ICloneable
	{
		[DataMember] public int number { get; set; }
		[DataMember] public string contractName { get; set; }
		[DataMember] public string name { get; set; }
		[DataMember] public string address { get; set; }
		[DataMember] public Dictionary<string, double> position { get; set; }
		[DataMember] public GeoCoordinate position2 { get; set; }
		[DataMember] public bool banking { get; set; }
		[DataMember] public bool bonus { get; set; }
		[DataMember] public string status { get; set; }
		[DataMember] public DateTime lastUpdate { get; set; }
		[DataMember] public bool connected { get; set; }
		[DataMember] public bool overflow { get; set; }
		[DataMember] public Stands totalStands { get; set; }
		[DataMember] public Stands mainStands { get; set; }

		[JsonConstructor]
		public Station(int number, string contractName, string name, string address, Dictionary<string, double> position,
			bool banking, bool bonus, string status, DateTime lastUpdate, bool connected,
			bool overflow, Stands totalStands, Stands mainStands)
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
			this.totalStands = totalStands;
			this.mainStands = mainStands;
		}

		public override string ToString()
		{
			return $"{name} ({address})\n\t" + position2; //String.Join(" ", position);
			//return $"{name}/{number} ({address})\n\t" + position2 + $"\n\t{contractName}, {banking}, {bonus}, " +
			//	$"{status}, {lastUpdate}, {connected}, {overflow}\n\t{shape}, {totalStands}, {mainStands}, {overflowStands}"; 
		}

        public object Clone()
        {
            return new Station(number, contractName, name, address, position, banking, bonus, status, lastUpdate,
                connected, overflow, totalStands, mainStands);
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
