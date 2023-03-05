using BepInEx;
using BepInEx.Configuration;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using System.IO;
using System.Reflection;
using UnityEngine;
using BepInEx.Logging;
using Object = UnityEngine.Object;

namespace SOTF_ModMenu
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Plugin : BasePlugin
    {
        public const string
            MODNAME = "SOTF_ModMenu",
            AUTHOR = "Nie",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.1.2";
        
        public static ConfigFile ConfigFile = new (Path.Combine(Paths.ConfigPath, "SOTF ModMenu.cfg"), true);
        public static ConfigEntry<KeyCode> ModmenuKeybind = ConfigFile.Bind("Hotkeys", "Toggle", KeyCode.Tilde, "Enables or disables the Mod Menu");
        public Plugin()
        {
            log = Log;
        }

        public override void Load()
        {
            try
            {
                ClassInjector.RegisterTypeInIl2Cpp<Main.MyMonoBehaviour>();
                GameObject gameObject = new GameObject("CoolObject");
                gameObject.AddComponent<Main.MyMonoBehaviour>();
                gameObject.hideFlags = HideFlags.HideAndDontSave;
                Object.DontDestroyOnLoad(gameObject);
            }
            catch
            {
                log.LogError($"FAILED to Register Il2Cpp Type: MyMonoBehaviour!");
            }
            try
            {
                Harmony.CreateAndPatchAll(Assembly.GetExecutingAssembly(), null);
            }
            catch
            {
                log.LogError($"FAILED to register patches!");
            }
            
        }

        public static ManualLogSource log;
    }
}
