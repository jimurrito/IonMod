using Newtonsoft.Json;

namespace IonMod
{
    /// <summary>
    /// The SetIonRecord class provides methods to update a DNS record in a zone in IONOS.
    /// </summary>
    public static class SetIonRecord
    {
        /// <summary>
        /// Updates a DNS record in a zone using an IonZone object and an IonRecord object.
        /// </summary>
        /// <param name="zone">The IonZone object representing the zone where the record is located.</param>
        /// <param name="record">The IonRecord object representing the record to be updated.</param>
        /// <returns>An IonRecord object representing the updated record.</returns>
        /// <example>
        /// This sample shows how to call the Run method with an IonZone object and an IonRecord object.
        /// <code>
        /// IonZone zone = new IonZone { Id = "exampleZoneId" };
        /// IonRecord record = new IonRecord { Id = "exampleRecordId" };
        /// IonRecord updatedRecord = SetIonRecord.Run(zone, record);
        /// </code>
        /// </example>
        public static IonRecord Run(IonZone zone, IonRecord record)
        {
            return Run(zone.Id, record);
        }

        /// <summary>
        /// Updates a DNS record in a zone using a zone ID and an IonRecord object.
        /// </summary>
        /// <param name="zoneId">The ID of the zone where the record is located.</param>
        /// <param name="record">The IonRecord object representing the record to be updated.</param>
        /// <returns>An IonRecord object representing the updated record.</returns>
        /// <example>
        /// This sample shows how to call the Run method with a zone ID and an IonRecord object.
        /// <code>
        /// string zoneId = "exampleZoneId";
        /// IonRecord record = new IonRecord { Id = "exampleRecordId" };
        /// IonRecord updatedRecord = SetIonRecord.Run(zoneId, record);
        /// </code>
        /// </example>
        public static IonRecord Run(string zoneId, IonRecord record)
        {
            return IonConnect.Put<IonRecord>("/" + zoneId + "/records/" + record.Id, JsonConvert.SerializeObject(record));
        }
    }
}
