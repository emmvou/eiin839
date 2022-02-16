using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BasicServerHTTPlistener
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {

            //if HttpListener is not supported by the Framework
            if (!HttpListener.IsSupported)
            {
                Console.WriteLine("A more recent Windows version is required to use the HttpListener class.");
                return;
            }
 
 
            // Create a listener.
            HttpListener listener = new HttpListener();

            // Add the prefixes.
            if (args.Length != 0)
            {
                foreach (string s in args)
                {
                    listener.Prefixes.Add(s);
                    // don't forget to authorize access to the TCP/IP addresses localhost:xxxx and localhost:yyyy 
                    // with netsh http add urlacl url=http://localhost:xxxx/ user="Tout le monde"
                    // and netsh http add urlacl url=http://localhost:yyyy/ user="Tout le monde"
                    // user="Tout le monde" is language dependent, use user=Everyone in english 

                }
            }
            else
            {
                Console.WriteLine("Syntax error: the call must contain at least one web server url as argument");
            }
            listener.Start();

            // get args 
            foreach (string s in args)
            {
                Console.WriteLine("Listening for connections on " + s);
            }

            // Trap Ctrl-C on console to exit 
            Console.CancelKeyPress += delegate {
                // call methods to close socket and exit
                listener.Stop();
                listener.Close();
                Environment.Exit(0);
            };


            while (true)
            {
                // Note: The GetContext method blocks while waiting for a request.
                HttpListenerContext context = listener.GetContext();
                HttpListenerRequest request = context.Request;

                string documentContents;
                using (Stream receiveStream = request.InputStream)
                {
                    using (StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8))
                    {
                        documentContents = readStream.ReadToEnd();
                    }
                }
                
                // get url 
                Console.WriteLine($"Received request for {request.Url}");

                //get url protocol
                Console.WriteLine(request.Url.Scheme);
                //get user in url
                Console.WriteLine(request.Url.UserInfo);
                //get host in url
                Console.WriteLine(request.Url.Host);
                //get port in url
                Console.WriteLine(request.Url.Port);
                //get path in url 
                Console.WriteLine(request.Url.LocalPath);

                // parse path in url 
                foreach (string str in request.Url.Segments)
                {
                    Console.WriteLine(str);
                }

                //get params un url. After ? and between &

                Console.WriteLine(request.Url.Query);

                //parse params in url
                Console.WriteLine("param1 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param1"));
                Console.WriteLine("param2 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param2"));
                Console.WriteLine("param3 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param3"));
                Console.WriteLine("param4 = " + HttpUtility.ParseQueryString(request.Url.Query).Get("param4"));

                //
                Console.WriteLine(documentContents);

                // Obtain a response object.
                HttpListenerResponse response = context.Response;

                if (request.Url.Segments.Length <= 2)
                {
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    return;
                }
                
                // Construct a response.
                string endpoint = request.Url.Segments[1][..^1];
                //string httpMeth = request.HttpMethod;
                string httpMeth = request.Url.Segments[2];

                var endClass = typeof(Program).Assembly.ExportedTypes.FirstOrDefault(t => t.Name == endpoint);
                
                if (endClass is null)
                {
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    return;
                }
                
                var end = Activator.CreateInstance(endClass);

                var meth = endClass.GetMethod(httpMeth);
               
                //var argss = HttpUtility.ParseQueryString(request.Url.Query).AllKeys.ToDictionary(k => k, k => HttpUtility.ParseQueryString(request.Url.Query).Get(k));
                
                string[] argss = HttpUtility.ParseQueryString(request.Url.Query).AllKeys.Select(k => HttpUtility.ParseQueryString(request.Url.Query).Get(k)).ToArray();
                
                //convert all argss to meth parameter types
                var arguments = new object[meth.GetParameters().Length];
                for (int i = 0; i < arguments.Length; i++)
                {
                    arguments[i] = Convert.ChangeType(argss[i], meth.GetParameters()[i].ParameterType);
                }
                

                var result = meth.Invoke(end, arguments);
                
                await using var output = response.OutputStream;
                await using var sw = new StreamWriter(output);
                await sw.WriteAsync(result?.ToString() ?? "");
             
            }
            // Httplistener neither stop ... But Ctrl-C do that ...
            // listener.Stop();
        }
    }
}