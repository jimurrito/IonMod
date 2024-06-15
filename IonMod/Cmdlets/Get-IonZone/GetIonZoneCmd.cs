using System.Management.Automation;

namespace IonMod


{
    //[CmdletBinding(DefaultParameterSetName = "none")]
    [Cmdlet(VerbsCommon.Get, "IonZone")]
    public class GetIonZoneCmd : PSCmdlet
    {
        //
        // Params
        [Parameter(ValueFromPipeline = true)]
        public required string ZoneId { get; set; }
        //
        [Parameter(ValueFromPipeline = true)]
        public required IonZone Zone { get; set; }
        //
        // Logic
        protected override void BeginProcessing()
        {
            // Zone obj takes priority over zoneId input
            if (Zone != null)
            {
                ZoneId = Zone.Id;
            }
        }
        //
        protected override void ProcessRecord()
        {
            if (ZoneId == null)
            {
                // pulls all Zones available
                WriteObject(GetIonZone.Run());
            }
            else
            {
                // pulls a single zone + associated records
                WriteObject(GetIonZone.Run(ZoneId));
            }
        }
    }
}