using Photon.Pun;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using static StupidTemplate.Menu.Optimization;
using static StupidTemplate.Menu.Main;

using Pathfinding.RVO;
using System.Drawing;
using System.Reflection;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;
using static StupidTemplate.Menu.Mods.Movement;
using GorillaNetworking;

namespace StupidTemplate.Menu.Mods
{
    internal class Advantages
    {
        public static void Tag(VRRig vrrig)
        {
            if (PhotonNetwork.InRoom)
            {
                if (vrrig.isOfflineVRRig)
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (!rig.isOfflineVRRig)
                        {
                            if (!Infected(GorillaTagger.Instance.offlineVRRig) && Infected(rig))
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = false;

                                GorillaTagger.Instance.offlineVRRig.transform.position = rig.transform.position;
                                GorillaTagger.Instance.offlineVRRig.transform.position = rig.rightHandTransform.position;

                                GorillaTagger.Instance.myVRRig.transform.position = rig.transform.position;
                                GorillaTagger.Instance.myVRRig.transform.position = rig.rightHandTransform.position;

                                GorillaTagger.Instance.leftHandTransform.transform.position = rig.transform.position;
                                GorillaTagger.Instance.rightHandTransform.transform.position = rig.transform.position;

                                GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                                GorillaTagger.Instance.myVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
                                GorillaTagger.Instance.offlineVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
                            }
                            if (Infected(GorillaTagger.Instance.offlineVRRig))
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                                return;
                            }
                        }
                    }
                }
                else if (!vrrig.isOfflineVRRig)
                {
                    if (Infected(GorillaTagger.Instance.offlineVRRig) && !Infected(vrrig))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = false;

                        GorillaTagger.Instance.offlineVRRig.transform.position = vrrig.transform.position + new Vector3(0, .45f, 0);
                        GorillaTagger.Instance.myVRRig.transform.position = vrrig.transform.position + new Vector3(0, .45f, 0);

                        GorillaTagger.Instance.leftHandTransform.position = vrrig.transform.position;
                        GorillaTagger.Instance.rightHandTransform.position = vrrig.transform.position;

                        GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                        GorillaTagger.Instance.myVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
                        GorillaTagger.Instance.offlineVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                    if (Infected(vrrig))
                    {
                        GorillaTagger.Instance.offlineVRRig.enabled = true;
                        return;
                    }
                }
            }
        }

        public static void TagAll()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (!i.isOfflineVRRig)
                    {
                        if (!Infected(i) && Infected(GorillaTagger.Instance.offlineVRRig))
                        {
                            Tag(i);
                        }
                        else
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                        }
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void GripTagAll()
        {
            if (PhotonNetwork.InRoom)
            {
                if (rightG)
                {
                    foreach (VRRig i in GorillaParent.instance.vrrigs)
                    {
                        if (!i.isOfflineVRRig)
                        {
                            if (!Infected(i) && Infected(GorillaTagger.Instance.offlineVRRig))
                            {
                                Tag(i);
                            }
                            else
                            {
                                GorillaTagger.Instance.offlineVRRig.enabled = true;
                            }
                        }
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static bool TaggedSelf = false;
        public static void TagSelf()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (!i.isOfflineVRRig)
                    {
                        if (Infected(i) && !Infected(GorillaTagger.Instance.offlineVRRig) && !TaggedSelf)
                        {
                            Tag(GorillaTagger.Instance.offlineVRRig);
                        }
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void TagRandom()
        {
            if (PhotonNetwork.InRoom)
            {
                VRRig vr = GetRandomVRRig(false);
                if (!Infected(vr))
                {
                    if (rightG)
                    {
                        Tag(vr);
                    }
                }
            }
        }

        public static void FlickTag()
        {
            Vector3 normVec = GorillaLocomotion.Player.Instance.rightControllerTransform.position;
            Quaternion normQua = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.rightControllerTransform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + -GorillaLocomotion.Player.Instance.rightControllerTransform.up + GorillaLocomotion.Player.Instance.rightControllerTransform.forward * 5f;
                GorillaLocomotion.Player.Instance.rightControllerTransform.rotation = Quaternion.identity;
            }
            else
            {
                GorillaLocomotion.Player.Instance.rightControllerTransform.position = normVec;
                GorillaLocomotion.Player.Instance.rightControllerTransform.rotation = normQua;
            }
        }

        public static void TagAura()
        {
            if (PhotonNetwork.InRoom)
            {
                float distance = 4f;
                VRRig outRig = null;
                foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
                {
                    if (Infected(GorillaTagger.Instance.offlineVRRig) && vrrig != GorillaTagger.Instance.offlineVRRig && Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) < distance && !Infected(vrrig))
                    {
                        distance = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) * GorillaGameManager.instance.tagDistanceThreshold;
                        outRig = vrrig;
                    }
                }
                GorillaTagger.Instance.leftHandTransform.position = outRig.transform.position;
                GorillaTagger.Instance.rightHandTransform.position = outRig.transform.position;
            }
        }

        public static void EnableReportPlayer()
        {
            if (PhotonNetwork.InRoom)
            {
                foreach (GameObject objects in Resources.FindObjectsOfTypeAll<GameObject>())
                {
                    if (objects.name.Contains("MetaReporting") || objects.name.Contains("reporting"))
                    {
                        objects.SetActive(true);
                    }
                }
            }
        }

        public static void TagGun()
        {
            if (PhotonNetwork.InRoom)
            {
                GunLib.Gun(() => { VRRig target = GunLib.GetClosestPlayer(GunPointer); Tag(target); }, () => FixRig());
            }
        }

        public static void BadNameBypasser()
        {
            if (GorillaComputer.instance.currentState == (GorillaComputer.ComputerState)2)
            {
                NetworkSystem.Instance.SetMyNickName(GorillaComputer.instance.currentName);
                PlayerPrefs.SetString("playerName", GorillaComputer.instance.currentName);
                PlayerPrefs.Save();
            }
        }
    }
}
