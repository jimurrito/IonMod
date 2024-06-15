# DO NOT MODIFY
Param(
    [parameter(Mandatory = $true)]
    [string]$DumpLocation,
    [parameter(Mandatory = $true)]
    [string]$SecretLocation
)
#
Import-Module -Name "$PSScriptRoot\..\testlib.ps1"
# Load secrets
$Secrets = Get-Secrets $SecretLocation
# Verbose path
Write-Host $PSCommandPath -ForegroundColor Blue
# Load test module
Import-Module -Name $Secrets.ModulePath
#
# DO NOT MODIFY
#
# Test
{
    Connect-Ion -PublicPrefix $Secrets.PublicPrefix -Secret $Secrets.Secret
    $Zone = Get-IonZone -ZoneId $Secrets.TestZoneId
    Assert-Assertion ($null -ne $Zone)
    #
    # SetIonZone modality tests (no need for assert as these are all void returns)
    # Zoneobj + List<RecordObj>
    $Zone | Set-IonZone -Records $Zone.Records
    Set-IonZone -Records $Zone.Records -Zone $Zone
    # string zone ID + List<RecordObj>
    Set-IonZone -Records $Zone.Records -ZoneId $Zone.Id
    return "void - Success"
} 
#
# DO NOT MODIFY
#
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])