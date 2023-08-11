using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoCold : Cheat
{
    private bool _previousValue = false;
    
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;

        if (Settings.NoCold != _previousValue)
        {
            _previousValue = Settings.NoCold;

            LocalPlayer.Stats.InteriorSpaceWarmth = Settings.NoCold;
        }
    }
}