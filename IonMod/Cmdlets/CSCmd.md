# IonMod - C# Class/Method Glossary
<br>

The namespace for this C# library is `IonMod`.
```cs
using IonMod;
```

<br>

# Resource Class List
These classes are either inputs or outputs from the Static Command Classes.
- [`IonZone`](#ionzone)
- [`IonRecord`](#ionrecord)
- [`IonToken`](#iontoken)

<br>

# Static Command Class List
Each one represents an IONOS Rest API endpoint. With `IonConnect` being the exception, all the Static Command Classes can be invoked by `<Static_Cmd_Class>.Run(...)`.
- [`IonConnect`](#ionconnect)
- [`GetIonZone`](#getionzone)
- [`GetIonRecord`](#getionrecord)
- [`NewIonRecord`](#newionrecord)
- [`RmIonRecord`](#rmionrecord)
- [`SetIonZone`](#setionzone)
- [`SetIonRecord`](#setionrecord)

<br>

# Resource Class Descriptions

## `IonZone`
### Description
Class object represents a single DNS Zone. Contains metadata regarding the Zone, and DNS Records.
### Properties
| Property  | Type              | Description                                                                                        | Mandatory | Read-Only |
| --------- | ----------------- | -------------------------------------------------------------------------------------------------- | --------- | --------- |
| `Name`    | string            | Name of the DNS name. This will be FQDN `Ex. domain.com`                                           | yes       | No        |
| `Id`      | string            | The IONOS set GUID for the DNS Zone.                                                               | No        | No        |
| `Type`    | string            | If Zone is imported or native to IONOS. This is set by IONOS.                                      | No        | No        |
| `Records` | `List<IonRecord>` | The DNS Records contained by this DNS Zone. Some endpoints do not populate this, so it maybe null. | No        | No        |
### Constructors
```cs
IonZone Zone = new(string name, string id, string type, List<IonRecord> records);
```
Zones can not be created via API, so manual creation of the object is likely not needed. Endpoints that return `IonZone` will deserialize it from the IONOS HTTP response.
### Methods
#### `InitRecords()`
Initializes the `Records` property in an instance where it is null. Takes no arguments.

<br>

## `IonRecord`
### Description
Class object represents a single DNS Record. Contains metadata regarding the name, contents, and general configurations.
### Properties
| Property     | Type   | Description                                                     | Mandatory | Read-Only |
| ------------ | ------ | --------------------------------------------------------------- | --------- | --------- |
| `Name`       | string | Name of the DNS Record. This will be FQDN `Ex. App.domain.com`  | yes       | No        |
| `RootName`   | string | Name of the DNS Zone. This will be FQDN `Ex. domain.com`        | yes       | No        |
| `Content`    | string | Content of the DNS Record. `Ex. 1.1.1.1`                        | yes       | No        |
| `Type`       | string | Type of DNS Record. `Ex. A/AAAA/NS/MX` Default: `A`             | No        | No        |
| `ChangeDate` | string | The last date the Record was set. This is set by IONOS.         | No        | No        |
| `TTL`        | int    | TTL (Time to live) value for the Record. Default: `3600`        | No        | No        |
| `Disabled`   | bool   | Determines if the record can be used via DNS.  Default: `false` | No        | No        |
| `Id`         | string | The IONOS set GUID for the DNS Record.                          | No        | No        |
### Constructors
```cs
IonRecord Record = new(
    string name, 
    string content, 
    string rootname = "", 
    string type = "A", 
    string changedate = "", 
    string ttl = "3600", 
    bool disabled = false, 
    string id = ""
    );
```

<br>

## IonToken
### Description
Stores PublicPrefix and Secret provided by IONOS for API access.
### Properties
| Property       | Type   | Description                                                | Mandatory | Read-Only |
| -------------- | ------ | ---------------------------------------------------------- | --------- | --------- |
| `PublicPrefix` | string | PublicPrefix portion of the IONOS provided API credential. | yes       | No        |
| `Secret`       | string | Secret portion of the IONOS provided API credential.       | yes       | No        |
### Constructors
``` cs
IonToken Token = new(string publicPrefix, string secret);
```

<br><br><br>

# Static Command Classes Descriptions

## `IonConnect`
### Description
A static class that contains the credentials and HTTP client configurations for connecting to IONOS Rest endpoints.
### Properties
| Property      | Type           | Description                                                                             | Mandatory | Read-Only |
| ------------- | -------------- | --------------------------------------------------------------------------------------- | --------- | --------- |
| `Client`      | Net.HttpCLient | The HTTP client that will be used for all the requests.                                 | No        | Yes       |
| `RootURI`     | string         | Root endpoint for IONOS Rest API requests. `https://api.hosting.ionos.com/dns/v1/zones` | No        | No        |
| `ContentType` | string         | ContentType header that will be used in the requests. `application/json`                | No        | No        |
| `Token`       | IonToken       | Contains PublicPrefix and Secret provided by IONOS for connection.                      | No        | No        |
### Methods
### `Login()`
#### Description
Stores credentials in the IonConnect Static class. This only needs to be done once at runtime, and again if you wish to switch IONOS account context.
#### Example
```cs
// With IonToken
IonConnect.Login(IonToken)
// With API credentials directly
IonConnect.Login(PubicPrefix, Secret)
```
<br>

### `Get<T>()`
#### Description
Performs a generic API GET request to IONOS. Used to make the other Static Command Classes.
#### Example
```cs
IonZone Zone = IonConnect.Get<IonZone>("/" + zoneId);
```
<br>

### `Put() | Put<T>()`
#### Description
Performs a generic API PUT request to IONOS. Used to make the other Static Command Classes.
#### Example
```cs
using Newtonsoft.Json;
// Void return.
IonConnect.Put("/" + zoneId + "/records/" + record.Id, JsonConvert.SerializeObject(record));
// or
// Object return variant.
IonRecord Record = IonConnect.Put<IonRecord>("/" + zoneId + "/records/" + record.Id, JsonConvert.SerializeObject(record));
```
<br>

### `Post() | Post<T>()`
#### Description
Performs a generic API POST request to IONOS. Used to make the other Static Command Classes.
#### Example
```cs
using Newtonsoft.Json;
// Void return.
IonConnect.Put("/" + zoneId, JsonConvert.SerializeObject(records))
// or
// Object return variant.
List<IonRecord> Records = IonConnect.Post<List<IonRecord>>("/" + zoneId + "/records", JsonConvert.SerializeObject(records));
```
<br>

### `Delete() | Delete<T>()`
#### Description
Performs a generic API DELETE request to IONOS. Used to make the other Static Command Classes.
#### Example
```cs
using Newtonsoft.Json;
// Void return.
IonConnect.Delete("/" + zoneId + "/records/" + recordId);
// or
// Object return variant.
IonRecord Record = IonConnect.Delete<IonRecord>("/" + zoneId + "/records/" + recordId);
```
<br>

## `GetIonZone`
### Description
Grabs information of either all zones, or a specific zone depending on the arguments given.
### Methods
### `Run()`
#### Description
Grabs information of either all zones, or a specific zone depending on the arguments given.
#### Example
```cs
using IonMod;
// Get all Zones
List<IonZone> Zones = GetIonZone.Run();
// Specific Zone.
IonZone Zone = GetIonZone.Run(string ZoneId);
```
<br>

## `GetIonRecord`
### Description
Grabs all DNS Records for a provided Zone.
### Methods
### `Run()`
#### Description
Grabs all DNS Records for a provided Zone.
#### Example
```cs
using IonMod;
// Get single record of a Zone.
IonRecord Record = GetIonRecord.Run(IonZone zone, string recordId);
// or
IonRecord Record = GetIonRecord.Run(IonZone zone, IonRecord record);
```
<br>

## `NewIonRecord`
### Description
Creates a new Record in an IONOS Zone.### Methods
### `Run()`
#### Description
Creates a new Record in an IONOS Zone.
#### Example
```cs
using IonMod;
// Create new Zone(s)
List<IonRecord> Records = NewIonRecord.Run(IonZone zone, List<IonRecord> records);
// or
List<IonRecord> Records = NewIonRecord.Run(string zoneId, List<IonRecord> records);
```
<br>

## `RmIonRecord`
### Description
Removes a Record from an IONOS Zone.### Methods
### `Run()`
#### Description
Removes a Record from an IONOS Zone.
#### Example
```cs
using IonMod;
// Delete a DNS Record
RmIonRecord.Run(IonZone zone, IonRecord record);
// or
RmIonRecord.Run(string zoneId, string recordId);
```
<br>

## `SetIonZone`
### Description
Sets configurations for a given Zone. Manifest provided will be set as the current configuration, discarding the previous one. This includes DNS Zones.
### Methods
### `Run()`
#### Description
Sets configurations for a given Zone. Manifest provided will be set as the current configuration, discarding the previous one. This includes DNS Zones.
#### Example
```cs
using IonMod;
// Set all the records in a DNS Zone.
SetIonZone.Run(IonZone zone);
// or
SetIonZone.Run(string zoneId, List<IonRecord> records);
```
<br>

## `SetIonRecord`
### Description
Uses the provided ZoneId and RecordId to change the configuration of a single Zone Record.
### Methods
### `Run()`
#### Description
Uses the provided ZoneId and RecordId to change the configuration of a single Zone Record.
#### Example
```cs
using IonMod;
// Sets the configurations of a single DNS Record.
IonRecord Record = SetIonRecord.Run(IonZone zone, IonRecord record);
// or
IonRecord Record = SetIonRecord.Run(string zoneId, string recordId);
```
<br>


