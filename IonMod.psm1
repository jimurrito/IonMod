# IonMod : Powershell Module for IONOS Domains
# By Jimurrito : jimurrito@gmail.com
#
# Root URL used to connect to IONOS
$ROOTURL = "https://api.hosting.ionos.com/dns/v1"
#
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Generic Function to send API requests to IONOS.

    .Description
    Generic Function used to send HTTP(s) requests to the IONOS developer API. Requires a Token PSObject to connect.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter Path
    Directory path of an API endpoint. Path is appended to the root url ($ROOTURL)

    .Parameter Body
    Body of the HTTP(s) request, that will be sent to IONOS. Only Supported by certain HTTP Methods.

    .Parameter Method
    HTTP Method used to connect to IONOS. Defaults to `GET`.

    .Example
    # Get all Zones from IONOS.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    Invoke-IonRequest -Path "/zones" -Token $Token

    .Example
    # Update information of a DNS Record.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $ZoneId = "XXXX"
    $RecordId = "XXXX"
    $Body = [json]|[PSObject]
    Invoke-IonRequest -Method Put -Path "/zones/$ZoneId/records/$RecordId" -Body $Body -Token $Token
#>
function Invoke-IonRequest {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [String]$Path,
        [String]$Body = $null,
        [Microsoft.PowerShell.Commands.WebRequestMethod]$Method = [Microsoft.PowerShell.Commands.WebRequestMethod]::Get

    )
    # Headers
    $headers = @{
        'X-Api-Key'    = ("{0}.{1}" -f $Token.PublicPrefix, $Token.Secret) 
        'Content-Type' = "application/json"
    }
    # No Body - no switch
    $out = if (!($Body)) {
        (Invoke-WebRequest -Uri "$ROOTURL$Path" -Headers $headers -Method $Method)
    } 
    # Lets go body!
    else {
        # Check if body is a PSObj
        if (($Obj.getType()).name -eq "PSCustomObject") {
            $Body = $Body | ConvertTo-JsonList
        }
        (Invoke-WebRequest -Uri "$ROOTURL$Path" -Headers $headers -Method $Method -Body $Body)
    }
    # Convert response into a PSObj
    return $out.content | ConvertFrom-Json
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Creates a token for IONOS API requests.

    .Description
    Combines the Public-prefix and Secret provided by IONOS for access.

    .Parameter PublicPrefix
    Public-Prefix provided by IONOS

    .Parameter Secret
    Secret provided by IONOS

    .Example
    # Generates a token
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
#>
function New-IonToken {
    param (
        [Parameter(Mandatory = $true)]
        [String]$PublicPrefix,
        [Parameter(Mandatory = $true)]
        [String]$Secret
    )
    return [PSCustomObject]@{
        PublicPrefix = $PublicPrefix
        Secret       = $Secret
    }
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Gets IONOS DNS Zone configurations.

    .Description
    Grabs information of either all zones, or a specific zone when using the -ZoneId parameter.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.
    When provided, the query provides indepth information over the entire Zone. Information like DNS records can be retrieved for the zone.

    .Example
    # Get all IONOS Zones
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Get-IonZone

    .Example
    # Get all Records for an IONOS Zone
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Zone = $Token | Get-IonZone -ZoneId "XXXX"
    $Zone.Records
#>
function Get-IonZone {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [string]$ZoneId
    )
    return (Invoke-IonRequest -Path "/zones/$ZoneId" -Token $Token)
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Sets IONOS DNS Zone configurations.

    .Description
    Sets configurations for a given Zone. Manifest provided will be set as the current configuration, discarding the previous one. This includes DNS Zones.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.

    .Parameter Body
    The Zone configuration that will be set. Can be either a [*]Json String or PSCustomObject.

    [*] For this endpoint only, Json strings must always be in a List format. Even if there is only one json object.
        This is an IONOS side limitation. This function uses `Invoke-IonRequest`, and it will compensate for this if you provide a Powershell Object (`PSCustomObj` or `PSObject`).

    - GOOD -
    [
    {
        Key: {
            Keya: Valuea,
            Keyb: Valueb
        }
    }
    ]

    - BAD -
    {
        Key: {
            Keya: Valuea,
            Keyb: Valueb
        }
    }    

    .Example
    # Set Zone DNS Configuration
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Set-IonZone -ZoneId "XXXX" -Body <[JSON | PSObject]>
#>
function Set-IonZone {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [Parameter(Mandatory = $true)]
        [string]$ZoneId,
        [Parameter(Mandatory = $true)]
        $Body
    )
    return  (Invoke-IonRequest -Method Put -Path "/zones/$ZoneId" -Body $Body -Token $Token)
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Gets DNS Records for a given Zone.

    .Description
    Grabs all DNS Records for a provided Zone. This is the same as `($Token | Get-IonZone -ZoneId "XXXX").Records`.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.

    .Parameter RecordId
    Specifies which record, within a Zone, will be used.

    .Parameter Types
    Specifies the type of record that should be returned. Ex: A, AAAA, TXT, MX ...

    .Example
    # Get all Records in a Zone.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Get-IonRecord -ZoneId "XXXX"

    .Example
    # Get a specific record based on RecordId
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Get-IonRecord -ZoneId "XXXX" -RecordId "XXXX"

    .Example
    # Return all 'A' records for a given Zone.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Get-IonRecord -ZoneId "XXXX" -Types "A"
#>
function Get-IonRecord   {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [Parameter(Mandatory = $true)]
        [string]$ZoneId,
        [string]$RecordId,
        # Filter record types
        [array]$Types = $null
    )
    $out = $Token | Invoke-IonRequest -Path "/zones/$ZoneId/records/$RecordId"
    if ($Types) { 
        return $out | Where-Object { $Types -eq $_.Type }
    }
    return $out
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Set configuration of a single Zone Record.

    .Description
    Uses the provided ZoneId and RecordId to change the configuration of a single Zone Record.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.

    .Parameter RecordId
    Specifies which record, within a Zone, will be used.
    
    .Parameter Body
    Body can be generated using `New-IonRecordObj`. This creates a JSON object that is ready to be pushed to IONOS.

    .Example
    # Sets configuration for a Zone Record.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Set-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | PSObject]>
#>
function Set-IonRecord {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [Parameter(Mandatory = $true)]
        [string]$ZoneId,
        [Parameter(Mandatory = $true)]
        [string]$RecordId,
        [Parameter(Mandatory = $true)]
        $Body
    )
    return (Invoke-IonRequest -Method Put -Path "/zones/$ZoneId/records/$RecordId" -Body $Body -Token $Token)
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Removes a Record from an IONOS Zone.

    .Description
    Removes a Record from an IONOS Zone.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.

    .Parameter RecordId
    Specifies which record, within a Zone, will be used.

    .Example
    # Removes a Record from a Zone.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | Remove-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | PSObject]>
#>
function Remove-IonRecord {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [Parameter(Mandatory = $true)]
        [string]$ZoneId,
        [Parameter(Mandatory = $true)]
        [string]$RecordId
    )
    return (Invoke-IonRequest -Method Delete -Path "/zones/$ZoneId/records/$RecordId" -Token $Token)
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Creates a new Record in an IONOS Zone.

    .Description
    Creates a new Record in an IONOS Zone.

    .Parameter Token
    Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject.

    .Parameter ZoneId
    Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.

    .Parameter Body
    Body can be generated using `New-IonRecordObj`. This creates a JSON object that is ready to be pushed to IONOS.

    .Example
    # Creates a Record in a Zone.
    $Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
    $Token | New-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | (New-IonRecordObj)]>
#>
function New-IonRecord {
    param (
        [Parameter(ValueFromPipeline = $true, Mandatory = $true)]
        [PSCustomObject]$Token,
        [Parameter(Mandatory = $true)]
        [string]$ZoneId,
        [Parameter(Mandatory = $true)]
        $Body
    )
    return (Invoke-IonRequest -Method Post -Path "/zones/$ZoneId/records" -Body $Body -Token $Token)
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Creates a new Record Object.

    .Description
    Creates a new Record Object, ready to be sent to IONOS.

    .Parameter ZoneName
    Name of the Zone the Record will be held under. Ex. Contoso.com

    .Parameter Name
    Name of the Record. This needs to be an FQDN. Ex. app.Contoso.com

    .Parameter Type
    Type of Record to be Created. Ex. A, AAAA, TXT, MX. Defaults to 'A'.

    .Parameter Content
    Value of the record. Ex. 192.168.2.7

    .Parameter TTL
    Time-to-live value for the Record. Defaults to '3600'

    .Parameter Prio
    Priority value for the Record. Defaults to '0'

    .Parameter Disabled
    Disables the Record. Records are Enabled by default.

    .Example
    # Creates a new A Record for App.Contoso.com
    New-IonRecordObj -ZoneName "Contoso.com" -Name "App.Contoso.com" -Content 192.168.2.7
#>
function New-IonRecordObj {
    param (
        # Zone (domain)
        [Parameter(Mandatory = $true)]
        [string]$ZoneName,
        #FQDN of record
        [Parameter(Mandatory = $true)]
        [String]$Name,
        # Record type (A,AAA,NS,MX)
        [String]$Type = "A",
        # Record value
        [Parameter(Mandatory = $true)]
        [String]$Content,
        [int]$TTL = 3600,
        [int]$Prio = 0,
        [switch]$Disabled
    )
    return [PSCustomObject]@{
        name     = $Name
        type     = $Type
        content  = $Content
        ttl      = $TTL
        prio     = $prio
        disabled = $disabled
    }
}
# # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # # #
<#
    .Synopsis
    Converts a Powershell Object into a Json List.

    .Description
    By default, Powershell will coerce single item lists into single items not within a list. Any single item list you input into ConvertTo-Json will be converted to a single JSON object.
    This function catches this scenario, and wraps the single JSON object into a JSON list. This is required as some IONOS endpoints only accept JSON List bodies.

    .Parameter Object
    Input Object that will be converted. Should be a Powershell Object.

    .Example
    # Removes a Record from a Zone.
    $Rec = New-IonRecordObj -ZoneName "Contoso.com" -Name "App.Contoso.com" -Content 192.168.2.7
    $Rec | ConvertTo-JsonList
#>
function ConvertTo-JsonList {
    param (
        [Parameter(ValueFromPipeline = $true)]
        [Array]$object
    )
    if (($object -isnot [System.Collections.IEnumerable]) -or ($object.Count -lt 2)) {
        return "[`n" + ($object | ConvertTo-Json) + "`n]"
    }
    return $object | ConvertTo-Json   
}