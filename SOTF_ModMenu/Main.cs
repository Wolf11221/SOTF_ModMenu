using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Sons.Crafting.Structures;
using Sons.Input;
using Sons.Items.Core;
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

            private void OnGUI()
            {
                if(!Settings.Visible) return;
                
                //Player
                UIHelper.Begin("Player", 10, 10, 150, 197, 2, 20, 2);
                Settings.Health = UIHelper.Button("Max Health: ", Settings.Health);
                Settings.Stamina = UIHelper.Button("Max Stamina: ", Settings.Stamina);
                Settings.Strength = UIHelper.Button("Max Strength: ", Settings.Strength);
                Settings.LungCapacity = UIHelper.Button("Max LungCapacity: ", Settings.LungCapacity);
                Settings.Cold = UIHelper.Button("No Cold: ", Settings.Cold);
                Settings.Hunger = UIHelper.Button("No Hunger: ", Settings.Hunger);
                Settings.Thirst = UIHelper.Button("No Thirst: ", Settings.Thirst);
                Settings.Rested = UIHelper.Button("Always Rested: ", Settings.Rested);

                //World
                UIHelper.Begin("World", 165, 10, 150, 100, 2, 20, 2);
                Settings.InstantBuild = UIHelper.Button("Instant Build: ", Settings.InstantBuild);  

                //Item Spawner
                UIHelper.Begin("Item Spawner", 10, 212, 150, 85, 2, 20, 2);
                UIHelper.Label("Enter id & amount");
                Settings.TextFieldItemID = GUI.TextField(new Rect(12, 252, 40, 20), Settings.TextFieldItemID);
                Settings.TextFieldAmount = GUI.TextField(new Rect(55, 252, 30, 20), Settings.TextFieldAmount);
                if (GUI.Button(new Rect(88, 252, 70, 20), "Spawn"))
                    SpawnItem();
                if (GUI.Button(new Rect(12, 274, 146, 20), "Show All ID's"))
                    Settings.ShowItemIDs = !Settings.ShowItemIDs;

                
                GUI.backgroundColor=Color.grey;
                //Show IDs Window
                if (Settings.ShowItemIDs)
                {
                    windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ShowAllIDsWindow, "Show ID's");
                }
            }

            private void Update()
            {
                RegisterHandlers();

                if(vitals == null)
                {
                    vitals = FindObjectOfType<Vitals>();
                }

                if (!LocalPlayer.IsInWorld) return;
                
                //Vitals
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
                StructureCraftingSystem scs = LocalPlayer.StructureCraftingSystem;
                scs.InstantBuild = Settings.InstantBuild;

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
                foreach (GameObject gObject in SceneManager.GetSceneByName("SonsMain").GetRootGameObjects())
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
            private Rect windowRect = new Rect(10, 305, 300, 500); // 20, 25
            public void ShowAllIDsWindow (int windowID)
            {
                sonsMainScene = SceneManager.GetSceneByName("SonsMain");
                if (sonsMainScene.isLoaded)
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

            private Scene sonsMainScene;
            private Vitals vitals;
        }
    }
}
