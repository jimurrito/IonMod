using System.Text;
using Microsoft.Management.Infrastructure.Options;
using Newtonsoft.Json;
using System.Net;

namespace IonMod
{
    public class IonHttp
    {
        private static readonly HttpClient client = new HttpClient();
        private string Rooturi = "https://api.hosting.ionos.com/dns/v1/zones";
        private string Contenttype = "application/json";
        public IonToken Token;

        // Constructor
        public IonHttp(IonToken token)
        {
            Token = token;
        }

        // Methods
        public string Request(HttpMethod method, string path = "/", string body = "")
        {
            // init request
            HttpRequestMessage request;
            // check if body needs to be injected
            if (method == HttpMethod.Get)
            {
                request = new HttpRequestMessage(method, Rooturi + path);
            }
            else
            {
                request = new HttpRequestMessage(method, Rooturi + path)
                {
                    // Body must be lowered as the IONOS api is case sensitive for JSON bodies.
                    Content = new StringContent(body.ToLower(), Encoding.UTF8, Contenttype)
                };
            }
            // Set Headers
            request.Headers.Add("X-Api-Key", Token.PublicPrefix + "." + Token.Secret);
            request.Headers.Add("User-Agent", "IonMod");
            //
            HttpResponseMessage response = client.Send(request);
            // Custom exception for non 200s
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:             //400
                    throw new IonBadRequestException();
                case HttpStatusCode.Unauthorized:           //401
                    throw new IonUnauthorizedException();
                case HttpStatusCode.InternalServerError:    //500
                    throw new IonServerErrorException();

            }
            // Pull content from HttpResponseMessage
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            string rend = reader.ReadToEnd();
            //
            return rend;
        }
        public string Get(string path = "/")
        {
            return Request(HttpMethod.Get, path);
        }
        public string Put(string path = "/", string body = "")
        {
            return Request(HttpMethod.Put, path, body);
        }
        public string Post(string path = "/", string body = "")
        {
            return Request(HttpMethod.Post, path, body);
        }
        public string Delete(string path = "/")
        {
            return Request(HttpMethod.Delete, path);
        }
    }
}