using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using UnityEngine.InputSystem;
using UnityEngine;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Settings;
using BepInEx;

using HarmonyLib;
using Photon.Pun;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static GorillaTelemetry;
using static StupidTemplate.Menu.Buttons;
using static StupidTemplate.Menu.Optimization;

using static StupidTemplate.Menu.Mods.Advantages;
using static StupidTemplate.Menu.Mods.Current;
using static StupidTemplate.Menu.Mods.Fun;
using static StupidTemplate.Menu.Mods.Minecraft;
using static StupidTemplate.Menu.Mods.Miscellaneous;
using static StupidTemplate.Menu.Mods.Movement;
using static StupidTemplate.Menu.Mods.Rig;
using static StupidTemplate.Menu.Mods.Visuals;
using static StupidTemplate.Menu.Mods.Safety;

using static StupidTemplate.Menu.Mods.Settings;

using UnityEngine.XR;
using StupidTemplate.Menu.Mods;
using System.Net;
using TMPro;
using GorillaNetworking;
using GorillaLocomotion.Gameplay;

namespace StupidTemplate.Menu.Mods
{
    internal class Settings
    {
        public static void SavePreferences()
        {
            try
            {
                if (!Directory.Exists("Rift_Preferences"))
                {
                    Directory.CreateDirectory("Rift_Preferences");
                    SavePreferences();
                    return;
                }
                else
                {
                    if (!File.Exists("Rift_Preferences\\Saved_Mods"))
                    {
                        File.Create("Rift_Preferences\\Saved_Mods");
                        SavePreferences();
                        return;
                    }
                    else
                    {
                        foreach (ButtonInfo[] buttons in Buttons.buttons)
                        {
                            foreach (ButtonInfo method in buttons)
                            {
                                if (method.enabled && !addedbuttons.Contains(method.buttonText))
                                {
                                    addedbuttons += $"{method.buttonText}\n";
                                    if (method != null)
                                    {
                                        File.WriteAllText("Rift_Preferences\\Saved_Mods", addedbuttons);
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { SavePreferences(); return; }
        }

        static string addedbuttons = "";
        public static void LoadPreferences()
        {
            try
            {
                foreach (ButtonInfo[] buttons in Buttons.buttons)
                {
                    foreach (ButtonInfo method in buttons)
                    {
                        string[] enabled = File.ReadAllLines("Rift_Preferences\\Saved_Mods");
                        if (enabled.Contains(method.buttonText))
                        {
                            method.enabled = true;
                        }
                    }
                }
                RecreateMenu();
            }
            catch { LoadPreferences(); return; }
        }

        static float debouncer = 1f;
        public static void Screenshot()
        {
            if ((rightG || Mouse.current.rightButton.isPressed) && debouncer < Time.time)
            {
                SteamScreenshots.TriggerScreenshot();
                debouncer = Time.time + 0.5f;
            }
        }

        static Vector3 originalPos;
        static Quaternion originalRot;
        static float originalFoV;
        public static GameObject tpc = GameObject.Find("Shoulder Camera");
        public static Camera TPC = tpc.GetComponent<Camera>();
        public static void FPC()
        {
            bool keyboardOpen = UnityInput.Current.GetKey(keyboardButton);
            if (tpc != null && !keyboardOpen)
            {
                originalPos = tpc.transform.position;
                originalRot = tpc.transform.rotation;
                originalFoV = TPC.fieldOfView;

                tpc.transform.position = GorillaTagger.Instance.headCollider.transform.position;
                tpc.transform.rotation = GorillaTagger.Instance.headCollider.transform.rotation;
                TPC.fieldOfView = 130;
            }
        }

        public static void NoFPC()
        {
            tpc.transform.position = originalPos;
            tpc.transform.rotation = originalRot;
            TPC.fieldOfView = originalFoV;
        }

        public static int timeMonicle = 1;
        public static void TimeChanger()
        {
            timeMonicle++;
            if (timeMonicle > 10)
                timeMonicle = 1;
            if (timeMonicle == 1)
                BetterDayNightManager.instance.SetTimeOfDay(1);
            if (timeMonicle == 2)
                BetterDayNightManager.instance.SetTimeOfDay(2);
            if (timeMonicle == 3)
                BetterDayNightManager.instance.SetTimeOfDay(3);
            if (timeMonicle == 4)
                BetterDayNightManager.instance.SetTimeOfDay(4);
            if (timeMonicle == 5)
                BetterDayNightManager.instance.SetTimeOfDay(5);
            if (timeMonicle == 6)
                BetterDayNightManager.instance.SetTimeOfDay(6);
            if (timeMonicle == 7)
                BetterDayNightManager.instance.SetTimeOfDay(7);
            if (timeMonicle == 8)
                BetterDayNightManager.instance.SetTimeOfDay(8);
            if (timeMonicle == 9)
                BetterDayNightManager.instance.SetTimeOfDay(9);
            if (timeMonicle == 10)
                BetterDayNightManager.instance.SetTimeOfDay(10);
        }

        private static int sound = 0;
        public static int chosenSound = 66;
        public static void ChangeSound()
        {
            sound++;
            if (sound > 10)
            {
                sound = 0;
            }
            if (sound == 0)
            {
                GetIndex("Choose Sound^").overlapText = "Choose Sound^";
                chosenSound = 66;
            }
            if (sound == 1)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 1";
                chosenSound = 5;
            }
            if (sound == 2)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 2";
                chosenSound = 88;
            }
            if (sound == 3)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 3";
                chosenSound = 58;
            }
            if (sound == 4)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 4";
                chosenSound = 103;
            }
            if (sound == 5)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 5";
                chosenSound = 210;
            }
            if (sound == 6)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 6";
                chosenSound = 62;
            }
            if (sound == 7)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 7";
                chosenSound = 90;
            }
            if (sound == 8)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 8";
                chosenSound = 34;
            }
            if (sound == 9)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 9";
                chosenSound = 85;
            }
            if (sound == 10)
            {
                GetIndex("Choose Sound^").overlapText = "Sound 10";
                chosenSound = 24;
            }
        }

        public static void StartRaining()
        {
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.Raining;
            }
        }

        public static void StopRaining()
        {
            for (int i = 1; i < BetterDayNightManager.instance.weatherCycle.Length; i++)
            {
                BetterDayNightManager.instance.weatherCycle[i] = BetterDayNightManager.WeatherType.None;
            }
        }

        public static float jumpMultiplier = 6.5f;

        public static float jumpSpeed = 1.1f;

        public static int jumpCount = 1;

        public static int slideCount;

        public static float slideControl = 1;

        public static void SpeedChanger()
        {
            jumpCount++;
            if (jumpCount > 4)
            {
                jumpCount = 1;
            }
            if (jumpCount == 1)
            {
                jumpMultiplier = 6.5f;
                jumpSpeed = 1.1f;

                GetIndex("Speed Changer [Normal]").overlapText = "Speed Changer [Normal]";
            }
            if (jumpCount == 2)
            {
                jumpMultiplier = 7.25f;
                jumpSpeed = 1.2f;

                GetIndex("Speed Changer [Normal]").overlapText = "Speed Changer [Mosa]";
            }
            if (jumpCount == 3)
            {
                jumpMultiplier = 8f;
                jumpSpeed = 1.3f;

                GetIndex("Speed Changer [Normal]").overlapText = "Speed Changer [Intermediate]";
            }
            if (jumpCount == 4)
            {
                jumpMultiplier = 8.5f;
                jumpSpeed = 1.45f;

                GetIndex("Speed Changer [Normal]").overlapText = "Speed Changer [Expert]";
            }
            if (jumpCount == 5)
            {
                jumpMultiplier = 9f;
                jumpSpeed = 1.6f;
            }
            RecreateMenu();
        }

        public static void SlideChanger()
        {
            slideCount++;
            if (slideCount > 5)
            {
                slideCount = 1;
            }
            if (slideCount == 1)
            {
                slideControl = 0.00425f;

                GetIndex("Slide Changer [Normal]").overlapText = "Slide Changer [Normal]";
            }
            if (slideCount == 2)
            {
                slideControl = 0.1f;

                GetIndex("Slide Changer [Normal]").overlapText = "Slide Changer [Increased]";
            }
            if (slideCount == 3)
            {
                slideControl = 1f;

                GetIndex("Slide Changer [Normal]").overlapText = "Slide Changer [Intermediate]";
            }
            if (slideCount == 4)
            {
                slideControl = 10f;

                GetIndex("Slide Changer [Normal]").overlapText = "Slide Changer [High]";
            }
            if (slideCount == 5)
            {
                slideControl = 0.0001f;

                GetIndex("Slide Changer [Normal]").overlapText = "Slide Changer [Low]";
            }
            RecreateMenu();
        }

        public static void DisableAllMods()
        {
            foreach (ButtonInfo[] buttons in Buttons.buttons)
            {
                foreach (ButtonInfo button in buttons)
                {
                    if (button.enabled)
                    {
                        Toggle(button.buttonText);
                    }
                }
            }
        }

        public static void AcceptTermsofService()
        {
            foreach (GameObject objects in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (objects.name.Contains("PrivateUIRoom"))
                {
                    objects.SetActive(false);
                }
            }
        }

        public static void DisableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(false);
        }

        public static void EnableNetworkTriggers()
        {
            GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab").SetActive(true);
        }

        public static void ForceLag(bool grip)
        {
            if (grip)
            {
                if (rightG)
                {
                    foreach (GameObject i in Resources.FindObjectsOfTypeAll<GameObject>()) { }
                }
            }
            else
            {
                foreach (GameObject i in Resources.FindObjectsOfTypeAll<GameObject>()) { }
            }
        }
    }
}
