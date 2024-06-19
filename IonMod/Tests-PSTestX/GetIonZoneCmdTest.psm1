<#
.DESCRIPTION
Tests the Powershell module Get-IonZone
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
$TestZones = Get-IonZone
# </Custom-Setup>
#
# <Tests>
#
function Test-GetIonZon_Gen {
    [PSTest(Assert={$r[0] -is [IonMod.IonZone]})]
    param ()
    #
    return $TestZones
}
#
#
#
function Test-GetIonZon_ObjPipe {
    [PSTest(Assert={$r -is [IonMod.IonZone]})]
    param ()
    #
    return $TestZones[0] | Get-IonZone
}
#
#
#
function Test-GetIonZon_StrIdPiped {
    [PSTest(Assert={$r -is [IonMod.IonZone]})]
    param ()
    #
    return $TestZones[0].Id | Get-IonZone
}
#
#
#
function Test-GetIonZon_StrId {
    [PSTest(Assert={$r -is [IonMod.IonZone]})]
    param ()
    #
    return  Get-IonZone -ZoneId $TestZones[0].Id
}
#
# </Tests>