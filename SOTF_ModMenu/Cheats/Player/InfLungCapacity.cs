using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfLungCapacity
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InfLungCapacity, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;

        LocalPlayer.Vitals.LungBreathing.CurrentLungAir = LocalPlayer.Vitals.LungBreathing.MaxLungAirCapacity;
    }
}