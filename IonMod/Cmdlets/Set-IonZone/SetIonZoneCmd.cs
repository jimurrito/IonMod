using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : Cmdlet
    {
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId")]
        public required string? ZoneId { get { return zoneid; } set { zoneid = value; } }
        private string? zoneid;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public required IonZone? Zone { get { return zone; } set { zone = value; } }
        private IonZone? zone;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required List<IonRecord>? Records { get { return records; } set { records = value; } }
        private  List<IonRecord>? records;
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Zone obj is provided
            if (zone != null)
            {
                WriteError("IonZone object used.");
                zoneid = zone.Id;
                records = zone.Records ?? throw new IonUninitException();
            }
            else
            {
                throw new IonUninitException();
            }
        }
        //
        //
        protected override void ProcessRecord()
        {
            SetIonZone.Run(zoneid, records);
        }
    }
}