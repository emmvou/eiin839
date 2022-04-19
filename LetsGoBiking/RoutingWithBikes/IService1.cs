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
        //SOAP
        [OperationContract]
        string GetData(int value);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Stations", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task<List<Station>> GetAllStations();

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Contracts", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task<List<Contract>> GetAllContracts();

        [OperationContract]
        Task<string> Test();

        [OperationContract]
        Task<Tuple<GeoCoordinate, GeoCoordinate>> GetTwoClosestStations(Tuple<Tuple<double, double>, Tuple<double, double>> locations);

        [OperationContract]
        CompositeType GetDataUsingDataContract(CompositeType composite);

        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Route?startLat={startLat}&startLong={startLong}&endLat={endLat}&endLong={endLong}&contract={contract}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        Task <ComputedRoute> GetRoute(double startLat, double startLong, double endLat, double endLong, string contract);


        //REST
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Add?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Bare, ResponseFormat = WebMessageFormat.Json)]
        int Add(int value1, int value2);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Mul?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]

        int Multiply(int value1, int value2);
        [OperationContract]
        [WebInvoke(Method = "GET", UriTemplate = "Sub?x={value1}&y={value2}", BodyStyle = WebMessageBodyStyle.Wrapped, ResponseFormat = WebMessageFormat.Json)]
        int Substract(int value1, int value2);

        
    }

    // Utilisez un contrat de données comme indiqué dans l'exemple ci-après pour ajouter les types composites aux opérations de service.
    // Vous pouvez ajouter des fichiers XSD au projet. Une fois le projet généré, vous pouvez utiliser directement les types de données qui y sont définis, avec l'espace de noms "RoutingWithBikes.ContractType".
    [DataContract]
    public class CompositeType
    {
        bool boolValue = true;
        string stringValue = "Hello ";

        [DataMember]
        public bool BoolValue
        {
            get { return boolValue; }
            set { boolValue = value; }
        }

        [DataMember]
        public string StringValue
        {
            get { return stringValue; }
            set { stringValue = value; }
        }
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
