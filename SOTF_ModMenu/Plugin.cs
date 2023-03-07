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
            VERSION = "1.1.4";
        
        public static ConfigFile ConfigFile = new (Path.Combine(Paths.ConfigPath, "SOTF_ModMenu.cfg"), true);
        public static ConfigEntry<KeyCode> ModMenuKeybind = ConfigFile.Bind("Hotkeys", "Toggle", KeyCode.BackQuote, "Enables or disables the Mod Menu");
        public static ConfigEntry<KeyCode> SpawnItemKeybind = ConfigFile.Bind("Hotkeys", "SpawnItem", KeyCode.F8, "Spawns the currently stored item ID");
        public static ConfigEntry<KeyCode> HideHUDKeybind = ConfigFile.Bind("Hotkeys", "HideHUD", KeyCode.None, "Hides the in game HUD");

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
            log.LogInfo($"ModMenu toggle keybind set to: {ModMenuKeybind.Value}");
        }

        public static ManualLogSource log;
    }
}
