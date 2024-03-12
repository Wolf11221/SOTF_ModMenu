using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfHeal
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError("Failed to toggle InfHealth, not in a world"); return; }
            
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;
        
        LocalPlayer.Vitals._targetHealth = LocalPlayer.Vitals._health._max;
        LocalPlayer.Vitals._health._currentValue = LocalPlayer.Vitals._health._max;
    }
}