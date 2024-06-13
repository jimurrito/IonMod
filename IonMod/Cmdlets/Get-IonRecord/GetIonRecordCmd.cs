using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Get, "IonRecord")]
    public class GetIonRecordCmd : Cmdlet
    {
        //
        //
        // Params
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId")]
        public required string ZoneId { get; set; }
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        public required string RecordId { get; set; }
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required IonZone Zone { get; set; }
        //
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required IonRecord Record { get; set; }
        //
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // Zone obj takes priority
            if (Zone != null)
            {
                ZoneId = Zone.Id;
            }
            // Record obj takes priority
            if (Record != null)
            {
                RecordId = Record.Id;
            }
        }
        //
        protected override void ProcessRecord()
        {
            WriteObject(GetIonRecord.Run(ZoneId, RecordId));
        }
    }
}