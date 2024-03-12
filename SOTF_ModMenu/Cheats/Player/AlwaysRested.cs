using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class AlwaysRested
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle AlwaysRested, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;

        LocalPlayer.Vitals._rested._currentValue = LocalPlayer.Vitals._rested._max;
    }
}