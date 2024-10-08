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
using System.Collections;
using Photon.Voice;

namespace StupidTemplate.Menu.Mods
{
    internal class Movement
    {
        public static void Keyboarding()
        {
            float currentSpeed = 10;
            Transform bodyTransform = Camera.main.transform;
            GorillaTagger.Instance.rigidbody.useGravity = false;
            GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
            if (UnityInput.Current.GetKey(KeyCode.LeftShift))
                currentSpeed *= 2.5f;
            if (UnityInput.Current.GetKey(KeyCode.W) || UnityInput.Current.GetKey(KeyCode.UpArrow))
                bodyTransform.position += bodyTransform.forward * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetKey(KeyCode.A) || UnityInput.Current.GetKey(KeyCode.LeftArrow))
                bodyTransform.position += -bodyTransform.right * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetKey(KeyCode.S) || UnityInput.Current.GetKey(KeyCode.DownArrow))
                bodyTransform.position += -bodyTransform.forward * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetKey(KeyCode.D) || UnityInput.Current.GetKey(KeyCode.RightArrow))
                bodyTransform.position += bodyTransform.right * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetKey(KeyCode.Space))
                bodyTransform.position += bodyTransform.up * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetKey(KeyCode.LeftControl))
                bodyTransform.position += -bodyTransform.up * currentSpeed * Time.deltaTime;
            if (UnityInput.Current.GetMouseButton(1))
            {
                Vector3 pos = UnityInput.Current.mousePosition - oldMousePos;
                float x = bodyTransform.localEulerAngles.x - pos.y * 0.3f;
                float y = bodyTransform.localEulerAngles.y + pos.x * 0.3f;
                bodyTransform.localEulerAngles = new Vector3(x, y, 0f);
            }
            oldMousePos = UnityInput.Current.mousePosition;
            GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }
        private static Vector3 oldMousePos;

        public static void LowGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * Time.deltaTime * (6.66f / Time.deltaTime), ForceMode.Acceleration);
        }

        public static void ZeroGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.up * Time.deltaTime * (9.81f / Time.deltaTime), ForceMode.Acceleration);
        }

        public static void HighGravity()
        {
            GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(Vector3.down * Time.deltaTime * (7.77f / Time.deltaTime), ForceMode.Acceleration);
        }

        public static GameObject rplat;
        public static GameObject lplat;
        public static GameObject platformsL;
        public static GameObject platformsR;
        public static GameObject outlineL;
        public static GameObject outlineR;
        public static bool rplatEnabled = false;
        public static bool lplatEnabled = false;

        public static bool sticky = false;

        public static int platChanger = 1;

        public static void Plattys(Color color, bool trigger)
        {
            if (trigger)
            {
                if (rightT > .2f || Mouse.current.rightButton.isPressed)
                {
                    if (!rplatEnabled)
                    {
                        platformsR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        platformsR.GetComponent<Renderer>().material.color = ButtonColor;
                        platformsR.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + new Vector3(0, -0.00009f, 0);
                        platformsR.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                        platformsR.transform.localScale = new Vector3(0.025f, 0.23f, 0.32f);

                        outlineR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(outlineR.GetComponent<Collider>());
                        outlineR.GetComponent<Renderer>().material.color = EnabledColor;
                        outlineR.transform.position = platformsR.transform.position;
                        outlineR.transform.rotation = platformsR.transform.rotation;
                        outlineR.transform.localScale = new Vector3(0.023f, 0.235f, 0.325f);

                        rplatEnabled = true;
                    }
                }
                if (leftT > .2f || Mouse.current.leftButton.isPressed)
                {
                    if (!lplatEnabled)
                    {
                        platformsL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        platformsL.GetComponent<Renderer>().material.color = ButtonColor;
                        platformsL.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + new Vector3(0, -0.00009f, 0);
                        platformsL.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                        platformsL.transform.localScale = new Vector3(0.025f, 0.23f, 0.32f);

                        outlineL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(outlineL.GetComponent<Collider>());
                        outlineL.GetComponent<Renderer>().material.color = EnabledColor;
                        outlineL.transform.position = platformsL.transform.position;
                        outlineL.transform.rotation = platformsL.transform.rotation;
                        outlineL.transform.localScale = new Vector3(0.023f, 0.235f, 0.325f);

                        lplatEnabled = true;
                    }
                }
                if (rightT < .2f)
                {
                    if (rplatEnabled)
                    {
                        UnityEngine.Object.Destroy(rplat);
                        UnityEngine.Object.Destroy(platformsR);
                        UnityEngine.Object.Destroy(outlineL);
                        rplatEnabled = false;
                    }
                }
                if (leftT < .2f)
                {
                    if (lplatEnabled)
                    {
                        UnityEngine.Object.Destroy(lplat);
                        UnityEngine.Object.Destroy(platformsL);
                        UnityEngine.Object.Destroy(outlineR);
                        lplatEnabled = false;
                    }
                }
            }
            else
            {
                if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
                {
                    if (!rplatEnabled)
                    {
                        platformsR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        platformsR.GetComponent<Renderer>().material.color = ButtonColor;
                        platformsR.transform.position = GorillaLocomotion.Player.Instance.rightControllerTransform.position + new Vector3(0, -0.00009f);
                        platformsR.transform.rotation = GorillaLocomotion.Player.Instance.rightControllerTransform.rotation;
                        platformsR.transform.localScale = new Vector3(0.025f, 0.23f, 0.32f);

                        outlineR = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(outlineR.GetComponent<Collider>());
                        outlineR.GetComponent<Renderer>().material.color = EnabledColor;
                        outlineR.transform.position = platformsR.transform.position;
                        outlineR.transform.rotation = platformsR.transform.rotation;
                        outlineR.transform.localScale = new Vector3(0.023f, 0.235f, 0.325f);

                        rplatEnabled = true;
                    }
                }
                if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
                {
                    if (!lplatEnabled)
                    {
                        platformsL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        platformsL.GetComponent<Renderer>().material.color = ButtonColor;
                        platformsL.transform.position = GorillaLocomotion.Player.Instance.leftControllerTransform.position + new Vector3(0, -0.00009f);
                        platformsL.transform.rotation = GorillaLocomotion.Player.Instance.leftControllerTransform.rotation;
                        platformsL.transform.localScale = new Vector3(0.025f, 0.23f, 0.32f);

                        outlineL = GameObject.CreatePrimitive(PrimitiveType.Cube);
                        UnityEngine.Object.Destroy(outlineL.GetComponent<Collider>());
                        outlineL.GetComponent<Renderer>().material.color = EnabledColor;
                        outlineL.transform.position = platformsL.transform.position;
                        outlineL.transform.rotation = platformsL.transform.rotation;
                        outlineL.transform.localScale = new Vector3(0.023f, 0.235f, 0.325f);

                        lplatEnabled = true;
                    }
                }
                if (!ControllerInputPoller.instance.rightGrab)
                {
                    if (rplatEnabled)
                    {
                        UnityEngine.Object.Destroy(rplat);
                        UnityEngine.Object.Destroy(platformsR);
                        UnityEngine.Object.Destroy(outlineR);
                        rplatEnabled = false;
                    }
                }
                if (!ControllerInputPoller.instance.leftGrab)
                {
                    if (lplatEnabled)
                    {
                        UnityEngine.Object.Destroy(lplat);
                        UnityEngine.Object.Destroy(platformsL);
                        UnityEngine.Object.Destroy(outlineL);
                        lplatEnabled = false;
                    }
                }
            }
        }

        public static void DrawPlatforms()
        {
            if (leftG)
            {
                CreatePlatform(ButtonColor, GorillaTagger.Instance.leftHandTransform.position, GorillaTagger.Instance.leftHandTransform.rotation, 1.5f);
            }
            if (rightG)
            {
                CreatePlatform(ButtonColor, GorillaTagger.Instance.rightHandTransform.position, GorillaTagger.Instance.rightHandTransform.rotation, 1.5f);
            }
        }

        public static void DrawPlatformsGun()
        {
            GunLib.Gun(() => CreatePlatform(ButtonColor, GunPointer.transform.position, GorillaTagger.Instance.rightHandTransform.rotation, 2.5f), null);
        }

        public static void Fly()
        {
            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void TriggerFly()
        {
            if (rightT > .2f || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void HandFly()
        {
            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.rightHandTransform.transform.forward * Time.deltaTime * 10f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }
        }

        public static void Slingshot()
        {
            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.forward * Time.deltaTime * 20f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * 7f;
            }
        }

        public static void HandSlingshot()
        {
            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.rightControllerTransform.forward * Time.deltaTime * 20f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.headCollider.transform.up * Time.deltaTime * 7f;
            }
        }

        public static void NoclipFly()
        {
            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.transform.position += GorillaTagger.Instance.headCollider.transform.forward * Time.deltaTime * 10f;
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                ToggleColliders(false);
            }
            else
            {
                ToggleColliders(true);
            }
        }

        public static GameObject gameObject = null;
        public static GameObject checkPoint = null;
        public static float iiii = 0f;
        public static float iii = 1f;
        public static void C4()
        {
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                if (gameObject == null)
                {
                    gameObject = GameObject.CreatePrimitive(0);
                    gameObject.transform.localScale = new Vector3(0.185f, 0.185f, 0.185f);
                    gameObject.GetComponent<Renderer>().material.color = new Color32(20, 20, 20, 50);
                    UnityEngine.Object.Destroy(gameObject.GetComponent<SphereCollider>());
                    UnityEngine.Object.Destroy(gameObject.GetComponent<Rigidbody>());
                }
                gameObject.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
            if ((ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed) && Time.time - iiii >= iii)
            {
                if (gameObject != null)
                {
                    Vector3 distance = GorillaTagger.Instance.bodyCollider.transform.position - gameObject.transform.position;
                    distance.Normalize();
                    GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += 10f * distance;
                    UnityEngine.Object.Destroy(gameObject);
                    gameObject = null;
                }
                iiii = Time.time;
            }
        }

        public static void Checkpoint()
        {
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.leftButton.isPressed)
            {
                if (checkPoint == null)
                {
                    checkPoint = GameObject.CreatePrimitive(0);
                    checkPoint.transform.localScale = new Vector3(0.185f, 0.185f, 0.185f);
                    checkPoint.GetComponent<Renderer>().material.color = new Color32(20, 20, 20, 50);
                    UnityEngine.Object.Destroy(checkPoint.GetComponent<SphereCollider>());
                    UnityEngine.Object.Destroy(checkPoint.GetComponent<Rigidbody>());
                }
                checkPoint.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            }
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                if (checkPoint != null)
                {
                    ToggleColliders(false);
                    GorillaTagger.Instance.GetComponent<Rigidbody>().transform.position = checkPoint.transform.position;
                    GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    if (Time.time > 0.1f)
                    {
                        ToggleColliders(true);
                    }
                    UnityEngine.Object.Destroy(checkPoint);
                    checkPoint = null;
                }
            }
        }

        public static void DisableC4()
        {
            if (gameObject != null)
            {
                UnityEngine.Object.Destroy(gameObject);
                gameObject = null;
            }
        }

        public static void DisableCheckPoint()
        {
            if (checkPoint != null)
            {
                UnityEngine.Object.Destroy(checkPoint);
                checkPoint = null;
            }
        }

        public static void NoClip()
        {
            if (rightT > .2f)
            {
                ToggleColliders(false);
            }
            else
            {
                ToggleColliders(true);
            }
        }

        public static void UpAndDown()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * 20f;
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += -Vector3.up * Time.deltaTime * 20f;
            }
        }

        public static void LeftAndRight()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += GorillaLocomotion.Player.Instance.transform.right * Time.deltaTime * 20f;
            }
            if (ControllerInputPoller.instance.leftGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += -GorillaLocomotion.Player.Instance.transform.right * Time.deltaTime * 20f;
            }
        }

        /*public static void Freecam()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            GorillaTagger.Instance.rigidbody.useGravity = false;

            if (rightG || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaLocomotion.Player.Instance.transform.right * Time.deltaTime * 5f;
            }
            if (leftG || Mouse.current.leftButton.isPressed)
            {
                GorillaTagger.Instance.rigidbody.velocity += -GorillaLocomotion.Player.Instance.transform.right * Time.deltaTime * 5f;
            }
            if (rightP || Input.GetKey(KeyCode.Space))
            {
                GorillaTagger.Instance.rigidbody.velocity += GorillaLocomotion.Player.Instance.transform.up * Time.deltaTime * 5f;
            }
            if (leftP || Input.GetKey(KeyCode.LeftControl))
            {
                GorillaTagger.Instance.rigidbody.velocity += -GorillaLocomotion.Player.Instance.transform.up * Time.deltaTime * 5f;
            }
        }*/

        public static void FixFreecam()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
            GorillaTagger.Instance.rigidbody.useGravity = true;
        }

        public static void Speedboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = Settings.jumpMultiplier;
            GorillaLocomotion.Player.Instance.jumpMultiplier = Settings.jumpSpeed;
        }

        public static void IntegratedSpeedboost()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed += (Settings.jumpMultiplier / 2);
            GorillaLocomotion.Player.Instance.jumpMultiplier += (Settings.jumpSpeed / 2);
        }

        public static void GripSpeedboost()
        {
            if (rightG)
            {
                Speedboost();
            }
        }

        public static void SlideControl()
        {
            GorillaLocomotion.Player.Instance.slideControl = slideControl;
        }

        public static void ForceTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = true;
        }

        public static void NoTagFreeze()
        {
            GorillaLocomotion.Player.Instance.disableMovement = false;
        }

        public static void UncapSpeed()
        {
            GorillaLocomotion.Player.Instance.maxJumpSpeed = 99999f;
        }

        public static void SuperJump()
        {
            if (GorillaLocomotion.Player.Instance.didAJump)
            {
                GorillaLocomotion.Player.Instance.StartCoroutine(StartJumpCoroutine());
            }
        }

        private static IEnumerator StartJumpCoroutine()
        {
            float timer = 0f;
            while (timer < .35f)
            {
                GorillaLocomotion.Player.Instance.GetComponent<Rigidbody>().velocity += Vector3.up * Time.deltaTime * 2f;
                timer += Time.deltaTime;
                yield return null;
            }
        }

        public static void TeleportGun()
        {
            GunLib.Gun(() => {
                GorillaTagger.Instance.transform.position = GunHit.point + new Vector3(0, 1, 0);
                GorillaTagger.Instance.myVRRig.transform.position = GunHit.point + new Vector3(0, 1, 0);
                GorillaTagger.Instance.offlineVRRig.transform.position = GunHit.point + new Vector3(0, 1, 0);

                GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
                GorillaTagger.Instance.myVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
                GorillaTagger.Instance.offlineVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
            }, FixRig);
        }

        public static void FixRig()
        {
            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static bool AllowedToGhost = true;
        public static void Ghost()
        {
            if (rightP || Mouse.current.rightButton.isPressed)
            {
                if (AllowedToGhost)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = !GorillaTagger.Instance.offlineVRRig.enabled;
                    AllowedToGhost = false;
                }
            }
            else
            {
                if (!AllowedToGhost)
                {
                    AllowedToGhost = true;
                }
            }
        }

        public static bool AllowedToInvis = true;
        public static void Invis()
        {
            if (rightP || Mouse.current.rightButton.isPressed)
            {
                if (AllowedToInvis)
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = !GorillaTagger.Instance.offlineVRRig.enabled;
                    GorillaTagger.Instance.offlineVRRig.transform.position = new Vector3(float.MinValue, float.MinValue, float.MaxValue);
                    AllowedToInvis = false;
                }
            }
            else
            {
                if (!AllowedToInvis)
                {
                    AllowedToInvis = true;
                }
            }
        }

        static float debouncer = 0f;
        static bool canDelay = false;
        public static void LaggyRig()
        {
            if (Time.time > debouncer)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                debouncer = Time.time + 0.12f;
                canDelay = true;
            }
            else
            {
                if (canDelay)
                {
                    canDelay = false;
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
            }
        }

        static float debouncer1 = 0f;
        static bool canDelay1 = false;
        public static void LaggyRig1()
        {
            if (Time.time > debouncer1)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
                debouncer1 = Time.time + 1f;
                canDelay1 = true;
            }
            else
            {
                if (canDelay1)
                {
                    canDelay1 = false;
                }
                else
                {
                    GorillaTagger.Instance.offlineVRRig.enabled = false;
                }
            }
        }

        public static void Spaz()
        {
            if (ControllerInputPoller.instance.rightControllerSecondaryButton || ControllerInputPoller.instance.rightGrab || Mouse.current.leftButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 360));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 180), UnityEngine.Random.Range(0, 180));
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 180), UnityEngine.Random.Range(0, 180));
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3(UnityEngine.Random.Range(0, 360), UnityEngine.Random.Range(0, 180), UnityEngine.Random.Range(0, 180));
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3(0, 0, 0);
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3(0, 0, 0);
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3(0, 0, 0);
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.eulerAngles = new Vector3(0, 0, 0);
                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.eulerAngles = new Vector3(0, 0, 0);
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.eulerAngles = new Vector3(0, 0, 0);
            }
        }

        public static void SpinHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x += 10f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y += 10f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z += 10f;
        }

        public static void SpinHeadX()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x += 10f;
        }

        public static void SpinHeadY()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y += 10f;
        }

        public static void SpinHeadZ()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z += 10f;
        }

        public static void FixHead()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 0f;
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 0f;
        }

        public static void SnapHeadX()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 90f;
        }

        public static void SnapHeadY()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 90f;
        }

        public static void SnapHeadZ()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 90f;
        }

        public static void FlipHeadX()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.x = 180f;
        }

        public static void FlipHeadY()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.y = 180f;
        }

        public static void FlipHeadZ()
        {
            GorillaTagger.Instance.offlineVRRig.head.trackingRotationOffset.z = 180f;
        }

        public static void RigGun()
        {
            GunLib.Gun(() => {
                GorillaTagger.Instance.offlineVRRig.enabled = false;
                GorillaTagger.Instance.offlineVRRig.transform.position = GunHit.point + new Vector3(0, 1, 0);
                GorillaTagger.Instance.myVRRig.transform.position = GunHit.point + new Vector3(0, 1, 0);
            }, FixRig);
        }

        public static void RealisticArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.09f, 1.09f, 1.09f);
        }

        public static void LongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.19f, 1.19f, 1.19f);
        }

        public static void SuperLongArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1.3f, 1.3f, 1.3f);
        }

        public static void ArmSizeChanger()
        {
            if (leftG)
            {
                GorillaLocomotion.Player.Instance.transform.localScale += new Vector3(.05f, .05f, .05f);
            }
            if (rightG)
            {
                GorillaLocomotion.Player.Instance.transform.localScale -= new Vector3(.05f, .05f, .05f);
            }
            if (leftP || rightP)
            {
                FixArms();
            }
        }

        public static void FixArms()
        {
            GorillaLocomotion.Player.Instance.transform.localScale = new Vector3(1f, 1f, 1f);
        }

        public static void FlipArms()
        {
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.position = GorillaTagger.Instance.leftHandTransform.position;
            GorillaLocomotion.Player.Instance.rightControllerTransform.transform.rotation = GorillaTagger.Instance.leftHandTransform.rotation;
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            GorillaLocomotion.Player.Instance.leftControllerTransform.transform.rotation = GorillaTagger.Instance.rightHandTransform.rotation;
        }

        public static void Helicopter()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position += new Vector3(0f, 0.075f, 0f);
                GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10f, 0f));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;

                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void Beyblade()
        {
            if (ControllerInputPoller.instance.rightGrab || Mouse.current.rightButton.isPressed)
            {
                GorillaTagger.Instance.offlineVRRig.enabled = false;

                GorillaTagger.Instance.offlineVRRig.transform.position = GorillaTagger.Instance.headCollider.transform.position;

                GorillaTagger.Instance.offlineVRRig.transform.rotation = Quaternion.Euler(GorillaTagger.Instance.offlineVRRig.transform.rotation.eulerAngles + new Vector3(0f, 10f, 0f));
                GorillaTagger.Instance.offlineVRRig.head.rigTarget.transform.rotation = GorillaTagger.Instance.offlineVRRig.transform.rotation;

                GorillaTagger.Instance.offlineVRRig.leftHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * -1f;
                GorillaTagger.Instance.offlineVRRig.rightHand.rigTarget.transform.position = GorillaTagger.Instance.offlineVRRig.transform.position + GorillaTagger.Instance.offlineVRRig.transform.right * 1f;
            }
            else
            {
                GorillaTagger.Instance.offlineVRRig.enabled = true;
            }
        }

        public static void IronMan()
        {
            if (leftG)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(10 * GorillaTagger.Instance.leftHandTransform.up, ForceMode.Acceleration);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(2 * -GorillaTagger.Instance.leftHandTransform.right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(true, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tagHapticDuration);
            }
            if (rightG)
            {
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(10 * GorillaTagger.Instance.rightHandTransform.up, ForceMode.Acceleration);
                GorillaLocomotion.Player.Instance.bodyCollider.attachedRigidbody.AddForce(2 * GorillaTagger.Instance.rightHandTransform.right, ForceMode.Acceleration);
                GorillaTagger.Instance.StartVibration(false, GorillaTagger.Instance.tapHapticStrength / 2, GorillaTagger.Instance.tagHapticDuration);
            }
        }

        /*public static GameObject enderpearl = null;
        public static float throwForce = 10f;
        public static bool isThrown = false; 
        public static float throwTime = 0f;
        public static Material endermat = TextureLoader("https://www.google.com/url?sa=i&url=https%3A%2F%2Femoji.gg%2Femoji%2F7993-enderpearl&psig=AOvVaw3vSVenIVelCXIRf0ArzwaW&ust=1728179677864000&source=images&cd=vfe&opi=89978449&ved=0CBQQjRxqGAoTCJCJhpGR9ogDFQAAAAAdAAAAABCTAQ");

        public static void Enderpearl()
        {
            if (rightG)
            {
                if (enderpearl == null)
                {
                    enderpearl = GameObject.CreatePrimitive(0);
                    UnityEngine.Object.Destroy(enderpearl.GetComponent<Collider>());
                    UnityEngine.Object.Destroy(enderpearl.GetComponent<Rigidbody>());
                    enderpearl.GetComponent<Renderer>().material = endermat;
                }
                enderpearl.transform.position = GorillaTagger.Instance.rightHandTransform.position;
            }
            if (!rightG && enderpearl != null)
            {
                enderpearl.GetComponent<Rigidbody>().AddForce(enderpearl.transform.forward * throwForce, ForceMode.Impulse);
                isThrown = true;
                throwTime = Time.time;
            }
            if (isThrown && Time.time >= throwTime + 2)
            {
                GorillaTagger.Instance.offlineVRRig.transform.position = enderpearl.transform.position;
                GorillaTagger.Instance.GetComponent<Rigidbody>().velocity = enderpearl.transform.position;
                UnityEngine.Object.Destroy(enderpearl);
                enderpearl = null;
                isThrown = false;
                throwTime = Time.time + 0.2f;
            }
        }*/ //crashing out
    }
}
