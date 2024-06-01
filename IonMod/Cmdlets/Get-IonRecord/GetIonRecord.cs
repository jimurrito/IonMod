using Newtonsoft.Json;

namespace IonMod
{
    public class GetIonRecord : IonHttp
    {
        public string ZoneId;
        public string RecordId;
        //
        //
        public GetIonRecord(IonToken token, string zoneid, string recordid) : base(token)
        {
            ZoneId = zoneid;
            RecordId = recordid;
        }
        //
        //
        public IonRecord? Run()
        {
            return JsonConvert.DeserializeObject<IonRecord>(Get("/" + ZoneId + "/records/" + RecordId));
        }
    }
}