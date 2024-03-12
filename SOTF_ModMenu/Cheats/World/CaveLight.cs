using TheForest.Utils;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace SOTF_ModMenu.Cheats.World;

public class CaveLight
{
    public static bool Enabled;
    private static bool _lightCreated;
    private static GameObject _light;
    
    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle CaveLight, not in a world"); return; }
        Enabled = !Enabled;

        ToggleCaveLight();
    }

    private static void ToggleCaveLight()
    {
        if(!_lightCreated)
        {
            _lightCreated = true;
                
            _light = new GameObject("CaveLight");
            _light.transform.SetParent(LocalPlayer.GameObject.transform);
            _light.transform.position = LocalPlayer.GameObject.transform.position + Vector3.up * 3f;
                
            var lightComponent = _light.AddComponent<Light>();
            lightComponent.intensity = 500000f;
                
            var additionalLightData = _light.AddComponent<HDAdditionalLightData>();
            additionalLightData.affectsVolumetric = false;

            lightComponent.intensity = Enabled ? 500000f : 0;
        }
        else
        {
            _light.GetComponent<Light>().intensity = Enabled ? 500000f : 0;
        }
    }

    public static void Update()
    {
        if (!LocalPlayer.IsInWorld && !SotfMain.SonsMainScene.isLoaded)
        {
            _lightCreated = false;
        }
    }
}