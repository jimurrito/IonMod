using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : PSCmdlet
    {
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public required string ZoneId;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj", ValueFromPipeline = true)]
        public required IonZone Zone;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required List<IonRecord> Records;
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