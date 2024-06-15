using System.Management.Automation;

namespace IonMod


{
    [Cmdlet(VerbsCommunications.Connect, "Ion")]
    public class ConnectIonCmd : PSCmdlet
    {
        //
        // Params
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public required string PublicPrefix { get; set; }
        //
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public required string Secret { get; set; }
        //
        // Logic
        protected override void ProcessRecord()
        {
            IonConnect.Login(new IonToken(PublicPrefix,Secret));
        }
    }
}