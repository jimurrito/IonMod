namespace IonMod
{
    /// <summary>
    /// Represents an IONOS token. Typically not needed. Only Connect-Ion consumes it, and it can also use the same switch arguments for the secret.
    /// </summary>
    public class IonToken
    {
        public string PublicPrefix;
        public string Secret;

        /// <summary>
        /// Initializes a new instance of the IonToken class.
        /// </summary>
        /// <param name="publicPrefix">The public prefix of the token.</param>
        /// <param name="secret">The secret of the token.</param>
        public IonToken(string publicPrefix, string secret)
        {
            PublicPrefix = publicPrefix;
            Secret = secret;
        }
    }
}
