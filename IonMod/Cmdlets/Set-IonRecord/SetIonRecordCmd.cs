using System.Management.Automation;           // Windows PowerShell namespace.

namespace IonMod
{

    [Cmdlet(VerbsCommon.Set, "IonRecord")]
    public class SetIonRecordCmd : PSCmdlet
    {

        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId;

        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public IonZone Zone;

        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public IonRecord Record;

        protected override void ProcessRecord()
        {
            // override if Zone obj is provided
            if (ParameterSetName == "ZoneObj+RecObj")
            {
                ZoneId = Zone.Id;
            }

            WriteObject(SetIonRecord.Run(ZoneId, Record));
        }
    }
}
