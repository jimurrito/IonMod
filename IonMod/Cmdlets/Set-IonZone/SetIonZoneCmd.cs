using System.Management.Automation;

namespace IonMod

{
    // New-IonToken
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : Cmdlet
    {
        //
        //
        // Params
        [Parameter(Mandatory = true, ValueFromPipeline = true)]        
        public required IonToken Token { get; set; }
        //
        [Parameter()]
        public IonZone? Zone { get; set; }
        //
        [Parameter()]
        public required string ZoneId { get; set; }
        //
        [Parameter()]
        public required List<IonRecord?> Records { get; set; }
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
            // override if Zone obj is provided
            if (Zone != null){
                ZoneId = Zone.Id;
                Records = Zone.Records;
            }
        }
        //
        //
        protected override void ProcessRecord()
        {
            SetIonZone client = new SetIonZone(Token, ZoneId, Records);
            client.Run();
        }
    }
}