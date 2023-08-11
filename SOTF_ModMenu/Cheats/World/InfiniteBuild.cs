using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.World;

public class InfiniteBuild : Cheat
{
    private bool _previousValue = false;
    
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;

        if (Settings.InfBuild != _previousValue)
        {
            _previousValue = Settings.InfBuild;

            LocalPlayer.Inventory.HeldOnlyItemController.InfiniteHack = Settings.InfBuild;
        }
    }
}