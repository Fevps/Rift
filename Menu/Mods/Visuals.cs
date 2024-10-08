using BepInEx;
using HarmonyLib;
using Photon.Pun;
using System;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using UnityEngine.Rendering;
using UnityEngine.UIElements;
using static GorillaTelemetry;
using static StupidTemplate.Settings;
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

using static StupidTemplate.Menu.Main;
using static StupidTemplate.Menu.Mods.Settings;

using UnityEngine.XR;
using StupidTemplate.Menu.Mods;
using System.Net;
using TMPro;
using GorillaNetworking;
using System.Collections.Generic;
using Photon.Realtime;
using Photon.Pun.UtilityScripts;

namespace StupidTemplate.Menu.Mods
{
    internal class Visuals
    {
        public static LightmapsMode brooksucksmypp;
        public static void FullBright()
        {
            brooksucksmypp = LightmapSettings.lightmapsMode;
            LightmapSettings.lightmapsMode = LightmapsMode.NonDirectional;
        }

        public static void FixBright()
        {
            LightmapSettings.lightmapsMode = brooksucksmypp;
        }

        public static void Xray()
        {
            try
            {
                foreach (GameObject friedchicken in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    Renderer renderer = friedchicken.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            Color color = mat.color;
                            color.a = 0.5f;
                            mat.color = color;
                            mat.SetFloat("_Mode", 3);
                            mat.SetInt("_SrcBlend", (int)(BlendMode)5);
                            mat.SetInt("_DstBlend", (int)(BlendMode)10);
                            mat.SetInt("_ZWrite", 0);
                            mat.renderQueue = 3000;
                        }
                    }
                }
            }
            catch
            {
                NotifiLib.SendNotification("Your game is broken, reload the mod/game to fix errors.");
            }
        }

        public static void DisableXray()
        {
            try
            {
                foreach (GameObject friedchicken in Resources.FindObjectsOfTypeAll(typeof(GameObject)))
                {
                    Renderer renderer = friedchicken.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        foreach (Material mat in renderer.materials)
                        {
                            Color color = mat.color;
                            color.a = 1.0f;
                            mat.color = color;
                            mat.SetFloat("_Mode", 0);
                            mat.SetInt("_SrcBlend", (int)(BlendMode)1);
                            mat.SetInt("_DstBlend", (int)(BlendMode)0);
                            mat.SetInt("_ZWrite", 1);
                            mat.renderQueue = -1;
                        }
                    }
                }
            }
            catch
            {
                NotifiLib.SendNotification("Your game is broken, reload the mod/game to fix errors.");
            }
        }

        public static void NoLeaves()
        {
            foreach (GameObject gameObject in Resources.FindObjectsOfTypeAll<GameObject>())
            {
                if (gameObject.name.Contains("leaves"))
                {
                    gameObject.SetActive(false);
                }
            }
        }

        public static void LaunchRocket()
        {
            GameObject.Find("Environment Objects/LocalObjects_Prefab/City/CosmeticsRoomAnchor/RocketShip_IdleDummy").SetActive(false);
            ScheduledTimelinePlayer.FindObjectOfType<ScheduledTimelinePlayer>().timeline.Play();
        }

        public static void InfectionHitboxes()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateBox(Infected(player) ? red : player.playerColor, player);
                    }
                }
            }
        }

        public static void CasualHitboxes()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateBox(player.playerColor, player);
                    }
                }
            }
        }

        public static void CompetitiveHitboxes()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateBox(Infected(player) ? red : green, player);
                    }
                }
            }
        }

        public static void InfectionTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(new Vector3(GorillaTagger.Instance.headCollider.transform.position.x, GorillaTagger.Instance.headCollider.transform.position.y + 0.3f, GorillaTagger.Instance.headCollider.transform.position.z), player.transform.position, Infected(player) ? red : player.playerColor, 0.007f);
                    }
                }
            }
        }

        public static void CasualTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(new Vector3(GorillaTagger.Instance.headCollider.transform.position.x, GorillaTagger.Instance.headCollider.transform.position.y + 0.3f, GorillaTagger.Instance.headCollider.transform.position.z), player.transform.position, player.playerColor, 0.007f);
                    }
                }
            }
        }

        public static void CompetitiveTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(new Vector3(GorillaTagger.Instance.headCollider.transform.position.x, GorillaTagger.Instance.headCollider.transform.position.y + 0.3f, GorillaTagger.Instance.headCollider.transform.position.z), player.transform.position, Infected(player) ? red : green, 0.007f);
                    }
                }
            }
        }

        public static void InfectionHandTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.transform.position + new Vector3(-0.05f, 0.1f, 0.1f), player.transform.position, Infected(player) ? red : player.playerColor, 0.007f);
                    }
                }
            }
        }

        public static void CasualHandTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.transform.position + new Vector3(-0.05f, 0.1f, 0.1f), player.transform.position, player.playerColor, 0.007f);
                    }
                }
            }
        }

        public static void CompetitiveHandTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.transform.position + new Vector3(-0.05f, 0.1f, 0.1f), player.transform.position, Infected(player) ? red : green, 0.007f);
                    }
                }
            }
        }

        public static void InfectionBeacons()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(player.transform.position + new Vector3(0, 100, 0), player.transform.position + new Vector3(0, -100, 0), Infected(player) ? red : player.playerColor, 0.025f);
                    }
                }
            }
        }

        public static void CasualBeacons()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(player.transform.position + new Vector3(0, 100, 0), player.transform.position + new Vector3(0, -100, 0), player.playerColor, 0.025f);
                    }
                }
            }
        }

        public static void CompetitiveBeacons()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(player.transform.position + new Vector3(0, 100, 0), player.transform.position + new Vector3(0, -100, 0), Infected(player) ? red : green, 0.025f);
                    }
                }
            }
        }

        public static void CasualChams()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (!i.isOfflineVRRig)
                    {
                        i.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                    }
                }
            }
        }

        public static void InfectionChams()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (!i.isOfflineVRRig)
                    {
                        i.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        if (Infected(i))
                        {
                            i.playerColor = new Color(255, 0, 0, 0.3f);
                        }
                    }
                }
            }
        }

        public static void CompetitiveChams()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (!i.isOfflineVRRig)
                    {
                        i.mainSkin.material.shader = Shader.Find("GUI/Text Shader");
                        if (Infected(i))
                        {
                            i.playerColor = new Color(255, 0, 0, 0.3f);
                        }
                        else
                        {
                            i.playerColor = new Color(0, 255, 0, 0.3f);
                        }
                    }
                }
            }
        }

        public static void UndoChams()
        {
            foreach (VRRig i in GorillaParent.instance.vrrigs)
            {
                if (i != GorillaTagger.Instance.offlineVRRig)
                {
                    i.mainSkin.material.shader = Shader.Find("GorillaTag/UberShader");
                }
            }
        }

        public static void InfectionTextEsp()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(Infected(player) ? red : player.playerColor, player.transform.position + new Vector3(0, .3f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void CasualTextEsp()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(player.playerColor, player.transform.position + new Vector3(0, .3f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void CompetitiveTextEsp()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(Infected(player) ? red : green, player.transform.position + new Vector3(0, .3f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void InfectionNametags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(Infected(player) ? red : player.playerColor, player.transform.position + new Vector3(0, .4f, 0), player.playerNameVisible.ToUpper());
                    }
                }
            }
        }

        public static void CasualNametags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(player.playerColor, player.transform.position + new Vector3(0, .4f, 0), player.playerNameVisible.ToUpper());
                    }
                }
            }
        }

        public static void CompetitiveNametags()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(Infected(player) ? red : green, player.transform.position + new Vector3(0, .4f, 0), player.playerNameVisible.ToUpper());
                    }
                }
            }
        }

        public static void CasualInformation()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig rig = GetVRRigFromPlayer(player);
                    CreateTextObject(rig.playerColor, rig.transform.position + new Vector3(0, .6f, 0), 10, $"Name: {rig.playerNameVisible}\nId: {player.UserId}\nColor: {configuringSkinCode(1, rig)} {configuringSkinCode(2, rig)} {configuringSkinCode(3, rig)}\n{GetMasterINFORMATION(player)}".ToUpper(), TextAlignment.Left);
                }
            }
        }

        public static void InfectionInformation()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig rig = GetVRRigFromPlayer(player);
                    CreateTextObject(Infected(rig) ? red : rig.playerColor, rig.transform.position + new Vector3(0, .6f, 0), 10, $"Name: {rig.playerNameVisible}\nId: {player.UserId}\nColor: {configuringSkinCode(1, rig)} {configuringSkinCode(2, rig)} {configuringSkinCode(3, rig)}\n{GetMasterINFORMATION(player)}".ToUpper(), TextAlignment.Left);
                }
            }
        }

        public static void CompetitiveInformation()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (Player player in PhotonNetwork.PlayerListOthers)
                {
                    VRRig rig = GetVRRigFromPlayer(player);
                    CreateTextObject(Infected(rig) ? red : green, rig.transform.position + new Vector3(0, .6f, 0), 10, $"Name: {rig.playerNameVisible}\nId: {player.UserId}\nColor: {configuringSkinCode(1, rig)} {configuringSkinCode(2, rig)} {configuringSkinCode(3, rig)}\n{GetMasterINFORMATION(player)}".ToUpper(), TextAlignment.Left);
                }
            }
        }

        public static void CasualDistanceTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.position, player.transform.position, player.playerColor, 0.007f);
                        CreateTextObjectNoBlur(player.playerColor, player.transform.position + new Vector3(0, .1f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void InfectionDistanceTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.position, player.transform.position, Infected(player) ? red : player.playerColor, 0.007f);
                        CreateTextObjectNoBlur(Infected(player) ? red : player.playerColor, player.transform.position + new Vector3(0, .1f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void CompetitiveDistanceTracers()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTracer(GorillaTagger.Instance.leftHandTransform.position, player.transform.position, Infected(player) ? red : green, 0.007f);
                        CreateTextObjectNoBlur(Infected(player) ? red : green, player.transform.position + new Vector3(0, .1f, 0), Vector3.Distance(Camera.main.transform.position, player.transform.position).ToString("F1"));
                    }
                }
            }
        }

        public static void CasualSpeedometer()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(player.playerColor, player.transform.position + new Vector3(0, .3f, 0), $"X{player.GetComponent<Rigidbody>().velocity.x.ToString("F1")} Z{player.GetComponent<Rigidbody>().velocity.z.ToString("F1")}");
                    }
                }
            }
        }

        public static void InfectionSpeedometer()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(player.playerColor, player.transform.position + new Vector3(0, .3f, 0), $"X{player.GetComponent<Rigidbody>().velocity.x.ToString("F1")} Z{player.GetComponent<Rigidbody>().velocity.z.ToString("F1")}");
                    }
                }
            }
        }

        public static void CompetitiveSpeedometer()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        CreateTextObject(player.playerColor, player.transform.position + new Vector3(0, .3f, 0), $"X{player.GetComponent<Rigidbody>().velocity.x.ToString("F1")} Z{player.GetComponent<Rigidbody>().velocity.z.ToString("F1")}");
                    }
                }
            }
        }
    }
}
