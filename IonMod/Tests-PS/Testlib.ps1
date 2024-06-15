#
function Start-Test {
    param(
        [parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [scriptblock]$Test,
        [parameter(Mandatory = $true)]
        [string]$DumpLocation,
        [int]$Timeout = 10
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

function Assert-Assertion {
    param($one = $1, $two = $2) if (!$one) { throw "Assertion Failed: Input[$two] Output[$one]" }
}

function New-Folder($Name) {
    New-Item -ItemType Directory -path $name -ErrorAction SilentlyContinue | Out-Null
}

function Get-Secrets($Path)
{
    Get-Content -Path $Path | ConvertFrom-Json
}