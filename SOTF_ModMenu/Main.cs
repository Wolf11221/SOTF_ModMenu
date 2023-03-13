using Construction;
using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Sons.Input;
using Sons.Items.Core;
using SOTF_ModMenu.Component;
using UnityEngine;
using TheForest.Utils;
using SOTF_ModMenu.Utilities;
using UnityEngine.SceneManagement;

namespace SOTF_ModMenu
{
    public class Main
    {
        public class MyMonoBehaviour : MonoBehaviour
        {
            static List<ItemData> itemList;
            static bool isInitialized = false;
            private Vector2 scrollPosition = Vector2.zero;
            public static Camera _cameraMain;
            public static HashSet<Structure> _dirtyStructures;
            public static Scene _sonsMainScene;
            private Vitals vitals;

            private void OnGUI()
            {
                
                //ESP Draw, placed before check for cheat visible to prevent the ESP from not rendering when GUI not visible
                if (Settings.EspEnable)
                    ESP.Enabled();
                else
                    ESP.Disabled();

                if(!Settings.Visible) return;
                GUI.color = Color.white;
                
                //Player
                UIHelper.Begin("Player", 10, 10, 165, 241, 2, 20, 2);
                Settings.Health = UIHelper.Button("Max Health: ", Settings.Health);
                Settings.Stamina = UIHelper.Button("Max Stamina: ", Settings.Stamina);
                Settings.Strength = UIHelper.Button("Max Strength: ", Settings.Strength);
                Settings.LungCapacity = UIHelper.Button("Max LungCapacity: ", Settings.LungCapacity);
                Settings.Cold = UIHelper.Button("No Cold: ", Settings.Cold);
                Settings.Hunger = UIHelper.Button("No Hunger: ", Settings.Hunger);
                Settings.Thirst = UIHelper.Button("No Thirst: ", Settings.Thirst);
                Settings.Rested = UIHelper.Button("Always Rested: ", Settings.Rested);
                Settings.InfAmmo = UIHelper.Button("Infinite Ammo: ", Settings.InfAmmo);
                Settings.SpeedyRun = UIHelper.Button("SpeedRun: ", Settings.SpeedyRun);

                //World
                UIHelper.Begin("World", 180, 10, 165, 100, 2, 20, 2);
                Settings.InstantBuild = UIHelper.Button("Instant Build: ", Settings.InstantBuild);  
                Settings.InfBuild = UIHelper.Button("Infinite Build: ", Settings.InfBuild);  
                Settings.CaveLight = UIHelper.Button("Cave Light: ", Settings.CaveLight);  
                
                //ESP
                UIHelper.Begin("ESP", 350, 10, 165, 130, 2, 20, 2);
                Settings.EspEnable = UIHelper.Button("Enabled: ", Settings.EspEnable);
                Settings.EspAnimalsEnable = UIHelper.Button("Animals: ", Settings.EspAnimalsEnable);
                Settings.EspEnemyEnable = UIHelper.Button("Enemies: ", Settings.EspEnemyEnable);
                Settings.EspFriendlyEnable = UIHelper.Button("Friendly: ", Settings.EspFriendlyEnable);
                Settings.EspStructureDamage = UIHelper.Button("Structure Damage: ", Settings.EspStructureDamage);
                
                //Other
                UIHelper.Begin("Other", 520, 10, 165, 100, 2, 20, 2);
                Settings.InfLogs = UIHelper.Button("Infinite Logs: ", Settings.InfLogs);
                //Settings.FreeCam = UIHelper.Button("FreeCam Enabled: ", Settings.FreeCam);

                //Item Spawner
                UIHelper.Begin("Item Spawner", 10, 256, 165, 84, 2, 20, 2);
                UIHelper.Label("Enter id & amount");
                Settings.TextFieldItemID = GUI.TextField(new Rect(12, 292, 40, 20), Settings.TextFieldItemID);
                Settings.TextFieldAmount = GUI.TextField(new Rect(55, 292, 30, 20), Settings.TextFieldAmount);
                if (GUI.Button(new Rect(88, 292, 85, 20), "Spawn"))
                    SpawnItem();
                if (GUI.Button(new Rect(12, 314, 161, 20), "Show All ID's"))
                    Settings.ShowItemIDs = !Settings.ShowItemIDs;
                
                GUI.backgroundColor = Color.grey;
                //Show IDs Window
                if (Settings.ShowItemIDs)
                    windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ShowAllIDsWindow, "Show ID's");
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
                if(Settings.InfLogs) CPlayer.InfLogs();

                //InfAmmo
                if(Settings.InfAmmo) CPlayer.InfAmmo();
                
                //SpeedyRun
                CPlayer.SpeedyRun();
                
                //if(Settings.CaveLight)
                CPlayer.CaveLight(Settings.CaveLight);

                //Vitals
                if(vitals == null) vitals = LocalPlayer.Vitals;
                
                if (Settings.Health) {
                    vitals._health._currentValue = vitals._health._max;
                    LocalPlayer.FpCharacter.allowFallDamage = false;
                }
                if (Settings.Stamina)
                    vitals._stamina._currentValue = vitals._stamina._max;
                if (Settings.Cold){
                    vitals._temperature._currentValue = vitals._temperature._max;
                    vitals._temperature._baseValue = vitals._temperature._max;
                    vitals._isCold = Settings.Cold;
                }
                if (Settings.Hunger)
                    vitals._fullness._currentValue = vitals._fullness._max;
                if (Settings.Thirst)
                    vitals._hydration._currentValue = vitals._hydration._max;
                if (Settings.Rested)
                    vitals._rested._currentValue = vitals._rested._max;
                if (Settings.Strength)
                    vitals._strength._currentValue = vitals._strength._max;
                if (Settings.LungCapacity)
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
                    Settings.Visible = !Settings.Visible;
                    if(Settings.Visible)
                    {
                        InputSystem.SetState(0, true);
                        Cursor.visible = true;
                        Cursor.lockState = CursorLockMode.None;
                        return;
                    }
                    if (LocalPlayer.IsInWorld || LocalPlayer.IsInInventory || LocalPlayer.IsConstructing || LocalPlayer.IsInMidAction)
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
            
            private void SpawnItem()
            {
                try
                {
                    int itemID = int.Parse(Settings.TextFieldItemID);
                    int amount = int.Parse(Settings.TextFieldAmount);
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

            // Add search, categories
            // Optimize this
            private Rect windowRect = new Rect(10, 345, 300, 500);
            public void ShowAllIDsWindow (int windowID)
            {
                if (_sonsMainScene.isLoaded)
                {
                    if (!isInitialized)
                    {
                        itemList ??= ItemDatabaseManager.Items;
                        isInitialized = true;
                    }
                }

                if (itemList == null || itemList.Count == 0)
                {
                    GUI.Label(new Rect(5, 15, 300, 500), "Item list is empty.");
                }
                else
                {
                    var writer = new StringWriter();
                    foreach (ItemData item in itemList)
                    {
                        writer.Write(item._name);
                        writer.Write(" : ");
                        writer.WriteLine(item._id);
                    }

                    GUILayout.BeginArea(new Rect(5, 15, 295, 480));
                    scrollPosition = GUILayout.BeginScrollView(scrollPosition);
                    GUILayout.BeginVertical();
                    GUILayout.Label(writer.ToString());
                    GUILayout.EndVertical();
                    GUILayout.EndScrollView();
                    GUILayout.EndArea();
                }
                
                GUI.DragWindow(new Rect(0, 0, 10000, 20));
            }
        }
    }
}
