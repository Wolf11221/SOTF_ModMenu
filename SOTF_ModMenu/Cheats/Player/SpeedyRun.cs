using TheForest;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class SpeedyRun
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle SpeedyRun, not in a world"); return; }
        Enabled = !Enabled;
        
        DebugConsole.Instance._speedyrun(Enabled ? "on" : "off");
    }
}