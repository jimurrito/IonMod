using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// <para type="synopsis">Sets the DNS zone records for a specific zone in IONOS.</para>
    /// <para type="description">This cmdlet allows you to set the DNS zone records for a specific zone in IONOS by providing either a Zone object or a ZoneId and a list of Records.</para>
    /// <example>
    /// <code>PS C:\> Set-IonZone -ZoneId "zone1" -Records $records</code>
    /// <para>This will set the DNS zone records for the zone with ID "zone1".</para>
    /// </example>
    /// <example>
    /// <code>PS C:\> Set-IonZone -Zone $zone</code>
    /// <para>This will set the DNS zone records for the zone specified by the Zone object.</para>
    /// </example>
    /// </summary>
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : PSCmdlet
    {
        /// <summary>
        /// <para type="description">The ID of the zone.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj")]
        public string ZoneId { get; set; }

        /// <summary>
        /// <para type="description">The IonZone object containing the ID of the zone and the list of records to be set.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj", ValueFromPipeline = true)]
        public IonZone Zone { get; set; }

        /// <summary>
        /// <para type="description">The list of IonRecord objects to be set for the zone.</para>
        /// </summary>
        [Parameter(Mandatory = true, ParameterSetName = "stringId+RecObj", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public List<IonRecord> Records { get; set; }

        /// <summary>
        /// The ProcessRecord method is called once for each input record.
        /// </summary>
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

            SetIonZone.Run(ZoneId, Records);
        }
    }
}
