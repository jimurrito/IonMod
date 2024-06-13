using Newtonsoft.Json;


namespace IonMod
{
    public static class NewIonRecord
    {
        //
        /*
        */
        public static List<IonRecord> Run(IonZone zone, List<IonRecord> records)
        {
            return Run(zone.Id, records);
        }
        //
        public static List<IonRecord> Run(string zoneId, List<IonRecord> records)
        {
            return IonConnect.Post<List<IonRecord>>("/" + zoneId + "/records", JsonConvert.SerializeObject(records));
        }
        //
    }
}