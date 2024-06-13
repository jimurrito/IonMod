using System.Management.Automation;

namespace IonMod


{
    //[CmdletBinding(DefaultParameterSetName = "none")]
    [Cmdlet(VerbsCommon.Get, "IonZone")]
    public class GetIonZoneCmd : Cmdlet
    {
        //
        // Params
        //[Parameter(ValueFromPipeline = true, ParameterSetName = "stringId")]
        [Parameter(ValueFromPipeline = true)]
        public string? ZoneId { get; set; }
        //
        //[Parameter(ValueFromPipeline = true, ParameterSetName = "ZoneObj")]
        [Parameter(ValueFromPipeline = true)]
        public IonZone? Zone { get; set; }
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