using System.Management.Automation;

namespace IonMod
{
    [Cmdlet(VerbsCommon.Get, "IonZone")]
    public class GetIonZoneCmd : PSCmdlet
    {
        [Parameter(ValueFromPipeline = true)]
        public string ZoneId;

        [Parameter(ValueFromPipeline = true)]
        public IonZone Zone;

        protected override void ProcessRecord()
        {
            if (Zone != null) { ZoneId = Zone.Id; }
            WriteObject(ZoneId == null ? GetIonZone.Run() : GetIonZone.Run(ZoneId));
        }
    }
}
