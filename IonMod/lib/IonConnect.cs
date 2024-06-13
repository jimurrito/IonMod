using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace IonMod
{
    public static class IonConnect
    {
        private static readonly HttpClient Client = new HttpClient();
        public static string Rooturi = "https://api.hosting.ionos.com/dns/v1/zones";
        private static string ContentType = "application/json";
        private static IonToken? Token;
        //
        //
        // Login - initialize functionality
        public static void Login(IonToken token)
        {
            Token = token;
        }
        // Login check
        private static void LoginCheck()
        {
            if (Token == null) { throw new IonUninitLoginException(); }
        }
        //
        // Generic request method
        private static string Request(HttpMethod method, string path = "/", string body = "")
        {
            //
            LoginCheck();
            // init request
            HttpRequestMessage request = new(method, Rooturi + path);
            // check if body needs to be injected
            if (method == HttpMethod.Put || method == HttpMethod.Post)
            {
                // Body must be lowered as the IONOS api is case sensitive for JSON bodies.
                request.Content = new StringContent(body.ToLower(), Encoding.UTF8, ContentType);
            }
            // Set Headers
            request.Headers.Add("X-Api-Key", Token.PublicPrefix + "." + Token.Secret);
            request.Headers.Add("User-Agent", "IonMod");
            //
            HttpResponseMessage response = Client.Send(request);
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
        // Deserializer
        private static T Deserialize<T>(string input)
        {
            return JsonConvert.DeserializeObject<T>(input) ?? throw new IonDeserialException();
        }
        //
        //
        public static T Get<T>(string path = "/")
        {
            return Deserialize<T>(Request(HttpMethod.Get, path));
        }
        //
        //
        public static T Put<T>(string path = "/", string body = "")
        {
            return Deserialize<T>(Request(HttpMethod.Put, path, body));
        }
        public static void Put(string path = "/", string body = "")
        {
            Request(HttpMethod.Put, path, body);
        }
        //
        //
        public static T Post<T>(string path = "/", string body = "")
        {
            return Deserialize<T>(Request(HttpMethod.Post, path, body));
        }
        public static void Post(string path = "/", string body = "")
        {
            Request(HttpMethod.Post, path, body);
        }
        //
        //
        public static T Delete<T>(string path = "/")
        {
            return Deserialize<T>(Request(HttpMethod.Delete, path));
        }
        public static void Delete(string path = "/")
        {
            Request(HttpMethod.Delete, path);
        }

    }
}