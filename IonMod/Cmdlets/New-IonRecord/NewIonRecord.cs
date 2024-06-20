using Newtonsoft.Json;

namespace IonMod
{
    /// <summary>
    /// This class provides methods to create new DNS records in a specific zone using IONOS's DNS domain REST API.
    /// </summary>
    public static class NewIonRecord
    {
        /// <summary>
        /// Creates new DNS records in the specified zone.
        /// </summary>
        /// <param name="zone">The IonZone object representing the zone where the records will be created.</param>
        /// <param name="records">A list of IonRecord objects representing the DNS records to be created.</param>
        /// <returns>A list of IonRecord objects representing the created DNS records.</returns>
        /// <example>
        /// <code>
        /// var zone = new IonZone { Id = "12345" };
        /// var records = new List<IonRecord> { new IonRecord { Type = "A", Value = "192.0.2.1" } };
        /// var newRecords = NewIonRecord.Run(zone, records);
        /// </code>
        /// This example creates a new A record in the zone with the ID 12345.
        /// </example>
        public static List<IonRecord> Run(IonZone zone, List<IonRecord> records)
        {
            return Run(zone.Id, records);
        }

        /// <summary>
        /// Creates new DNS records in the specified zone.
        /// </summary>
        /// <param name="zoneId">The ID of the zone where the records will be created.</param>
        /// <param name="records">A list of IonRecord objects representing the DNS records to be created.</param>
        /// <returns>A list of IonRecord objects representing the created DNS records.</returns>
        /// <example>
        /// <code>
        /// var zoneId = "12345";
        /// var records = new List<IonRecord> { new IonRecord { Type = "A", Value = "192.0.2.1" } };
        /// var newRecords = NewIonRecord.Run(zoneId, records);
        /// </code>
        /// This example creates a new A record in the zone with the ID 12345.
        /// </example>
        public static List<IonRecord> Run(string zoneId, List<IonRecord> records)
        {
            return IonConnect.Post<List<IonRecord>>("/" + zoneId + "/records", JsonConvert.SerializeObject(records));
        }
    }
}
