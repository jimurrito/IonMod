namespace IonMod
{
    public class RmIonRecord : IonHttp
    {
        public string ZoneId;
        public string RecordId;
        //
        //
        public RmIonRecord(IonToken token, string zoneid, string recordid) : base(token)
        {
            ZoneId = zoneid;
            RecordId = recordid;
        }
        //
        public RmIonRecord(IonToken token, string zoneid, IonRecord record) : base(token)
        {
            ZoneId = zoneid;
            RecordId = record.Id;
        }
        //
        //
        public void Run()
        {
            Delete<IonRecord>("/" + ZoneId + "/records/" + RecordId);
        }
    }
}