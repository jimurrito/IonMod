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
# test
{
    Connect-Ion -PublicPrefix $Secrets.PublicPrefix -Secret $Secrets.Secret
    # get all zones
    $Zones = Get-IonZone
    Assert-Assertion ($Zones) '$null -ne $Zones'
    # get one zone (obj - piped)
    Assert-Assertion (($Zones[0] | Get-IonZone)) '$Zones[0] | Get-IonZone'
    # get one zone (Id - piped)
    Assert-Assertion (($Zones[0].Id | Get-IonZone)) '$Zones[0].Id | Get-IonZone'
    # get one zone (Id)
    Assert-Assertion ((Get-IonZone -ZoneId $Zones[0].Id)) 'Get-IonZone -ZoneId $Zones[0].Id'
    #
    return $Zones
} 
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])



