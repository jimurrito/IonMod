using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : Cmdlet
    {
        //
        //
        [Parameter(ValueFromPipeline = true)]
        public IonZone? Zone { get; set; }
        //
        [Parameter()]
        public string? ZoneId { get; set; }
        //
        [Parameter(ValueFromPipeline = true)]
        public List<IonRecord?>? Records { get; set; }
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Zone obj is provided
            if (Zone != null)
            {
                ZoneId = Zone.Id;
                Records = Zone.Records;
            }
            //
            if (ZoneId == null || Records == null)
            {
                throw new Exception("-ZoneId and a list of records (-Records) are required for this command.");
            }
        }
        //
        //
        protected override void ProcessRecord()
        {
            SetIonZone.Run(ZoneId,Records);
        }
    }
}