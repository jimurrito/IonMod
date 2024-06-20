using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// This cmdlet is used to create a new IonRecord in IONOS.
    /// </summary>
    /// <example>
    /// This sample shows how to call the New-IonRecord cmdlet.
    /// <code>
    /// PS C:\> New-IonRecord -ZoneId "exampleZoneId" -Records $exampleRecords
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "IonRecord")]
    public class NewIonRecordCmd : PSCmdlet
    {
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId;

        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj", ValueFromPipeline = true)]
        public IonZone Zone;

        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public List<IonRecord> Records;

        protected override void ProcessRecord()
        {
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

            WriteObject(NewIonRecord.Run(ZoneId, Records));
        }
    }
}
