# IonMod : <u>**[Unofficial]**</u> Powershell/C# SDK for IONOS Domains
A simple and fast SDK for managing IONOS domains and records. This an unofficial project, and is not maintained by IONOS.
This library is for C# and Powershell use.

> **Warning!**
> The change to C# was breaking. Please see below examples on the login change.

# Getting started

- [**PowerShell Gallery/Nuget**](https://www.powershellgallery.com/packages/IonMod)
  - `install-module IonMod`

- **Git**
  - `git clone https://github.com/jimurrito/IonMod`
  - `import-module path/to/IonMod.psd1`


## Example

Simple set of cmdlets to get all DNS Zones accessible by the API credentials provided.
```Powershell
Import-Module IonMod

Connect-Ion -PublicPrefix "XXXX" -Secret "XXXX"
Get-IonZone


Records Name                  Id                                   Type
------- ----                  --                                   ----
        contoso.com           00000000-0000-0000-0000-000000000000 NATIVE
        favicon.com           00000000-0000-0000-0000-000000000000 NATIVE
        whatdoesthefedsay.com 00000000-0000-0000-0000-000000000000 NATIVE
```

# Cmdlet List

- [`Connect-Ion`](#connect-ion)
- [`Get-IonZone`](#get-ionzone)
- [`Get-IonRecord`](#get-ionrecord)
- [`New-IonRecord`](#new-ionrecord)
- [`New-IonRecordObj`](#new-ionrecordobj)
- [`Remove-IonRecord`](#remove-ionrecord)
- [`Set-IonZone`](#set-ionzone)
- [`Set-IonRecord`](#set-ionrecord)


# Cmdlet Descriptions
Help descriptions for each cmdlet. Running `Get-Help <cmdlet>` in Powershell should yield the same result. Documentation on the C# library should be similar to this. Both support being used with a linter.

<br><br>

## `Connect-Ion`
### Description
Combines the Public-prefix and Secret provided by IONOS for access to the API.
### Parameters
| Parameters      | Type   | Description                      | Mandatory | Piped |
| --------------- | ------ | -------------------------------- | --------- | ----- |
| `-PublicPrefix` | String | Public-Prefix provided by IONOS. | Yes       | No    |
| `-Secret`       | string | Secret provided by IONOS.        | Yes       | No    |
### Examples
Stores the credentials in a static class. This is required to do any other cmdlets.
```PowerShell
Connect-Ion -PublicPrefix "XXXX" -Secret "XXXX"
```
<br><br>

## `Get-IonZone`
### Description
Grabs information of either all zones, or a specific zone depending on the arguments given.
### Parameters
| Parameters | Type    | Description                                                                 | Mandatory | Piped |
| ---------- | ------- | --------------------------------------------------------------------------- | --------- | ----- |
| `-ZoneId`  | String  | Id attached to the DNS Zone.                                                | No        | Yes   |
| `-Zone`    | IonZone | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`. | No        | Yes   |
### Examples
Get all Zones accessible by the API Credentials.
```PowerShell
Get-IonZone
```
Get all Records for an IONOS Zone.
```PowerShell
$Zone = Get-IonZone -ZoneId "XXXX"
$Zone.Records
```
<br><br>

## `Get-IonRecord`
### Description
Grabs all DNS Records for a provided Zone. This is the same as `(Get-IonZone -ZoneId "XXXX").Records`.
### Parameters
| Parameters  | Type      | Description                                                                    | Mandatory | Piped |
| ----------- | --------- | ------------------------------------------------------------------------------ | --------- | ----- |
| `-ZoneId`   | String    | The id attached to the DNS Zone.                                               | Yes       | No    |
| `-Zone`     | IonZone   | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`.    | No        | Yes   |
| `-RecordId` | String    | The id attached to the DNS Record.                                             | Yes       | No    |
| `-Record`   | IonRecord | Class object that represents a DNS Record. Can be used instead of `-RecordId`. | No        | Yes   |
### Examples
Get all Records in a Zone.
```PowerShell
Get-IonRecord -ZoneId "XXXX"
# or
Get-IonRecord -Zone <[IonZone]>
```
Get a specific record based on RecordId
```PowerShell
Get-IonRecord -ZoneId "XXXX" -RecordId "XXXX"
# or
Get-IonRecord -Zone <[IonZone]> -Record <[IonRecord]>
```
<br><br>

## `New-IonRecord`
### Description
Creates a new Record in an IONOS Zone.
### Parameters
| Parameters | Type        | Description                                                                 | Mandatory | Piped |
| ---------- | ----------- | --------------------------------------------------------------------------- | --------- | ----- |
| `-ZoneId`  | String      | The id attached to the DNS Zone.                                            | Yes       | No    |
| `-Zone`    | IonZone     | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`. | No        | Yes   |
| `-Records` | IonRecord[] | Array of IonRecord Objects.                                                 | Yes       | Yes   |
### Examples
```PowerShell
New-IonRecord  -ZoneId "XXXX" -Records <[IonRecords[]]>
# or
$Zone | New-IonRecord -Records <[IonRecords[]]>
```
<br><br>

## `New-IonRecordObj`
### Description
Creates a new Record Object, ready to be sent to IONOS.
### Parameters
| Parameters    | Type    | Description                                                                 | Mandatory | Piped |
| ------------- | ------- | --------------------------------------------------------------------------- | --------- | ----- |
| `-Name`       | String  | Name of the Record. This needs to be an FQDN. `Ex. app.Contoso.com`         | Yes       | No    |
| `-ZoneName`   | String  | Name of the DNS Zone. `Ex. Contoso.com `                                    | Yes       | no    |
| `-Zone`       | IonZone | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`. | No        | Yes   |
| `-Type`       | String  | Type of DNS record to be set. `Ex. A/AAAA/NS/MX/...`                        | Yes       | No    |
| `-Content`    | String  | Content of the DNS record.                                                  | Yes       | no    |
| `-ChangeDate` | String  | Last change of this record. This property is set from IONOS side.           | No        | No    |
| `-TTL`        | String  | The TTL amount for the record.                                              | No        | no    |
| `-Disabled`   | bool    | Should the record be enabled on the Zone.                                   | No        | No    |

#### Examples
Creates a new A Record for App.Contoso.com
```PowerShell
New-IonRecordObj -Name "App.Contoso.com" -ZoneName "Contoso.com" -Type "A" -Content 192.168.2.7
# or
$ZoneObj | New-IonRecordObj -Name "App.Contoso.com" -Type "A" -Content 192.168.2.7
```
<br><br>

## `Remove-IonRecord`
### Description
Removes a Record from an IONOS Zone.
### Parameters
| Parameters  | Type      | Description                                                                    | Mandatory | Piped |
| ----------- | --------- | ------------------------------------------------------------------------------ | --------- | ----- |
| `-ZoneId`   | String    | The id attached to the DNS Zone.                                               | Yes       | No    |
| `-Zone`     | IonZone   | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`.    | No        | Yes   |
| `-RecordId` | String    | The id attached to the DNS Record.                                             | Yes       | No    |
| `-Record`   | IonRecord | Class object that represents a DNS Record. Can be used instead of `-RecordId`. | No        | Yes   |
### Examples
Removes an A Record from a Zone.
```PowerShell
Remove-IonRecord  -ZoneId "XXXX" -RecordId "XXXX"
# or
$RecordObj | Remove-IonRecord -ZoneId "XXXX"
# or
$ZoneObj | Remove-IonRecord -Record $RecordObj
```
<br><br>

## `Set-IonZone`
### Description
Sets configurations for a given Zone. Manifest provided will be set as the current configuration, discarding the previous one. This includes DNS Zones.
### Parameters
| Parameters | Type        | Description                                                                 | Mandatory | Piped |
| ---------- | ----------- | --------------------------------------------------------------------------- | --------- | ----- |
| `-ZoneId`  | String      | The id attached to the DNS Zone.                                            | Yes       | No    |
| `-Zone`    | IonZone     | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`. | No        | Yes   |
| `-Records` | IonRecord[] | Array of IonRecord Objects.                                                 | Yes       | Yes   |
### Examples
Set Zone DNS Configuration.
```PowerShell
Set-IonZone -ZoneId "XXXX" -Records $RecordObjList
# or
$ZoneObj | Set-IonZone -Records $RecordObjList
# or
$RecordObjList | Set-IonZone -Zone $ZoneObj
```
> **Important Note**
> 
> This will set the configuration of ALL records in a DNS Zone. The list of records pushed will be the new set of records. If you wish to add and remove individual Records, please use `New-IonRecord` or `Remove-IonRecord` respectively. If you post an empty list to this cmdlet, all DNS records on the Zone will be lost.
> <u>**Use at your own risk!**</u>

<br><br>

## `Set-IonRecord`
### Description
Uses the provided ZoneId and RecordId to change the configuration of a single Zone Record.
### Parameters
| Parameters | Type      | Description                                                                 | Mandatory | Piped |
| ---------- | --------- | --------------------------------------------------------------------------- | --------- | ----- |
| `-ZoneId`  | String    | The id attached to the DNS Zone.                                            | Yes       | No    |
| `-Zone`    | IonZone   | Class object that represents a DNS Zone. Can be used in place of `-ZoneId`. | No        | Yes   |
| `-Record`  | IonRecord | Class object that represents a DNS Record.                                  | Yes       | Yes   |
### Examples
Sets configuration for a Zone Record.
```PowerShell
Set-IonRecord -ZoneId "XXXX" -Record $RecordObj
# or
$RecordObj | Set-IonRecord -ZoneId "XXXX"
# or
$ZoneObj | Set-IonRecord -Record $RecordObj
```
<br><br>

# Any issues?
Please open an issue on this repo!