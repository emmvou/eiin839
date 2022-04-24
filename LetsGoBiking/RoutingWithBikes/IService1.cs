using System;
using System.Collections.Generic;
using System.Device.Location;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace RoutingWithBikes
{
    [ServiceContract]
    public interface IService1
    {

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Stations", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task<List<Station>> GetAllStations();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Contracts", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task<List<Contract>> GetAllContracts();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "SearchAddress?location={location}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task <string> SearchAddress(string location);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Route?startLat={startLat}&startLong={startLong}&endLat={endLat}&endLong={endLong}&contract={contract}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task<ComputedRoute> GetRoute(double startLat, double startLong, double endLat, double endLong, string contract);

        
        
    }

    [DataContract]
    public class ComputedRoute
    {
        private string startToBike;
        private string bikeToBike;
        private string bikeToEnd;

        [DataMember]
        public string StartToBike
        {
            get { return startToBike; }
            set { startToBike = value; }
        }
        [DataMember]
        public string BikeToBike
        {
            get { return bikeToBike; }
            set { bikeToBike = value; }
        }
        [DataMember]
        public string BikeToEnd
        {
            get { return bikeToEnd; }
            set { bikeToEnd = value; }
        }
    }
}
