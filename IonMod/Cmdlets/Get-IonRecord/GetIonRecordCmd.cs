using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// A Cmdlet that retrieves an IonRecord instance.
    /// </summary>
    /// <example>
    /// This example shows how to use this Cmdlet with a zone ID and a record ID.
    /// <code>
    /// Get-IonRecord -ZoneId "zoneId" -RecordId "recordId"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Get, "IonRecord")]
    public class GetIonRecordCmd : PSCmdlet
    {
        /// <summary>
        /// The ID of the zone. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId { get; set; }

        /// <summary>
        /// The ID of the record. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        public string RecordId { get; set; }

        /// <summary>
        /// The IonZone instance. This parameter is mandatory and can be piped into this Cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public IonZone Zone { get; set; }

        /// <summary>
        /// The IonRecord instance. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public IonRecord Record { get; set; }

        /// <summary>
        /// The logic to retrieve the IonRecord instance.
        /// </summary>
        protected override void ProcessRecord()
        {
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

            WriteObject(GetIonRecord.Run(ZoneId, RecordId));
        }
    }
}
