using Newtonsoft.Json;

namespace IonMod
{
    public class GetIonRecord : IonHttp
    {
        public string ZoneId;
        //
        //
        public GetIonRecord(IonToken token, string zoneid) : base(token)
        {
            ZoneId = zoneid;
        }
        //
        //
        public IonRecord? Run(string recordid)
        {
            return JsonConvert.DeserializeObject<IonRecord>(Get("/" + ZoneId + "/records/" + recordid));
        }
    }
}