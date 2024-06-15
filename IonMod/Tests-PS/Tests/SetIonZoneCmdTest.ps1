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
    $Zone | Set-IonZone -Records $Zone.Records -Debug
    #Set-IonZone -Records $Zone.Records -ZoneId $Zone.Id
    return "void - success"
} 
#
# DO NOT MODIFY
#
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}.log" -f $PSCommandPath.Split("\")[-1].Split(".")[0])