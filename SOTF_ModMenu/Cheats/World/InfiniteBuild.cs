using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.World;

public class InfiniteBuild
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InfiniteBuild, not in a world"); return; }
        Enabled = !Enabled;
        
        LocalPlayer.Inventory.HeldOnlyItemController.InfiniteHack = Enabled;
    }
}