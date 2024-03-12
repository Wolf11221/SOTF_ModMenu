using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoThirst
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle NoThirst, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;

        LocalPlayer.Vitals._hydration._currentValue = LocalPlayer.Vitals._hydration._max;
    }
}