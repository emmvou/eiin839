using System.Net;
using System.Net.Http.Json;
using System.Text.Json.Serialization;
using GeoCoordinatePortable;

public class Program
{
	static async Task Main(string[] args)
	{
		var contract = "lyon";
		GeoCoordinate gps = new GeoCoordinate(4, 5);
		var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
		var baseUri = "https://api.jcdecaux.com/vls/v1/";
		var resContract = "contracts";
		var resStation = "stations";
		var client = new HttpClient();
		var response = await client.GetAsync(baseUri + "/" + resStation + "?" + "contract=" + contract + "&" + key);
		if (response.StatusCode == HttpStatusCode.BadRequest)
		{
			Console.WriteLine("Bad request");
			return;
		}

		var res = await response.Content.ReadFromJsonAsync<List<Station>>();

		//res.ForEach(Console.WriteLine);
		var closest = res.First(_ => gps.GetDistanceTo(_.position2) == res.Min(_ => gps.GetDistanceTo(_.position2)));
		Console.WriteLine("closest station is " + closest.name);
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
	public string lastUpdate { get; set; }
	public bool connected { get; set; }
	public bool overflow { get; set; }
	public object shape { get; set; }
	public Stands totalStands { get; set; }
	public Stands mainStands { get; set; }
	public object? overflows { get; set; }

	[JsonConstructor]
	public Station(int number, string contractName, string name, string address, Dictionary<string, double> position,
		bool banking, bool bonus, string status, string lastUpdate, bool connected, bool overflow, object shape,
		Stands totalStands, Stands mainStands, object? overflows)
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
		this.overflow = overflow;
		this.shape = shape;
		this.totalStands = totalStands;
		this.mainStands = mainStands;
		this.overflows = overflows;
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