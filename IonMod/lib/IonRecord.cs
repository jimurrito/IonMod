namespace IonMod
{
    /*
    Example:
        "name": "virtrillo.com",
        "rootName": "virtrillo.com",
        "type": "NS",
        "content": "ns1090.ui-dns.biz",
        "changeDate": "2021-07-12T07:27:57.958Z",
        "ttl": 86400,
        "disabled": false,
        "id": "733f7f2d-d73b-7f0d-a3b1-73d77f3479d1"
    */

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
        //
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
        //
        //
    }
}