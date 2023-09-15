using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.World;

public class InstantBuild : Cheat
{
    private bool _previousValue = false;
    
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;

        if (Settings.InstantBuild != _previousValue)
        {
            _previousValue = Settings.InstantBuild;

            LocalPlayer.StructureCraftingSystem.InstantBuild = Settings.InstantBuild;
        }
    }
}