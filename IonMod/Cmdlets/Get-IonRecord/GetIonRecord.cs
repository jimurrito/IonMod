namespace IonMod
{
    public static class GetIonRecord
    {
        //
        //
        public static IonRecord Run(IonZone zone, string recordId)
        {
            return Run(zone.Id,recordId);
        }
        //
        public static IonRecord Run(IonZone zone, IonRecord record)
        {
            return Run(zone.Id,record.Id);
        }
        //
        public static IonRecord Run(string zoneId, string recordId)
        {
            return IonConnect.Get<IonRecord>("/" + zoneId + "/records/" + recordId);
        }
    }
}