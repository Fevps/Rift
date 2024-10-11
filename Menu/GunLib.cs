using Pathfinding.RVO;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
using static StupidTemplate.Menu.Main;
using static StupidTemplate.Menu.Optimization;
using Object = UnityEngine.Object;

namespace StupidTemplate.Menu
{
    internal class GunLib
    {
        public static void Gun(Action enableMethod, Action disableMethod)
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
                    if (enableMethod != null) { enableMethod?.Invoke(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    if (disableMethod != null) { disableMethod?.Invoke(); }
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
                support.SetPosition(0, GorillaTagger.Instance.headCollider.transform.position + new Vector3(0, .3f, 0));
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
                    if (enableMethod != null) { enableMethod?.Invoke(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    if (disableMethod != null) { disableMethod?.Invoke(); }
                }
            }
        }

        public static void GunLock(Action enableMethod, Action disableMethod)
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
                    copyingRig = GetClosestPlayer(GunPointer);
                    GunPointer.transform.position = copyingRig.transform.position;
                    if (enableMethod != null) { enableMethod?.Invoke(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    GunPointer.transform.position = GunHit.point;
                    if (disableMethod != null) { disableMethod?.Invoke(); }
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
                support.SetPosition(0, GorillaTagger.Instance.headCollider.transform.position + new Vector3(0, .3f, 0));
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
                    copyingRig = GetClosestPlayer(GunPointer);
                    GunPointer.transform.position = copyingRig.transform.position;
                    if (enableMethod != null) { enableMethod?.Invoke(); }
                }
                else
                {
                    GunPointer.GetComponent<Renderer>().material.color = green;
                    support.startColor = green;
                    support.endColor = green;
                    GunPointer.transform.position = GunHit.point;
                    if (disableMethod != null) { disableMethod?.Invoke(); }
                }
            }
        }

        public static VRRig GetClosestPlayer(GameObject objectr)
        {
            float num = 2;
            VRRig outRig = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (Vector3.Distance(objectr.transform.position, vrrig.transform.position) < num)
                {
                    num = Vector3.Distance(objectr.transform.position, vrrig.transform.position);
                    outRig = vrrig;
                }
            }
            return outRig;
        }
    }
}