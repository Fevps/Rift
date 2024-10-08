using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using StupidTemplate.Menu;
using System;
using UnityEngine;
[BepInPlugin("RiftHUD", "", "0.1.5")]
public class RiftUI : BaseUnityPlugin
{
    public static bool arrayTrue = true;
    public static bool guibld = true;
    public static bool roomList = true;
    public static bool GUIEnabled = true;
    private bool act = false;
    private float debo;
    private Rect rect = new Rect(0f, 0f, 200f, 60f);
    private string code = "";


    void OnGUI()
    {
        if (GUIEnabled)
        {
            if (UnityInput.Current.GetKey(KeyCode.F3) && Time.time > debo + 0.5f)
            {
                debo = Time.time;
                act = !act;
            }
            if (PhotonNetwork.InRoom)
            {
                PhotonNetworkController.Instance.disableAFKKick = true;
            }
            if (guibld)
            {
                try
                {
                    string room = PhotonNetwork.InRoom ? PhotonNetwork.CurrentRoom.Name : "No";
                    GUIStyle gui = new GUIStyle { fontSize = 19, normal = { textColor = Color.Lerp(Color.grey, Color.white, Mathf.PingPong(Time.time, 1))} };
                    GUILayout.Label($"<color=white>In Room:</color> <color=lime>{room}</color>\n<color=white>Username:</color> <color=lime>{PhotonNetwork.LocalPlayer.NickName}</color>\n", "\n" + gui);
                    Texture2D boxColor = new Texture2D(1, 1);
                    boxColor.SetPixel(0, 0, Color.Lerp(Color.grey, Color.white, Mathf.PingPong(Time.time, 1)));
                    boxColor.Apply();
                    GUIStyle boxStyle = new GUIStyle(GUI.skin.box)
                    {
                        fontSize = 20,
                        alignment = TextAnchor.MiddleCenter,
                        normal =
                        {
                            textColor = Color.white,
                            background = boxColor
                        },
                        padding = new RectOffset(10, 10, 10, 10)
                    };
                }
                catch { }
            }
            if (arrayTrue)
            {
                try
                {
                    GUIStyle style = new GUIStyle
                    {
                        fontSize = 15,
                        normal =
                        {
                            textColor = Color.Lerp(Color.grey, Color.white, Mathf.PingPong(Time.time, 1))
                        }
                    };
                    GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                    foreach (ButtonInfo[] buttons in Buttons.buttons)
                    {
                        foreach (ButtonInfo enabled in buttons)
                        {
                            if (enabled.enabled)
                            {
                                GUILayout.Label(enabled.buttonText, style, Array.Empty<GUILayoutOption>());
                            }
                        }
                    }
                    GUILayout.EndVertical();
                }
                catch { }
            }
            if (roomList)
            {
                try
                {
                    GUIStyle style = new GUIStyle
                    {
                        fontSize = 18,
                        normal = { textColor = Color.Lerp(Color.grey, Color.white, Mathf.PingPong(Time.time, 1)) } };
                    GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
                    foreach (Photon.Realtime.Player names in PhotonNetwork.PlayerList)
                    {
                        VRRig Tagged = Optimization.GetVRRigFromPlayer(names);
                        string i = names.NickName;
                        GUILayout.Label(i + isTagged(Tagged), style, Array.Empty<GUILayoutOption>());
                    }
                    GUILayout.EndVertical();
                }
                catch { }
            }
            GUI.color = Color.black;
            GUI.contentColor = Color.magenta;
            if (act)
            {
                rect = GUILayout.Window(0, rect, new GUI.WindowFunction(Draw), PhotonNetwork.InRoom ? "<color=red>Room Manager</color>" : "<color=lime>Room Manager</color>", Array.Empty<GUILayoutOption>());
            }
        }
    }

    private void Draw(int id)
    {
        GUI.color = PhotonNetwork.InRoom ? Color.Lerp(Color.red, Color.red, Mathf.PingPong(Time.time, 1)) : Color.Lerp(Color.green, Color.green, Mathf.PingPong(Time.time, 1));
        GUI.contentColor = Color.white;
        GUI.backgroundColor = PhotonNetwork.InRoom ? Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1)) : Color.Lerp(Color.white, Color.green, Mathf.PingPong(Time.time, 1));
        GUILayout.BeginVertical(Array.Empty<GUILayoutOption>());
        code = GUILayout.TextField(code.ToUpper(), Array.Empty<GUILayoutOption>());
        string text = PhotonNetwork.InRoom ? "Leave" : "Join";
        GUI.backgroundColor = PhotonNetwork.InRoom ? Color.Lerp(Color.white, Color.red, Mathf.PingPong(Time.time, 1)) : Color.Lerp(Color.white, Color.green, Mathf.PingPong(Time.time, 1));
        if (GUILayout.Button(text, Array.Empty<GUILayoutOption>()))
        {
            if (PhotonNetwork.InRoom)
                PhotonNetwork.Disconnect();
            else
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(code.ToUpper(), JoinType.Solo);
        }
        GUILayout.EndVertical();
        GUI.DragWindow();
    }

    string isTagged(VRRig i)
    {
        if (i.mainSkin.material.name.Contains("fected") || i.mainSkin.material.name.Contains("It"))
        {
            return " - Tagged";
        }
        return "";
    }
}