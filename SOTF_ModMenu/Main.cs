using Construction;
using Il2CppSystem.Collections.Generic;
using Sons.Input;
using SOTF_ModMenu.Cheats.Other;
using SOTF_ModMenu.Cheats.Player;
using SOTF_ModMenu.Cheats.World;
using SOTF_ModMenu.UI;
using SOTF_ModMenu.Utilities;
using TheForest.Items.Inventory;
using TheForest.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SOTF_ModMenu
{
    public class Main
    {
        public class MyMonoBehaviour : MonoBehaviour
        {
            public static Camera CameraMain;
            public static HashSet<Structure> DirtyStructures;
            public static Scene SonsMainScene;

            private List<PlayerInventory.PlayerViews> _playerViews = new();
            
            public void Start()
            {
                foreach (Cheat cheat in Cheat.Cheats)
                {
                    cheat.Start();
                }
                
                _playerViews.Add(PlayerInventory.PlayerViews.PlaneCrash);
                _playerViews.Add(PlayerInventory.PlayerViews.WakingUp);
                _playerViews.Add(PlayerInventory.PlayerViews.Inventory);
                _playerViews.Add(PlayerInventory.PlayerViews.World);
                _playerViews.Add(PlayerInventory.PlayerViews.Sleep);
                _playerViews.Add(PlayerInventory.PlayerViews.GrabBag);
            }

            public void Awake()
            {
                // Automate this if possible 
                
                // Player
                Cheat.Cheats.Add(new MaxHealth());
                Cheat.Cheats.Add(new MaxStamina());
                Cheat.Cheats.Add(new InfLungCapacity());
                Cheat.Cheats.Add(new NoCold());
                Cheat.Cheats.Add(new NoHunger());
                Cheat.Cheats.Add(new NoThirst());
                Cheat.Cheats.Add(new AlwaysRested());
                Cheat.Cheats.Add(new InfAmmo());
                Cheat.Cheats.Add(new SpeedyRun());
                
                // orld
                Cheat.Cheats.Add(new InstantBuild());
                Cheat.Cheats.Add(new InfiniteBuild());
                Cheat.Cheats.Add(new CaveLight());
                
                // Other
                Cheat.Cheats.Add(new InfiniteLogs());
            }
            
            private void OnGUI()
            {
                UIManager.Display();
            }

            private void Update()
            {
                foreach (Cheat cheat in Cheat.Cheats)
                {
                    cheat.Update();
                }
                
                RegisterHandlers();
                
                //cache SonsMainScene
                if(!SonsMainScene.isLoaded) SonsMainScene = SceneManager.GetSceneByName("SonsMain");
                
                //Cache CameraMain
                if(CameraMain == null) CameraMain = Camera.main ?? null;
            }
            
            private void RegisterHandlers()
            {
                ShowMenu();
                SpawnItemHotkeyPressed();
            }

            private void ShowMenu()
            {
                if (Input.GetKeyDown(Plugin.ModMenuKeybind.Value))
                {
                    Settings.MenuVisible = !Settings.MenuVisible;
                    if(Settings.MenuVisible)
                    {
                        InputSystem.SetState(0, true);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        return;
                    }
                    if (_playerViews.Contains(LocalPlayer.CurrentView))
                    {
                        InputSystem.SetState(0, false);
                        Cursor.visible = false;
                        Cursor.lockState = CursorLockMode.Locked;
                    }
                }
            }
            
            private void SpawnItemHotkeyPressed()
            {
                if (Input.GetKeyDown(Plugin.SpawnItemKeybind.Value))
                {
                    SpawnItem();
                }
            }

            public static void SpawnItem()
            {
                try
                {
                    int itemID = int.Parse(Settings.ItemIDTextField);
                    int amount = int.Parse(Settings.AmountTextField);
                    LocalPlayer.Inventory.AddItem(itemID, amount);
                }
                catch
                {
                    Plugin.log.LogError("Failed to add item!");
                }
            }
        }
    }
}
