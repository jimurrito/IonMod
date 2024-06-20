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


# Example

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

# Documentation
- [**Powershell Cmdlets**](/IonMod/Cmdlets/PSCmd.md)
- [**C# Librarry**](/IonMod/Cmdlets/CSCmd.md)

# Known issue with `using` keyword when using `IonMod.psd1` from source in Powershell.
[As mentioned here](https://github.com/jimurrito/PSTest?tab=readme-ov-file#how-to-use-pstest), the `using` keyword will not work with the named module when importing the module from source. Rule of thumb is that if you are using `Import-Module` and the module manifest file `.psd1`, you will also need to use the path when declaring `using` in Powershell.

```Powershell
# Choose one!
# From Nuget
Install-Module IonMod
Import-Module IonMod
using module IonMod

# From Source
git <....>
Import-Module /path/to/IonMod.psd1
using module /path/to/IonMod.psd1
```

This is not an issue in C#, and is purely a limitation of how Powershell's `using` keyword queries the available modules.

## Any issues?
Please open an issue on this repo!