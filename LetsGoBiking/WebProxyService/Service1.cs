using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Runtime.Caching;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace WebProxyService
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class Service1 : IService1
    {
        ObjectCache cache = MemoryCache.Default;

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

        public async Task<string> Stations()
        {

            var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
            var baseUri = "https://api.jcdecaux.com/vls/v3/";
            var client = new HttpClient();
            var response = await client.GetAsync(baseUri + "/stations?" + "&" + key);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "Bad request";
            }
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }

        public string GetInfo(int number)
        {
            throw new NotImplementedException();
        }
    }
}
