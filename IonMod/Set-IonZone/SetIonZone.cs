using Newtonsoft.Json;

namespace IonMod
{
    public class SetIonZone : IonHttp
    {
        public string ZoneId;
        public string Records;

        public SetIonZone(IonToken token, string zoneId, List<IonRecord> records) : base(token)
        {
            ZoneId = zoneId;
            Records = JsonConvert.SerializeObject(records);
        }

        public List<IonRecord>? Run()
        {
            return JsonConvert.DeserializeObject<List<IonRecord>>(Put("/" + ZoneId, Records)?.ToString());
        }


    }
}