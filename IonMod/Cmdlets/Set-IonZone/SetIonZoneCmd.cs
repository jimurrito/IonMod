using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : PSCmdlet
    {
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public required string ZoneId { get; set; }
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj", ValueFromPipeline = true)]
        public required IonZone Zone { get; set; }
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required List<IonRecord> Records { get; set; }
        //
        //
        // Logic
        protected override void ProcessRecord()
        {
            //
            switch (ParameterSetName)
            {
                case "ZoneObj+RecObj":
                    ZoneId = Zone.Id;
                    break;
                case "ZoneObj":
                    ZoneId = Zone.Id;
                    Records = Zone.Records;
                    break;
            }
            //
            SetIonZone.Run(ZoneId, Records);
        }
    }
}