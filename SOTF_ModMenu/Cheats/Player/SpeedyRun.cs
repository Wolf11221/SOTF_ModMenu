using SOTF_ModMenu.Utilities;
using TheForest;
using TheForest.Utils;
using UnityEngine;

namespace SOTF_ModMenu.Cheats.Player;

public class SpeedyRun : Cheat
{
    private bool _previousValue = false;
    
    public override void Update()
    {
        if (!LocalPlayer.IsInWorld) return;

        if (Settings.SpeedyRun != _previousValue)
        {
            _previousValue = Settings.SpeedyRun;

            DebugConsole.Instance._speedyrun(Settings.SpeedyRun ? "on" : "off");
        }
    }
}