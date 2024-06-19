using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// This cmdlet is used to create a new IonRecordObj.
    /// </summary>
    /// <example>
    /// This sample shows how to call the New-IonRecordObj cmdlet.
    /// <code>
    /// PS C:\> New-IonRecordObj -Name "record.domain.com" -ZoneName "domain.com" -Type "A" -Content "192.0.2.1"
    /// </code>
    /// </example>
    [Cmdlet(VerbsCommon.New, "IonRecordObj")]
    public class NewIonRecordObjCmd : PSCmdlet
    {
        /// <summary>
        /// Gets or sets the Name. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ZoneName. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        public string ZoneName { get; set; }

        /// <summary>
        /// Gets or sets the Zone. This parameter is mandatory and can be piped.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str", ValueFromPipeline = true)]
        public IonZone Zone { get; set; }

        /// <summary>
        /// Gets or sets the Type. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the Content. This parameter is mandatory.
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "default")]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+Str")]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the ChangeDate. This parameter is optional.
        /// </summary>
        [Parameter()]
        public string ChangeDate { get; set; } = "";

        /// <summary>
        /// Gets or sets the TTL. This parameter is optional.
        /// </summary>
        [Parameter()]
        public string TTL { get; set; } = "3600";

        /// <summary>
        /// Gets or sets the Disabled. This parameter is optional.
        /// </summary>
        [Parameter()]
        public bool Disabled { get; set; } = false;

        /// <summary>
        /// The ProcessRecord method is overridden to provide record processing functionality for the cmdlet.
        /// </summary>
        protected override void ProcessRecord()
        {
            if (ParameterSetName == "ZoneObj+Str") { ZoneName = Zone.Name; }

            WriteObject(new IonRecord(Name, Content, ZoneName, Type, ChangeDate, TTL, Disabled));
        }
    }
}
