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

        public async Task<ComputedRoute> GetRoute(double startLat, double startLong, double endLat, double endLong, string contract)
        {
            return await Routing.GetComputedRoute(new Tuple<double, double>(startLat, startLong), new Tuple<double, double>(endLat, endLong), contract);
        }

        public async Task<string> SearchAddress(string location)
        {
            return await Routing.CallGeoCodeSearch(location);
        }

        public async Task<Station> GetStation(int id, string contract)
        {
            return await Routing.GetStation(id, contract);
        }
    }
}
