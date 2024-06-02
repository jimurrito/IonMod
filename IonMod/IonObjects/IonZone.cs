namespace IonMod
{
    public class IonZone
    {
        public string Name;
        public string Id;
        public string Type;
        public List<IonRecord?>? Records;
        //
        //
        public IonZone(string name, string id, string type, List<IonRecord?>? records = default)
        {
            Name = name;
            Id = id;
            Type = type;
            Records = records;
        }
    }
}