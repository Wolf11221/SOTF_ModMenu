using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Other;

public class InfiniteLogs : Cheat
{
    public override void Update()
    {
        if(!LocalPlayer.IsInWorld) return;
        if (!Settings.InfLogs) return;

        if (LocalPlayer.Inventory.Logs.HasLogs && LocalPlayer.Inventory.Logs.Amount < 2)
            LocalPlayer.Inventory.Logs._heldItemController._heldCount = 2;
    }
}