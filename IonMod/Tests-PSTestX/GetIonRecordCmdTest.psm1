<#
.DESCRIPTION
Tests the Powershell module Get-IonRecord
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
# Login to IONOS
Connect-Ion -PublicPrefix $Secrets.PublicPrefix -Secret $Secrets.Secret
#
# </Boilerplate>
#
# var used to store test objects. Avoids redundent API requests.
$TestRecord = Get-IonRecord -ZoneId $Secrets.TestZoneId -RecordId $Secrets.TestRecordId
$TestZone = Get-IonZone -ZoneId $Secrets.TestZoneId
#
# <Tests>
#
function Test-GetIonRec_strids {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestRecord
}
#
#
#
function Test-GetIonRec_stridRecObj {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestRecord | Get-IonRecord -ZoneId $Secrets.TestZoneId
}
#
#
#
function Test-GetIonRec_ZonObjStrId {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestZone | Get-IonRecord -RecordId $Secrets.TestRecordId
}
#
#
#
function Test-GetIonRec_ZonObjRecObj{
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestZone | Get-IonRecord -Record $TestRecord
}
#
# </Tests>