using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Other;

public class InfiniteLogs
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InfiniteBuild, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        // INFO: LocalPlayer.Inventory.Logs no longer exists
        
        /*

        if (!Enabled) return;
        
        if (LocalPlayer.Inventory.Logs.HasLogs && LocalPlayer.Inventory.Logs.Amount < 2)
            LocalPlayer.Inventory.Logs._heldItemController._heldCount = 2;

         */
    }
}