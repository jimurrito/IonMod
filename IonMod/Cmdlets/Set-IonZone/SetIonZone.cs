using Newtonsoft.Json;
using System.Collections.Generic;

/// <summary>
/// The IonMod namespace contains all the classes for the IonMod library.
/// </summary>
namespace IonMod
{
    /// <summary>
    /// The SetIonZone class provides methods to set the DNS zone records for a specific zone in IONOS.
    /// </summary>
    public static class SetIonZone
    {
        /// <summary>
        /// Sets the DNS zone records for the specified zone.
        /// </summary>
        /// <param name="zone">The IonZone object containing the ID of the zone and the list of records to be set.</param>
        /// <example>
        /// This sample shows how to call the Run method.
        /// <code>
        ///     IonZone zone = new IonZone { Id = "zone1", Records = new List<IonRecord> { new IonRecord { /* record data */ } } };
        ///     SetIonZone.Run(zone);
        /// </code>
        /// </example>
        public static void Run(IonZone zone)
        {
            Run(zone.Id, zone.Records);
        }

        /// <summary>
        /// Sets the DNS zone records for the specified zone.
        /// </summary>
        /// <param name="zoneId">The ID of the zone.</param>
        /// <param name="records">The list of IonRecord objects to be set for the zone.</param>
        /// <example>
        /// This sample shows how to call the Run method.
        /// <code>
        ///     List<IonRecord> records = new List<IonRecord> { new IonRecord { /* record data */ } };
        ///     SetIonZone.Run("zone1", records);
        /// </code>
        /// </example>
        public static void Run(string zoneId, List<IonRecord> records)
        {
            IonConnect.Put("/" + zoneId, JsonConvert.SerializeObject(records));
        }
    }
}
