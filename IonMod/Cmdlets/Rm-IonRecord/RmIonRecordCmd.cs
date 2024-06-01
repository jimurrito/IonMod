using System.Management.Automation;

namespace IonMod

{
    // New-IonToken
    [Cmdlet(VerbsCommon.Remove, "IonRecord")]
    public class RemoveIonRecordCmd : Cmdlet
    {
        //
        //
        // Params
        [Parameter(Mandatory = true, ValueFromPipeline = true)] 
        public required IonToken Token { get; set; }
        //
        [Parameter()]
        public IonRecord? Record {get; set;}
        //
        [Parameter(Mandatory = true)]
        public required string ZoneId { get; set; }
        //
        [Parameter(Mandatory = true)]
        public required string RecordId { get; set; }
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Record obj is provided
            if (Record != null){
                RecordId = Record.Id;
            }
        }
        protected override void ProcessRecord()
        {
            RmIonRecord client = new RmIonRecord(Token, ZoneId, RecordId);
            client.Run();
        }
    }
}