using IonMod;
using Newtonsoft.Json;

namespace IonModTest
{
    internal static class TestIonParameters
    {
        /*
        Create a static private prop that pulls from the SECRETS.json file
        Use the new obj to fill in the below info.
        */
        private static readonly Secrets Secrets = JsonConvert.DeserializeObject<Secrets>(File.ReadAllText("C:/Users/james/Documents/code/IonMod/SECRETS.json")) ?? throw new Exception("SECRETS.json not found at path provided.");
        //
        private static readonly string PublicPrefix = Secrets.PublicPrefix;
        private static readonly string Secret = Secrets.Secret;
        private static readonly IonToken Token = new(PublicPrefix, Secret);
        public static readonly string ZoneId = Secrets.TestZoneId;
        public static readonly string RecordId = Secrets.TestRecordId;
        //
        //
        public static void LoginTest()
        {
            IonConnect.Login(Token);
        }

    }

    //
    // Secret Deserialization template
    internal class Secrets
    {
        #pragma warning disable // Suppresses warnings about the properties not being assigned.
        //
        public required string DumpPath;
        public required string ModulePath;
        public required string PublicPrefix;
        public required string Secret;
        public required string TestZoneId;
        public required string TestRecordId;
        //
        #pragma warning restore
    }




}