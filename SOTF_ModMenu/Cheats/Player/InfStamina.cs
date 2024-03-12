using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfStamina
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InfStamina, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;
        
        LocalPlayer.Vitals._stamina._currentValue = LocalPlayer.Vitals._stamina._max;
    }
}