# Decor Lights modern standalone build

This folder has been modernized so Decor Lights can build without the legacy CaiLib/ILMerge infrastructure used by the original ONI-Mods repository.

## Requirements

- .NET SDK with `netstandard2.1` support
- A current Oxygen Not Included installation

By default the project looks for ONI at:

`C:\Program Files (x86)\Steam\steamapps\common\OxygenNotIncluded\OxygenNotIncluded_Data\Managed`

If ONI is installed elsewhere, either set the `ONI_ROOT` environment variable to the OxygenNotIncluded game directory or pass the managed folder explicitly:

```powershell
dotnet build .\DecorLights.csproj -c Release -p:ONIManagedPath="D:\SteamLibrary\steamapps\common\OxygenNotIncluded\OxygenNotIncluded_Data\Managed"
```

The build output is written to `bin\Release\` and includes the DLL, mod metadata, animations, and previews.

## Compatibility identity

The original building IDs are unchanged, and `staticID` remains `Cairath.DecorLights`, so existing saves should continue to recognize Decor Lights buildings.

## What was removed

- CaiLib runtime dependency
- ILMerge
- Cairath metadata-generator package
- repo-wide legacy build targets
- separate Vanilla/Spaced Out build configurations

The mod now uses direct ONI APIs for strings, plan-screen registration, and research unlock registration.
