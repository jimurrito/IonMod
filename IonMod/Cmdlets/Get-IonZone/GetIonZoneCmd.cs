using System.Management.Automation;

namespace IonMod
{
    /// <summary>
    /// This PowerShell cmdlet is used to get information about a specific zone or all zones from IONOS's DNS domain REST API.
    /// </summary>
    /// <example>
    /// <code>
    /// PS C:\> Get-IonZone -ZoneId "12345"
    /// </code>
    /// This command gets the zone with the ID 12345.
    /// </example>
    /// <example>
    /// <code>
    /// PS C:\> Get-IonZone
    /// </code>
    /// This command gets all zones.
    /// </example>
    [Cmdlet(VerbsCommon.Get, "IonZone")]
    public class GetIonZoneCmd : PSCmdlet
    {
        /// <summary>
        /// The ID of the zone to retrieve. This parameter can be piped into this cmdlet.
        /// If both ZoneId and Zone object are provided, Zone object takes priority.
        /// </summary>
        /// <example>
        /// <code>
        /// PS C:\> Get-IonZone -ZoneId "12345"
        /// </code>
        /// This command gets the zone with the ID 12345.
        /// </example>
        [Parameter(ValueFromPipeline = true)]
        public string ZoneId 

        /// <summary>
        /// The Zone object to retrieve. This parameter can be piped into this cmdlet.
        /// If provided, its ID will be used as the ZoneId.
        /// </summary>
        /// <example>
        /// <code>
        /// PS C:\> $zone = New-Object -TypeName IonMod.IonZone
        /// PS C:\> $zone.Id = "12345"
        /// PS C:\> Get-IonZone -Zone $zone
        /// </code>
        /// This command gets the zone using a Zone object.
        /// </example>
        [Parameter(ValueFromPipeline = true)]
        public IonZone Zone 

        /// <summary>
        /// This method is called once for each cmdlet in the pipeline when the pipeline starts executing.
        /// </summary>
        protected override void ProcessRecord()
        {
            // Pipe validation
            // Zone object takes priority over ZoneId input
            if (Zone != null) { ZoneId = Zone.Id; }

            // If ZoneId is null, it pulls all Zones available
            // Otherwise, it pulls a single zone and associated records
            WriteObject(ZoneId == null ? GetIonZone.Run() : GetIonZone.Run(ZoneId));
        }
    }
}
