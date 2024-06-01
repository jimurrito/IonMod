using Newtonsoft.Json;

namespace IonMod
{
    public class SetIonZone : IonHttp
    {
        public string ZoneId;
        public string Records;
        //
        //
        public SetIonZone(IonToken token, string zoneId, List<IonRecord?> records) : base(token)
        {
            ZoneId = zoneId;
            Records = JsonConvert.SerializeObject(records);
        }
        //
        // Constructor using Zone class
        public SetIonZone(IonToken token, IonZone zone) : base(token)
        {
            ZoneId = zone.Id;
            Records = JsonConvert.SerializeObject(zone.Records);
        }
        //
        //
        public void Run()
        {
            Put("/" + ZoneId, Records);
        }
    }
}