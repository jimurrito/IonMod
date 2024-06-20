using System.Management.Automation;

namespace IonMod
{
    [Cmdlet(VerbsCommon.New, "IonRecordObj")]
    public class NewIonRecordObjCmd : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Name;

        [Parameter(Mandatory = true, ParameterSetName = "default")]
        public string ZoneName;

        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str", ValueFromPipeline = true)]
        public IonZone Zone;

        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Type;

        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Content;

        [Parameter()]
        public string ChangeDate = "";

        [Parameter()]
        public string TTL = "3600";

        [Parameter()]
        public bool Disabled = false;


        protected override void ProcessRecord()
        {
            if (ParameterSetName == "ZoneObj+Str") { ZoneName = Zone.Name; }

            WriteObject(new IonRecord(Name, Content, ZoneName, Type, ChangeDate, TTL, Disabled));
        }
    }
}
