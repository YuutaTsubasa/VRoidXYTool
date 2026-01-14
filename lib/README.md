# Build Dependencies for CI

This folder contains the DLL dependencies required to build VRoidXYTool in GitHub Actions CI.

## Required Files

To enable automated builds, you need to populate this folder with DLLs from a working VRoid Studio + BepInEx IL2CPP installation.

### Directory Structure

```
lib/
├── BepInEx/
│   ├── core/
│   │   ├── 0Harmony.dll
│   │   ├── BepInEx.dll
│   │   ├── Il2CppInterop.Runtime.dll
│   │   └── Il2CppInterop.Common.dll
│   └── interop/
│       ├── UnityEngine.dll
│       ├── UnityEngine.*.dll (various modules)
│       ├── VRoid.Studio.dll
│       ├── VRoid.UI.dll
│       ├── VRoidCore.dll
│       ├── VRoidCore.Protobuf.dll
│       ├── VRoidSDK.dll
│       ├── VRoidStudio.dll
│       ├── MToon.dll
│       ├── Newtonsoft.Json.dll
│       └── UnityTablet.dll
```

## How to Populate

1. Install VRoid Studio 1.26.1 or later
2. Install BepInEx IL2CPP (6.0.0-pre.2 or later) 
3. Run VRoid Studio once to generate interop assemblies
4. Copy files from your installation:
   - From `VRoid Studio/BepInEx/core/` → `lib/BepInEx/core/`
   - From `VRoid Studio/BepInEx/interop/` → `lib/BepInEx/interop/`

## Note

These DLLs are **reference assemblies only** for compilation. The actual runtime DLLs must be installed in the user's VRoid Studio installation.

You may also want to add `lib/` to `.gitignore` if you don't want to commit these DLLs to the repository.
