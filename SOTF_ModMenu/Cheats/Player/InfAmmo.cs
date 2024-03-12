using Sons.Inventory;
using Sons.Items.Core;
using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class InfAmmo
{
    public static bool Enabled;

    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle InfAmmo, not in a world"); return; }
        Enabled = !Enabled;
    }
    
    public static void Update()
    {
        // INFO: LocalPlayer.Inventory.Logs no longer exists
        
        /*
        //if holding item, try to get RangedWeaponItemInstanceModule and set ammo to max
        if (!Enabled || !LocalPlayer.IsInWorld) return;
        
        if (!LocalPlayer.Inventory.IsRightHandEmpty() && LocalPlayer.Inventory.RightHandItem.Data._type.HasFlag(Types.RangedWeapon))
            if (LocalPlayer.Inventory.RightHandItem.TryGetModule(out RangedWeaponItemInstanceModule module))
                if (module._rangedWeapon._ammo._currentCount < module._rangedWeapon._ammo._maxCount)
                    module._rangedWeapon._ammo._currentCount = module._rangedWeapon._ammo._maxCount;
        */
    }
}