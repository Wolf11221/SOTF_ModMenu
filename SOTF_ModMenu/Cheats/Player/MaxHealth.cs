using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class MaxHealth : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.MaxHealth) return;
        
        LocalPlayer.FpCharacter._allowFallDamage = false;
        LocalPlayer.Vitals._health._currentValue = LocalPlayer.Vitals._health._max;
    }
}