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
function Test-SetIonZone_ZonObjRecLs {
    [PSTest()]
    param ()
    #
    $TestZone | Set-IonZone -Records $TestZone.Records
    return
}
#  ^ ^
# These two tests do the same thing - pull all data from the Zone Object.
#  V V 
function Test-SetIonZone_ZonObjs {
    [PSTest()]
    param ()
    #
    $TestZone | Set-IonZone
    return
}
#
#
#
function Test-SetIonZone_StrIdRecLs {
    [PSTest()]
    param ()
    #
    Set-IonZone -Records $TestZone.Records -ZoneId $TestZone.Id
    return
}
#
# </Tests>