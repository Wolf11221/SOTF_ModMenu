using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfLungCapacity : Cheat
{
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.InfLungCapacity) return;

        LocalPlayer.Vitals.LungBreathing.CurrentLungAir = LocalPlayer.Vitals.LungBreathing.MaxLungAirCapacity;
    }
}