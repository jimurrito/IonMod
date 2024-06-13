using System.Management.Automation;           // Windows PowerShell namespace.
namespace IonMod


{
    // New-IonToken
    [Cmdlet(VerbsCommon.Set, "IonRecord")]
    public class SetIonRecordCmd : Cmdlet
    {
        // 
        // Remove required if this cmd doesnt work
        //
        //
        [Parameter(ValueFromPipeline = true)]
        public required IonZone Zone { get; set; }
        //
        [Parameter()]
        public required string ZoneId { get; set; }
        //
        [Parameter(ValueFromPipeline = true)]
        public required IonRecord Record { get; set; }
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Zone obj is provided
            if (Zone != null)
            {
                ZoneId = Zone.Id;
            }
            //
            if (ZoneId == null || Record == null)
            {
                throw new Exception("-RecordId and -ZoneId are required parameters for this command.");
            }

        }
        //
        //
        protected override void ProcessRecord()
        {
            SetIonRecord.Run(ZoneId, Record);
        }
    }
}