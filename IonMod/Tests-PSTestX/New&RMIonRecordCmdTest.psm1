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
$TestRecordObj = $TestZone | New-IonRecordObj -Name ("cmdtest2." + $TestZone.Name) -Type "A" -Content "7.7.7.7"
$TestRecord = $TestZone | New-IonRecord -Records $TestRecordObj
# </Custom-Setup>
#
# <Tests>
#
function Test-NewIonRec {
    [PSTest(Assert={$r -is [IonMod.IonRecord]})]
    param ()
    #
    return $TestRecord
}
#
#
#
function Test-RmIonRec {
    [PSTest()]
    param ()
    #
    return $TestZone | Remove-IonRecord -RecordId $TestRecord.Id
}
#
# </Tests>