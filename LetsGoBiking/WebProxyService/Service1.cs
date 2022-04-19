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
        private static CustomCache cache = new();

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

        public async Task<string> GetInfo(string id, string contract)
        {
            return await cache.Get(id, contract);
        }

        public async Task<string> Contracts()
        {
            var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
            var baseUri = "https://api.jcdecaux.com/vls/v3/";
            var client = new HttpClient();
            var response = await client.GetAsync(baseUri + "/contracts?" + "&" + key);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "Bad request";
            }
            var res = await response.Content.ReadAsStringAsync();
            return res;
        }
    }

    class CustomCache
    {
        ObjectCache cache = MemoryCache.Default;
        private DateTimeOffset offset;

        public async Task<string> Get(string id, string contract)
        {
            if (cache.Get(id) is string x)
                return x;

            var key = "apiKey=f51f7e272f7aa2c0b30e9e3f6d6d3ea8fa202c8e";
            var baseUri = "https://api.jcdecaux.com/vls/v3/";
            var client = new HttpClient();
            var response = await client.GetAsync(baseUri + "/stations/"+id+"?contract=" + contract + "&" + key);
            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                return "Bad request";
            }
            var res = await response.Content.ReadAsStringAsync();
            cache.Add(id, res, offset);
            return res;
        }
    }
}
