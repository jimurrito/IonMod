namespace IonMod
{
    /// <summary>
    /// Represents a DNS zone in IONOS.
    /// </summary>
    public class IonZone
    {
        public string Name;
        public string Id;
        public string Type;

        /// <summary>
        /// Gets or sets the list of DNS records in the zone.
        /// </summary>
        /// <exception cref="IonUninitException">Thrown when the records list is null.</exception>
        public List<IonRecord> Records
        {
            get { return records ?? throw new IonUninitException("Zone object provided does not contain Records: (List<IonRecord>)."); }
            set { records = value; }
        }
        private List<IonRecord>? records;

        /// <summary>
        /// Initializes a new instance of the IonZone class.
        /// </summary>
        /// <param name="name">The name of the DNS zone.</param>
        /// <param name="id">The ID of the DNS zone.</param>
        /// <param name="type">The type of the DNS zone.</param>
        /// <param name="records">The list of DNS records in the zone.</param>
        public IonZone(string name, string id, string type, List<IonRecord> records)
        {
            Name = name;
            Id = id;
            Type = type;
            Records = records;
        }

        /// <summary>
        /// Initializes the list of DNS records in the zone by sending a GET request to the IONOS API.
        /// </summary>
        public void InitRecords()
        {
            Records = IonConnect.Get<IonZone>(Id).Records;
        }
    }
}
