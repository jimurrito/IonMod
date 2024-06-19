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
$Secrets = Get-Secrets "C:\Users\james\Documents\code\IonMod\SECRETS.json"
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
# </Custom-Setup>
#
# <Tests>
# </Tests>