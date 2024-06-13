namespace IonMod
{
    public static class GetIonZone
    {
        //
        /*
        Gets Zones from IONOS. This returns ALL zones under your IONOS account.
        */
        public static List<IonZone> Run()
        {
            return IonConnect.Get<List<IonZone>>();
        }
        //
        /*
        Gets data on a specfic Zone. Returns a single Zone, and its associated records.
        */
        public static IonZone Run(string zoneId)
        {
            return IonConnect.Get<IonZone>("/" + zoneId);
        }
    }
}