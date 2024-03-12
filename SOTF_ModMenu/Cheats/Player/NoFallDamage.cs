using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoFallDamage
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError("Failed to toggle NoFallDamage, not in a world"); return; }
            
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        if (!Enabled || !LocalPlayer.IsInWorld) return;
        
        LocalPlayer.FpCharacter._allowFallDamage = !Enabled;
    }
}