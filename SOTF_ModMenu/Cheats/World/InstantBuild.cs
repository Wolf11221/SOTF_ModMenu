using Sons.Crafting.Structures;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.World;

public class InstantBuild
{
    public static bool Enabled;
    
    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InstantBuild, not in a world"); return; }
        Enabled = !Enabled;

        //LocalPlayer.StructureCraftingSystem.InstantBuild = Enabled;
        StructureCraftingSystem._instance.InstantBuild = Enabled;
    }
}