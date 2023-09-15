using SOTF_ModMenu.Utilities;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.Rendering.HighDefinition;

namespace SOTF_ModMenu.Cheats.World;

public class CaveLight : Cheat
{
    private static GameObject _lightGameObject;
    private bool _previousValue = false;
    
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;

        if (Settings.CaveLight != _previousValue)
        {
            _previousValue = Settings.CaveLight;

            if (Settings.CaveLight)
            {
                _lightGameObject = new GameObject("Light");
                _lightGameObject.transform.SetParent(LocalPlayer.GameObject.transform);
                _lightGameObject.transform.position = LocalPlayer.GameObject.transform.position + Vector3.up * 3f;

                var lightComponent = _lightGameObject.AddComponent<Light>();
                lightComponent.intensity = 500000f;

                var additionalLightData = _lightGameObject.AddComponent<HDAdditionalLightData>();
                additionalLightData.affectsVolumetric = false;
            }
            else
            {
                Destroy(_lightGameObject);
            }
        }
    }
}