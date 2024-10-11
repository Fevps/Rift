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
using static StupidTemplate.Menu.Mods.Movement;
using static StupidTemplate.Menu.Mods.Visuals;
using static StupidTemplate.Menu.Mods.Safety;

using static StupidTemplate.Menu.Mods.Settings;

using UnityEngine.XR;
using StupidTemplate.Menu.Mods;
using System.Net;
using TMPro;
using GorillaNetworking;
using GorillaLocomotion.Gameplay;
using static UnityEngine.Rendering.DebugUI;

namespace StupidTemplate.Menu.Mods
{
    internal class Settings
    {
        private static string Saved_Values = "Rift_Preferences\\Saved_Values";
        private static string Saved_Bools = "Rift_Preferences\\Saved_Bools";
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
                    if (!Directory.Exists(Saved_Values))
                    {
                        Directory.CreateDirectory(Saved_Values);
                        SavePreferences();
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (!File.Exists(Saved_Values + "\\jumpCount"))
                            {
                                using (File.Create(Saved_Values + "\\jumpCount")) { }
                            }
                            File.WriteAllText(Saved_Values + "\\jumpCount", jumpCount.ToString());

                            if (!File.Exists(Saved_Values + "\\slideCount"))
                            {
                                using (File.Create(Saved_Values + "\\slideCount")) { }
                            }
                            File.WriteAllText(Saved_Values + "\\slideCount", slideCount.ToString());

                            if (!File.Exists(Saved_Values + "\\timeCount"))
                            {
                                using (File.Create(Saved_Values + "\\timeCount")) { }
                            }
                            File.WriteAllText(Saved_Values + "\\timeCount", timeMonicle.ToString());

                            if (!File.Exists(Saved_Values + "\\themeCount"))
                            {
                                using (File.Create(Saved_Values + "\\themeCount")) { }
                            }
                            File.WriteAllText(Saved_Values + "\\themeCount", theme.ToString());

                            if (!File.Exists(Saved_Values + "\\soundCount"))
                            {
                                using (File.Create(Saved_Values + "\\soundCount")) { }
                            }
                            File.WriteAllText(Saved_Values + "\\soundCount", sound.ToString());
                        }
                        catch (Exception ex)
                        {
                            NotifiLib.SendNotification($"<color=red>ERROR</color>: {ex}");
                            return;
                        }
                    }
                    if (!Directory.Exists(Saved_Bools))
                    {
                        Directory.CreateDirectory(Saved_Bools);
                        SavePreferences();
                        return;
                    }
                    else
                    {
                        try
                        {
                            if (!File.Exists(Saved_Bools + "\\invisui"))
                            {
                                using (File.Create(Saved_Bools + "\\invisui")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\invisui", menuBark.ToString());

                            if (!File.Exists(Saved_Bools + "\\menudrop"))
                            {
                                using (File.Create(Saved_Bools + "\\menudrop")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\menudrop", menuDrop.ToString());

                            if (!File.Exists(Saved_Bools + "\\triggerpages"))
                            {
                                using (File.Create(Saved_Bools + "\\triggerpages")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\triggerpages", triggerPages.ToString());

                            if (!File.Exists(Saved_Bools + "\\outlinedmenu"))
                            {
                                using (File.Create(Saved_Bools + "\\outlinedmenu")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\outlinedmenu", outlinedMenu.ToString());

                            if (!File.Exists(Saved_Bools + "\\rainbowoutline"))
                            {
                                using (File.Create(Saved_Bools + "\\rainbowoutline")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\rainbowoutline", rainbowOutline.ToString());

                            if (!File.Exists(Saved_Bools + "\\menutext"))
                            {
                                using (File.Create(Saved_Bools + "\\menutext")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\menutext", menuText.ToString());

                            if (!File.Exists(Saved_Bools + "\\customboard"))
                            {
                                using (File.Create(Saved_Bools + "\\customboard")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\customboard", customBoard.ToString());

                            if (!File.Exists(Saved_Bools + "\\stickyplats"))
                            {
                                using (File.Create(Saved_Bools + "\\stickyplats")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\stickyplats", sticky.ToString());

                            if (!File.Exists(Saved_Bools + "\\righthandmenu"))
                            {
                                using (File.Create(Saved_Bools + "\\righthandmenu")) { }
                            }
                            File.WriteAllText(Saved_Bools + "\\righthandmenu", rightHanded.ToString());
                        }
                        catch (Exception ex)
                        {
                            NotifiLib.SendNotification($"<color=red>ERROR</color>: {ex}");
                            return;
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
                    if (File.Exists(Saved_Values + "\\jumpCount"))
                    {
                        string floatValue = File.ReadAllText(Saved_Values + "\\jumpCount");
                        if (float.TryParse(floatValue, out float loadedFloat))
                        {
                            jumpCount = (int)loadedFloat - 1;
                            SpeedChanger();
                        }
                    }
                    if (File.Exists(Saved_Values + "\\slideCount"))
                    {
                        string floatValue = File.ReadAllText(Saved_Values + "\\slideCount");
                        if (float.TryParse(floatValue, out float loadedFloat))
                        {
                            slideCount = (int)loadedFloat - 1;
                            SlideChanger();
                        }
                    }
                    if (File.Exists(Saved_Values + "\\timeCount"))
                    {
                        string floatValue = File.ReadAllText(Saved_Values + "\\timeCount");
                        if (float.TryParse(floatValue, out float loadedFloat))
                        {
                            timeMonicle = (int)loadedFloat - 1;
                            TimeChanger();
                        }
                    }
                    if (File.Exists(Saved_Values + "\\themeCount"))
                    {
                        string floatValue = File.ReadAllText(Saved_Values + "\\themeCount");
                        if (float.TryParse(floatValue, out float loadedFloat))
                        {
                            theme = (int)loadedFloat - 1;
                            ChangeTheme();
                        }
                    }
                    if (File.Exists(Saved_Values + "\\soundCount"))
                    {
                        string floatValue = File.ReadAllText(Saved_Values + "\\soundCount");
                        if (float.TryParse(floatValue, out float loadedFloat))
                        {
                            sound = (int)loadedFloat - 1;
                            ChangeSound();
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\invisui"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\invisui");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            menuBark = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\menudrop"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\menudrop");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            menuDrop = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\triggerpages"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\triggerpages");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            triggerPages = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\outlinedmenu"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\outlinedmenu");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            outlinedMenu = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\rainbowoutline"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\rainbowoutline");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            rainbowOutline = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\menutext"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\menutext");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            menuText = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\customboard"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\customboard");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            customBoard = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\stickyplats"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\stickyplats");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            sticky = loadedBool;
                        }
                    }
                    if (File.Exists(Saved_Bools + "\\righthandmenu"))
                    {
                        string boolValue = File.ReadAllText(Saved_Bools + "\\righthandmenu");
                        if (bool.TryParse(boolValue, out bool loadedBool))
                        {
                            rightHanded = loadedBool;
                        }
                    }
                }
                catch (Exception ex)
                {
                    NotifiLib.SendNotification($"<color=red>ERROR</color>: {ex}");
                }
                RecreateMenu();
            }
            catch { LoadPreferences(); return; }
        }

        public static void ResetPreferences()
        {
            try
            {
                File.Delete(Saved_Values + "\\jumpCount");
                File.Delete(Saved_Values + "\\slideCount");
                File.Delete(Saved_Values + "\\timeCount");
                File.Delete(Saved_Values + "\\themeCount");
                File.Delete(Saved_Values + "\\soundCount");

                File.Delete(Saved_Bools + "\\invisui");
                File.Delete(Saved_Bools + "\\menudrop");
                File.Delete(Saved_Bools + "\\triggerpages");
                File.Delete(Saved_Bools + "\\outlinedmenu");
                File.Delete(Saved_Bools + "\\rainbowoutline");
                File.Delete(Saved_Bools + "\\menutext");
                File.Delete(Saved_Bools + "\\customboard");
            }
            catch { }
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

        public static int theme = 1;
        public static void ChangeTheme()
        {
            theme++;
            if (theme > 10)
            {
                theme = 1;
            }
            if (theme == 1)
            {
                MenuColor = new Color32(20, 20, 20, 1);
                ButtonColor = new Color32(50, 50, 50, 1);
                EnabledColor = new Color32(20, 20, 20, 1);
                TextColor = Color.white;
                EnabledTextColor = Color.red;
                TitleColor = Color.white;
            }
            if (theme == 2)
            {
                MenuColor = Color.yellow;
                ButtonColor = Color.black;
                EnabledColor = Color.yellow;
                TextColor = Color.white;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 3)
            {
                MenuColor = new Color32(255, 228, 225, 255);
                ButtonColor = new Color32(255, 105, 180, 255);
                EnabledColor = new Color32(255, 20, 147, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 4)
            {
                MenuColor = new Color32(224, 255, 255, 255);
                ButtonColor = new Color32(70, 130, 180, 255);
                EnabledColor = new Color32(135, 206, 250, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 5)
            {
                MenuColor = new Color32(240, 255, 240, 255);
                ButtonColor = new Color32(34, 139, 34, 255);
                EnabledColor = new Color32(144, 238, 144, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 6)
            {
                MenuColor = new Color32(255, 250, 205, 255);
                ButtonColor = new Color32(255, 165, 0, 255);
                EnabledColor = new Color32(255, 69, 0, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.white;
                TitleColor = Color.black;
            }
            if (theme == 7)
            {
                MenuColor = new Color32(230, 230, 250, 255);
                ButtonColor = new Color32(128, 0, 128, 255);
                EnabledColor = new Color32(75, 0, 130, 255);
                TextColor = Color.white;
                EnabledTextColor = Color.yellow;
                TitleColor = Color.black;
            }
            if (theme == 8)
            {
                MenuColor = new Color32(240, 240, 240, 255);
                ButtonColor = new Color32(105, 105, 105, 255);
                EnabledColor = new Color32(169, 169, 169, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 9)
            {
                MenuColor = new Color32(255, 255, 224, 255);
                ButtonColor = new Color32(255, 255, 0, 255);
                EnabledColor = new Color32(255, 255, 224, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.black;
                TitleColor = Color.black;
            }
            if (theme == 10)
            {
                MenuColor = new Color32(224, 255, 255, 255);
                ButtonColor = new Color32(224, 255, 255, 255);
                EnabledColor = new Color32(0, 191, 255, 255);
                TextColor = Color.black;
                EnabledTextColor = Color.grey;
                TitleColor = Color.black;
            }
            RecreateMenu();
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
