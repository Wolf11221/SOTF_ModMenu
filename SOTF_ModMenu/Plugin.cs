using System.IO;
using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using BepInEx.Logging;
using BepInEx.Unity.IL2CPP;
using HarmonyLib;
using Il2CppInterop.Runtime.Injection;
using UnityEngine;

namespace SOTF_ModMenu
{
    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class Plugin : BasePlugin
    {
        public const string
            MODNAME = "SOTF_ModMenu",
            AUTHOR = "Nie",
            GUID = AUTHOR + "_" + MODNAME,
            VERSION = "1.5.0";
        
        public static ConfigFile ConfigFile = new (Path.Combine(Paths.ConfigPath, "SOTF_ModMenu.cfg"), true);
        public static ConfigEntry<KeyCode> ModKey = ConfigFile.Bind("Hotkeys", "Toggle", KeyCode.BackQuote, "Toggles menu visibility");
        public static ConfigEntry<bool> OgMenu = ConfigFile.Bind("Other", "Enabled", false, "Plays the og menu music form The Forest");
        
        public Plugin()
        {
            log = Log;
        }

        public override void Load()
        {
            try
            {
                ClassInjector.RegisterTypeInIl2Cpp<SotfMain>();
                GameObject gameObject = new GameObject("SotfBehavior");
                gameObject.AddComponent<SotfMain>();
                gameObject.hideFlags = HideFlags.HideAndDontSave;
                Object.DontDestroyOnLoad(gameObject);
            }
            catch
            {
                log.LogError($"FAILED to Register Il2Cpp Type!");
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
