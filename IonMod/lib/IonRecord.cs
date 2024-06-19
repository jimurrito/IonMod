namespace IonMod
{
    /// <summary>
    /// Represents a DNS record in an IONOS domain.
    /// </summary>
    public class IonRecord
    {
        public string Name;
        public string RootName;
        public string Type;
        public string Content;
        public string ChangeDate;
        public string TTL;
        public bool Disabled;
        public string Id;

        /// <summary>
        /// Initializes a new instance of the IonRecord class.
        /// </summary>
        /// <param name="name">The name of the DNS record. Ex. rec.domain.com</param>
        /// <param name="content">The content of the DNS record.</param>
        /// <param name="rootname">The root name of the DNS record. Ex. domain.com</param>
        /// <param name="type">The type of the DNS record. Default is "A".</param>
        /// <param name="changedate">The change date of the DNS record. Set by IONOS.</param>
        /// <param name="ttl">The time-to-live of the DNS record. Default is "3600".</param>
        /// <param name="disabled">A value indicating whether the DNS record is disabled. Default is false.</param>
        /// <param name="id">The ID of the DNS record. Set by IONOS.</param>
        public IonRecord(string name, string content, string rootname = "", string type = "A",
             string changedate = "", string ttl = "3600", bool disabled = false, string id = "")
        {
            Name = name;
            Content = content;
            Id = id;
            Type = type;
            RootName = rootname;
            ChangeDate = changedate;
            TTL = ttl;
            Disabled = disabled;
        }
    }
}
