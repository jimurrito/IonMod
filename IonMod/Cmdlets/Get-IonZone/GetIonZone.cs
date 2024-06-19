namespace IonMod
{
    /// <summary>
    /// A static class that provides methods to retrieve IonZone instances.
    /// </summary>
    public static class GetIonZone
    {
        /// <summary>
        /// Retrieves all IonZone instances under your IONOS account.
        /// </summary>
        /// <returns>A list of IonZone instances.</returns>
        /// <example>
        /// <code>
        /// var zones = GetIonZone.Run();
        /// </code>
        /// </example>
        public static List<IonZone> Run()
        {
            return IonConnect.Get<List<IonZone>>();
        }

        /// <summary>
        /// Retrieves data on a specific Zone. Returns a single Zone and its associated records.
        /// </summary>
        /// <param name="zoneId">The ID of the zone.</param>
        /// <returns>An IonZone instance.</returns>
        /// <example>
        /// <code>
        /// var zone = GetIonZone.Run("zoneId");
        /// </code>
        /// </example>
        public static IonZone Run(string zoneId)
        {
            return IonConnect.Get<IonZone>("/" + zoneId);
        }
    }
}
