namespace IonMod
{
    public class IonToken
    {
        public string PublicPrefix;
        public string Secret;

        // Constructor
        public IonToken(string publicPrefix, string secret)
        {
            PublicPrefix = publicPrefix;
            Secret = secret;
        }
    }
}