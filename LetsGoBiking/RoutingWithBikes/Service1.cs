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
        
        //SOAP
        public async Task<List<Station>> GetAllStations()
        {
            return await Routing.InitStationList();
        }

        public async Task<List<Contract>> GetAllContracts()
        {
            return await Routing.InitContractList();
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public async Task<string> Test()
        {
            return await Routing.Fct();
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

        public async Task<ComputedRoute> GetRoute(double startLat, double startLong, double endLat, double endLong, string contract)
        {
            return await Routing.GetComputedRoute(new Tuple<double, double>(startLat, startLong), new Tuple<double, double>(endLat, endLong), contract);
        }

        public async Task<string> SearchAddress(string location)
        {
            return await Routing.CallGeoCodeSearch(location);
        }
    }
}
