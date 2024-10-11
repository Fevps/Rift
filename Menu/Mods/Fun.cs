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
using UnityEngine.XR.Interaction.Toolkit;
using TagEffects;
using UnityEngine.ProBuilder;

namespace StupidTemplate.Menu.Mods
{
    internal class Fun
    {
        public static void ShowSkeletons()
        {
            foreach (VRRig player in GorillaParent.instance.vrrigs)
            {
                if (!FirstPersonXRaySpecs.IsWearing)
                {
                    player.ShowSkeleton(true);
                }
            }
        }

        public static void HideSkeletons()
        {
            foreach (VRRig player in GorillaParent.instance.vrrigs)
            {
                player.ShowSkeleton(false);
            }
        }

        public static void InfiniteTaps()
        {
            GorillaTagger.Instance.tapCoolDown = float.MinValue;
        }

        public static void SlowTaps()
        {
            GorillaTagger.Instance.tapCoolDown = 0.15f;
        }

        public static void LoudTaps()
        {
            GorillaTagger.Instance.handTapVolume = float.MaxValue;
        }

        public static void QuietTaps()
        {
            GorillaTagger.Instance.handTapVolume = float.MinValue;
        }

        public static void ResetTapVolume()
        {
            GorillaTagger.Instance.handTapVolume = 0.1f;
        }

        public static void Jesus()
        {
            GameObject.Find("Beach/B_WaterVolumes/OceanWater").AddComponent<MeshCollider>().enabled = true;
        }

        public static void FixWater()
        {
            GameObject.Find("Beach/B_WaterVolumes/OceanWater").GetComponent<MeshCollider>().enabled = false;
        }

        public static void DisableWater()
        {
            GameObject.Find("Beach/B_WaterVolumes/OceanWater").SetActive(false);
        }

        public static void EnableWater()
        {
            GameObject.Find("Beach/B_WaterVolumes/OceanWater").SetActive(true);
        }

        private static float debounce = 1f;
        public static void Splashing()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] {
                        GorillaTagger.Instance.rightHandTransform.position,
                        GorillaTagger.Instance.rightHandTransform.rotation,
                        0.5f, 0.3f, true, false
                    });
                    RPC_PATCHER_KICK_PATCHER();
                    debounce = Time.time + 0.2f;
                }
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] {
                        GorillaTagger.Instance.leftHandTransform.position,
                        GorillaTagger.Instance.leftHandTransform.rotation,
                        0.5f, 0.3f, true, false
                    });
                    RPC_PATCHER_KICK_PATCHER();
                    debounce = Time.time + 0.2f;
                }
            }
        }

        public static void SoundSpammer()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    if (debounce < Time.time)
                    {
                        GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { chosenSound, false, 999f });
                        RPC_PATCHER_KICK_PATCHER();
                        debounce = Time.time + 0.2f;
                    }
                }
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { chosenSound, false, 999f });
                    RPC_PATCHER_KICK_PATCHER();
                    debounce = Time.time + 0.2f;
                }
            }
        }

        public static void RandomSoundSpammer()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    if (debounce < Time.time)
                    {
                        GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { UnityEngine.Random.Range(1, 240), false, 999f });
                        RPC_PATCHER_KICK_PATCHER();
                        debounce = Time.time + 0.2f;
                    }
                }
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                if (debounce < Time.time)
                {
                    GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { UnityEngine.Random.Range(1, 240), false, 999f });
                    RPC_PATCHER_KICK_PATCHER();
                    debounce = Time.time + 0.2f;
                }
            }
        }

        public static void SpamWhenTouching()
        {
            if (GorillaLocomotion.Player.Instance.wasLeftHandTouching || GorillaLocomotion.Player.Instance.wasRightHandTouching)
            {
                if (debounce < Time.time)
                {
                    if (debounce < Time.time)
                    {
                        GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { chosenSound, false, 999f });
                        RPC_PATCHER_KICK_PATCHER();
                        debounce = Time.time + 0.2f;
                    }
                }
            }
        }

        public static void SplashAura()
        {
            if (debounce < Time.time)
            {
                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlaySplashEffect", RpcTarget.All, new object[] {
                        GorillaTagger.Instance.bodyCollider.transform.position,
                        GorillaTagger.Instance.headCollider.transform.rotation,
                        4f, 100f, true, false
                });
                RPC_PATCHER_KICK_PATCHER();
                debounce = Time.time + 0.2f;
            }
        }

        private static float bouncer079 = 0f;
        public static void HitPlayer()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig player in GorillaParent.instance.vrrigs)
                {
                    if (!player.isOfflineVRRig)
                    {
                        if (Vector3.Distance(GorillaTagger.Instance.leftHandTransform.position, player.headMesh.transform.position) < .3f)
                        {
                            if (bouncer079 < Time.time)
                            {
                                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { 8, true, 999f });
                                RPC_PATCHER_KICK_PATCHER();
                                bouncer079 = Time.time + 0.2f;
                            }
                        }
                        if (Vector3.Distance(GorillaTagger.Instance.rightHandTransform.position, player.headMesh.transform.position) < .3f)
                        {
                            if (bouncer079 < Time.time)
                            {
                                GorillaTagger.Instance.myVRRig.SendRPC("RPC_PlayHandTap", RpcTarget.All, new object[] { 8, false, 999f });
                                RPC_PATCHER_KICK_PATCHER();
                                bouncer079 = Time.time + 0.2f;
                            }
                        }
                    }
                }
            }
        }

        public static void FasterTurnSpeed()
        {
            foreach (GorillaSnapTurn SpinAmount in (GorillaSnapTurn[])UnityEngine.Object.FindObjectsOfType(typeof(GorillaSnapTurn)))
            {
                SpinAmount.ChangeTurnMode("SMOOTH", 80);
                SpinAmount.turnAmount = 80f;
            }
        }

        public static void FixTurnSpeed()
        {
            foreach (GorillaSnapTurn SpinAmount in UnityEngine.Object.FindObjectsOfType<GorillaSnapTurn>())
            {
                SpinAmount.ChangeTurnMode("SMOOTH", 8);
                SpinAmount.turnAmount = 8;
            }
        }
    }
}
