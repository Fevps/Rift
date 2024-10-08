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

namespace StupidTemplate.Menu.Mods
{
    internal class Safety
    {
        public static void AntiFinger()
        {
            ControllerInputPoller.instance.rightControllerGripFloat = 0f;
            if (menu == null) { ControllerInputPoller.instance.rightControllerIndexFloat = 0f; }

            ControllerInputPoller.instance.rightControllerPrimaryButton = false;
            ControllerInputPoller.instance.rightControllerSecondaryButton = false;

            ControllerInputPoller.instance.rightControllerPrimaryButtonTouch = false;
            ControllerInputPoller.instance.rightControllerSecondaryButtonTouch = false;

            ControllerInputPoller.instance.leftControllerGripFloat = 0f;
            if (menu == null) { ControllerInputPoller.instance.leftControllerIndexFloat = 0f; }

            ControllerInputPoller.instance.leftControllerPrimaryButton = false;
            ControllerInputPoller.instance.leftControllerSecondaryButton = false;

            ControllerInputPoller.instance.leftControllerPrimaryButtonTouch = false;
            ControllerInputPoller.instance.leftControllerSecondaryButtonTouch = false;
        }

        public static void RPC_PATCHER_KICK_PATCHER()
        {
            PhotonNetwork.MaxResendsBeforeDisconnect = int.MaxValue;
            GorillaNot.instance.rpcCallLimit = int.MaxValue;
            GorillaNot.instance.rpcErrorMax = int.MaxValue;
            GorillaNot.instance.logErrorMax = int.MaxValue;
            PhotonNetwork.CleanRpcBufferIfMine(Optimization.GetPhotonViewFromVRRig(GorillaTagger.Instance.offlineVRRig));
            PhotonNetwork.OpCleanActorRpcBuffer(PhotonNetwork.LocalPlayer.ActorNumber);
            PhotonNetwork.OpCleanRpcBuffer(Optimization.GetPhotonViewFromVRRig(GorillaTagger.Instance.offlineVRRig));
            PhotonNetwork.RemoveBufferedRPCs(GorillaTagger.Instance.myVRRig.ViewID, null, null);
            PhotonNetwork.RemoveBufferedRPCs();
            PhotonNetwork.RemoveRPCs(PhotonNetwork.LocalPlayer);
            PhotonNetwork.RemoveRPCsInGroup(int.MaxValue);
            PhotonNetwork.SendAllOutgoingCommands();
            GorillaNot.instance.OnPlayerLeftRoom(PhotonNetwork.LocalPlayer);
        }

        private static GameObject gameobject = null;
        public static void AntiReport(int i)
        {
            foreach (VRRig player in GorillaParent.instance.vrrigs)
            {
                if (!player.isOfflineVRRig)
                {
                    if (gameobject == null)
                    {
                        GorillaScoreBoard[] ScoreBoard = GameObject.FindObjectsOfType<GorillaScoreBoard>();
                        foreach (GorillaScoreBoard boardObject in ScoreBoard)
                        {
                            if (boardObject != null && ScoreBoard != null)
                            {
                                gameobject = GameObject.CreatePrimitive(PrimitiveType.Cube);
                                gameobject.transform.localScale = new Vector3(float.MinValue, float.MinValue, float.MinValue);
                                gameobject.transform.position = boardObject.transform.position;
                                UnityEngine.Object.Destroy(gameobject, Time.deltaTime);
                                UnityEngine.Object.Destroy(gameobject.GetComponent<Rigidbody>());
                                UnityEngine.Object.Destroy(gameobject.GetComponent<BoxCollider>());
                            }
                        }
                    }
                    bool disconnected = false;
                    if (gameobject != null)
                    {
                        if ((Vector3.Distance(gameobject.transform.position, player.leftHandTransform.position) < 1.45f) || (Vector3.Distance(gameobject.transform.position, player.rightHandTransform.position) < 1.45f))
                        {
                            if (i==1)
                            {
                                NotifiLib.SendNotification("Someone got too close or attempted to report you.");
                                PhotonNetwork.Disconnect();
                            }
                            else
                            {
                                if (i==2)
                                {
                                    NotifiLib.SendNotification("Someone got too close or attempted to report you.");
                                    PhotonNetwork.Disconnect();
                                    disconnected = true;
                                }
                                else
                                {
                                    if (i==3)
                                    {
                                        NotifiLib.SendNotification("Someone got too close or attempted to report you.");
                                        ServerHop();
                                    }
                                }
                            }
                        }
                    }
                    if (disconnected)
                    {
                        Join(RoomCode);
                        disconnected = false;
                    }
                }
            }
        }

        public static void AntiFamous()
        {
            for (int i = 0; i < SpecialCosmetics.Length; i++)
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                    if (!vrrig.isOfflineVRRig && vrrig.concatStringOfCosmeticsAllowed.Contains(SpecialCosmetics[i]))
                        PhotonNetwork.Disconnect();
        }

        public static void QuestSupportTab()
        {
            GorillaComputer.instance.screenText.Text = GorillaComputer.instance.screenText.Text.Replace("STEAM", "QUEST");
        }
    }
}
