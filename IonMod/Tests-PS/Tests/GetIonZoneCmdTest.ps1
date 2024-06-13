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
    $Zones = Get-IonZone
    Assert-Assertion ($null -ne $Zones) '$null -ne $Zones'
    return $Zones
} 
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}.log" -f $PSCommandPath.Split("\")[-1].Split(".")[0])



