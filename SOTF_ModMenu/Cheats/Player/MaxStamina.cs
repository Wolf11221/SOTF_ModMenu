using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class MaxStamina : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.MaxStamina) return;
        
        LocalPlayer.Vitals._stamina._currentValue = LocalPlayer.Vitals._stamina._max;
    }
}