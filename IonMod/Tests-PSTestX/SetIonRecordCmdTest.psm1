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
$TestRecord = Get-IonRecord -ZoneId $TestZone.Id -RecordId $Secrets.TestRecordId
# </Custom-Setup>
#
# <Tests>
#
function Test-SetIonRec_StrIdRecObj {
    [PSTest()]
    param ()
    #
    Set-IonRecord -ZoneId $TestZone.Id -Record $TestRecord
    return
}
#
#
#
function Test-SetIonRec_ZonObjRecObj {
    [PSTest()]
    param ()
    #
    $TestZone | Set-IonRecord -Record $TestRecord
    return
}
#
# </Tests>