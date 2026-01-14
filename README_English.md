# VRoidXYTool

[中文说明](README.md)

Extension Plugin for VRoid Studio

## VRoid Studio 1.26.1 and Later IL2CPP Support

Starting from version 0.8.3, this plugin now supports VRoid Studio's IL2CPP version (1.26.1 and higher).

### Installation Requirements
1. Install **BepInEx IL2CPP** version (not the regular Mono version)
2. Ensure BepInEx has generated IL2CPP interop assemblies (automatically generated after running VRoid Studio for the first time)
3. Place the plugin DLL in the `BepInEx\plugins` folder

### Important Notes
- If you are using VRoid Studio 1.26.0 or earlier Mono versions, please use plugin version 0.8.2 or earlier
- IL2CPP and Mono plugin versions are **NOT** interchangeable

## Why is the plug-in unavailable after VRoid Studio 1.18?
You can edit VRoid Studio\BepInEx\config\BepInEx.cfg, change `HideManagerGameObject = false` to `HideManagerGameObject = true`, then VRoidXYTool can work on the new version VRoid Studio.

## Introduce

- Base on [BeplnEx][1]
- Link Texture Tool. You can edit image in drawing tools(eg PS/SAI), when you save file, the file will auto sync to VRoidStudio.
- Camera Tool. Quickly set the position of the camera around the body or around the head. Set camera orthographic or perspective mode.
- Guide Tool. Add grid and guide image to VRoid Studio.
- Pose Perset Tool. In the PhotoBooth and pose mode, you can save and load custom pose perset.
- Anti-Aliasing.
- MMD Player(WIP). You can import VMD  files in the VRoid Studio for play
- Video Record. Do not rely on external software to record HD video in vroidstudio
- Wireframe Mode.

![Preview](LinkTexturePreview.gif)

![Preview](MMDPreview.gif)

![Preview](WireframePreview.png)

## Tutorial

- Tutorial Video[bilibili][2] (now only chinese video, If you have recorded tutorials in other languages, you can submit links to me.)

## Q&A

`Q:` I don't have the bepinex folder in the video. What should I do?

`A:` Install [BeplnEx][1]

`Q:` I installed the plugin. How can I open it in the VRoid Studio?

`A:` Tab or edit hotkey in BepInEx/config/me.xiaoye97.plugin.VRoidStudio.VRoidXYTool.cfg, `Hotkey = Tab`

`Q:` How can I contact you?

`A:` VRoid QQGroup(684544577), My private QQGroup (528385469), discord xiaoye#3171(Slow reply), Twitter @xiaoye1997 (Slow reply)

[1]: https://github.com/BepInEx/BepInEx/releases
[2]: https://www.bilibili.com/video/BV1TP4y1V7Qn/
[3]: https://www.bilibili.com/video/BV1BL41137Tc/