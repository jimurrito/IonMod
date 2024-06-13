
namespace IonMod
{
    public static class RmIonRecord
    {
        //
        //
        public static void Run(IonZone zone, IonRecord record)
        {
            Run(zone.Id, record.Id);
        }
        //
        public static void Run(string zoneId, IonRecord record)
        {
            Run(zoneId, record.Id);
        }
        //
        public static void Run(IonZone zone, string recordId)
        {
            Run(zone.Id, recordId);
        }
        //
        public static void Run(string zoneId, string recordId)
        {
            IonConnect.Delete("/" + zoneId + "/records/" + recordId);
        }
    }
}