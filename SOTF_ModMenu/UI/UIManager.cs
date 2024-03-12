using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Sons.Input;
using Sons.Items.Core;
using SOTF_ModMenu.Cheats.ESP;
using SOTF_ModMenu.Cheats.Other;
using SOTF_ModMenu.Cheats.Player;
using SOTF_ModMenu.Cheats.World;
using TheForest;
using TheForest.Utils;
using UnityEngine;

namespace SOTF_ModMenu.UI;

public class UIManager
{
    public static bool MenuVisible;
    public static bool IdsMenuVisible;
    
    public static void Display()
    {
        //ESP Draw, placed before check for cheat visible to prevent the ESP from not rendering when GUI not visible
        if (ESP.Enabled)
        {
            ESP.Enable();
        }
        else
        {
            ESP.Disable();
        }
        
        if(!MenuVisible) return;
        
        GUI.color = Color.white;
        
        // Player
        UIHelper.Begin("Player", 10, 10, 150, 272, 0, 22, 1);
        UIHelper.Button("God Mode", GodMode.Enabled, GodMode.Toggle);
        UIHelper.Button("Inf Heal", InfHeal.Enabled, InfHeal.Toggle);
        UIHelper.Button("Inf Stamina", InfStamina.Enabled, InfStamina.Toggle);
        UIHelper.Button("Inf Lung Capacity", InfLungCapacity.Enabled, InfLungCapacity.Toggle);
        UIHelper.Button("No Fall Damage", NoFallDamage.Enabled, NoFallDamage.Toggle);
        UIHelper.Button("No Cold", NoCold.Enabled, NoCold.Toggle);
        UIHelper.Button("No Hunger", NoHunger.Enabled, NoHunger.Toggle);
        UIHelper.Button("No Thirst", NoThirst.Enabled, NoThirst.Toggle);
        UIHelper.Button("Always Rested", AlwaysRested.Enabled, AlwaysRested.Toggle);
        UIHelper.Button("Infinite Ammo <color=yellow><b>[Broken]</b></color>", InfAmmo.Enabled, InfAmmo.Toggle);
        UIHelper.Button("SpeedRun", SpeedyRun.Enabled, SpeedyRun.Toggle);
        
        // World
        UIHelper.Begin("World", 165, 10, 165, 88, 0, 22, 1);
        UIHelper.Button("Instant Build", InstantBuild.Enabled, InstantBuild.Toggle);
        UIHelper.Button("Infinite Build", InfiniteBuild.Enabled, InfiniteBuild.Toggle);
        UIHelper.Button("Cave Light", CaveLight.Enabled, CaveLight.Toggle);

        // ESP
        UIHelper.Begin("ESP", 335, 10, 165, 134, 0, 22, 1);
        UIHelper.Button("Enable", ESP.Enabled, ESP.ToggleEnabled );
        UIHelper.Button("Animals", ESP.AnimalsEsp, ESP.ToggleAnimalsEsp );
        UIHelper.Button("Enemies", ESP.EnemyEsp, ESP.ToggleEnemiesEsp );
        UIHelper.Button("Friendly", ESP.FriendlyEsp, ESP.ToggleFriendlyEsp );
        UIHelper.Button("Structure Damage", ESP.StructureDamageEsp, ESP.ToggleStructureDamageEsp );
        
        // Other
        UIHelper.Begin("Other", 505, 10, 165, 88, 0, 22, 1);
        UIHelper.Button("OG Menu Music", OgMenuMusic.Enabled, OgMenuMusic.Toggle);
        UIHelper.Button("Infinite Logs <color=yellow><b>[Broken]</b></color>", InfiniteLogs.Enabled, InfiniteLogs.Toggle);
        if (UIHelper.Button("Fix Stuck Mouse"))
        {
            InputSystem.SetState(0, false);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            MenuVisible = false;
        }
            
        ItemSpawnerWindow();

        if (IdsMenuVisible)
        {
            AllIdsWindow();
        }
        
        GUI.color = Color.gray;
    }
    
    private static string _itemIdInput = "392";
    private static string _amountInput = "1";
    private static void ItemSpawnerWindow()
    {
        UIHelper.Begin("Item Spawner", 165, 103, 165, 83, 2, 20, 2);
        UIHelper.Label("Enter <b>id</b> and <b>amount</b>");
        
        _itemIdInput = GUI.TextField(new Rect(167, 142, 40, 20), _itemIdInput, UIHelper.TextFieldStyle);
        _amountInput = GUI.TextField(new Rect(209, 142, 30, 20), _amountInput, UIHelper.TextFieldStyle);
        
        if (GUI.Button(new Rect(241, 142, 87, 20), "Spawn", UIHelper.ButtonStyle))
        {
            if(!LocalPlayer.IsInWorld) return;
            if (int.TryParse(_itemIdInput, out int itemId) && int.TryParse(_amountInput, out int amount)) {
                LocalPlayer._instance.AddItem(itemId, amount, true);
            } else {
                Plugin.log.LogError($"Failed to add item with id {itemId}");
            }
        }
        GUI.Label(new Rect(324, 142 + 3f, 3f, 20 - 6f), GUIContent.none, UIHelper.PanelStyle);

        if (GUI.Button(new Rect(167, 164, 161, 20), "Show all ID's", UIHelper.SpawnerButtonStyle))
        {
            IdsMenuVisible = !IdsMenuVisible;
        }
    }
    
    private static Vector2 _scrollPosition = Vector2.zero;
    private static List<ItemData> _itemList;
    private static string _searchQuery = "";
    private static bool _initialized;
    
    private static void AllIdsWindow()
    {
        UIHelper.Begin("All Ids", 165, 191, 300, 500, 0, 22, 2);
        if (LocalPlayer.IsInWorld)
        {
            if (!_initialized)
            {
                _initialized = true;
                _itemList ??= ItemDatabaseManager.Items;
            }
        }

        var text = _searchQuery.Length == 0 ? "Search" : "";
        
        GUILayout.BeginArea(new Rect(166, 215, 295, 470));
        
        _searchQuery = GUILayout.TextField(_searchQuery, UIHelper.TextFieldStyle);
        GUI.Label(new Rect(9, 1, 60, 30), text);
        
        if (_itemList == null || _itemList.Count == 0)
        {
            GUI.Label(new Rect(0, 22, 300, 500), "Item list is empty load a save to populate the list");
        }
        else
        {
            var writer = new StringWriter();
            foreach (ItemData item in _itemList)
            {
                if (item._name.ToLower().Contains(_searchQuery.ToLower()))
                {
                    writer.Write(item._name);
                    writer.Write(" : ");
                    writer.WriteLine(item._id);
                }
            }
            
            _scrollPosition = GUILayout.BeginScrollView(_scrollPosition);
            GUILayout.BeginVertical();
            GUILayout.Label(writer.ToString());
            GUILayout.EndVertical();
            GUILayout.EndScrollView();
        }
        
        GUILayout.EndArea();
    }
}