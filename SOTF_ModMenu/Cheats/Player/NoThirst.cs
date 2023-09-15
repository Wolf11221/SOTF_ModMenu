using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class NoThirst : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.NoThirst) return;

        LocalPlayer.Vitals._hydration._currentValue = LocalPlayer.Vitals._hydration._max;
    }
}