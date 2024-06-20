<#
.DESCRIPTION
#>
#
# <Boilerplate>
#
# Import Testing suite
using module PSTestLib # this throws a warning for some reason
# Import Local testing lib
Import-Module "$PSScriptRoot/TestLib.psm1"
# Load secrets
$Secrets = Get-Secrets
# Load IonMod PSModule
Import-Module $Secrets.ModulePath
# Workaround for `using` not being at the top. - this imports the IonMod module classes
. ([scriptblock]::create(("using module {0}" -f $Secrets.ModulePath)))
#
# Login to IONOS
Connect-Ion -PublicPrefix $Secrets.PublicPrefix -Secret $Secrets.Secret
#
# </Boilerplate>
#
# <Custom-Setup>
$TestZone = Get-IonZone -ZoneId $Secrets.TestZoneId
# </Custom-Setup>
#
# <Tests>
#
function Test-NewIonRecObj_ZoneObj {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestZone | New-IonRecordObj -Name ("cmdtest." + $TestZone.Name) -Type "A" -Content "8.8.8.8"
}
#
#
#
function Test-NewIonRecObj_StrId {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return New-IonRecordObj -ZoneName $TestZone.Name -Name ("cmdtest." + $TestZone.Name) -Type "A" -Content "8.8.8.8"
}
#
# </Tests>