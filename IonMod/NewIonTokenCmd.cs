using System.Management.Automation;           // Windows PowerShell namespace.

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
  [Cmdlet(VerbsCommon.New, "IonToken")]
  public class NewIonTokenCmd : Cmdlet
  {
    // parameters doc:
    // https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/declaring-properties-as-parameters?view=powershell-7.4
    // https://learn.microsoft.com/en-us/powershell/scripting/developer/cmdlet/types-of-cmdlet-parameters?view=powershell-7.4
    [Parameter(Mandatory = true)]
    public required string PublicPrefix
    {
      get { return publicPrefix; }
      set { publicPrefix = value; }
    }
    private string publicPrefix;
    //
    //
    [Parameter(Mandatory = true)]
    public required string Secret
    {
      get { return  secret; }
      set { secret = value; }
    }
    private string secret;
    //
    //
    protected override void ProcessRecord()
    {
      WriteObject(new IonToken(publicPrefix, secret));
    }
  }
}