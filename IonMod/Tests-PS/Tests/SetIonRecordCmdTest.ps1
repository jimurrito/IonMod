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
    Assert-Assertion ($Zone) '$zone'
    $Record = Get-IonRecord -ZoneId $Zone.Id -RecordId $Secrets.TestRecordId
    Assert-Assertion ($Record) '$record'
    #
    # Modality tests
    #
    # stringId+RecObj
    Assert-Assertion (Set-IonRecord -ZoneId $Zone.Id -Record $Record) 'Set-IonRecord -ZoneId $Zone.Id -Record $Record'
    # ZoneObj+RecObj
    Assert-Assertion ($Zone | Set-IonRecord -Record $Record) '$Zone | Set-IonRecord -Record $Record'
    #
    return $Record
} 
#
# DO NOT MODIFY
#
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])