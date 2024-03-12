using Construction;
using Il2CppSystem.Collections.Generic;
using Sons.Input;
using SOTF_ModMenu.Cheats.ESP;
using SOTF_ModMenu.Cheats.Other;
using SOTF_ModMenu.Cheats.Player;
using SOTF_ModMenu.Cheats.World;
using SOTF_ModMenu.UI;
using TheForest.Items.Inventory;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SOTF_ModMenu;

public class SotfMain : MonoBehaviour
{
    public static Camera CameraMain;
    public static HashSet<Structure> DirtyStructures;
    public static Scene SonsMainScene;
    private List<PlayerInventory.PlayerViews> _playerViewsList = new();
    private bool _stylesInitialized;
    
    private void Start()
    {
        InvokeRepeating(nameof(SlowUpdate), 0.0f, 0.5f);
        OgMenuMusic.Start();
        
        _playerViewsList.Add(PlayerInventory.PlayerViews.PlaneCrash);
        _playerViewsList.Add(PlayerInventory.PlayerViews.WakingUp);
        _playerViewsList.Add(PlayerInventory.PlayerViews.World);
        _playerViewsList.Add(PlayerInventory.PlayerViews.Inventory);
        _playerViewsList.Add(PlayerInventory.PlayerViews.GrabBag);
        _playerViewsList.Add(PlayerInventory.PlayerViews.Sleep);
    }
    
    private void OnGUI()
    {
        if (!_stylesInitialized)
        {
            _stylesInitialized = true;
            UIHelper.InitializeStyles();
        }
        
        UIManager.Display();
    }

    private void SlowUpdate()
    {
        // Disable all cheats when in main menu
        if (!SonsMainScene.isLoaded)
        {
            ESP.Enabled = false;
            InfiniteLogs.Enabled = false;
            AlwaysRested.Enabled = false;
            GodMode.Enabled = false;
            InfAmmo.Enabled = false;
            InfHeal.Enabled = false;
            InfLungCapacity.Enabled = false;
            InfStamina.Enabled = false;
            NoCold.Enabled = false;
            NoFallDamage.Enabled = false;
            NoHunger.Enabled = false;
            NoThirst.Enabled = false;
            SpeedyRun.Enabled = false;
            CaveLight.Enabled = false;
            InfiniteBuild.Enabled = false;
            InstantBuild.Enabled = false;
        }
    }
    
    private void Update()
    {
        // Cache Scene, Camera
        if(!SonsMainScene.isLoaded) SonsMainScene = SceneManager.GetSceneByName("SonsMain");
        if(CameraMain == null) CameraMain = Camera.main ?? null;
        
        GodMode.Update();
        CaveLight.Update();
        NoThirst.Update();
        NoHunger.Update();
        InfStamina.Update();
        InfHeal.Update();
        InfLungCapacity.Update();
        InfAmmo.Update();
        AlwaysRested.Update();
        InfiniteLogs.Update();
        NoFallDamage.Update();
        
        if(!Input.anyKey || !Input.anyKeyDown) return;
        
        if (Input.GetKeyDown(Plugin.ModKey.Value))
        {
            UIManager.MenuVisible = !UIManager.MenuVisible;
            
            if (UIManager.MenuVisible)
            {
                InputSystem.SetState(0, true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                return;
            }
            
            if (_playerViewsList.Contains(LocalPlayer.CurrentView))
            {
                InputSystem.SetState(0, false);
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
    }
}