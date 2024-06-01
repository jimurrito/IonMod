using System.Management.Automation;           // Windows PowerShell namespace.
using System.Security.Policy;
using Microsoft.PowerShell.Commands;

// Namespace == Module
// Each class with the [cmdlet] derivation is a powershell cmdlet
// name of the cmdlet is from the derive, not the name of the class
// ex. [Cmdlet(<VerbClass.verb>,"CmdName")] == "verb-Cmdname"
// Cmdlet verbclasses are named System.Management.Automation.Verb*
// https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/approved-verbs-for-windows-powershell-commands?view=powershell-7.4
//
// The Cmdlet is executed by overriding existing methods of the Cmdlet class.
// https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/cmdlet-input-processing-methods?view=powershell-7.4
// Important methods (ran for each record in the pipeline):
// BeginProcessing(Pre-processes input data), ProcessRecord (Logic for code), EndProcess(Ran after the cmd has successfully completed)
// https://learn.microsoft.com/en-us/previous-versions/ms714429(v=vs.85)
// methods do not need to return a value, and must be typed for void. Use WriteObject to push output to the Powershell pipeline.


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