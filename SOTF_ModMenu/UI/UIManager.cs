using Il2CppSystem.Collections.Generic;
using Il2CppSystem.IO;
using Sons.Items.Core;
using SOTF_ModMenu.Cheats.ESP;
using SOTF_ModMenu.Utilities;
using UnityEngine;
using static SOTF_ModMenu.Main.MyMonoBehaviour;

namespace SOTF_ModMenu.UI;

public class UIManager
{
    static List<ItemData> itemList;
    private static string searchQuery = "";
    static bool isInitialized = false;
    private static Vector2 scrollPosition = Vector2.zero;
    private static Rect windowRect = new Rect(10, 345, 300, 500);
    
    public static void Display()
    {
        //ESP Draw, placed before check for cheat visible to prevent the ESP from not rendering when GUI not visible
        if (Settings.EspEnable)
            ESP.Enabled();
        else 
            ESP.Disabled();
        
        if(!Settings.MenuVisible) return;
        GUI.color = Color.white;
        
        PlayerWindow();
        WorldWindow();
        ESPWindow();
        ItemSpawnerWindow();
        OtherWindow();
        
        GUI.color = Color.gray;
        
        if (Settings.ShowItemIDs)
            ShowAllIDsWindowToggle();
    }

    private static void PlayerWindow()
    {
        UIHelper.Begin("Player", 10, 10, 165, 241, 2, 20, 2);
        Settings.MaxHealth = UIHelper.Button("Max Health: ", Settings.MaxHealth);
        Settings.MaxStamina = UIHelper.Button("Max Stamina: ", Settings.MaxStamina);
        Settings.MaxStrength = UIHelper.Button("Max Strength: ", Settings.MaxStrength);
        Settings.InfLungCapacity = UIHelper.Button("Max LungCapacity: ", Settings.InfLungCapacity);
        Settings.NoCold = UIHelper.Button("No Cold: ", Settings.NoCold);
        Settings.NoHunger = UIHelper.Button("No Hunger: ", Settings.NoHunger);
        Settings.NoThirst = UIHelper.Button("No Thirst: ", Settings.NoThirst);
        Settings.AlwaysRested = UIHelper.Button("Always Rested: ", Settings.AlwaysRested);
        Settings.InfAmmo = UIHelper.Button("Infinite Ammo: ", Settings.InfAmmo);
        Settings.SpeedyRun = UIHelper.Button("SpeedRun: ", Settings.SpeedyRun);
    }

    private static void WorldWindow()
    {
        UIHelper.Begin("World", 180, 10, 165, 100, 2, 20, 2);
        Settings.InstantBuild = UIHelper.Button("Instant Build: ", Settings.InstantBuild);  
        Settings.InfBuild = UIHelper.Button("Infinite Build: ", Settings.InfBuild);  
        Settings.CaveLight = UIHelper.Button("Cave Light: ", Settings.CaveLight);  
    }

    private static void ESPWindow()
    {
        UIHelper.Begin("ESP", 350, 10, 165, 130, 2, 20, 2);
        Settings.EspEnable = UIHelper.Button("Enabled: ", Settings.EspEnable);
        Settings.EspAnimalsEnable = UIHelper.Button("Animals: ", Settings.EspAnimalsEnable);
        Settings.EspEnemyEnable = UIHelper.Button("Enemies: ", Settings.EspEnemyEnable);
        Settings.EspFriendlyEnable = UIHelper.Button("Friendly: ", Settings.EspFriendlyEnable);
        Settings.EspStructureDamage = UIHelper.Button("Structure Damage: ", Settings.EspStructureDamage);
    }

    private static void ItemSpawnerWindow()
    {
        UIHelper.Begin("Item Spawner", 10, 256, 165, 84, 2, 20, 2);
        UIHelper.Label("Enter id & amount");
        Settings.ItemIDTextField = GUI.TextField(new Rect(12, 292, 40, 20), Settings.ItemIDTextField);
        Settings.AmountTextField = GUI.TextField(new Rect(55, 292, 30, 20), Settings.AmountTextField);
        if (GUI.Button(new Rect(88, 292, 85, 20), "Spawn"))
            SpawnItem();
        if (GUI.Button(new Rect(12, 314, 161, 20), "Show All ID's"))
            Settings.ShowItemIDs = !Settings.ShowItemIDs;
    }

    private static void OtherWindow()
    {
        UIHelper.Begin("Other", 520, 10, 165, 100, 2, 20, 2);
        Settings.InfLogs = UIHelper.Button("Infinite Logs: ", Settings.InfLogs);
    }

    private static void ShowAllIDsWindowToggle()
    {
        windowRect = GUI.Window(0, windowRect, (GUI.WindowFunction)ShowAllIDsWindow, "Show ID's");
    }
    private static void ShowAllIDsWindow(int windowID)
    {
        if (SonsMainScene.isLoaded)
        {
            if (!isInitialized)
            {
                itemList ??= ItemDatabaseManager.Items;
                isInitialized = true;
            }
        }

        var text = searchQuery.Length == 0 ? "Search" : "";
        GUI.Label(new Rect(15, 20, 300, 500), text);
        
        searchQuery = GUILayout.TextField(searchQuery);

        if (itemList == null || itemList.Count == 0)
        {
            GUI.Label(new Rect(5, 40, 300, 500), "Item list is empty.");
        }
        else
        {
            var writer = new StringWriter();
            foreach (ItemData item in itemList)
            {
                if (item._name.ToLower().Contains(searchQuery.ToLower()))
                {
                    writer.Write(item._name);
                    writer.Write(" : ");
                    writer.WriteLine(item._id);
                }
            }

            GUILayout.BeginArea(new Rect(5, 43, 295, 455));
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