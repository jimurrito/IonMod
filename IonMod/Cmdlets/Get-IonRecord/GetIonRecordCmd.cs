using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Get, "IonRecord")]
    public class GetIonRecordCmd : PSCmdlet
    {
        //
        //
        // Params
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public required string ZoneId { get; set; }
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        public required string RecordId { get; set; }
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public required IonZone Zone { get; set; }
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required IonRecord Record { get; set; }
        //
        //
        //
        // Logic
        protected override void ProcessRecord()
        {
            // 
            switch (ParameterSetName)
            {
                case "stringId+RecObj":
                    RecordId = Record.Id;
                    break;
                case "ZoneObj+stringId":
                    ZoneId = Zone.Id;
                    break;
                case "ZoneObj+RecObj":
                    ZoneId = Zone.Id;
                    RecordId = Record.Id;
                    break;
            }
            //
            WriteObject(GetIonRecord.Run(ZoneId, RecordId));
        }
    }
}