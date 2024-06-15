using System.Management.Automation;

namespace IonMod

{
    [Cmdlet(VerbsCommon.Set, "IonZone")]
    public class SetIonZoneCmd : PSCmdlet
    {
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId")]
        public required string ZoneId {get; set;}
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj", ValueFromPipeline = true)]
        public required IonZone Zone {get; set;}
        //
        //
        [Parameter(Mandatory = true, ParameterSetName = "RecObj+stringId", ValueFromPipeline = true)]
        [Parameter(Mandatory = true, ParameterSetName = "ZoneObj+RecObj")]
        public required List<IonRecord> Records {get; set;}
        //
        //
        // Logic
        protected override void BeginProcessing()
        {
 
            /*
            else
            {
                throw new IonUninitException("null check");
            }
            */
        }
        //
        //
        protected override void ProcessRecord()
        {
            Console.WriteLine(ParameterSetName);
            // override if Zone obj is provided
            if (ParameterSetName == "ZoneObj+RecObj")
            {
                Console.WriteLine("ZONE!");
                //WriteError("IonZone object used.");
                ZoneId = Zone.Id;
                Records = Zone.Records ?? throw new IonUninitException("Zone object provided does not contain Records: (List<IonRecord>).");
            }
            SetIonZone.Run(ZoneId, Records);
        }
    }
}