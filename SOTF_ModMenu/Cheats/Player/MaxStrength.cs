using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class MaxStrength : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (Settings.MaxStrength) return;

        LocalPlayer.Vitals._strength._currentValue = LocalPlayer.Vitals._strength._max;
    }
}