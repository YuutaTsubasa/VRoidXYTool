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

**IMPORTANT:** VRoid Studio uses Unity 2022/2023, which requires BepInEx bleeding edge builds that support IL2CPP metadata version 31.

1. Install VRoid Studio 1.26.1 or later
2. Download and install **BepInEx IL2CPP Bleeding Edge** build:
   - Visit [BepInEx Bleeding Edge Build Server](https://builds.bepinex.dev/projects/bepinex_be)
   - Download the latest `BepInEx_UnityIL2CPP_x64` bleeding edge build
   - **DO NOT use** BepInEx 6.0.0-pre.2 or earlier - they only support metadata versions 23-29
3. Extract BepInEx to your VRoid Studio installation directory
4. Run VRoid Studio once to generate interop assemblies
   - If you see "Unsupported metadata version found! We support 23-29, got 31" error, your BepInEx version is too old
5. Copy files from your installation:
   - From `VRoid Studio/BepInEx/core/` → `lib/BepInEx/core/`
   - From `VRoid Studio/BepInEx/interop/` → `lib/BepInEx/interop/`

## Note

These DLLs are **reference assemblies only** for compilation. The actual runtime DLLs must be installed in the user's VRoid Studio installation.

You may also want to add `lib/` to `.gitignore` if you don't want to commit these DLLs to the repository.
