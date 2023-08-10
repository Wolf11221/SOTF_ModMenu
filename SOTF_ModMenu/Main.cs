using Construction;
using Il2CppSystem.Collections.Generic;
using Sons.Input;
using SOTF_ModMenu.Component;
using UnityEngine;
using TheForest.Utils;
using SOTF_ModMenu.Utilities;
using TheForest.Items.Inventory;
using UnityEngine.SceneManagement;

namespace SOTF_ModMenu
{
    public class Main
    {
        public class MyMonoBehaviour : MonoBehaviour
        {
            public static Camera _cameraMain;
            public static HashSet<Structure> _dirtyStructures;
            public static Scene _sonsMainScene;
            private Vitals vitals;

            private void OnGUI()
            {
                UI.UIManager.Display();
            }

            private void Update()
            {
                RegisterHandlers();
                
                //cache SonsMainScene
                if(!_sonsMainScene.isLoaded) _sonsMainScene = SceneManager.GetSceneByName("SonsMain");
                //Cache CameraMain
                if(_cameraMain == null) _cameraMain = Camera.main ?? null;
                
                //player in world
                if (!LocalPlayer.IsInWorld) return;

                //InfLogs
                CPlayer.InfLogs();

                //InfAmmo
                CPlayer.InfAmmo();
                
                //SpeedyRun
                CPlayer.SpeedyRun();
                
                //if(Settings.CaveLight)
                //CPlayer.CaveLight();
                CPlayer.CaveLight(Settings.CaveLight);

                //Vitals
                if(vitals == null) vitals = LocalPlayer.Vitals;
                
                if (Settings.MaxHealth) {
                    vitals._health._currentValue = vitals._health._max;
                    LocalPlayer.FpCharacter._allowFallDamage = false;
                }
                if (Settings.MaxStamina)
                    vitals._stamina._currentValue = vitals._stamina._max;
                
                LocalPlayer.Stats.InteriorSpaceWarmth = Settings.NoCold;
                
                if (Settings.NoHunger)
                    vitals._fullness._currentValue = vitals._fullness._max;
                if (Settings.NoThirst)
                    vitals._hydration._currentValue = vitals._hydration._max;
                if (Settings.AlwaysRested)
                    vitals._rested._currentValue = vitals._rested._max;
                if (Settings.MaxStrength)
                    vitals._strength._currentValue = vitals._strength._max;
                if (Settings.InfLungCapacity)
                    vitals.LungBreathing.CurrentLungAir = vitals.LungBreathing.MaxLungAirCapacity;
                
                //World
                LocalPlayer.StructureCraftingSystem.InstantBuild = Settings.InstantBuild;
                
                //replenishes items being placed in world. ie. logs, sticks. anything placed with building mechanic
                LocalPlayer.Inventory.HeldOnlyItemController.InfiniteHack = Settings.InfBuild;
            }
            
            private void RegisterHandlers()
            {
                ShowMenu();
                SpawnItemHotkeyPressed();
                HideHUDHotkeyPressed();
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
                    if (LocalPlayer.IsInWorld || LocalPlayer.IsInInventory || LocalPlayer.IsConstructing || LocalPlayer.IsInMidAction || LocalPlayer.CurrentView == PlayerInventory.PlayerViews.Hidden)
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
            private void HideHUDHotkeyPressed()
            {
                if (Input.GetKeyDown(Plugin.HideHUDKeybind.Value))
                {
                    HideHUD();
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

            private void HideHUD()
            {
                Settings.HideHUD = !Settings.HideHUD;
                foreach (GameObject gObject in _sonsMainScene.GetRootGameObjects())
                {
                    if (gObject.name == "PlayerStandin")
                    {
                        gObject.SetActive(Settings.HideHUD);
                        break;
                    }
                }
            }
        }
    }
}
