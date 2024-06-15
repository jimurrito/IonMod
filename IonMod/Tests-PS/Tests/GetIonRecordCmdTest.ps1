#
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
#
#
# Test
{
    Connect-Ion -PublicPrefix $Secrets.PublicPrefix -Secret $Secrets.Secret
    # Get record (stringIds)
    $Record = Get-IonRecord -ZoneId $Secrets.TestZoneId -RecordId $Secrets.TestRecordId
    Assert-Assertion ($Record) '$null -ne $Record'
    # Get record (stringId+RecObj)
    Assert-Assertion ($Record | Get-IonRecord -ZoneId $Secrets.TestZoneId) '$Record | Get-IonRecord -ZoneId $Secrets.TestZoneId'
    #
    # Test zone obj
    $TestZone = (Get-IonZone -ZoneId $Secrets.TestZoneId)
    # Get record (ZoneObj+stringId)
    Assert-Assertion ($TestZone | Get-IonRecord -RecordId $Secrets.TestRecordId) '$TestZone | Get-IonRecord -ZoneId $Secrets.TestZoneId'
    # Get record (ZoneObj+RecObj)
    Assert-Assertion ($TestZone | Get-IonRecord -Record $Record) '$TestZone | Get-IonRecord -Record $Record'
    #
    return $Record
} 
#
#
#
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])