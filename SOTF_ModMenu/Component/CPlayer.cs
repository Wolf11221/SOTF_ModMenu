using Sons.Inventory;
using Sons.Items.Core;
using SOTF_ModMenu.Utilities;
using TheForest;
using TheForest.Utils;

namespace SOTF_ModMenu.Component;

internal static class CPlayer
{
    /// <summary>
    ///     Enable/Disable Infinite Logs
    /// </summary>
    public static void InfLogs()
    {
        if(LocalPlayer.IsInWorld)
            if (LocalPlayer.Inventory.Logs.HasLogs && LocalPlayer.Inventory.Logs.Amount < 2)
                LocalPlayer.Inventory.Logs._heldItemController._heldCount = 2;
        //can also use this but the way above is faster
        //DebugConsole.Instance._loghack(Settings.InfLogs ? "on" : "off");
    }

    /// <summary>
    ///     Enable/Disable Infinite Ammo
    /// </summary>
    public static void InfAmmo()
    {
        //if holding item, try to get RangedWeaponItemInstanceModule and set ammo to max
        if (!LocalPlayer.Inventory.IsRightHandEmpty() && !LocalPlayer.Inventory.Logs.HasLogs)
            if(LocalPlayer.Inventory.RightHandItem.Data._type.HasFlag(Types.RangedWeapon))
                if (LocalPlayer.Inventory.RightHandItem.TryGetModule(out RangedWeaponItemInstanceModule module))
                    if (module._rangedWeapon._ammo._currentCount < module._rangedWeapon._ammo._maxCount)
                        module._rangedWeapon._ammo._currentCount = module._rangedWeapon._ammo._maxCount;
    }

    public static void SpeedyRun()
    {
        DebugConsole.Instance._speedyrun(Settings.SpeedyRun ? "on" : "off");
    }
}