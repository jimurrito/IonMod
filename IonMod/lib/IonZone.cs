namespace IonMod
{
    public class IonZone
    {
        // Data
        public string Name;
        public string Id;
        public string Type;
        public List<IonRecord> Records
        // throw custom exception to avoid null object error
        {
            get { return records ?? throw new IonUninitException("Zone object provided does not contain Records: (List<IonRecord>)."); }
            set { records = value; }
        }
        private List<IonRecord>? records;
        //
        // Constructor
        public IonZone(string name, string id, string type, List<IonRecord> records)
        {
            Name = name;
            Id = id;
            Type = type;
            Records = records;
        }
        //
        // Init Records (Pulls records)
        public void InitRecords()
        {
            Records = IonConnect.Get<IonZone>(Id).Records;
        }
    }
}