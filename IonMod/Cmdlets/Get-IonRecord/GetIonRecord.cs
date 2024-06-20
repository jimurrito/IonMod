namespace IonMod
{
    /// <summary>
    /// A static class that provides methods to retrieve IonRecord from IONOS Zones.
    /// </summary>
    public static class GetIonRecord
    {
        /// <summary>
        /// Retrieves an IonRecord instance using an IonZone instance and a record ID.
        /// </summary>
        /// <param name="zone">The IonZone instance.</param>
        /// <param name="recordId">The ID of the record.</param>
        /// <returns>An IonRecord instance.</returns>
        /// <example>
        /// <code>
        /// var record = GetIonRecord.Run(zone, "recordId");
        /// </code>
        /// </example>
        public static IonRecord Run(IonZone zone, string recordId)
        {
            return Run(zone.Id, recordId);
        }

        /// <summary>
        /// Retrieves an IonRecord instance using an IonZone instance and an IonRecord instance.
        /// </summary>
        /// <param name="zone">The IonZone instance.</param>
        /// <param name="record">The IonRecord instance.</param>
        /// <returns>An IonRecord instance.</returns>
        /// <example>
        /// <code>
        /// var record = GetIonRecord.Run(zone, record);
        /// </code>
        /// </example>
        public static IonRecord Run(IonZone zone, IonRecord record)
        {
            return Run(zone.Id, record.Id);
        }

        /// <summary>
        /// Retrieves an IonRecord instance using a zone ID and a record ID.
        /// </summary>
        /// <param name="zoneId">The ID of the zone.</param>
        /// <param name="recordId">The ID of the record.</param>
        /// <returns>An IonRecord instance.</returns>
        /// <example>
        /// <code>
        /// var record = GetIonRecord.Run("zoneId", "recordId");
        /// </code>
        /// </example>
        public static IonRecord Run(string zoneId, string recordId)
        {
            return IonConnect.Get<IonRecord>("/" + zoneId + "/records/" + recordId);
        }
    }
}
