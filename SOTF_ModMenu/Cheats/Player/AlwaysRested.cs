using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class AlwaysRested : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.AlwaysRested) return;

        LocalPlayer.Vitals._rested._currentValue = LocalPlayer.Vitals._rested._max;
    }
}