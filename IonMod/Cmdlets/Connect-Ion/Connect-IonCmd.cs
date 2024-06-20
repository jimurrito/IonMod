using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// Connects to IONOS using the public prefix and a secret provided by IONOS.
    /// </summary>
    /// <example>
    /// <code>
    /// Connect-Ion -PublicPrefix "publicPrefix" -Secret "secret"
    /// </code>
    /// </example>
    /// <remarks>
    /// This cmdlet is required to interact with the rest of the SDK
    /// </remarks>


    [Cmdlet(VerbsCommunications.Connect, "Ion")]
    public class ConnectIonCmd : PSCmdlet
    {
        /// <summary>
        /// The public prefix. This parameter is mandatory and can be piped into this Cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string PublicPrefix;

        /// <summary>
        /// The secret. This parameter is mandatory and can be piped into this Cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Secret;

        /// <summary>
        /// The logic to connect to IONOS.
        /// </summary>
        protected override void ProcessRecord()
        {
            IonConnect.Login(new IonToken(PublicPrefix, Secret));
        }
    }
}
