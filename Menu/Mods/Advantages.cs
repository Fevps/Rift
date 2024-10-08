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
                if (vrrig == GorillaTagger.Instance.offlineVRRig)
                {
                    foreach (VRRig rig in GorillaParent.instance.vrrigs)
                    {
                        if (rig != GorillaTagger.Instance.offlineVRRig)
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
                else if (vrrig != GorillaTagger.Instance.offlineVRRig)
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
            if (PhotonNetwork.InRoom || PhotonNetwork.InLobby)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (i != GorillaTagger.Instance.offlineVRRig)
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

        public static void TagSelf()
        {
            bool tagged = false;
            if (PhotonNetwork.InRoom)
            {
                foreach (VRRig i in GorillaParent.instance.vrrigs)
                {
                    if (i != GorillaTagger.Instance.offlineVRRig)
                    {
                        if (Infected(i) && !Infected(GorillaTagger.Instance.offlineVRRig) && !tagged)
                        {
                            Tag(GorillaTagger.Instance.offlineVRRig);
                        }
                        if (Infected(GorillaTagger.Instance.offlineVRRig))
                        {
                            GorillaTagger.Instance.offlineVRRig.enabled = true;
                            tagged = true;
                        }
                    }
                }
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
            if (Infected(GorillaTagger.Instance.offlineVRRig) || tagged)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                GetIndex("Tag Self").enabled = false;
            }
        }

        public static void TagRandom()
        {
            if (PhotonNetwork.InRoom)
            {
                Tag(GetRandomVRRig(false));
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
            float distance = 4f;
            VRRig outRig = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (Infected(GorillaTagger.Instance.offlineVRRig) && vrrig != GorillaTagger.Instance.offlineVRRig && Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) < distance && !Infected(vrrig))
                {
                    distance = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) * 4f;
                    outRig = vrrig;
                }
            }
            GorillaTagger.Instance.leftHandTransform.position = outRig.transform.position;
            GorillaTagger.Instance.rightHandTransform.position = outRig.transform.position;
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
            if (rightG)
            {
                Physics.Raycast(GorillaTagger.Instance.rightHandTransform.position, -GorillaTagger.Instance.rightHandTransform.up, out GunHit);
                GunPointer = GameObject.CreatePrimitive(0);
                if (GunPointer == null) { GunPointer = GameObject.CreatePrimitive(0); }
                if (GunTracer == null) { GunTracer = new GameObject("Line"); }
                LineRenderer support = GunTracer.GetComponent<LineRenderer>();
                if (support == null) { support = GunTracer.AddComponent<LineRenderer>(); }
                GunPointer.transform.position = GunHit.point;
                GunPointer.transform.localScale = new Vector3(.03f, .03f, .03f);
                GunPointer.GetComponent<Renderer>().material.color = green;
                support.material.shader = Shader.Find("GUI/Text Shader");
                support.startColor = green;
                support.endColor = green;
                support.startWidth = .01f;
                support.endWidth = .01f;
                support.useWorldSpace = true;
                support.positionCount = 2;
                support.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                support.SetPosition(1, GunPointer.transform.position);
                SphereCollider guncollider = GunPointer.GetComponent<SphereCollider>();
                guncollider.radius = .001f;
                Object.Destroy(GunTracer, Time.deltaTime);
                Object.Destroy(GunPointer, Time.deltaTime);
                Object.Destroy(GunPointer.GetComponent<Rigidbody>());
                Object.Destroy(GunPointer.GetComponent<SphereCollider>());
                if (rightT > .2f)
                {
                    GunPointer.GetComponent<Renderer>().material.color = red;
                    support.startColor = red;
                    support.endColor = red;
                    VRRig i = GunLib.GetClosestPlayer(GunPointer);
                    if (!Infected(i)) { Tag(i); } else { FixRig(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    FixRig();
                }
            }
            if (Mouse.current.rightButton.isPressed)
            {
                Ray ruh = GameObject.Find("Shoulder Camera").GetComponent<Camera>().ScreenPointToRay(Mouse.current.position.ReadValue(), Camera.MonoOrStereoscopicEye.Mono);
                Physics.Raycast(ruh, out GunHit, int.MaxValue);
                GunPointer = GameObject.CreatePrimitive(0);
                if (GunPointer == null) { GunPointer = GameObject.CreatePrimitive(0); }
                if (GunTracer == null) { GunTracer = new GameObject("Line"); }
                LineRenderer support = GunTracer.GetComponent<LineRenderer>();
                if (support == null) { support = GunTracer.AddComponent<LineRenderer>(); }
                GunPointer.transform.position = GunHit.point;
                GunPointer.transform.localScale = new Vector3(.03f, .03f, .03f);
                GunPointer.GetComponent<Renderer>().material.color = green;
                support.material.shader = Shader.Find("GUI/Text Shader");
                support.startColor = green;
                support.endColor = green;
                support.startWidth = .01f;
                support.endWidth = .01f;
                support.useWorldSpace = true;
                support.positionCount = 2;
                support.SetPosition(0, GorillaTagger.Instance.rightHandTransform.position);
                support.SetPosition(1, GunPointer.transform.position);
                SphereCollider guncollider = GunPointer.GetComponent<SphereCollider>();
                guncollider.radius = .001f;
                Object.Destroy(GunTracer, Time.deltaTime);
                Object.Destroy(GunPointer, Time.deltaTime);
                Object.Destroy(GunPointer.GetComponent<Rigidbody>());
                Object.Destroy(GunPointer.GetComponent<SphereCollider>());
                if (Mouse.current.leftButton.isPressed)
                {
                    GunPointer.GetComponent<Renderer>().material.color = red;
                    support.startColor = red;
                    support.endColor = red;
                    VRRig i = GunLib.GetClosestPlayer(GunPointer);
                    if (!Infected(i)) { Tag(i); } else { FixRig(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    FixRig();
                }
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
