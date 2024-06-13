using Newtonsoft.Json;

namespace IonMod
{
    public static class SetIonZone
    {
        //
        //
        public static void Run(IonZone zone)
        {
            Run(zone.Id, zone.Records);
        }
        //
        public static void Run(string zoneId, List<IonRecord> records)
        {
            IonConnect.Put("/" + zoneId, JsonConvert.SerializeObject(records));
        }
    }
}