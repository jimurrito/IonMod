using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// A Cmdlet that connects to IONOS using a public prefix and a secret.
    /// </summary>
    /// <example>
    /// This example shows how to use this Cmdlet with a public prefix and a secret.
    /// <code>
    /// Connect-Ion -PublicPrefix "publicPrefix" -Secret "secret"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommunications.Connect, "Ion")]
    public class ConnectIonCmd : PSCmdlet
    {
        /// <summary>
        /// The public prefix. This parameter is mandatory and can be piped into this Cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string PublicPrefix { get; set; }

        /// <summary>
        /// The secret. This parameter is mandatory and can be piped into this Cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Secret { get; set; }

        /// <summary>
        /// The logic to connect to IONOS.
        /// </summary>
        protected override void ProcessRecord()
        {
            IonConnect.Login(new IonToken(PublicPrefix, Secret));
        }
    }
}
