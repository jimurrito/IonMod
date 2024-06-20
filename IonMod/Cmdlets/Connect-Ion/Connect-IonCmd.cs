using System.Management.Automation;

namespace IonMod
{
    [Cmdlet(VerbsCommunications.Connect, "Ion")]
    public class ConnectIonCmd : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string PublicPrefix;

        [Parameter(Mandatory = true, ValueFromPipeline = true)]
        public string Secret;

        protected override void ProcessRecord()
        {
            IonConnect.Login(new IonToken(PublicPrefix, Secret));
        }
    }
}
