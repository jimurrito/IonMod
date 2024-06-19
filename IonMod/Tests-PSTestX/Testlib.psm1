# 
# Functions of aiding in module testing
#
$DefaultPath = "C:\Users\james\Documents\code\IonMod\SECRETS.json" 
#
#
function Get-Secrets($Path = $DefaultPath) {
    Get-Content -Path $Path | ConvertFrom-Json
}