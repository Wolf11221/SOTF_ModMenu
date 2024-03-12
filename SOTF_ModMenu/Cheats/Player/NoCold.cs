using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoCold
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle NoCold, not in a world"); return; }
        Enabled = !Enabled;

        LocalPlayer.Stats.InteriorSpaceWarmth = Enabled;
    }
}