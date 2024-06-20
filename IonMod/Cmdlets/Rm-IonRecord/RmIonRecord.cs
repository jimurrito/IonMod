namespace IonMod
{
    /// <summary>
    /// This class provides methods to remove an IonRecord.
    /// </summary>
    public static class RmIonRecord
    {
        /// <summary>
        /// Removes an IonRecord from a given IonZone.
        /// </summary>
        /// <param name="zone">The IonZone from which the record will be removed.</param>
        /// <param name="record">The IonRecord to be removed.</param>
        public static void Run(IonZone zone, IonRecord record)
        {
            Run(zone.Id, record.Id);
        }

        /// <summary>
        /// Removes an IonRecord from a given zoneId.
        /// </summary>
        /// <param name="zoneId">The ID of the zone from which the record will be removed.</param>
        /// <param name="record">The IonRecord to be removed.</param>
        public static void Run(string zoneId, IonRecord record)
        {
            Run(zoneId, record.Id);
        }

        /// <summary>
        /// Removes an IonRecord with a given recordId from a given IonZone.
        /// </summary>
        /// <param name="zone">The IonZone from which the record will be removed.</param>
        /// <param name="recordId">The ID of the IonRecord to be removed.</param>
        public static void Run(IonZone zone, string recordId)
        {
            Run(zone.Id, recordId);
        }

        /// <summary>
        /// Removes an IonRecord with a given recordId from a given zoneId.
        /// </summary>
        /// <param name="zoneId">The ID of the zone from which the record will be removed.</param>
        /// <param name="recordId">The ID of the IonRecord to be removed.</param>
        public static void Run(string zoneId, string recordId)
        {
            IonConnect.Delete("/" + zoneId + "/records/" + recordId);
        }
    }
}
