using System.Management.Automation;

namespace IonMod


{
    [Cmdlet(VerbsCommon.Get, "IonZone")]
    public class GetIonZoneCmd : PSCmdlet
    {
        //
        // Params
        [Parameter(ValueFromPipeline = true)]
        public required string ZoneId;
        //
        [Parameter(ValueFromPipeline = true)]
        public required IonZone Zone;
        //
        // Logic
        protected override void ProcessRecord()
        {
            // Pipe validation
            // Zone obj takes priority over zoneId input
            if (Zone != null) { ZoneId = Zone.Id; }
            //
            // pulls
            // pulls all Zones available
            if (ZoneId == null) { WriteObject(GetIonZone.Run()); }
            // pulls a single zone + associated records
            else { WriteObject(GetIonZone.Run(ZoneId)); }
        }
    }
}