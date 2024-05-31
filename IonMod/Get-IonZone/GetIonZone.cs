using Newtonsoft.Json;

namespace IonMod
{
    public class GetIonZone : IonHttp
    {
        public GetIonZone(IonToken token) : base(token) { }
        //
        /*
        Gets Zones from IONOS. This returns ALL zones under your IONOS account.
        */
        public List<IonZone>? Run()
        {
            return JsonConvert.DeserializeObject<List<IonZone>>(Get("/"));
        }
        //
        /*
        Gets data on a specfic Zone. Returns a single Zone, and its associated records.
        */
        public IonZone? Run(string zoneId)
        {
            return JsonConvert.DeserializeObject<IonZone>(Get("/" + zoneId));
        }
    }
}