using Sons.Inventory;
using SOTF_ModMenu.Utilities;
using TheForest;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;
using Types = Sons.Items.Core.Types;

namespace SOTF_ModMenu.Component;

internal static class CPlayer
{
    private static GameObject _lightGameObject;
    private static bool _spawned;

    /// <summary>
    ///     Enable/Disable Infinite Logs
    /// </summary>
    public static void InfLogs()
    {
        if (!Settings.InfLogs) return;
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
        if (!Settings.InfAmmo) return;
        if (!LocalPlayer.Inventory.IsRightHandEmpty() && !LocalPlayer.Inventory.Logs.HasLogs)
            if (LocalPlayer.Inventory.RightHandItem.Data._type.HasFlag(Types.RangedWeapon))
                if (LocalPlayer.Inventory.RightHandItem.TryGetModule(out RangedWeaponItemInstanceModule module))
                    if (module._rangedWeapon._ammo._currentCount < module._rangedWeapon._ammo._maxCount)
                        module._rangedWeapon._ammo._currentCount = module._rangedWeapon._ammo._maxCount;
    }
    
    /// <summary>
    /// Enable/Disable Speed Run
    /// </summary>
    public static void SpeedyRun() => DebugConsole.Instance._speedyrun(Settings.SpeedyRun ? "on" : "off");
    
    /// <summary>
    /// Enable/Disable CaveLight
    /// </summary>
    public static void CaveLight() =>DebugConsole.Instance._caveLight(Settings.CaveLight ? "on" : "off");

    public static void CaveLight(bool enableLight)
    {
        //turn on cave light, not sure which would be better
        //DebugConsole.Instance._caveLight(Settings.CaveLight ? "on" : "off");
        if (LocalPlayer.IsInWorld)
        {
            if (enableLight && !_spawned)
            {
                _spawned = true;
                _lightGameObject = new GameObject("Light");
                _lightGameObject.transform.SetParent(LocalPlayer.GameObject.transform);
                _lightGameObject.transform.position = LocalPlayer.GameObject.transform.position + Vector3.up * 3f;

                var lightComponent = _lightGameObject.AddComponent<Light>();
                lightComponent.intensity = 500000f;

                var additionalLightData = _lightGameObject.AddComponent<HDAdditionalLightData>();
                additionalLightData.affectsVolumetric = false;
            }
            else if (!enableLight && _lightGameObject != null)
            {
                _spawned = false;
                Object.Destroy(_lightGameObject);
            }
        }
    }
}