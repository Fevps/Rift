using BepInEx;
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

using static StupidTemplate.Menu.Main;
using static StupidTemplate.Menu.Mods.Settings;

using UnityEngine.XR;
using StupidTemplate.Menu.Mods;
using System.Net;
using TMPro;
using GorillaNetworking;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Photon.Realtime;

namespace StupidTemplate.Menu.Mods
{
    internal class Current
    {
        public static void PDisconnect()
        {
            if (leftP || rightP)
                PhotonNetwork.Disconnect();
        }

        public static void ServerHop()
        {
            if (PhotonNetwork.InRoom)
            {
                PhotonNetwork.Disconnect();
                if (!PhotonNetwork.InRoom)
                {
                    if (GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").activeSelf == true)
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    if (GameObject.Find("Environment Objects/LocalObjects_Prefab/City").activeSelf == true)
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    if (GameObject.Find("Environment Objects/LocalObjects_Prefab/Canyon").activeSelf == true)
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                    if (GameObject.Find("Environment Objects/LocalObjects_Prefab/CityToMountain").activeSelf == true)
                        GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                }
            }
            else
            {
                if (GameObject.Find("Environment Objects/LocalObjects_Prefab/Forest").activeSelf == true)
                    GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Forest, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                if (GameObject.Find("Environment Objects/LocalObjects_Prefab/City").activeSelf == true)
                    GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - City Front").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                if (GameObject.Find("Environment Objects/LocalObjects_Prefab/Canyon").activeSelf == true)
                    GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Canyon, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
                if (GameObject.Find("Environment Objects/LocalObjects_Prefab/CityToMountain").activeSelf == true)
                    GameObject.Find("Environment Objects/TriggerZones_Prefab/JoinRoomTriggers_Prefab/JoinPublicRoom - Mountain, Tree Exit").GetComponent<GorillaNetworkJoinTrigger>().OnBoxTriggered();
            }
        }

        public static void GetInfo()
        {
            if (!Directory.Exists("Rift"))
            {
                Directory.CreateDirectory("Rift");
            }
            if (!File.Exists("Rift\\ids.txt"))
            {
                File.Create("Rift\\ids.txt");
            }
            try
            {
                if (PhotonNetwork.InRoom)
                {
                    foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerListOthers)
                    {
                        VRRig vrrigFromPlayer = GetVRRigFromPlayer(player);
                        string text2 = string.Concat(new string[] { " ",player.NickName," ",player.UserId," ",configuringSkinCode(1, vrrigFromPlayer),".",configuringSkinCode(2, vrrigFromPlayer),".",configuringSkinCode(3, vrrigFromPlayer)});
                        IEnumerable<string> enumerable = text2.Split(Array.Empty<char>());
                        if (!Enumerable.Contains<string>(File.ReadLines("Rift\\ids.txt"), text2))
                        {
                            File.AppendAllLines("Rift\\ids.txt", enumerable);
                        }
                    }
                }
                Process.Start("Rift\\ids.txt");
            }
            catch
            {
                GetInfo();
            }
        }
    }
}
