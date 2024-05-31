using System.Management.Automation;

namespace IonMod

{
    // New-IonToken
    [Cmdlet(VerbsCommon.Get, "IonRecord")]
    public class GetIonRecordCmd : Cmdlet
    {
        //
        //
        // Params
        [Parameter(Mandatory = true, ValueFromPipeline = true)] 
        public required IonToken Token { get; set; }
        //
        [Parameter(Mandatory = true)]
        public required string ZoneId { get; set; }
        //
        [Parameter(Mandatory = true)]
        public required string RecordId { get; set; }
        //
        //
        // Logic
        protected override void ProcessRecord()
        {
            GetIonRecord client = new GetIonRecord(Token, ZoneId);
            WriteObject(client.Run(RecordId));
        }
    }
}