# Core Test Script runner
#
$SecretPath = "C:\Users\james\Documents\code\IonMod\SECRETS.json"
#
# Import TestLib
Import-Module -Name "$PSScriptRoot/Testlib.ps1"
#
# Pull dump location
$DumpLocation = (Get-Secrets $SecretPath).DumpPath
#
# get scripts
$Scripts = Get-ChildItem -Path "$PSScriptRoot/Tests" -Filter "*CmdTest.ps1"
#
# Ensure root dump folder is intact
$DumpLocation = "$DumpLocation/{0}" -f (get-date -Format MMMM-dd-hhmm)
New-Folder $DumpLocation
#
#
foreach ($i in $Scripts){
    Write-Host ("Running Script: " + $i.Name) -ForegroundColor Green
    start-process "pwsh" -ArgumentList "-file $i -DumpLocation $DumpLocation -SecretLocation $SecretPath"
}