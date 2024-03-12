using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoHunger
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle NoHunger, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;

        LocalPlayer.Vitals._fullness._currentValue = LocalPlayer.Vitals._fullness._max;
    }
}