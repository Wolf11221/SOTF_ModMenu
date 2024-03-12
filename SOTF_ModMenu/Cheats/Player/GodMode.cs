using TheForest.Utils;

namespace SOTF_ModMenu.Cheats.Player;

public class GodMode
{
    public static bool Enabled;

    private static bool _gotMaxHealthValue = false;
    private static float _maxHealthBefore;

    private static bool _check = false;
    
    public static void Toggle()
    {
        if (!LocalPlayer.IsInWorld) { Plugin.log.LogError($"Failed to toggle GodMode, not in a world"); return; }
        Enabled = !Enabled;

        if (Enabled)
        {
            if (!_gotMaxHealthValue)
            {
                _check = true;
                _gotMaxHealthValue = true;
                _maxHealthBefore = LocalPlayer.Vitals._health._max;
            }
            
        }
        else
        {
            LocalPlayer.Vitals._health._max = _maxHealthBefore;
        }
    }
    
    public static void Update()
    {
        if (!Enabled && _check)
        {
            if (!LocalPlayer.IsInWorld)
            {
                _check = false;
                _gotMaxHealthValue = false;
                _maxHealthBefore = 0;
            }
        }
        
        if (!Enabled || !LocalPlayer.IsInWorld)  return;
        
        LocalPlayer.Vitals._health._max = 99999;
        LocalPlayer.Vitals._targetHealth = LocalPlayer.Vitals._health._max;
        LocalPlayer.Vitals._health._currentValue = LocalPlayer.Vitals._health._max;
    }
}