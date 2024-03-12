using Assemblies.Sons.Cinematics;
using HarmonyLib;

namespace SOTF_ModMenu.Cheats.Other;

public class OgMenuMusic
{
    public static bool Enabled;
    private static bool _executed;

    public static void Start()
    {
        Enabled = Plugin.OgMenu.Value;
    }
    
    public static void Toggle()
    {
        if(SotfMain.SonsMainScene.isLoaded) return;
        Enabled = !Enabled;
        Plugin.OgMenu.Value = Enabled;
    }

    [HarmonyPatch(typeof(KonamiCode), nameof(KonamiCode.Update))]
    public class KonamiCodePatch
    {
        public static void Prefix(KonamiCode __instance)
        {
            if (!_executed && Plugin.OgMenu.Value)
            {
                _executed = true;
                __instance._onCodeCompleted.Invoke();
            }
        }
    }
}