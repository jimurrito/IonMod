using Newtonsoft.Json;

namespace IonMod
{
    public static class SetIonRecord
    {
        //
        //
        public static IonRecord Run(IonZone zone, IonRecord record)
        {
            return Run(zone.Id, record);
        }
        //
        public static IonRecord Run(string zoneId, IonRecord record)
        {
            return IonConnect.Put<IonRecord>("/" + zoneId + "/records/" + record.Id, JsonConvert.SerializeObject(record));
        }
    }
}