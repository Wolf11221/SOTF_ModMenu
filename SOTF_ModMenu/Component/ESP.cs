using System;
using System.Linq;
using Construction;
using Il2CppSystem.Collections.Generic;
using Sons.Ai.Vail;
using SOTF_ModMenu.Utilities;
using UnityEngine;

namespace SOTF_ModMenu.Component;

internal static class ESP
{
    public static void Enabled()
    {
        //didn't want to check camera for null in each new ESP added so moved to one
        if (Main.MyMonoBehaviour._cameraMain != null)
        {
            Actors();
            Structures();
        }
    }

    public static void Disabled()
    {
        Settings.EspAnimalsEnable = false;
        Settings.EspEnemyEnable = false;
        Settings.EspFriendlyEnable = false;
        Settings.EspStructureDamage = false;
    }

    /// <summary>
    ///     ESP for Damaged Structures
    /// </summary>
    private static void Structures()
    {
        if (Settings.EspStructureDamage)
        {
            if (Main.MyMonoBehaviour._sonsMainScene.isLoaded && Main.MyMonoBehaviour._dirtyStructures == null)
            {
                var gameManagers = Main.MyMonoBehaviour._sonsMainScene.GetRootGameObjects()
                    .FirstOrDefault(ob => ob.name == "GameManagers");
                if (gameManagers != default)
                    Main.MyMonoBehaviour._dirtyStructures = gameManagers
                        .GetComponentInChildren<StructureDestructionManager>()._distortedStructures;
            }

            if (Main.MyMonoBehaviour._sonsMainScene.isLoaded && Main.MyMonoBehaviour._dirtyStructures != null)
                foreach (var s in Main.MyMonoBehaviour._dirtyStructures)
                {
                    if (s == null) continue;
                    var structurePosition = s.transform.position;
                    var worldToScreen = Main.MyMonoBehaviour._cameraMain.WorldToScreenPoint(structurePosition);
                    var reiDistance = Vector3.Distance(Main.MyMonoBehaviour._cameraMain.transform.position,
                        structurePosition);

                    if (worldToScreen.z >= 0f && reiDistance < 250f && Settings.EspStructureDamage)
                    {
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Repair",
                            Color.magenta, 12);
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12),
                            Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                    }
                }
        }
    }

    /// <summary>
    ///     ESP for Actors in world
    /// </summary>
    private static void Actors()
    {
        //get actors from VailActorManager
        List<VailActor> actors;
        try
        {
            actors = VailActorManager._instance._activeActors;
        }
        catch (Exception)
        {
            return;
            // ignored
        }

        if (actors != null)
            foreach (var actor in actors)
            {
                if (!actor || actor._isDead) continue;
                //get player and actors position in world, and distance from each other
                var actorPosition = actor.transform.position;
                var worldToScreen = Main.MyMonoBehaviour._cameraMain.WorldToScreenPoint(actorPosition);
                var reiDistance = Vector3.Distance(Main.MyMonoBehaviour._cameraMain.transform.position, actorPosition);
                //variable to determine if actor was drawn within section, trying to prevent drawing distance when more than one ESP is active
                var drawn = false;
                //check the distance to actor is not too far or too close for unnecessary renders
                if (worldToScreen.z >= 0f && reiDistance < 250f && Settings.EspEnemyEnable)
                {
                    drawn = actor.TypeId switch
                    {
                        VailActorTypeId.Fingers => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fingers", Color.red, 12),
                        VailActorTypeId.Andy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Andy", Color.red, 12),
                        VailActorTypeId.Danny => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Danny", Color.red, 12),
                        VailActorTypeId.Billy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Billy", Color.red, 12),
                        VailActorTypeId.Baby => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Baby", Color.red, 12),
                        VailActorTypeId.Twins => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Twins", Color.red, 12),
                        VailActorTypeId.GoldMask => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Gold Mask", Color.red, 12),
                        VailActorTypeId.Slug => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Slug", Color.red, 12),
                        VailActorTypeId.MuddyFemale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Muddy Female", Color.red,
                            12),
                        VailActorTypeId.MuddyMale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Muddy Male", Color.red, 12),
                        VailActorTypeId.HeavyMale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Heavy Male", Color.red, 12),
                        VailActorTypeId.FatMale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fat Male", Color.red, 12),
                        VailActorTypeId.FatFemale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fat Female", Color.red, 12),
                        VailActorTypeId.John2 => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "John 2.0", Color.red, 12),
                        VailActorTypeId.FacelessMale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Faceless Male", Color.red,
                            12),
                        VailActorTypeId.Demon => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Demon", Color.red, 12),
                        VailActorTypeId.PaintedMale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Painted Male", Color.red,
                            12),
                        VailActorTypeId.PaintedFemale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Painted Female", Color.red,
                            12),
                        VailActorTypeId.Timmy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Timmy", Color.red, 12),
                        VailActorTypeId.Carl => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Carl", Color.red, 12),
                        VailActorTypeId.MrPuffy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Mr. Puffy", Color.red, 12),
                        VailActorTypeId.MissPuffy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Mrs. Puffy", Color.red, 12),
                        VailActorTypeId.Angel => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Angel", Color.red, 12),
                        VailActorTypeId.Brandy => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Brandy", Color.red, 12),
                        VailActorTypeId.Crystal => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Crystal", Color.red, 12),
                        VailActorTypeId.Destiny => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Destiny", Color.red, 12),
                        _ => drawn
                    };

                    if (drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12),
                            Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }

                if (worldToScreen.z >= 0f && Settings.EspFriendlyEnable)
                {
                    drawn = actor.TypeId switch
                    {
                        VailActorTypeId.Robby => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Robby", Color.cyan, 12),
                        VailActorTypeId.Virginia => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Virginia", Color.cyan, 12),
                        _ => false
                    };
                    if (drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12),
                            Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }

                if (worldToScreen.z >= 0f && reiDistance < 250f && Settings.EspAnimalsEnable)
                {
                    drawn = actor.TypeId switch
                    {
                        VailActorTypeId.Rabbit => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Rabbit", Color.green, 12),
                        VailActorTypeId.Squirrel => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Squirrel", Color.green, 12),
                        VailActorTypeId.Turtle => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Turtle", Color.green, 12),
                        VailActorTypeId.Seagull => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Seagull", Color.green, 12),
                        VailActorTypeId.Eagle => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Eagle", Color.green, 12),
                        VailActorTypeId.Duck => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Duck", Color.green, 12),
                        VailActorTypeId.Moose => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Moose", Color.green, 12),
                        VailActorTypeId.Salmon => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Salmon", Color.green, 12),
                        VailActorTypeId.Bat => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Bat", Color.green, 12),
                        VailActorTypeId.Deer => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Deer", Color.green, 12),
                        VailActorTypeId.Bluebird => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Bluebird", Color.green, 12),
                        VailActorTypeId.Hummingbird => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Hummingbird", Color.green,
                            12),
                        VailActorTypeId.LandTurtle => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Land Turtle", Color.green,
                            12),
                        VailActorTypeId.Shark => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Shark", Color.red, 12),
                        VailActorTypeId.KillerWhale => UIHelper.DrawString(
                            new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Killer Whale", Color.red,
                            12),
                        _ => false
                    };
                    if (drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12),
                            Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }
            }
    }
}