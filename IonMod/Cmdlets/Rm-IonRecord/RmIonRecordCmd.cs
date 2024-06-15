using System.Management.Automation;

namespace IonMod

{
    // New-IonToken
    [Cmdlet(VerbsCommon.Remove, "IonRecord")]
    public class RemoveIonRecordCmd : PSCmdlet
    {
        //
        //
        //
        [Parameter(ValueFromPipeline = true)]
        public IonZone? Zone { get; set; }
        //
        [Parameter()]
        public string? ZoneId { get; set; }
        //        
        [Parameter()]
        public string? RecordId { get; set; }
        //
        [Parameter(ValueFromPipeline = true)]
        public IonRecord? Record { get; set; }
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Zone/Record obj is provided
            if (Zone != null) { ZoneId = Zone.Id; }
            if (Record != null) { RecordId = Record.Id; }
            //
            if (ZoneId == null || RecordId == null)
            {
                throw new Exception("-RecordId and -ZoneId are required parameters for this command.");
            }
        }
        protected override void ProcessRecord()
        {
            RmIonRecord.Run(ZoneId, RecordId);
        }
    }
}