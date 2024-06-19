using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// The RemoveIonRecordCmd class is a PowerShell cmdlet that removes a DNS record from a zone in IONOS.
    /// </summary>
    /// <example>
    /// This sample shows how to call the RemoveIonRecordCmd cmdlet.
    /// <code>
    /// PS> Remove-IonRecord -ZoneId "exampleZoneId" -RecordId "exampleRecordId"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Remove, "IonRecord")]
    public class RemoveIonRecordCmd : PSCmdlet
    {
        /// <summary>
        /// Gets or sets the ZoneId parameter. This parameter is required.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId { get; set; }

        /// <summary>
        /// Gets or sets the RecordId parameter. This parameter is required.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringIds")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId")]
        public string RecordId { get; set; }

        /// <summary>
        /// Gets or sets the Zone object. This parameter is required and can be piped to the cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public IonZone Zone { get; set; }

        /// <summary>
        /// Gets or sets the Record object. This parameter is required and can be piped to the cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public IonRecord Record { get; set; }

        /// <summary>
        /// The ProcessRecord method overrides the base method to remove a DNS record from a zone.
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
            
            RmIonRecord.Run(ZoneId, RecordId);
        }
    }
}
