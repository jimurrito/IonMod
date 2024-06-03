using System.Text;
using System.Net;
using Newtonsoft.Json;

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
            HttpRequestMessage request = new HttpRequestMessage(method, Rooturi + path);
            // check if body needs to be injected
            if (method == HttpMethod.Put && method == HttpMethod.Post)
            {
                // Body must be lowered as the IONOS api is case sensitive for JSON bodies.
                request.Content = new StringContent(body.ToLower(), Encoding.UTF8, Contenttype);
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
                case HttpStatusCode.Unauthorized:           //401 (Most common)
                    throw new IonUnauthorizedException();
                case HttpStatusCode.InternalServerError:    //500
                    throw new IonServerErrorException();

            }
            // Pull content from HttpResponseMessage
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            // return content deserialized into an Obj
            return reader.ReadToEnd();
        }
        //
        //
        public T Get<T>(string path = "/")
        {
            return JsonConvert.DeserializeObject<T>(Request(HttpMethod.Get, path)) ?? throw new IonDeserializationError();
        }
        //
        //
        public T Put<T>(string path = "/", string body = "")
        {
            return JsonConvert.DeserializeObject<T>(Request(HttpMethod.Put, path, body)) ?? throw new IonDeserializationError();
        }
        public void Put(string path = "/", string body = "")
        {
            Request(HttpMethod.Put, path, body);
        }
        //
        //
        public T Post<T>(string path = "/", string body = "")
        {
            return JsonConvert.DeserializeObject<T>(Request(HttpMethod.Post, path, body)) ?? throw new IonDeserializationError();
        }
        public void Post(string path = "/", string body = "")
        {
            Request(HttpMethod.Post, path, body);
        }
        //
        //
        public T Delete<T>(string path = "/")
        {
            return JsonConvert.DeserializeObject<T>(Request(HttpMethod.Delete, path)) ?? throw new IonDeserializationError();
        }
        public void Delete(string path = "/")
        {
            Request(HttpMethod.Delete, path);
        }
    }
}