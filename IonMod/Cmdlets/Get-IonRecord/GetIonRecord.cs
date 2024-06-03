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
        public IonRecord Run()
        {
            return Get<IonRecord>("/" + ZoneId + "/records/" + RecordId);
        }
    }
}