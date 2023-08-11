using Sons.Inventory;
using Sons.Items.Core;
using SOTF_ModMenu.Utilities;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfAmmo : Cheat
{
    public override void Update()
    {
        //if holding item, try to get RangedWeaponItemInstanceModule and set ammo to max
        if (!LocalPlayer.IsInWorld) return;
        if (!Settings.InfAmmo) return;
        
        if (!LocalPlayer.Inventory.IsRightHandEmpty() && !LocalPlayer.Inventory.Logs.HasLogs)
            if (LocalPlayer.Inventory.RightHandItem.Data._type.HasFlag(Types.RangedWeapon))
                if (LocalPlayer.Inventory.RightHandItem.TryGetModule(out RangedWeaponItemInstanceModule module))
                    if (module._rangedWeapon._ammo._currentCount < module._rangedWeapon._ammo._maxCount)
                        module._rangedWeapon._ammo._currentCount = module._rangedWeapon._ammo._maxCount;
    }
}