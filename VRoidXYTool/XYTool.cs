using System;
using BepInEx;
using BepInEx.Unity.IL2CPP;
using BepInEx.Logging;
using Il2CppInterop.Runtime.Injection;
using HarmonyLib;
using UnityEngine;
using VRoid.Studio;
using BepInEx.Configuration;
using System.IO;
using VRoidStudio.GUI.AvatarEditor.PhotoBooth;
using VRoidStudio.GUI.AvatarEditor;

namespace VRoidXYTool
{
    [BepInPlugin(PluginID, PluginName, PluginVersion)]
    public partial class XYTool : BasePlugin
    {
        public const string PluginID = "me.xiaoye97.plugin.VRoidStudio.VRoidXYTool";
        public const string PluginName = "VRoidXYTool";
        public const string PluginVersion = "0.8.2";

        public static XYTool Inst;

        #region 工具
        public CameraTool CameraTool;
        public GuideTool GuideTool;
        public LinkTextureTool LinkTextureTool;
        //public PosePersetTool PosePersetTool;
        public MMDTool MMDTool;
        public VideoTool VideoTool;
        public WireframeTool WireframeTool;
        #endregion

        #region 引用
        public AvatarEditor AvatarEditor;
        public MainViewModel MainVM
        {
            get
            {
                if (AvatarEditor != null)
                {
                    return AvatarEditor._viewModel;
                }
                return null;
            }
        }
        public PhotoBoothViewModel PhotoBoothVM
        {
            get
            {
                if (AvatarEditor != null)
                {
                    return AvatarEditor._instantiatedPhotoBoothViewModel;
                }
                return null;
            }
        }

        public CurrentFileModel CurrentFileM
        {
            get
            {
                if (CurrentFileVM != null)
                {
                    return CurrentFileVM.model;
                }
                return null;
            }
        }

        public CurrentFileViewModel CurrentFileVM
        {
            get
            {
                if (MainVM != null)
                {
                    return MainVM.CurrentFile;
                }
                return null;
            }
        }

        /// <summary>
        /// 模型是否为空
        /// </summary>
        public bool IsModelNull
        {
            get
            {
                if (CurrentFileVM == null) return true;
                if (CurrentFileM == null) return true;
                return false;
            }
        }

        /// <summary>
        /// 当前模型的名字，如果还未保存，则返回null
        /// </summary>
        public string CurrentModelName
        {
            get
            {
                if (XYTool.Inst.IsModelNull) return null;
                // 获取模型名字
                string modelPath = XYTool.Inst.CurrentFileM.path;
                if (string.IsNullOrWhiteSpace(modelPath)) return null;
                FileInfo modelFile = new FileInfo(modelPath);
                if (!modelFile.Exists) return null;
                string modelName = modelFile.Name.Replace(".vroid", "");
                return modelName;
            }
        }
        #endregion

        #region 配置
        public ConfigEntry<SystemLanguage> PluginLanguage;
        public ConfigEntry<bool> RunInBG;
        public ConfigEntry<KeyCode> GUIHotkey;
        public ConfigEntry<KeyCode> MiniGUIHotkey;
        #endregion

        public override void Load()
        {
            Inst = this;
            // 多语言
            I18N.Init();
            PluginLanguage = Config.Bind<SystemLanguage>("Common", "Language", Application.systemLanguage, "Plugin language");
            I18N.SetLanguage(PluginLanguage.Value);
            // 绑定配置
            RunInBG = Config.Bind<bool>("Common", "RunInBG", true, "XYTool.RunInBGDesc".Translate());
            GUIHotkey = Config.Bind<KeyCode>("Common", "GUIHotkey", KeyCode.Tab, "XYTool.GUIHotkey".Translate());
            MiniGUIHotkey = Config.Bind<KeyCode>("Common", "MiniGUIHotkey", KeyCode.BackQuote, "XYTool.MiniGUIHotkey".Translate());

            Log.LogInfo("XYTool启动");
            
            // Add the MonoBehaviour component to the scene for Update() support
            ClassInjector.RegisterTypeInIl2Cpp<XYToolMonoBehaviour>();
            var go = new UnityEngine.GameObject("XYTool");
            UnityEngine.Object.DontDestroyOnLoad(go);
            go.AddComponent<XYToolMonoBehaviour>();
        }
    }
    
    // MonoBehaviour component for Unity lifecycle methods
    public class XYToolMonoBehaviour : MonoBehaviour
    {
        public XYToolMonoBehaviour(IntPtr ptr) : base(ptr) { }

        private void Start()
        {
            XYTool.Inst.AvatarEditor = GameObject.FindObjectOfType<AvatarEditor>();
            // Patch
            Harmony.CreateAndPatchAll(typeof(XYToolPatches));
            XYTool.Inst.CameraTool = new CameraTool();
            XYTool.Inst.GuideTool = new GuideTool();
            XYTool.Inst.LinkTextureTool = new LinkTextureTool();
            //XYTool.Inst.PosePersetTool = new PosePersetTool();
            XYTool.Inst.MMDTool = new MMDTool();
            XYTool.Inst.VideoTool = new VideoTool();
            XYTool.Inst.WireframeTool = new WireframeTool();
            // UI窗口
            XYTool.Inst.InitWindow();
            XYTool.Inst.headTex = XYModLib.ResourceUtils.GetTex("head_xiaoye.png");
        }

        private void Update()
        {
            // 控制界面显示
            if (Input.GetKeyDown(XYTool.Inst.GUIHotkey.Value))
            {
                XYTool.Inst.Window.Show = !XYTool.Inst.Window.Show;
            }
            if (Input.GetKeyDown(XYTool.Inst.MiniGUIHotkey.Value))
            {
                XYTool.Inst.MiniWindow.Show = !XYTool.Inst.MiniWindow.Show;
            }
            // 控制配置中的值同步
            if (Application.runInBackground != XYTool.Inst.RunInBG.Value)
            {
                Application.runInBackground = XYTool.Inst.RunInBG.Value;
            }
            // 工具的Update
            XYTool.Inst.CameraTool.Update();
            XYTool.Inst.LinkTextureTool.Update();
            XYTool.Inst.VideoTool.Update();
            XYTool.Inst.WireframeTool.Update();
        }
    }
    
    // Harmony patches
    [HarmonyPatch]
    public static class XYToolPatches
    {
        /// <summary>
        /// 切换到主界面时，清理各种数据
        /// </summary>
        [HarmonyPostfix, HarmonyPatch(typeof(VRoid.Studio.StartScreen.ViewModel), MethodType.Constructor, new Type[] { typeof(VRoid.Studio.MainViewModel), typeof(VRoid.UI.BindableResources), typeof(VRoid.Studio.GlobalBus) })]
        public static void StartScreenPatch()
        {
            if (XYTool.Inst != null && XYTool.Inst.LinkTextureTool != null)
            {
                XYTool.Inst.LinkTextureTool.Clear();
            }
        }
    }
}
}