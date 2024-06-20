using System.Text;
using System.Net;
using Newtonsoft.Json;

namespace IonMod
{
    /// <summary>
    /// Provides generic methods to connect to the IONOS domain REST API.
    /// </summary>
    public static class IonConnect
    {
        private static readonly HttpClient Client = new HttpClient();
        public static string RootURI = "https://api.hosting.ionos.com/dns/v1/zones";
        private static string ContentType = "application/json";
        private static IonToken? Token;

        /// <summary>
        /// Initializes the IONOS token using an existing IonToken object.
        /// </summary>
        /// <param name="token">The IonToken object.</param>
        public static void Login(IonToken token)
        {
            Token = token;
        }

        /// <summary>
        /// Initializes the IONOS token using a public prefix and secret.
        /// </summary>
        /// <param name="publicprefix">The public prefix.</param>
        /// <param name="secret">The secret.</param>
        public static void Login(string publicprefix, string secret)
        {
            Token = new(publicprefix, secret);
        }

        /// <summary>
        /// Checks if the IONOS token has been initialized.
        /// </summary>
        private static void LoginCheck()
        {
            if (Token == null) { throw new IonUninitLoginException("Login for IONOS is required. Please use the method `IonConnect.Login()` to initialize the IONOS token."); }
        }

        /// <summary>
        /// Sends a request to the IONOS API.
        /// </summary>
        /// <param name="method">The HTTP method.</param>
        /// <param name="path">The API path.</param>
        /// <param name="body">The request body.</param>
        /// <returns>The response content.</returns>
        private static string Request(HttpMethod method, string path = "/", string body = "")
        {
            LoginCheck();
            HttpRequestMessage request = new(method, RootURI + path);
            if (method == HttpMethod.Put || method == HttpMethod.Post)
            {
                request.Content = new StringContent(body.ToLower(), Encoding.UTF8, ContentType);
            }
            request.Headers.Add("X-Api-Key", Token.PublicPrefix + "." + Token.Secret);
            request.Headers.Add("User-Agent", "IonMod");
            HttpResponseMessage response = Client.Send(request);
            StreamReader reader = new StreamReader(response.Content.ReadAsStream());
            switch (response.StatusCode)
            {
                case HttpStatusCode.BadRequest:
                    throw new IonBadRequestException("[HTTP::400] " + reader.ReadToEnd());
                case HttpStatusCode.Unauthorized:
                    throw new IonUnauthorizedException("[HTTP::401] " + reader.ReadToEnd());
                case HttpStatusCode.InternalServerError:
                    throw new IonServerErrorException("[HTTP::500] " + reader.ReadToEnd());
            }
            return reader.ReadToEnd();
        }

        /// <summary>
        /// Deserializes a JSON string into an object of type T.
        /// </summary>
        /// <param name="input">The JSON string.</param>
        /// <returns>The deserialized object.</returns>
        private static T Deserialize<T>(string input)
        {
            Func<string, IonDeserialException> throwE = input =>
             {
                 throw new IonDeserialException("Deserialization on <[" + input + "]> failed. Please check it is compatible with the target class.");
             };

            try { return JsonConvert.DeserializeObject<T>(input) ?? throw throwE(input); }
            catch { throw throwE(input); }
        }

        /// <summary>
        /// Sends a GET request to the IONOS API and returns the response as an object of type T.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <returns>The response object.</returns>
        public static T Get<T>(string path = "/")
        {
            return Deserialize<T>(Request(HttpMethod.Get, path));
        }

        /// <summary>
        /// Sends a PUT request to the IONOS API and returns the response as an object of type T.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <param name="body">The request body.</param>
        /// <returns>The response object.</returns>
        public static T Put<T>(string path = "/", string body = "")
        {
            return Deserialize<T>(Request(HttpMethod.Put, path, body));
        }

        /// <summary>
        /// Sends a PUT request to the IONOS API.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <param name="body">The request body.</param>
        public static void Put(string path = "/", string body = "")
        {
            Request(HttpMethod.Put, path, body);
        }

        /// <summary>
        /// Sends a POST request to the IONOS API and returns the response as an object of type T.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <param name="body">The request body.</param>
        /// <returns>The response object.</returns>
        public static T Post<T>(string path = "/", string body = "")
        {
            return Deserialize<T>(Request(HttpMethod.Post, path, body));
        }

        /// <summary>
        /// Sends a POST request to the IONOS API.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <param name="body">The request body.</param>
        public static void Post(string path = "/", string body = "")
        {
            Request(HttpMethod.Post, path, body);
        }

        /// <summary>
        /// Sends a DELETE request to the IONOS API and returns the response as an object of type T.
        /// </summary>
        /// <param name="path">The API path.</param>
        /// <returns>The response object.</returns>
        public static T Delete<T>(string path = "/")
        {
            return Deserialize<T>(Request(HttpMethod.Delete, path));
        }

        /// <summary>
        /// Sends a DELETE request to the IONOS API.
        /// </summary>
        /// <param name="path">The API path.</param>
        public static void Delete(string path = "/")
        {
            Request(HttpMethod.Delete, path);
        }
    }
}
