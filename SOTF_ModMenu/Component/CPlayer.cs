using SOTF_ModMenu.Utilities;
using TheForest;
using TheForest.Utils;

namespace SOTF_ModMenu.Component;

internal static class CPlayer
{
    /// <summary>
    ///     Enable/Disable Infinite Logs
    /// </summary>
    public static void InfLogs()
    {
        if (LocalPlayer.Inventory.Logs.HasLogs && LocalPlayer.Inventory.Logs.Amount < 2)
            LocalPlayer.Inventory.Logs._heldItemController._heldCount = 2;
        //can also use this but the way above is faster
        //DebugConsole.Instance._loghack(Settings.InfLogs ? "on" : "off");
    }

    /// <summary>
    ///     Enable/Disable Infinite Ammo
    /// </summary>
    public static void InfAmmo()
    {
        //Plugin.log.LogInfo("InfAmmo");
        //Cheats.Bridge.SetInfiniteAmmo(Settings.InfAmmo);
        //DebugConsole.GetInstance()._showVailRadar = VailRadarType.Verbose;
        //DebugConsole.GetInstance().ShowVailActorOverlay(VailOverlayType.Stats, "");
        //DebugConsole.Instance._loghack("on");
        //DebugConsole.Instance._ammohack(Settings.InfAmmo ? "on" : "off");

        //var t = Object.FindObjectOfType<FreeCamera>();
        //t.enabled = true;
        //t.
        //DebugConsole.Instance._freecamera(Settings.InfAmmo ? "on" : "off");
        //DebugConsole.GetInstance()._ammohack("on");
    }

    public static void SpeedyRun()
    {
        DebugConsole.Instance._speedyrun(Settings.SpeedyRun ? "on" : "off");
    }
}