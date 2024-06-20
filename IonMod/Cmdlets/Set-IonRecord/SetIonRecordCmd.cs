using System.Management.Automation;           // Windows PowerShell namespace.

namespace IonMod
{
    /// <summary>
    /// The SetIonRecordCmd class is a PowerShell cmdlet that sets a DNS record in a zone in IONOS.
    /// </summary>
    /// <example>
    /// This sample shows how to call the SetIonRecordCmd cmdlet.
    /// <code>
    /// PS> Set-IonRecord -ZoneId "exampleZoneId" -Record $record
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.Set, "IonRecord")]
    public class SetIonRecordCmd : PSCmdlet
    {
        /// <summary>
        /// Gets or sets the ZoneId parameter. This parameter is required.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId; 

        /// <summary>
        /// Gets or sets the Zone object. This parameter is required and can be piped to the cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public IonZone Zone; 

        /// <summary>
        /// Gets or sets the Record object. This parameter is required and can be piped to the cmdlet.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public IonRecord Record;

        /// <summary>
        /// The ProcessRecord method overrides the base method to set a DNS record in a zone.
        /// </summary>
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
