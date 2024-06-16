using System.Management.Automation;
namespace IonMod
//
{
    [Cmdlet(VerbsCommon.New, "IonRecordObj")]
    public class NewIonRecordObjCmd : PSCmdlet
    {
        //
        //
        // FQDN of the record Ex:'record.domain.com'
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public required string Name;
        //
        //
        // Higher level sub/domain name Ex:'domain.com'
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        public required string ZoneName;
        //
        //
        // Higher level sub/domain name Ex:'domain.com'
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str", ValueFromPipeline = true)]
        public required IonZone Zone;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public required string Type;
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public required string Content;
        //
        //
        [Parameter()]
        public required string ChangeDate = "";
        //
        //
        [Parameter()]
        public required string TTL = "3600";
        //
        //
        [Parameter()]
        public required bool Disabled = false;
        //
        //
        // Logic
        protected override void ProcessRecord()
        {
            //
            if (ParameterSetName == "ZoneObj+Str") { ZoneName = Zone.Name; }
            //
            WriteObject(new IonRecord(Name, Content, ZoneName, Type, ChangeDate, TTL, Disabled));
        }
    }
}