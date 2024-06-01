using Newtonsoft.Json;

namespace IonMod
{
    public class RmIonRecord : IonHttp
    {
        public string ZoneId;
        //
        //
        public RmIonRecord(IonToken token, string zoneid) : base(token)
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