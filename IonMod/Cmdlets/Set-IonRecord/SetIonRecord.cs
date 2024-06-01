using Newtonsoft.Json;

namespace IonMod
{
    public class SetIonRecord : IonHttp
    {
        public string ZoneId;
        public string Record;
        public string RecordId;
        //
        //
        public SetIonRecord(IonToken token, string zoneId, IonRecord record) : base(token)
        {
            ZoneId = zoneId;
            RecordId = record.Id;
            Record = JsonConvert.SerializeObject(record);
        }
        //
        //
        public void Run()
        {
            Put("/" + ZoneId + "/records/" + RecordId, Record);
        }
    }
}