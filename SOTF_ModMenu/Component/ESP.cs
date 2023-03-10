using System;
using SOTF_ModMenu.Utilities;
using Sons.Ai.Vail;
using UnityEngine;

namespace SOTF_ModMenu.Component;

internal static class ESP
{
    public static void Enabled()
    {
        //get actors from VailActorManager
        Il2CppSystem.Collections.Generic.List<VailActor> actors;
        try
        {
            actors = VailActorManager._instance._activeActors;
        }
        catch (Exception)
        {
            return;
            // ignored
        }
        foreach (var actor in actors)
        {
            if (!actor) continue;
            if (Main.MyMonoBehaviour._cameraMain != null)
            {
                //get player and actors position in world, and distance from each other
                var actorPosition = actor.transform.position;
                var worldToScreen = Main.MyMonoBehaviour._cameraMain.WorldToScreenPoint(actorPosition);
                var reiDistance = Vector3.Distance(Main.MyMonoBehaviour._cameraMain.transform.position, actorPosition);
                //variable to determine if actor was drawn within section, trying to prevent drawing distance when more than one ESP is active
                bool drawn = false;
                //check the distance to actor is not too far or too close for unnecessary renders
                if (worldToScreen.z >= 0f && reiDistance < 250f && Settings.EspEnemyEnable)
                {
                    if (actor.TypeId == VailActorTypeId.Fingers)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fingers", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Andy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Andy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Danny)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Danny", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Billy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Billy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Baby)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Baby", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Twins)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Twins", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.GoldMask)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Gold Mask", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Slug)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Slug", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.MuddyFemale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Muddy Female", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.MuddyMale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Muddy Male", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.HeavyMale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Heavy Male", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.FatMale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fat Male", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.FatFemale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Fat Female", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.John2)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "John 2.0", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.FacelessMale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Faceless Male", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Demon)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Demon", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.PaintedMale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Painted Male", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.PaintedFemale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Painted Female", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Timmy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Timmy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Carl)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Carl", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.MrPuffy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Mr. Puffy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.MissPuffy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Mrs. Puffy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Angel)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Angel", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Brandy)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Brandy", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Crystal)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Crystal", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.Destiny)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Destiny", Color.red, 12);
                    if (drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12), Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }
                if (worldToScreen.z >= 0f && Settings.EspFriendlyEnable)
                {
                    drawn = false;
                    if (actor.TypeId == VailActorTypeId.Robby)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Robby", Color.cyan, 12);
                    if (actor.TypeId == VailActorTypeId.Virginia)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Virginia", Color.cyan, 12);
                    if(drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12), Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }
                if (worldToScreen.z >= 0f && reiDistance < 250f && Settings.EspAnimalsEnable)
                {
                    drawn = false;
                    if (actor.TypeId == VailActorTypeId.Rabbit)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Rabbit", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Squirrel)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Squirrel", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Turtle)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Turtle", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Seagull)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Seagull", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Eagle)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Eagle", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Duck)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Duck", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Moose)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Moose", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Salmon)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Salmon", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Bat)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Bat", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Deer)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Deer", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Bluebird)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Bluebird", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Hummingbird)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Hummingbird", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.LandTurtle)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Land Turtle", Color.green, 12);
                    if (actor.TypeId == VailActorTypeId.Shark)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Shark", Color.red, 12);
                    if (actor.TypeId == VailActorTypeId.KillerWhale)
                        drawn = UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y), "Killer Whale", Color.red, 12);
                    if(drawn)
                        UIHelper.DrawString(new Vector2(worldToScreen.x, Screen.height - worldToScreen.y + 12), Mathf.Round(reiDistance) + "m", Color.yellow, 12);
                }
            }
        }
    }
}