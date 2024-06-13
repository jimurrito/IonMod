namespace IonMod
{
    public class IonZone
    {
        // Data
        public string Name;
        public string Id;
        public string Type;
        public List<IonRecord>? Records;
        //
        // Constructor
        public IonZone(string name, string id, string type, List<IonRecord>? records = default)
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