using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace RoutingWithBikes
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        Dictionary<string, Station> Stations = new Dictionary<string, Station>();
        //SOAP
        public async Task<string> GetAllStations()
        {

            if (Stations.Count == 0)
            {
                Stations = await Routing.InitStationList();
            }
            
            return String.Join(",", Stations.Select((k, v) => k + "=" + v).ToArray());
        }
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public async Task<string> Test()
        {
            return await Routing.fct();
        }

        public async Task<Tuple<GeoCoordinate, GeoCoordinate>> GetTwoClosestStations(Tuple<Tuple<double, double>, Tuple<double, double>> locations)
        {
            return await Routing.ClosestStations(locations.Item1, locations.Item2);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        //REST
        public int Add(int value1, int value2)
        {
            return value1 + value2;
        }

        public int Multiply(int value1, int value2)
        {
            return value1 * value2;
        }

        public int Substract(int value1, int value2)
        {
            return value1 - value2;
        }

        
    }
}
