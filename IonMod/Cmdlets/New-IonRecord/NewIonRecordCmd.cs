using System.Management.Automation;

namespace IonMod


{
    [Cmdlet(VerbsCommon.New, "IonRecord")]
    public class NewIonRecordCmd : PSCmdlet
    {
        //
        // Params
        [Parameter(ValueFromPipeline = true)]
        public IonZone? Zone { get; set; }
        //
        [Parameter()]
        public string? ZoneId { get; set; }
        //
        [Parameter(ValueFromPipeline = true)]
        public List<IonRecord>? Records { get; set; }
        //
        // Logic
        protected override void BeginProcessing()
        {
            // Zone obj takes priority
            if (Zone != null)
            {
                ZoneId = Zone.Id;
            }
        }
        //
        protected override void ProcessRecord()
        {
            if (ZoneId == null)
            {
                WriteObject(GetIonZone.Run());
            }
            else
            {
                WriteObject(GetIonZone.Run(ZoneId));
            }
        }
    }
}