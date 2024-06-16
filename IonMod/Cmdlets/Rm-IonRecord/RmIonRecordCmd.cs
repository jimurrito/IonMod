using System.Management.Automation;

namespace IonMod

{
    // New-IonToken
    [Cmdlet(VerbsCommon.Remove, "IonRecord")]
    public class RemoveIonRecordCmd : PSCmdlet
    {
        //
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public required string ZoneId;
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        public required string RecordId;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public required IonZone Zone;
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required IonRecord Record;
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
            RmIonRecord.Run(ZoneId, RecordId);
        }
    }
}