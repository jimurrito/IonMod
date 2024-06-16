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
    # Create New Record Obj
    $RecordObj = $Zone | New-IonRecordObj -Name ("cmdtest2." + $Zone.Name) -Type "A" -Content "7.7.7.7"
    Assert-Assertion ( $RecordObj ) '$Zone | New-IonRecordObj -Name ("cmdtest2." + $Zone.Name) -Type "A" -Content "7.7.7.7"'
    # Post obj to Ionos
    $NewRecObj = $Zone | New-IonRecord -Records $RecordObj
    Assert-Assertion $NewRecObj '$Zone | New-IonRecord -Records [$RecordObj]'
    # Rm from Ionos
    $Zone | Remove-IonRecord -RecordId $NewRecObj.Id
    #
    return "void - Success"
} 
#
# DO NOT MODIFY
#
# Pipe test block into fn to run and validate
| start-test -Timeout 30 -DumpLocation ("$DumpLocation/{0}" -f $PSCommandPath.Split("\")[-1].Split(".")[0])