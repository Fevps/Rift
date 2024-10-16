﻿using BepInEx;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GorillaTelemetry;
using static StupidTemplate.Settings;
using static StupidTemplate.Menu.Buttons;
using static StupidTemplate.Menu.Optimization;

using static StupidTemplate.Menu.Mods.Advantages;
using static StupidTemplate.Menu.Mods.Current;
using static StupidTemplate.Menu.Mods.Fun;
using static StupidTemplate.Menu.Mods.Movement;
using static StupidTemplate.Menu.Mods.Visuals;
using static StupidTemplate.Menu.Mods.Safety;

using static StupidTemplate.Menu.GunLib;

using static StupidTemplate.Menu.Main;
using static StupidTemplate.Menu.Mods.Settings;

using UnityEngine.XR;
using StupidTemplate.Menu.Mods;
using System.Net;
using TMPro;
using GorillaNetworking;
using System.Diagnostics;
using GorillaTagScripts;
using System.IO;
using Photon.Realtime;
using Unity.XR.CoreUtils;
using System.Collections;

namespace StupidTemplate.Menu
{
    public class Buttons
    {
        /// Method, Invokes action and continues for aslong as you keep,
        /// EnableMethod, Invokes action once [count's as enabled] 
        /// DisableMethod, Invokes action once toggled off
        /// this has made it easier to fix mods that broke for no reason
        public static ButtonInfo[][] buttons = new ButtonInfo[][]
        {
            new ButtonInfo[] { // [0] Home
                new ButtonInfo { buttonText = "Settings", isTogglable = false, method =() => Category(1)},
                new ButtonInfo { buttonText = "Current", isTogglable = false, method =() => Category(2)},
                new ButtonInfo { buttonText = "Fun", isTogglable = false, method =() => Category(3)},
                new ButtonInfo { buttonText = "Movement", isTogglable = false, method =() => Category(4)},
                new ButtonInfo { buttonText = "Visuals", isTogglable = false, method =() => Category(5)},
                new ButtonInfo { buttonText = "Advantages", isTogglable = false, method =() => Category(6)},
                new ButtonInfo { buttonText = "Safety", isTogglable = false, method =() => Category(7)},
            },

            new ButtonInfo[] { // [1] Settings
                new ButtonInfo { buttonText = "<color=lime>Save</color> Preferences", isTogglable = false, method =() => SavePreferences()},
                new ButtonInfo { buttonText = "<color=lime>Load</color> Preferences", isTogglable = false, method =() => LoadPreferences()},
                new ButtonInfo { buttonText = "<color=red>Reset</color> Preferences", isTogglable = false, method =() => ResetPreferences()},

                new ButtonInfo { buttonText = "Panic", isTogglable = false, method =() => DisableAllMods()},

                new ButtonInfo { buttonText = "Accept Terms of Service", isTogglable = false, method =() => AcceptTermsofService()},

                new ButtonInfo { buttonText = "Clear Notifications", isTogglable = false, method =() => NotifiLib.ClearAllNotifications()},
                new ButtonInfo { buttonText = "Longer Notifications", isTogglable = true, enableMethod =() => NotifiLib.NoticationThreshold = 75, disableMethod =() => NotifiLib.NoticationThreshold = 30},
                new ButtonInfo { buttonText = "Disable Notifications", isTogglable = true, enableMethod =() => NotifiLib.IsEnabled = false, disableMethod =() => NotifiLib.IsEnabled = true},

                new ButtonInfo { buttonText = "First Person", isTogglable = true, method =() => FPC(), disableMethod =() => NoFPC()},
                new ButtonInfo { buttonText = "Steam Screenshot [RG]", isTogglable = true, method =() => Screenshot()},

                new ButtonInfo { buttonText = "Toggle World Text", isTogglable = false, enableMethod =() => menuText = !menuText},
                new ButtonInfo { buttonText = "InvisUI", isTogglable = true, enableMethod =() => menuBark = true, disableMethod =() => menuBark = false},
                new ButtonInfo { buttonText = "Trigger Pages", isTogglable = true, enableMethod =() => triggerPages = true, disableMethod =() => triggerPages = false},
                new ButtonInfo { buttonText = "Change Menu Theme", isTogglable = false, method =() => ChangeTheme()},
                new ButtonInfo { buttonText = "Disable Menu Drop", isTogglable = true, enableMethod =() => menuDrop = false, disableMethod =() => menuDrop = true},
                new ButtonInfo { buttonText = "Disable Outline", isTogglable = true, enableMethod =() => outlinedMenu = false, disableMethod =() => outlinedMenu = true},
                new ButtonInfo { buttonText = "Rainbow Outline", isTogglable = true, enableMethod =() => rainbowOutline = true, disableMethod =() => rainbowOutline = false},
                new ButtonInfo { buttonText = "Right Hand Menu", isTogglable = true, enableMethod =() => rightHanded = true, disableMethod =() => rightHanded = false},

                new ButtonInfo { buttonText = "Speed Changer [Normal]", isTogglable = false, method =() => SpeedChanger()},
                new ButtonInfo { buttonText = "Slide Changer [Normal]", isTogglable = false, method =() => SlideChanger()},
                new ButtonInfo { buttonText = "Sticky Platforms", isTogglable = true, enableMethod =() => sticky = true, disableMethod =() => sticky = false},

                new ButtonInfo { buttonText = "Uncap FPS", isTogglable = true, method =() => Application.targetFrameRate = 1000, disableMethod =() => Application.targetFrameRate = 144},
                new ButtonInfo { buttonText = "Capped FPS [<color=lime>60</color>]", isTogglable = true, method =() => Application.targetFrameRate = 60, disableMethod =() => Application.targetFrameRate = 144},
                new ButtonInfo { buttonText = "Capped FPS [<color=lime>30</color>]", isTogglable = true, method =() => Application.targetFrameRate = 15, disableMethod =() => Application.targetFrameRate = 144},
                new ButtonInfo { buttonText = "Capped FPS [<color=lime>15</color>]", isTogglable = true, method =() => Application.targetFrameRate = 15, disableMethod =() => Application.targetFrameRate = 144},

                new ButtonInfo { buttonText = "Force Low HZ", isTogglable = true, method =() => ForceLag(false)},
                new ButtonInfo { buttonText = "Force Low HZ [G]", isTogglable = true, method =() => ForceLag(true)},

                new ButtonInfo { buttonText = "Change Time", isTogglable = false, method =() => TimeChanger()},

                new ButtonInfo { buttonText = "Start Raining", isTogglable = false, method =() => StartRaining()},
                new ButtonInfo { buttonText = "Stop Raining", isTogglable = false, method =() => StopRaining()},

                new ButtonInfo { buttonText = "Unlock Competitive", isTogglable = false, method =() => GorillaComputer.instance.CompQueueUnlockButtonPress()},

                new ButtonInfo { buttonText = "testmod", isTogglable = false, method =() => NotifiLib.SendNotification(GorillaLocomotion.Player.Instance.transform.position.ToString())},
                new ButtonInfo { buttonText = "testmod2", isTogglable = false, method =() => NotifiLib.SendNotification(GorillaLocomotion.Player.Instance.transform.rotation.ToString())},
            },

            new ButtonInfo[] { // [2] Current
                new ButtonInfo { buttonText = "Server Information", isTogglable = false, method =() => NotifiLib.SendNotification(PhotonNetwork.InRoom ? $"PING: {PhotonNetwork.GetPing()}\nIP: {PhotonNetwork.ServerAddress}\nPlayercount: {PhotonNetwork.CountOfPlayers}\nServer's Time Elapsed: {PhotonNetwork.ServerTimestamp}" : "[<color=red>ERROR</color>] Not in room!")},
                new ButtonInfo { buttonText = "Photon Information", isTogglable = false, method =() => NotifiLib.SendNotification($"Active Users: {PhotonNetwork.CountOfPlayersInRooms}")},

                new ButtonInfo { buttonText = "Grab IDs", isTogglable = false, method =() => GetInfo()},

                new ButtonInfo { buttonText = "Disconnect [P]", isTogglable = true, method =() => PDisconnect()},
                new ButtonInfo { buttonText = "Reconnect", isTogglable = false, method =() => Join(RoomCode)},
                new ButtonInfo { buttonText = "Connect/Swap Rooms", isTogglable = false, method =() => ServerHop()},

                new ButtonInfo { buttonText = "Create Private", isTogglable = false, method =() => Join(PrivateCode[UnityEngine.Random.Range(1, PrivateCode.Length)].ToUpper())},

                new ButtonInfo { buttonText = "Report Player [PC?]", isTogglable = true, enableMethod =() => EnableReportPlayer()},

                new ButtonInfo { buttonText = "Disable Network Triggers", isTogglable = true, method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false), disableMethod =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true)},
                new ButtonInfo { buttonText = "Disable Quitbox", isTogglable = true, method =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").SetActive(false), disableMethod =() => GameObject.Find("Environment Objects/TriggerZones_Prefab/ZoneTransitions_Prefab/QuitBox").SetActive(false)},

                new ButtonInfo { buttonText = "Join Custom Menu Code", isTogglable = false, method =() => Join("_RIFT_")},
                new ButtonInfo { buttonText = "Join Ghost Code", isTogglable = false, method =() => Join(GhostCodes[UnityEngine.Random.Range(1, GhostCodes.Length)].ToUpper())},
                new ButtonInfo { buttonText = "Join Youtube Code", isTogglable = false, method =() => Join(YoutubeCodes[UnityEngine.Random.Range(1, YoutubeCodes.Length)].ToUpper())},
                new ButtonInfo { buttonText = "Join Comp Code", isTogglable = false, method =() => Join(CompCodes[UnityEngine.Random.Range(1, CompCodes.Length)].ToUpper())},

                new ButtonInfo { buttonText = "Switch Region [<color=lime>US</color>]", isTogglable = false, method =() => PhotonNetwork.ConnectToRegion("us")},
                new ButtonInfo { buttonText = "Switch Region [<color=lime>EU</color>]", isTogglable = false, method =() => PhotonNetwork.ConnectToRegion("eu")},
                new ButtonInfo { buttonText = "Switch Region [<color=lime>AU</color>]", isTogglable = false, method =() => PhotonNetwork.ConnectToRegion("au")},
                new ButtonInfo { buttonText = "Connect to Local Region", isTogglable = false, method =() => PhotonNetwork.ConnectToBestCloudServer()},
            },

            new ButtonInfo[] { // [3] Fun
                new ButtonInfo { buttonText = "Splash", isTogglable = true, method =() => Splashing()},
                new ButtonInfo { buttonText = "Splash <color=lime>Aura</color>", isTogglable = true, method =() => SplashAura()},
                new ButtonInfo { buttonText = "Hit Player", isTogglable = true, method =() => HitPlayer()},

                new ButtonInfo { buttonText = "Choose Sound^", isTogglable = false, method =() => ChangeSound()},
                new ButtonInfo { buttonText = "Spam Sounds [G]", isTogglable = true, method =() => SoundSpammer()},
                new ButtonInfo { buttonText = "Spam Random Sounds [G]", isTogglable = true, method =() => SoundSpammer()},
                new ButtonInfo { buttonText = "Spam When Touching", isTogglable = true, method =() => SpamWhenTouching()},

                new ButtonInfo { buttonText = "Leave Party", isTogglable = false, method =() => FriendshipGroupDetection.Instance.LeaveParty()},

                new ButtonInfo { buttonText = "Faster Turn Speed", isTogglable = true, method =() => FasterTurnSpeed(), disableMethod =() => FixTurnSpeed()},

                new ButtonInfo { buttonText = "Change Name [<color=lime>Invisible</color>]", isTogglable = false, method =() => Name("__________")},
                new ButtonInfo { buttonText = "Change Name [<color=lime>Random</color>]", isTogglable = false, method =() => Name(Names[UnityEngine.Random.Range(1, Names.Length)].ToUpper())},
                new ButtonInfo { buttonText = "Change Name [<color=lime>Lowercase</color>]", isTogglable = false, method =() => yourName().ToLower()},
                new ButtonInfo { buttonText = "Change Name [<color=lime>Uppercase</color>]", isTogglable = false, method =() => yourName().ToUpper()},
                new ButtonInfo { buttonText = "Change Name [<color=lime>Separated</color>]", isTogglable = false, method =() => SeparateText(yourName())},

                new ButtonInfo { buttonText = "Fast Taps", isTogglable = true, enableMethod =() => InfiniteTaps(), disableMethod =() => SlowTaps()},

                new ButtonInfo { buttonText = "Walk-on-Water", isTogglable = true, method =() => Jesus(), disableMethod =() => FixWater()},
                new ButtonInfo { buttonText = "Disable Water", isTogglable = true, method =() => DisableWater(), disableMethod =() => EnableWater()},
            },

            new ButtonInfo[] { // [4] Movement
                new ButtonInfo { buttonText = "Platforms", isTogglable = true, method =() => Plattys(ButtonColor, rightG, leftG)},
                new ButtonInfo { buttonText = "<color=lime>Trigger</color> Platforms", method =() => Plattys(ButtonColor, rightT > .2f, leftT > .2f)},

                new ButtonInfo { buttonText = "Draw Platforms [G]", isTogglable = true, method =() => DrawPlatforms()},
                new ButtonInfo { buttonText = "Draw Platform Gun", isTogglable = true, method =() => DrawPlatformsGun()},

                new ButtonInfo { buttonText = "<color=lime>Realistic</color> Long Arms", isTogglable = true, method =() => RealisticArms(), disableMethod =() => FixArms()},
                new ButtonInfo { buttonText = "<color=lime>Steam</color> Long Arms", isTogglable = true, method =() => LongArms(), disableMethod =() => FixArms()},
                new ButtonInfo { buttonText = "<color=lime>Super</color> Long Arms", isTogglable = true, method =() => SuperLongArms(), disableMethod =() => FixArms()},
                new ButtonInfo { buttonText = "Arm Size Changer [G] [P]", isTogglable = true, method =() => ArmSizeChanger(), disableMethod =() => FixArms()},

                new ButtonInfo { buttonText = "Fly [G]", isTogglable = true, method =() => Fly()},
                new ButtonInfo { buttonText = "Fly [<color=lime>Hand</color>] [G]", isTogglable = true, method =() => HandFly()},
                new ButtonInfo { buttonText = "Fly [<color=lime>Trigger</color>] [T]", isTogglable = true, method =() => TriggerFly()},
                new ButtonInfo { buttonText = "Fly [<color=lime>Noclip</color>] [G]", isTogglable = true, method =() => NoclipFly()},
                new ButtonInfo { buttonText = "Fly [<color=lime>Slingshot</color>] [G]", isTogglable = true, method =() => Slingshot()},
                new ButtonInfo { buttonText = "Fly [<color=lime>Hand Slingshot</color>] [G]", isTogglable = true, method =() => Slingshot()},

                new ButtonInfo { buttonText = "Ghost [P]", isTogglable = true, method =() => Ghost(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Invis [P]", isTogglable = true, method =() => Invis(), disableMethod =() => FixRig()},

                new ButtonInfo { buttonText = "Laggy Rig", isTogglable = true, method =() => LaggyRig(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Delayed Rig", isTogglable = true, method =() => LaggyRig1(), disableMethod =() => FixRig()},

                new ButtonInfo { buttonText = "Rig Gun", isTogglable = true, method =() => RigGun(), disableMethod =() => FixRig()},

                new ButtonInfo { buttonText = "Spaz <color=lime>Body</color>", isTogglable = true, method =() => Spaz(), disableMethod =() => FixHead()},
                //new ButtonInfo { buttonText = "Spaz <color=lime>Head</color>", isTogglable = true, method =() => SpinHead(), disableMethod =() => FixHead()},

                new ButtonInfo { buttonText = "Helicopter", isTogglable = true, method =() => Helicopter(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Beyblade", isTogglable = true, method =() => Beyblade(), disableMethod =() => FixRig()},

                new ButtonInfo { buttonText = "Key Movement", isTogglable = true, method =() => Keyboarding(), disableMethod =() => GorillaTagger.Instance.rigidbody.useGravity = true},

                new ButtonInfo { buttonText = "Speedboost", isTogglable = true, method =() => Speedboost()},
                new ButtonInfo { buttonText = "Grip Speedboost", isTogglable = true, method =() => GripSpeedboost()},
                new ButtonInfo { buttonText = "Integrated Speedboost", isTogglable = true, method =() => IntegratedSpeedboost()},
                new ButtonInfo { buttonText = "Uncap Speed", isTogglable = true, method =() => UncapSpeed()},
                new ButtonInfo { buttonText = "Slide Control", isTogglable = true, method =() => SlideControl()},

                new ButtonInfo { buttonText = "Teleport Gun", isTogglable = true, method =() => TeleportGun()},

                new ButtonInfo { buttonText = "C4", isTogglable = true, method =() => C4(), disableMethod =() => DisableC4()},
                new ButtonInfo { buttonText = "Checkpoint", isTogglable = true, method =() => Checkpoint(), disableMethod =() => DisableCheckPoint()},

                new ButtonInfo { buttonText = "Left Right", isTogglable = true, method =() => LeftAndRight()},
                new ButtonInfo { buttonText = "Up Down", isTogglable = true, method =() => UpAndDown()},
                new ButtonInfo { buttonText = "Super Jump", isTogglable = true, method =() => SuperJump()},
                new ButtonInfo { buttonText = "Jetpack [G]", isTogglable = true, method =() => IronMan()},
                new ButtonInfo { buttonText = "No Tag Freeze", isTogglable = false, method =() => NoTagFreeze()},
                new ButtonInfo { buttonText = "Force Tag Freeze", isTogglable = false, method =() => ForceTagFreeze()},

                new ButtonInfo { buttonText = "No Clip [T]", isTogglable = true, method =() => NoClip()},

                new ButtonInfo { buttonText = "High Gravity", isTogglable = true, method =() => HighGravity()},
                new ButtonInfo { buttonText = "Zero Gravity", isTogglable = true, method =() => ZeroGravity()},
                new ButtonInfo { buttonText = "Low Gravity", isTogglable = true, method =() => LowGravity()},

                new ButtonInfo { buttonText = "Fish Arms", isTogglable = true, method =() => FlipArms()},
            },

            new ButtonInfo[] { // [5] Visuals
                new ButtonInfo { buttonText = "Appear as Skeletons", isTogglable = true, enableMethod =() => ShowSkeletons(), disableMethod =() => HideSkeletons()},

                new ButtonInfo { buttonText = "Disable Leaves", isTogglable = false, method =() => NoLeaves()},

                new ButtonInfo { buttonText = "Low Quality", isTogglable = true, method =() => QualitySettings.globalTextureMipmapLimit = int.MaxValue, disableMethod =() => QualitySettings.globalTextureMipmapLimit = int.MinValue},
                new ButtonInfo { buttonText = "Full Bright", isTogglable = true, enableMethod =() => FullBright(), disableMethod =() => FixBright()},
                new ButtonInfo { buttonText = "Xray", isTogglable = true, enableMethod =() => Xray(), disableMethod =() => DisableXray()},

                new ButtonInfo { buttonText = "Launch Rocket", isTogglable = false, method =() => LaunchRocket()},

                new ButtonInfo { buttonText = "Casual Tracers", isTogglable = true, method =() => CasualTracers()},
                new ButtonInfo { buttonText = "Infection Tracers", isTogglable = true, method =() => InfectionTracers()},
                new ButtonInfo { buttonText = "Competitive Tracers", isTogglable = true, method =() => CompetitiveTracers()},

                new ButtonInfo { buttonText = "Casual Hand Tracers", isTogglable = true, method =() => CasualHandTracers()},
                new ButtonInfo { buttonText = "Infection Hand Tracers", isTogglable = true, method =() => InfectionHandTracers()},
                new ButtonInfo { buttonText = "Competitive Hand Tracers", isTogglable = true, method =() => CompetitiveHandTracers()},

                new ButtonInfo { buttonText = "Casual Beacons", isTogglable = true, method =() => CasualBeacons()},
                new ButtonInfo { buttonText = "Infection Beacons", isTogglable = true, method =() => InfectionBeacons()},
                new ButtonInfo { buttonText = "Competitive Beacons", isTogglable = true, method =() => CompetitiveBeacons()},

                new ButtonInfo { buttonText = "Casual Chams", isTogglable = true, method =() => CasualChams(), disableMethod =() => UndoChams()},
                new ButtonInfo { buttonText = "Infection Chams", isTogglable = true, method =() => InfectionChams(), disableMethod =() => UndoChams()},
                new ButtonInfo { buttonText = "Competitive Chams", isTogglable = true, method =() => CompetitiveChams(), disableMethod =() => UndoChams()},

                new ButtonInfo { buttonText = "Casual Hitboxes", isTogglable = true, method =() => CasualHitboxes()},
                new ButtonInfo { buttonText = "Infection Hitboxes", isTogglable = true, method =() => InfectionHitboxes()},
                new ButtonInfo { buttonText = "Competitive Hitboxes", isTogglable = true, method =() => CompetitiveHitboxes()},

                new ButtonInfo { buttonText = "Casual Distance", isTogglable = true, method =() => CasualTextEsp()},
                new ButtonInfo { buttonText = "Infection Distance", isTogglable = true, method =() => InfectionTextEsp()},
                new ButtonInfo { buttonText = "Competitive Distance", isTogglable = true, method =() => CompetitiveTextEsp()},

                new ButtonInfo { buttonText = "Casual Nametags", isTogglable = true, method =() => CasualNametags()},
                new ButtonInfo { buttonText = "Infection Nametags", isTogglable = true, method =() => InfectionNametags()},
                new ButtonInfo { buttonText = "Competitive Nametags", isTogglable = true, method =() => CompetitiveNametags()},

                new ButtonInfo { buttonText = "Casual Information", isTogglable = true, method =() => CasualInformation()},
                new ButtonInfo { buttonText = "Infection Information", isTogglable = true, method =() => InfectionInformation()},
                new ButtonInfo { buttonText = "Competitive Information", isTogglable = true, method =() => CompetitiveInformation()},

                new ButtonInfo { buttonText = "Casual Distance Tracers", isTogglable = true, method =() => CasualDistanceTracers()},
                new ButtonInfo { buttonText = "Infection Distance Tracers", isTogglable = true, method =() => InfectionDistanceTracers()},
                new ButtonInfo { buttonText = "Competitive Distance Tracers", isTogglable = true, method =() => CompetitiveDistanceTracers()},

                new ButtonInfo { buttonText = "Casual Speedometers", isTogglable = true, method =() => CasualSpeedometer()},
                new ButtonInfo { buttonText = "Infection Speedometers", isTogglable = true, method =() => InfectionSpeedometer()},
                new ButtonInfo { buttonText = "Competitive Speedometers", isTogglable = true, method =() => CompetitiveSpeedometer()},

                new ButtonInfo { buttonText = "Nearest Target", isTogglable = true, method =() => NearestTarget()},

                /// [+] Text Mods, [+] World Visual Mods
            },

            new ButtonInfo[] { // [6] Advantages
                new ButtonInfo { buttonText = "Tag <color=lime>Self</color>", isTogglable = true, method =() => TagSelf(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>All</color>", isTogglable = true, method =() => TagAll(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>All</color> [G]", isTogglable = true, method =() => GripTagAll(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>Aura</color>", isTogglable = true, method =() => TagAura(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>Gun</color>", isTogglable = true, method =() => TagGun(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>Flick</color>", isTogglable = true, method =() => FlickTag(), disableMethod =() => FixRig()},
                new ButtonInfo { buttonText = "Tag <color=lime>Random</color> [G]", isTogglable = true, method =() => TagRandom(), disableMethod =() => FixRig()},
            },

            new ButtonInfo[] { // [7] Safety
                new ButtonInfo { buttonText = "Flush RPCs", isTogglable = false, method =() => RPC_PATCHER_KICK_PATCHER()},

                new ButtonInfo { buttonText = "Anti <color=grey>Report</color> [<color=lime>Disconnect</color>]", isTogglable = true, method =() => AntiReport(1)},
                new ButtonInfo { buttonText = "Anti <color=grey>Report</color> [<color=lime>Reconnect</color>]", isTogglable = true, method =() => AntiReport(2)},
                new ButtonInfo { buttonText = "Anti <color=grey>Report</color> [<color=lime>Serverhop</color>]", isTogglable = true, method =() => AntiReport(3)},

                new ButtonInfo { buttonText = "Anti <color=lime>Famous/Mod</color>", isTogglable = true, method =() => AntiFamous()},
                new ButtonInfo { buttonText = "[<color=yellow>FIXED</color>] Anti <color=lime>Finger Movement</color>", isTogglable = true, method =() => AntiFinger()},

                new ButtonInfo { buttonText = "Spoof Name", isTogglable = false, method =() => Name(playerNames[UnityEngine.Random.Range(1, 100)].ToUpper())},
                new ButtonInfo { buttonText = "Spoof Name Ordinary", isTogglable = false, method =() => Name(simpleNames[UnityEngine.Random.Range(1, 100)].ToUpper())},

                new ButtonInfo { buttonText = "Fake Quest Platform", isTogglable = false, method =() => QuestSupportTab()},

                new ButtonInfo { buttonText = "Bad Name Bypasser [NO ENTER]", isTogglable = true, method =() => BadNameBypasser()},
            }
        };
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("LateUpdate", MethodType.Normal)]
    public class Main : MonoBehaviour
    {
        public static void Prefix()
        {
            #region Main Menu Handler
            try
            {
                bool toOpen = !rightHanded && ControllerInputPoller.instance.leftControllerSecondaryButton || rightHanded && ControllerInputPoller.instance.rightControllerSecondaryButton;
                bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);

                if (menu == null)
                {
                    if (toOpen || keyboardOpen)
                    {
                        Initialization();
                        RecenterMenu(rightHanded, keyboardOpen);
                        if (reference == null)
                        {
                            CreateReference(rightHanded);
                        }
                    }
                }
                else
                {
                    if (toOpen || keyboardOpen)
                        RecenterMenu(rightHanded, keyboardOpen);
                    else
                    {
                        Rigidbody rigid = menu.AddComponent(typeof(Rigidbody)) as Rigidbody;
                        if (menuDrop)
                        {
                            if (rightHanded)
                                rigid.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                            else
                                rigid.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0);
                            rigid.AddTorque(new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)), ForceMode.Impulse);
                            Destroy(menu, 3.25f);
                            Destroy(reference);
                            menu = null;
                            reference = null;
                        }
                        else
                        {
                            rigid.useGravity = false;
                            if (rightHanded)
                                rigid.velocity = GorillaLocomotion.Player.Instance.rightHandCenterVelocityTracker.GetAverageVelocity(true, 0) * 0.5f;
                            else
                                rigid.velocity = GorillaLocomotion.Player.Instance.leftHandCenterVelocityTracker.GetAverageVelocity(true, 0) * 0.5f;
                            rigid.AddTorque(new Vector3(UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5), UnityEngine.Random.Range(-5, 5)), ForceMode.Impulse);
                            if (rigid != null)
                            {
                                rigid.AddForce(Vector3.up * 0.1f, ForceMode.Acceleration);
                                rigid.velocity = Vector3.Lerp(rigid.velocity, Vector3.zero, Time.deltaTime * 0.5f);
                            }
                            Destroy(menu, 2.5f);
                            Destroy(reference);
                            menu = null;
                            reference = null;
                        }
                    }
                }
            }
            catch { }
            #endregion

            #region Button_Prefix
            try
            {
                foreach (ButtonInfo[] buttonlist in buttons)
                {
                    foreach (ButtonInfo v in buttonlist)
                    {
                        if (v.enabled)
                        {
                            if (v.method != null)
                            {
                                try
                                {
                                    v.method.Invoke();
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
            catch { }
            #endregion

            #region Ghost Rig
            try
            {
                if (!GorillaTagger.Instance.offlineVRRig.enabled)
                {
                    if (copyrig == null)
                    {
                        if (copyrig != null)
                        {
                            Destroy(copyrig.gameObject);
                            copyrig = null;
                        }
                        copyrig = Instantiate<VRRig>(GorillaTagger.Instance.offlineVRRig, GorillaLocomotion.Player.Instance.transform.position, GorillaLocomotion.Player.Instance.transform.rotation);
                        copyrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        copyrig.mainSkin.material.color = new Color32(ButtonColor.r, ButtonColor.g, ButtonColor.b, 50);
                    }
                    else if (!copyrig.enabled)
                    {
                        copyrig.enabled = true;
                    }
                    copyrig.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    copyrig.mainSkin.material.color = new Color32(ButtonColor.r, ButtonColor.g, ButtonColor.b, 50);
                }
                else if (GorillaTagger.Instance.offlineVRRig.enabled && copyrig != null)
                {
                    Destroy(copyrig.gameObject);
                    copyrig = null;
                }
            }
            catch { }
            #endregion

            #region Triggers [MENU]
            if (triggerPages)
            {
                if ((ControllerInputPoller.instance.leftControllerIndexFloat > 0.5f || UnityInput.Current.GetKeyDown(KeyCode.Q)) && menu != null && Time.time - j >= k)
                {
                    pageNumber--;
                    if (pageNumber < 0)
                        pageNumber = (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1;
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, true, 0.4f);
                    RecreateMenu();
                    j = Time.time;
                }
                if ((ControllerInputPoller.instance.rightControllerIndexFloat > 0.5f || UnityInput.Current.GetKeyDown(KeyCode.E)) && menu != null && Time.time - j >= k)
                {
                    pageNumber++;
                    if (pageNumber > (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1)
                        pageNumber = 0;
                    GorillaTagger.Instance.offlineVRRig.PlayHandTapLocal(66, false, 0.4f);
                    RecreateMenu();
                    j = Time.time;
                }
            }
            #endregion

            #region instantiate
            bool instantiated = false;
            if (!instantiated)
            {
                rightT = ControllerInputPoller.TriggerFloat(XRNode.RightHand);
                rjs = ControllerInputPoller.instance.rightControllerPrimary2DAxis;
                rightP = ControllerInputPoller.instance.rightControllerPrimaryButton;
                rightS = ControllerInputPoller.instance.rightControllerSecondaryButton;
                rightG = ControllerInputPoller.instance.rightGrab;

                leftT = ControllerInputPoller.TriggerFloat(XRNode.LeftHand);
                ljs = ControllerInputPoller.instance.leftControllerPrimary2DAxis;
                leftP = ControllerInputPoller.instance.leftControllerPrimaryButton;
                leftS = ControllerInputPoller.instance.leftControllerSecondaryButton;
                leftG = ControllerInputPoller.instance.leftGrab;
                instantiated = true;
            }
            #endregion

            #region room
            bool fault = false;
            if (PhotonNetwork.InRoom)
            {
                if (!fault)
                {
                    RoomCode = PhotonNetwork.CurrentRoom.Name;
                    fault = true;
                }
            }
            else if (fault)
                fault = false;
            #endregion

            #region Text & Boards
            if (menuText)
            {
                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-63.83f, 12.57f, -82.77f)) < 4f)
                {
                    MenuStumpText();
                }

                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-63.66f, 3.97f, -63.59f)) < 7.5f)
                {
                    UserIDText();
                }

                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-54.23f, 16.82f, -105.80f)) < 9f)
                {
                    CreditText();
                    CreditText2();
                }

                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-68.79f, 12.73f, -82.14f)) < 2f)
                {
                    DisclaimerText();
                }

                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-65.01f, 12.63f, -80.57f)) < 1.5f)
                {
                    NewsText();
                    NewsTitleText();
                }

                if (Vector3.Distance(GorillaTagger.Instance.transform.position, new Vector3(-67.12f, 12.58f, -80.30f)) < 1.85f)
                {
                    UsageText();
                }
            }
            if (customBoard)
            {
                UpdateBoards();
            }
            #endregion

            #region Tag Self Prefix
            if (GetIndex("Tag <color=lime>Self</color>").enabled && TaggedSelf)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                TaggedSelf = false;
            }
            #endregion
        }

        public static void Initialization()
        {
            menu = GameObject.CreatePrimitive(PrimitiveType.Cube);
            back = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Destroy(menu.GetComponent<Rigidbody>());
            Destroy(menu.GetComponent<BoxCollider>());
            Destroy(menu.GetComponent<Renderer>());
            Destroy(back.GetComponent<Rigidbody>());
            Destroy(back.GetComponent<BoxCollider>());
            back.transform.parent = menu.transform;
            ConfigureMenu(back, menu, false);
            canvasObject = new GameObject();
            canvasObject.transform.parent = menu.transform;
            Canvas canvas = canvasObject.AddComponent<Canvas>();
            ConfigureCanva(canvas);
            Text text = new GameObject().AddComponent<Text>();
            text.transform.parent = canvasObject.transform;
            ConfigureText(text);

            GameObject Return = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Return.transform.parent = menu.transform;
            ConfigureReturn(Return, canvasObject);
            GameObject Disconnect = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Disconnect.transform.parent = menu.transform;
            ConfigureDisconnect(Disconnect, canvasObject);
            GameObject Serverhop = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Serverhop.transform.parent = menu.transform;
            ConfigureHopConfigureRightPage(Serverhop, canvasObject);
            GameObject FPS = GameObject.CreatePrimitive(PrimitiveType.Cube);
            FPS.transform.parent = menu.transform;
            ConfigureFPSConfigureLeftPage(FPS, canvasObject);
            GameObject Discord = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Discord.transform.parent = menu.transform;
            ConfigureDiscord(Discord, canvasObject);
            GameObject Panic = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Panic.transform.parent = menu.transform;
            ConfigurePanic(Panic, canvasObject);

            for (int i = 0; i < Enumerable.ToArray<ButtonInfo>(Enumerable.Take<ButtonInfo>(Enumerable.Skip<ButtonInfo>(Buttons.buttons[buttonsType], pageNumber * buttonsPerPage), buttonsPerPage)).Length; i++)
            {
                ConfigureButtons(i * 0.15f, Enumerable.ToArray<ButtonInfo>(Enumerable.Take<ButtonInfo>(Enumerable.Skip<ButtonInfo>(buttons[buttonsType], pageNumber * buttonsPerPage), buttonsPerPage))[i]);
            }
        }

        public static void MenuStumpText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 110;
            memorymesh.fontStyle = FontStyle.Normal;
            memorymesh.characterSize = 0.009f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-63.83f, 12.57f, -82.77f);
            memorymesh.text = $"STATUS\n<color=lime>{MENUSTATUS}</color>\n\n<color=grey>RIFT</color> <color=blue>{PluginInfo.Version}</color>\n{GetModCount()} MODS.";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void UserIDText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.007f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Left;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-63.66f, 3.97f, -63.59f);
            memorymesh.text = $"In Room: <color=lime>{configuringRoomCode()}</color>\nUsername: <color=lime>{PhotonNetwork.LocalPlayer.NickName}</color>\nColor: {configuringTaggedText()}";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void CreditText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.007f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-54.23f, 16.82f, -105.80f);
            memorymesh.text = $"<color=grey>{PluginInfo.Name}</color>'s Credits\nfunctionkey0 - Creator/Developer/Designer\nitsmonkeyz_ - Designer/Suggestor\n.catlicker - EX Developer/EX Designer\nAgent 999 - Boards/Designer\n\nDISCORD.GG/NsSwxqg8D2";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void CreditText2()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.007f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-54.23f, 17.58f, -105.80f);
            memorymesh.text = $"Testers\nconstantsans, ikoniccodes, cyx4,\nchip124_2, itsmonkeyz_, psychothinker, calixlala";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void DisclaimerText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.005f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-68.79f, 12.73f, -82.14f);
            memorymesh.text = $"<color=red>Disclaimer</color>:\nThis menu uses links to get materials and our Discord servers,\nand this menu add/saves text files within your Gorilla Tag folder,\nif you don't want this to happen, after using this menu\ndelete the added directories and text files as you wish.";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void NewsTitleText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.007f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-65.01f, 12.805f, -80.57f);
            memorymesh.text = "<color=red>NEWS</color>";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void NewsText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Italic;
            memorymesh.characterSize = 0.005f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-65.01f, 12.63f, -80.57f);
            memorymesh.text = news;
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void UsageText()
        {
            GameObject mymemorymymemory = new GameObject("wdwadawdawddawdwawadadawdawd");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Bold;
            memorymesh.characterSize = 0.005f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Left;
            memorymesh.color = Color.white;
            mymemorymymemory.transform.position = new Vector3(-67.12f, 12.58f, -80.30f);
            memorymesh.text = "[R] Opens menu on PC\n[Y] Opens menu on VR\n[F3] Opens Room Manager on PC";
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void UpdateBoards()
        {
            for (int i = 0; i < TreeRoom.transform.childCount; i++)
            {
                GameObject children = TreeRoom.transform.GetChild(i).gameObject;
                if (children.name.Contains("forestatlas"))//forest atlas
                {
                    children.GetComponent<Renderer>().material = boardmat;
                }
            }

            MessageDayTitle.text = PluginInfo.Version;
            MessageDayText.alignment = TextAlignmentOptions.Top;
            MessageDayText.text = $"{DateTime.Now:hh:mm tt}\nFrom 6/18/24 to {DateTime.Now:MM/dd/yy}, <color=red>Disclaimer</color>:\nOur mod menu is designed to be fully undetected (UD), open sourced and regularly updated for safety. However, we are not responsible for any bans or reports resulting from its use. By using <color=grey>{PluginInfo.Name}</color>, you accept full responsibility for anything that happens.\n\n\n\nTime Elasped: {Time.time.ToString("F1")}\nhttps://discord.gg/NsSwxqg8D2";

            CodeOfConductTitle.text = MENUSTATUS;
            CodeOfConductText.text = coc;
        }
        public static Material boardmat = TextureLoader("https://www.google.com/url?sa=i&url=https%3A%2F%2Fwww.vectorstock.com%2Froyalty-free-vectors%2Fblack-sky-stars-vectors&psig=AOvVaw2psVbQ5FdykIuhtLsQlFm2&ust=1728403997160000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqFwoTCNDgw-TU_IgDFQAAAAAdAAAAABAE");

        public static string configuringRoomCode()
        {
            return PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.Name : "No";
        }

        public static string configuringTaggedText()
        {
            return Infected(GorillaTagger.Instance.offlineVRRig) ? "<color=red>Infected</color>" : $"<color=red>{configuringSkinCode(1)}</color> <color=lime>{configuringSkinCode(2)}</color> <color=blue>{configuringSkinCode(3)}</color>";
        }

        public static string configuringSkinCode(int i)
        {
            float value = 0;
            if (i == 1) value = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color.r;
            if (i == 2) value = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color.g;
            if (i == 3) value = GorillaTagger.Instance.offlineVRRig.mainSkin.material.color.b;
            return Mathf.Clamp(Mathf.RoundToInt(value * 9), 0, 9).ToString();
        }

        public static string configuringSkinCode(int i, VRRig target)
        {
            float value = 0;
            if (i == 1) value = target.playerColor.r;
            if (i == 2) value = target.playerColor.g;
            if (i == 3) value = target.playerColor.b;
            return Mathf.Clamp(Mathf.RoundToInt(value * 9), 0, 9).ToString();
        }

        public static int GetModCount()
        {
            return buttons.Sum(i => i.Length);
        }

        public static void ConfigureText(Text text)
        {
            text.font = currentFont;
            text.text = PluginInfo.Name;
            text.fontSize = 1000;
            text.color = TitleColor;
            text.supportRichText = true;
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.fontStyle = FontStyle.Italic;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;

            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.058f, .018f);
            component.position = new Vector3(0.052f, 0f, 0.1535f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));
        }

        public static void ConfigureCanva(Canvas canvas)
        {
            CanvasScaler canvasScaler = Main.canvasObject.AddComponent<CanvasScaler>();
            Main.canvasObject.AddComponent<GraphicRaycaster>();
            canvas.renderMode = RenderMode.WorldSpace;
            canvasScaler.dynamicPixelsPerUnit = 1000f;
        }

        public static void ConfigureMenu(GameObject background, GameObject menu, bool buttonsToo)
        {
            background.transform.rotation = Quaternion.identity;
            background.transform.localScale = menuSize;
            if (buttonsToo) { }
            background.GetComponent<Renderer>().material.color = MenuColor;
            if (menuBark)
            {
                background.transform.position = new Vector3(999, 999, 999);
            }
            else
            {
                background.transform.position = new Vector3(0.45f, 0f, 0f);
            }
            menu.transform.localScale = new Vector3(0.1f, 0.28f, 0.34f);

            if (outlinedMenu && !menuBark)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = background.transform.position;
                outline.transform.rotation = background.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.09f, 1.01f, 1.01f);
            }
        }

        public static void ConfigureButtons(float offset, ButtonInfo method)
        {
            Main.button = GameObject.CreatePrimitive(PrimitiveType.Cube);
            if (!UnityInput.Current.GetKey(keyboardButton))
            {
                Main.button.layer = 2;
            }
            Destroy(Main.button.GetComponent<Rigidbody>());
            Main.button.GetComponent<BoxCollider>().isTrigger = true;
            Main.button.transform.parent = Main.menu.transform;
            Main.button.transform.rotation = Quaternion.identity;
            Main.button.transform.localScale = new Vector3(0.06f, 0.875f, 0.1f);
            Main.button.transform.localPosition = new Vector3(0.53f, 0f, 0.26f - offset + 0.09f);
            Main.button.AddComponent<Button>().relatedText = method.buttonText;
            if (method.enabled)
            {
                Main.button.GetComponent<Renderer>().material.color = Main.EnabledColor;
            }
            else
            {
                Main.button.GetComponent<Renderer>().material.color = Main.ButtonColor;
            }
            Text text = new GameObject { transform = { parent = Main.canvasObject.transform } }.AddComponent<Text>();
            text.font = currentFont;
            text.text = method.buttonText;
            if (method.overlapText != null)
            {
                text.text = method.overlapText;
            }
            text.supportRichText = true;
            text.fontSize = 1;
            if (method.enabled)
            {
                text.color = EnabledTextColor;
            }
            else
            {
                text.color = TextColor;
            }
            text.alignment = TextAnchor.MiddleCenter;
            text.fontStyle = FontStyle.Italic;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(.15f, .02f);
            component.localPosition = new Vector3(.058f, 0, .111f - offset / 3 + 0.009f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = button.transform.position;
                outline.transform.rotation = button.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.045f, 0.885f, 0.11169f);
            }
        }

        public static void ConfigureReturn(GameObject HomeButton, GameObject canvasObject)
        {
            HomeButton.GetComponent<BoxCollider>().isTrigger = true;
            HomeButton.transform.rotation = Quaternion.identity;
            HomeButton.transform.localScale = new Vector3(0.09f, 0.109f, 0.09f);
            HomeButton.transform.localPosition = new Vector3(0.447f, -0.44f , -0.56f);
            HomeButton.AddComponent<Button>().relatedText = "_back_";
            HomeButton.GetComponent<Renderer>().material.color = ButtonColor;

            /*GameObject HomeButtonOutline = GameObject.CreatePrimitive(PrimitiveType.Cube);
            HomeButtonOutline.transform.rotation = Quaternion.identity;
            HomeButtonOutline.transform.localScale = new Vector3(0.08f, 0.2f, 0.1f);
            HomeButtonOutline.transform.localPosition = new Vector3(0.427f, -0.44f, -0.56f);
            HomeButtonOutline.GetComponent<Renderer>().material.color = EnabledColor;*/

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "⌂"; // ⌂ ⇦
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.11f, 0.03f) * GorillaLocomotion.Player.Instance.scale;
            component.localPosition = new Vector3(0.0493f, HomeButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.023f/*0.021*/);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = HomeButton.transform.position;
                outline.transform.rotation = HomeButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.113f, 0.094f);
            }
        }

        public static void ConfigureDisconnect(GameObject LeaveButton, GameObject canvasObject)
        {
            LeaveButton.GetComponent<BoxCollider>().isTrigger = true;
            LeaveButton.transform.rotation = Quaternion.identity;
            LeaveButton.transform.localScale = new Vector3(0.09f, 0.209f, 0.09f);
            LeaveButton.transform.localPosition = new Vector3(0.447f, -0.26f, -0.56f);
            LeaveButton.AddComponent<Button>().relatedText = "_leave_";
            LeaveButton.GetComponent<Renderer>().material.color = ButtonColor;

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "Leave";
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.07f, 0.014f);
            component.localPosition = new Vector3(0.0493f, LeaveButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.0222f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = LeaveButton.transform.position;
                outline.transform.rotation = LeaveButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.214f, 0.095f);
            }
        }

        public static void ConfigureHopConfigureRightPage(GameObject HopButton, GameObject canvasObject)
        {
            HopButton.GetComponent<BoxCollider>().isTrigger = true;
            HopButton.transform.rotation = Quaternion.identity;
            HopButton.transform.localScale = new Vector3(0.09f, 0.159f, 0.09f);
            HopButton.transform.localPosition = new Vector3(0.447f, -0.057f, -0.56f);
            HopButton.AddComponent<Button>().relatedText = !triggerPages ? "_next_" : "_hop_";
            HopButton.GetComponent<Renderer>().material.color = ButtonColor;

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = !triggerPages ? ">" : "Hop";
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = !triggerPages ? new Vector2(.1f, .02f) : new Vector2(0.07f, 0.014f);
            component.localPosition = new Vector3(0.0493f, HopButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.021f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = HopButton.transform.position;
                outline.transform.rotation = HopButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.163f, 0.094f);
            }
        }

        public static void ConfigureFPSConfigureLeftPage(GameObject FPSButton, GameObject canvasObject)
        {
            if (!triggerPages)
            {
                FPSButton.GetComponent<BoxCollider>().isTrigger = true;
            }
            else
            {
                FPSButton.GetComponent<BoxCollider>().isTrigger = false;
            }
            FPSButton.transform.rotation = Quaternion.identity;
            FPSButton.transform.localScale = new Vector3(0.09f, 0.159f, 0.09f);
            FPSButton.transform.localPosition = new Vector3(0.447f, 0.12f, -0.56f);
            if (!triggerPages)
            {
                FPSButton.AddComponent<Button>().relatedText = "_prev_";
            }
            FPSButton.GetComponent<Renderer>().material.color = ButtonColor;

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = !triggerPages ? "<" : Mathf.Ceil(1f / Time.unscaledDeltaTime).ToString();
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = !triggerPages ? new Vector2(.1f, .02f) : new Vector2(0.07f, 0.014f);
            component.localPosition = new Vector3(0.0493f, FPSButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.021f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = FPSButton.transform.position;
                outline.transform.rotation = FPSButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.163f, 0.094f);
            }
        }

        public static void ConfigurePanic(GameObject HomeButton, GameObject canvasObject)
        {
            HomeButton.GetComponent<BoxCollider>().isTrigger = true;
            HomeButton.transform.rotation = Quaternion.identity;
            HomeButton.transform.localScale = new Vector3(0.09f, 0.149f, 0.09f);
            HomeButton.transform.localPosition = new Vector3(0.447f, 0.29f, -0.56f);
            HomeButton.AddComponent<Button>().relatedText = "_panic_";
            HomeButton.GetComponent<Renderer>().material.color = ButtonColor;

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "Panic";
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.07f, 0.014f) * GorillaLocomotion.Player.Instance.scale;
            component.localPosition = new Vector3(0.0493f, HomeButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.021f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = HomeButton.transform.position;
                outline.transform.rotation = HomeButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.153f, 0.094f);
            }
        }

        public static void ConfigureDiscord(GameObject HomeButton, GameObject canvasObject)
        {
            HomeButton.GetComponent<BoxCollider>().isTrigger = true;
            HomeButton.transform.rotation = Quaternion.identity;
            HomeButton.transform.localScale = new Vector3(0.09f, 0.109f, 0.09f);
            HomeButton.transform.localPosition = new Vector3(0.447f, 0.44f, -0.56f);
            HomeButton.AddComponent<Button>().relatedText = "_discord_";
            HomeButton.GetComponent<Renderer>().material.color = ButtonColor;

            Text text = new GameObject { transform = { parent = canvasObject.transform } }.AddComponent<Text>();
            text.font = Resources.GetBuiltinResource<Font>("Arial.ttf");
            text.text = "★";
            text.color = TextColor;
            text.fontSize = 1000;
            text.alignment = TextAnchor.MiddleCenter;
            text.resizeTextForBestFit = true;
            text.resizeTextMinSize = 0;
            RectTransform component = text.GetComponent<RectTransform>();
            component.localPosition = Vector3.zero;
            component.sizeDelta = new Vector2(0.11f, 0.03f) * GorillaLocomotion.Player.Instance.scale;
            component.localPosition = new Vector3(0.0493f, HomeButton.transform.localPosition.y / 3.59f, 0.118f - 0.84f / 2.55f + 0.021f);
            component.rotation = Quaternion.Euler(new Vector3(180f, 90f, 90f));

            if (outlinedMenu)
            {
                GameObject outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
                Destroy(outline.GetComponent<Collider>());
                outline.transform.parent = menu.transform;
                outline.transform.position = HomeButton.transform.position;
                outline.transform.rotation = HomeButton.transform.rotation;
                ColorChanger colorChanger = outline.AddComponent<ColorChanger>();
                colorChanger.colorInfo = rainbowOutline ? backgroundColor : backgroundColor1;
                colorChanger.Start();
                outline.transform.localScale = new Vector3(0.08f, 0.113f, 0.094f);
            }
        }

        public static void RecreateMenu()
        {
            if (menu != null)
            {
                Destroy(menu);
                menu = null;

                Initialization();
                RecenterMenu(rightHanded, UnityInput.Current.GetKey(keyboardButton));
            }
        }

        public static void RecenterMenu(bool isRightHanded, bool isKeyboardCondition)
        {
            if (!isKeyboardCondition)
            {
                if (!isRightHanded)
                {
                    menu.transform.position = GorillaTagger.Instance.leftHandTransform.position;
                    menu.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
                }
                else
                {
                    menu.transform.position = GorillaTagger.Instance.rightHandTransform.position;
                    Vector3 rotation = GorillaTagger.Instance.rightHandTransform.rotation.eulerAngles;
                    rotation += new Vector3(0f, 0f, 180f);
                    menu.transform.rotation = Quaternion.Euler(rotation);
                }
            }
            else
            {
                try
                {
                    TPC = GameObject.Find("Player Objects/Third Person Camera/Shoulder Camera").GetComponent<Camera>();
                }
                catch { }
                if (TPC != null)
                {
                    TPC.transform.position = new Vector3(-999f, -999f, -999f);
                    TPC.transform.rotation = Quaternion.identity;
                    GameObject bg = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    bg.transform.localScale = new Vector3(10f, 10f, 0.01f);
                    bg.transform.transform.position = TPC.transform.position + TPC.transform.forward;
                    bg.GetComponent<Renderer>().material.color = new Color32((byte)(backgroundColor.colors[0].color.r * 50), (byte)(backgroundColor.colors[0].color.g * 50), (byte)(backgroundColor.colors[0].color.b * 50), 255);
                    Destroy(bg, Time.deltaTime);
                    menu.transform.parent = TPC.transform;
                    menu.transform.position = (TPC.transform.position + (Vector3.Scale(TPC.transform.forward, new Vector3(0.5f, 0.5f, 0.5f)))) + (Vector3.Scale(TPC.transform.up, new Vector3(-0.02f, -0.02f, -0.02f)));
                    Vector3 rot = TPC.transform.rotation.eulerAngles;
                    rot = new Vector3(rot.x - 90, rot.y + 90, rot.z);
                    menu.transform.rotation = Quaternion.Euler(rot);

                    if (reference != null)
                    {
                        if (Mouse.current.leftButton.isPressed)
                        {
                            Ray ray = TPC.ScreenPointToRay(Mouse.current.position.ReadValue());
                            RaycastHit hit;
                            bool worked = Physics.Raycast(ray, out hit, 100);
                            if (worked)
                            {
                                Button collide = hit.transform.gameObject.GetComponent<Button>();
                                if (collide != null)
                                {
                                    collide.OnTriggerEnter(buttonCollider);
                                }
                            }
                        }
                        else
                        {
                            reference.transform.position = new Vector3(999f, -999f, -999f);
                        }
                    }
                }
            }
        }

        public static void CreateReference(bool isRightHanded)
        {
            reference = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            if (isRightHanded)
            {
                reference.transform.parent = GorillaTagger.Instance.leftHandTransform;
            }
            else
            {
                reference.transform.parent = GorillaTagger.Instance.rightHandTransform;
            }
            reference.GetComponent<Renderer>().material.color = backgroundColor.colors[0].color;
            reference.transform.localPosition = new Vector3(0f, -0.1f, 0f);
            reference.transform.localScale = new Vector3(0.01f, 0.01f, 0.01f);
            buttonCollider = reference.GetComponent<SphereCollider>();

            ColorChanger colorChanger = reference.AddComponent<ColorChanger>();
            colorChanger.colorInfo = backgroundColor;
            colorChanger.Start();
        }

        public static GradientColorKey[] GetSolidGradient(Color color)
        {
            return new GradientColorKey[] { new GradientColorKey(color, 0f), new GradientColorKey(color, 1f) };
        }

        public static ButtonInfo GetIndex(string buttonText)
        {
            foreach (ButtonInfo[] buttons in Menu.Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.buttonText == buttonText)
                    {
                        return button;
                    }
                }
            }
            return null;
        }

        public static Material TextureLoader(string url)
        {
            WebClient webClient = new WebClient();
            byte[] array = webClient.DownloadData(url);
            Material material = new Material(Shader.Find("GorillaTag/UberShader"));
            material.shaderKeywords = new string[]
            {
                "_USE_TEXTURE"
            };
            string text = Application.dataPath;
            text = text.Replace("/Gorilla Tag_Data", "");
            Texture2D texture2D = new Texture2D(4096, 4096);
            ImageConversion.LoadImage(texture2D, array);
            material.mainTexture = texture2D;
            texture2D.Apply();
            return material;
        }

        public static void Toggle(string buttonText)
        {
            if (buttonText == "_back_")
            {
                buttonsType = 0;
                InCategory = false;
                if (!ResetToDefault)
                {
                    pageNumber = SavePage;
                }
            }
            if (buttonText == "_leave_")
            {
                if (PhotonNetwork.InRoom)
                {
                    PhotonNetwork.Disconnect();
                }
            }
            if (buttonText == "_hop_")
            {
                ServerHop();
            }
            if (buttonText == "_discord_")
            {
                Process.Start("https://discord.gg/NsSwxqg8D2");
                Process.Start("https://github.com/Fevps/Rift");
            }
            if (buttonText == "_panic_")
            {
                DisableAllMods();
            }
            if (buttonText == "_next_")
            {
                pageNumber++;
                if (pageNumber > (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1)
                    pageNumber = 0;
                RecreateMenu();
            }
            if (buttonText == "_prev_")
            {
                pageNumber--;
                if (pageNumber < 0)
                    pageNumber = (buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage - 1;
                RecreateMenu();
            }
            int lastPage = ((buttons[buttonsType].Length + buttonsPerPage - 1) / buttonsPerPage) - 1;
            ButtonInfo target = GetIndex(buttonText);
            if (target != null)
            {
                if (target.isTogglable)
                {
                    target.enabled = !target.enabled;
                    if (target.enabled)
                    {
                        if (target.enableMethod != null)
                        {
                            try { target.enableMethod.Invoke(); } catch { }
                        }
                    }
                    else
                    {
                        if (target.disableMethod != null)
                        {
                            try { target.disableMethod.Invoke(); } catch { }
                        }
                    }
                }
                else
                {
                    if (target.method != null)
                    {
                        try { target.method.Invoke(); } catch { }
                    }
                }
            }
            RecreateMenu();
        }

        public static GameObject menu;
        public static GameObject back;
        public static GameObject outline;
        public static GameObject reference;
        public static GameObject canvasObject;

        public static GameObject button;

        public static GameObject HomeButton;

        public static SphereCollider buttonCollider;

        public static int pageNumber = 0;
        public static int buttonsType = 0;

        public static float j = 0f;
        public static float k = 0.2f;

        public static bool InCategory = false;

        public static VRRig copyrig = null;

        public static string RoomCode;

        public static int SavePage;
        public static bool ResetToDefault;

        public static bool menuBark = false;
        public static bool menuDrop = true;

        public static bool triggerPages = false;
        public static bool outlinedMenu = true;
        public static bool rainbowOutline = false;

        public static bool menuText = true;
        public static bool customBoard = true;

        public static GameObject TreeRoom = GameObject.Find("Environment Objects/LocalObjects_Prefab/TreeRoom");
        public static TMP_Text CodeOfConductText = GameObject.Find("COC Text").GetComponent<TMP_Text>();
        public static TMP_Text CodeOfConductTitle = GameObject.Find("CodeOfConduct").GetComponent<TMP_Text>();
        public static TMP_Text MessageDayText = GameObject.Find("motdtext").GetComponent<TMP_Text>();
        public static TMP_Text MessageDayTitle = GameObject.Find("motd (1)").GetComponent<TMP_Text>();

        public static Color32 MenuColor = new Color32(20, 20, 20, 1);
        public static Color32 ButtonColor = new Color32(50, 50, 50, 1);
        public static Color32 EnabledColor = new Color32(20, 20, 20, 1);
        public static Color32 TextColor = Color.white;
        public static Color32 EnabledTextColor = Color.red;
        public static Color32 TitleColor = Color.white;

        public static Color red = new Color32(255, 0, 0, 255);
        public static Color green = new Color32(0, 255, 0, 255);

        public static float rightT = ControllerInputPoller.TriggerFloat(XRNode.RightHand);
        public static Vector2 rjs = ControllerInputPoller.instance.rightControllerPrimary2DAxis;
        public static bool rightP = ControllerInputPoller.instance.rightControllerPrimaryButton;
        public static bool rightS = ControllerInputPoller.instance.rightControllerSecondaryButton;
        public static bool rightG = ControllerInputPoller.instance.rightGrab;

        public static float leftT = ControllerInputPoller.TriggerFloat(XRNode.LeftHand);
        public static Vector2 ljs = ControllerInputPoller.instance.leftControllerPrimary2DAxis;
        public static bool leftP = ControllerInputPoller.instance.leftControllerPrimaryButton;
        public static bool leftS = ControllerInputPoller.instance.leftControllerSecondaryButton;
        public static bool leftG = ControllerInputPoller.instance.leftGrab;

        public static string[] SpecialCosmetics = new string[] { "LBADE", "LBAGS", "LBACP", "LBAAK", "LBAAD" };

        public static string[] GhostCodes = new string[] { "ghost", "daisy", "daisy09", "09", "sren", "sren17", "pbbv", "echo", "statue", "spider", "tr33", "h3lp", "bot", "scary", "j3vu" };
        public static string[] YoutubeCodes = new string[] { "jman", "elliot", "k9", "gt", "famous", "mod", "modding" };
        public static string[] CompCodes = new string[] { "scrim", "1v1", "2v2", "scrims", "gtc", "cgt" };
        public static string[] Names = new string[] { "jimmy", "timmy", "dan", "spark", "gtagkid", "lunar", "monkey", "gorilla123" };

        public static string[] playerNames = new string[] {
            "monke", "shibagt", "banan", "vrchimp", "tagger", "goape", "chimpvr", "gibbon", 
            "apeking", "banvr", "gorilvr", "jumpmon", "proape", "banmon", "vrpro", "climbvr", 
            "speedmon", "kingape", "gorivive", "taggod", "tagchimp", "chimpgod", "apeboss", 
            "vrmonkey", "vrrunner", "apeclimb", "tagpro", "tagninja", "vrninja", "monkvr", 
            "runape", "tagape", "jumpape", "speedrun", "apeclan", "chimpman", "ninjagor", 
            "jumpgor", "swingape", "bananaqt", "monkqt", "apeqt", "banqt", "banapewr", 
            "cheato", "vrking", "cheatvr", "gorunner", "monketag", "chimprun", "vrrush", 
            "rushvr", "bananaxd", "rusher", "vrrunner", "apevr", "tagrush", "swingvr", 
            "cheaterg", "apejump", "runchimp", "vrcheato", "tagmaster", "vrcheater", 
            "proclimb", "chillmon", "vrmonq", "banqvr", "monkeban", "monrush", "rungoap", 
            "taglord", "vrswing", "bananav", "chimpqt", "monkbro", "banmonk", "runbro", 
            "cheatermon", "kingban", "gorilltag", "taggor", "swingmon", "monkeypro", 
            "tagprovr", "apekingvr", "cheatape", "kingchimp", "vrpwner", "tagboss", "banjr", 
            "tagclimb", "cheatqt", "banagor", "banavr", "monch", "prochimp", "rushking", 
            "vrban", "speedchimp"
        };

        public static string[] simpleNames = new string[] {
            "John", "William", "James", "Robert", "Michael", "David", "Richard", "Charles",
            "Joseph", "Thomas", "Christopher", "Daniel", "Paul", "Mark", "Donald", "George",
            "Kenneth", "Steven", "Edward", "Brian", "Ronald", "Anthony", "Kevin", "Jason",
            "Matthew", "Gary", "Timothy", "Jose", "Larry", "Jeffrey", "Frank", "Scott",
            "Eric", "Stephen", "Andrew", "Raymond", "Gregory", "Joshua", "Jerry", "Dennis",
            "Walter", "Patrick", "Peter", "Harold", "Douglas", "Henry", "Carl", "Arthur",
            "Ryan", "Roger", "Joe", "Jack", "Albert", "Jonathan", "Justin", "Terry",
            "Gerald", "Keith", "Samuel", "Willie", "Ralph", "Lawrence", "Nicholas",
            "Roy", "Benjamin", "Bruce", "Brandon", "Adam", "Harry", "Fred", "Wayne",
            "Billy", "Steve", "Louis", "Jeremy", "Aaron", "Randy", "Howard", "Eugene",
            "Carlos", "Russell", "Bobby", "Victor", "Martin", "Ernest", "Phillip",
            "Todd", "Jesse", "Craig", "Alan", "Shawn", "Clarence", "Sean", "Philip",
            "Chris", "Johnny", "Earl", "Jimmy", "Antonio", "Danny", "Bryan", "Tony"
        };

	    public static GameObject Forest = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest");
        public static GameObject City = GameObject.Find("Environment Objects/LocalObjects_Prefab/City");
        public static GameObject Canyon = GameObject.Find("Environment Objects/LocalObjects_Prefab/Canyon");
        public static GameObject Cave = GameObject.Find("Environment Objects/LocalObjects_Prefab/Cave_Main_Prefab");
        public static GameObject Mountain = GameObject.Find("Environment Objects/LocalObjects_Prefab/Mountain");
        public static GameObject Clouds = GameObject.Find("Environment Objects/LocalObjects_Prefab/skyjungle");
        //public static GameObject CloudsBottom = GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest/Sky Jungle Bottom (1)/CloudSmall (22)");
        public static GameObject Beach = GameObject.Find("Environment Objects/LocalObjects_Prefab/Beach");
        //public static GameObject BeachThing = GameObject.Find("Environment Objects/LocalObjects_Prefab/ForestToBeach");
        public static GameObject Basement = GameObject.Find("Environment Objects/LocalObjects_Prefab/Basement");

        public static string[] PrivateCode = new string[] { "6825", "9866", "1095", "9582", "0969", "7969", "1245", "1059", "2589", "2308", "0966", "3095", "2395" };

        public static string coc = $"Free use, competitive, frequent updates, high fps, customizable menu, Rift.\n\n\n\n\n\n\n\n\n\n\n\n\n     https://github.com/Fevps/Rift";
        public static string news = "We are very excited to tell you guys 2.0.0 is coming sooner than expected,\nyou guys are expected to get the 2.0.0 update before 11/30/24!\n\nThank you all for the support.";

        public static string MENUSTATUS = "UNDETECTED";

        public static GameObject GunPointer = null;
        public static GameObject GunTracer = null;
        public static RaycastHit GunHit;
        public static bool isLocking = false;
        public static VRRig copyingRig = null;

        public static bool arrayTrue = true;
        public static bool guibld = true;
        public static bool GUIEnabled = true;
        public static int wrigj4wgj = 0;

        public static GameObject gameobject = null;

        public static Vector3 originalPos;
        public static Quaternion originalRot;
        public static float originalFoV;
        public static GameObject tpc = GameObject.Find("Shoulder Camera");
        public static Camera TPC = tpc.GetComponent<Camera>();

        public static float jumpMultiplier = 6.5f;
        public static float jumpSpeed = 1.1f;
        public static int jumpCount = 1;
        public static int slideCount;
        public static float slideControl = 1;
        public static int chosenSound = 66;
        public static int platCount = 1;
        public static bool rplatEnabled = false;
        public static bool lplatEnabled = false;
        public static bool sticky = false;
    }
}