#
function Start-Test {
    param(
        [parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [scriptblock]$Test,
        [parameter(Mandatory = $true)]
        [string]$DumpLocation,
        [int]$Timeout = 2
    )
    #
    $Result; # Dont remove
    #
    try {
        # Execute test
        $Result = & $Test
        # Output to console
        $Result | Format-Table
        #
        Write-Host "Test Passed!" -ForegroundColor green
        #
        $Result = "Test Passed!`nTest Output:`n" + ($Result | ConvertTo-Json)
        $DumpLocation = "{0}_passed.log" -f $DumpLocation
    }
    catch {
        #
        Write-Output $_
        Write-Host "Test Failed!" -ForegroundColor red
        #
        $Result = "Test Failed!`nError Message:`n" + ($_ | ConvertTo-Json)
        $DumpLocation = "{0}_failed.log" -f $DumpLocation
        $Timeout += 30 # have failed runs stay live longer
    }
    finally {
        # Write
        # Output to file
        Set-Content -Value $Result -Path $DumpLocation -Force
        #
        # Start timer
        Write-Host "`nWindow will close after $Timeout seconds..."
        Start-Sleep $Timeout
        exit
    }

}

function Assert-Assertion($inputs = $1, $FncString = $2) {
    if (!$inputs) { throw "Assertion Failed: Input[$FncString] Output[$inputs]" }
}

function New-Folder($Name) {
    New-Item -ItemType Directory -path $name -ErrorAction SilentlyContinue | Out-Null
}

function Get-Secrets($Path) {
    Get-Content -Path $Path | ConvertFrom-Json
}