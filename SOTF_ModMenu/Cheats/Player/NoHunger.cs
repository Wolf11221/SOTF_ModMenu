using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoHunger : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.NoHunger) return;

        LocalPlayer.Vitals._fullness._currentValue = LocalPlayer.Vitals._fullness._max;
    }
}