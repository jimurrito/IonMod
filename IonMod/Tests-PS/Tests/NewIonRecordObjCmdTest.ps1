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
    # get test zone
    $Zone = Get-IonZone -ZoneId $Secrets.TestZoneId
    # create recordObj (ZoneObj)
    Assert-Assertion  ($Zone | New-IonRecordObj -Name ("cmdtest." + $Zone.Name) -Type "A" -Content "8.8.8.8")  '$Zone | New-IonRecordObj -Name ("cmdtest."+$Zone.Name) -Type "A" -Content "8.8.8.8"'
    # create recordObj (ZoneId)
    $Record = New-IonRecordObj -ZoneName $Zone.Name -Name ("cmdtest." + $Zone.Name) -Type "A" -Content "8.8.8.8"
    Assert-Assertion  ($Record)  'New-IonRecordObj -ZoneName $Zone.Name -Name ("cmdtest." + $Zone.Name) -Type "A" -Content "8.8.8.8"'
    #
    return $Record
} 
#
# DO NOT MODIFY
#
# Pipe test block into fn to run and validate
| start-test -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])