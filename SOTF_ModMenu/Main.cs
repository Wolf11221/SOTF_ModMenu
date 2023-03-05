using Sons.Crafting.Structures;
using Sons.Input;
using Sons.Items.Core;
using Sons.StatSystem;
using UnityEngine;
using TheForest.Utils;
using SOTF_ModMenu.Utilities;

namespace SOTF_ModMenu
{
    public class Main
    {
        public class MyMonoBehaviour : MonoBehaviour
        {
            private string TextFieldItemID = "392";
            private string TextFieldAmount = "1";

            private void OnGUI()
            {
                if(!Settings.Visible) return;
                
                //Vitals
                UIHelper.Begin("Vitals", 10, 10, 150, 197, 2, 20, 2);
                if (UIHelper.Button("Max Health: ", Settings.Health))
                    Settings.Health = !Settings.Health;
                if (UIHelper.Button("Max Stamina: ", Settings.Stamina))
                    Settings.Stamina = !Settings.Stamina;
                if (UIHelper.Button("Max Strength: ", Settings.Strength))
                    Settings.Strength = !Settings.Strength;
                if (UIHelper.Button("Max LungCapacity: ", Settings.LungCapacity))
                    Settings.LungCapacity = !Settings.LungCapacity;
                if (UIHelper.Button("No Cold: ", Settings.Cold))
                    Settings.Cold = !Settings.Cold;
                if (UIHelper.Button("No Hunger: ", Settings.Hunger))
                    Settings.Hunger = !Settings.Hunger;
                if (UIHelper.Button("No Thirst: ", Settings.Thirst))
                    Settings.Thirst = !Settings.Thirst;
                if (UIHelper.Button("Always Rested: ", Settings.Rested))
                    Settings.Rested = !Settings.Rested;

                //World
                UIHelper.Begin("World", 165, 10, 150, 100, 2, 20, 2);
                if (UIHelper.Button("Instant Build: ", Settings.InstantBuild))
                    Settings.InstantBuild = !Settings.InstantBuild;
                
                //Item Spawner
                UIHelper.Begin("Item Spawner", 10, 212, 150, 85, 2, 20, 2); //190
                UIHelper.Label("Enter id & amount");
                TextFieldItemID = GUI.TextField(new Rect(12, 252, 40, 20), TextFieldItemID);
                TextFieldAmount = GUI.TextField(new Rect(55, 252, 30, 20), TextFieldAmount);
                if (GUI.Button(new Rect(88, 252, 70, 20), "Spawn"))
                {
                    try
                    {
                        int itemID = int.Parse(TextFieldItemID);
                        int amount = int.Parse(TextFieldAmount);
                        LocalPlayer.Inventory.AddItem(itemID, amount);
                    }
                    catch
                    {
                        Plugin.log.LogError("Failed to add item!");
                    }
                }
                GUI.Button(new Rect(12, 274, 146, 20), "Show All ID's (Soon!)");
            }

            private void Update()
            {
                ShowMenu();

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
            
            private void ShowMenu()
            {
                if (Input.GetKeyDown(Plugin.modmenuKeybind.Value))
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
            
            private Vitals vitals;
        }
    }
}
