# IonMod : Ionos Domain Module for Powershell
A simple and fast powershell module to handle API requests to Ionos's Domain Management API.

# Getting started

Install the Module into Powershell.

```Powershell
# From PSGallery
Install-Module IonMod

# From Source
Install-Module /path/to/IonMod.psm1
```

Simple script to get all DNS Zones accessible by the API credentials provided.
```Powershell
Import-Module IonMod

$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Get-IonZone
```

# Function List

### `New-IonToken`
#### Description
Combines the Public-prefix and Secret provided by IONOS for access.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-PublicPrefix` | Public-Prefix provided by IONOS
| `-Secret` | Secret provided by IONOS
#### Examples
Generates a token
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
```
---


### `Get-IonZone`
#### Description
Grabs information of either all zones, or a specific zone when using the -ZoneId parameter.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs. When provided, the query provides indepth information over the entire Zone. Information like DNS records can be retrieved for the zone.
#### Examples
Get all Zones accessible by the API Credentials.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Get-IonZone
```
Get all Records for an IONOS Zone.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Zone = $Token | Get-IonZone -ZoneId "XXXX"
$Zone.Records
```
---


### `Set-IonZone`
#### Description
Sets configurations for a given Zone. Manifest provided will be set as the current configuration, discarding the previous one. This includes DNS Zones.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs. When provided, the query provides indepth information over the entire Zone. Information like DNS records can be retrieved for the zone.
| `-Body` | The Zone configuration that will be set. Can be either a **[*]** Json String or PSCustomObject.

>**[*]** For this endpoint only, Json strings must always be in a List format. Even if there is only one json object.
This is an IONOS side limitation. This function uses `Invoke-IonRequest`, and it will compensate for this if you provide a Powershell Object (`PSCustomObj` or `PSObject`).
>
> **GOOD** ->
[
{
    Key: {
        Keya: "Valuea",
        Keyb: "Valueb"
    }
}
]
>
> **BAD** ->
{
    Key: {
        Keya: "Valuea",
        Keyb: "Valueb"
    }
}
>
> *See [`ConvertTo-JsonList`](#convertto-jsonlist) for more information. This is the core function that handles this scenario.*


#### Examples
Set Zone DNS Configuration.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Set-IonZone -ZoneId "XXXX" -Body <[JSON | PSObject]>
```
---


### `Get-IonRecord`
#### Description
Grabs all DNS Records for a provided Zone. This is the same as `($Token | Get-IonZone -ZoneId "XXXX").Records`.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.
| `-RecordId` | Specifies which record, within a Zone, will be used.
| `-Types` | Specifies the type of record that should be returned. Ex: A, AAAA, TXT, MX ... | "A"
#### Examples
Get all Records in a Zone.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Get-IonRecord -ZoneId "XXXX"
```
Get a specific record based on RecordId
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Get-IonRecord -ZoneId "XXXX" -RecordId "XXXX"
```
Return all 'A' records for a given Zone.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Get-IonRecord -ZoneId "XXXX" -Types "A"
```
---


### `Set-IonRecord`
#### Description
Uses the provided ZoneId and RecordId to change the configuration of a single Zone Record.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.
| `-RecordId` | Specifies which record, within a Zone, will be used.
| `-Body` | PSObject or JSON String of your desired configuration.
#### Examples
Sets configuration for a Zone Record.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Set-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | PSObject]>
```
---


### `Remove-IonRecord`
#### Description
Removes a Record from an IONOS Zone.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.
| `-RecordId` | Specifies which record, within a Zone, 
#### Examples
Removes an A Record from a Zone.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | Remove-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | PSObject]>
```
---


### `New-IonRecord`
#### Description
Creates a new Record in an IONOS Zone.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use 
| `-ZoneId` | Specifies which IONOS Zone will be used. Use `Get-IonZone` to retrieve the IDs.
| `-RecordId` | Specifies which record, within a Zone, will be used.
| `-Body` | PSObject or JSON String of your desired configuration. Body can be generated using `New-IonRecordObj`. This creates a JSON object that is ready to be pushed to IONOS.
#### Examples
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$Token | New-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"  -Body <[JSON | (New-IonRecordObj)]>
```
---


### `New-IonRecordObj`
#### Description
Creates a new Record Object, ready to be sent to IONOS.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-ZoneName` | Name of the Zone the Record will be held under. Ex. Contoso.com
| `-Name` | Name of the Record. This needs to be an FQDN. Ex. app.Contoso.com
| `-Type` | Type of Record to be Created. Ex. A, AAAA, TXT, MX. Defaults to 'A'.
| `-Content` | Value of the record. Ex. 192.168.2.7
| `-TTL` | Time-to-live value for the Record. | `3600`
| `-Prio` | Priority value for the Record. | `0`
| `-Disabled` | Disables the Record. | Records are Enabled by default.
#### Examples
Creates a new A Record for App.Contoso.com
```PowerShell
New-IonRecordObj -ZoneName "Contoso.com" -Name "App.Contoso.com" -Content 192.168.2.7
```
---


### `Invoke-IonRequest`
#### Description
Generic Function used to send HTTP(s) requests to the IONOS developer API. Requires a Token PSObject to connect.
#### Parameters
| Parameter | Description | Default |
| --- | --- | --- |
| `-Token` | Combination of the public prefix and secret provided by IONOS for API access. You can use `New-IonToken` to generate this PSObject. |
| `-Path` | Directory path of an API endpoint. Path is appended to the root url (`$ROOTURL`) | `$PWD`
| `-Body `| Body of the HTTP(s) request, that will be sent to IONOS. Only Supported by certain HTTP Methods. |
| `-Method` | HTTP Method used to connect to IONOS. | `'Get'`
#### Examples
Get all Zones accessible by the API Credentials.
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
Invoke-IonRequest -Path "/zones" -Token $Token
```
Update the information of a DNS Record
```PowerShell
$Token = New-IonToken -PublicPrefix "XXXX" -Secret "XXXX"
$ZoneId = "XXXX"
$RecordId = "XXXX"
$Body = [json]|[PSObject]
Invoke-IonRequest -Method Put -Path "/zones/$ZoneId/records/$RecordId" -Body $Body -Token $Token
```
---


### `ConvertTo-JsonList`
#### Description
By default, Powershell will coerce single item lists into single items not within a list. Any single item list you input into ConvertTo-Json will be converted to a single JSON object. This function catches this scenario, and wraps the single JSON object into a JSON list. This is required as some IONOS endpoints only accept JSON List bodies.
#### Parameters
| Parameters | Description | Default |
| --- | --- | --- |
| `-Object` | Input Object that will be converted. Should be a Powershell Object.
#### Examples
Removes a Record from a Zone.
```PowerShell
$Rec = New-IonRecordObj -ZoneName "Contoso.com" -Name "App.Contoso.com" -Content 192.168.2.7
$Rec | ConvertTo-JsonList
```
---