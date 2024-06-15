using System.Management.Automation;           // Windows PowerShell namespace.
namespace IonMod


{
    [Cmdlet(VerbsCommon.Set, "IonRecord")]
    public class SetIonRecordCmd : PSCmdlet
    {
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public required string ZoneId { get; set; }
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public required IonZone Zone { get; set; }
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required IonRecord Record { get; set; }
        //
        //
        // Logic
        protected override void ProcessRecord()
        {
            // override if Zone obj is provided
            if (ParameterSetName == "ZoneObj+RecObj")
            {
                ZoneId = Zone.Id;
            }
            //
            WriteObject(SetIonRecord.Run(ZoneId, Record));
        }
    }
}