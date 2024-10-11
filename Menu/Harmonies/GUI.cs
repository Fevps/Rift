using BepInEx;
using GorillaNetworking;
using Photon.Pun;
using StupidTemplate.Menu;
using StupidTemplate.Menu.Mods;
using System;
using UnityEngine;
using static Photon.Pun.UtilityScripts.TabViewManager;
using static StupidTemplate.Menu.Main;
[BepInPlugin("RiftHUD", "", "0.1.5")]
public class RiftUI : BaseUnityPlugin
{
    public bool act = false;
    public float debo;
    public Rect rect = new Rect(0f, 0f, 200f, 60f);
    public string code = "";
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
                    GUIStyle legacyStyle = new GUIStyle
                    {
                        fontSize = 15,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleRight,
                        normal =
                        {
                            textColor = Color.Lerp(Color.magenta, Color.green, Mathf.PingPong(Time.time, 1))
                        },
                        padding = new RectOffset(10, 10, 5, 5)
                    };
                    GUIStyle playerlistStyle = new GUIStyle
                    {
                        fontSize = 12,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.UpperRight,
                        normal =
                        {
                            textColor = Color.white
                        }
                    };
                    GUIStyle titleStyle = new GUIStyle
                    {
                        fontSize = 20,
                        fontStyle = FontStyle.Bold,
                        alignment = TextAnchor.MiddleRight,
                        normal =
                        {
                            textColor = Color.Lerp(Color.red, Color.grey, Mathf.PingPong(Time.time, 1))
                        }
                    };
                    GUIStyle lineStyle = new GUIStyle(GUI.skin.box)
                    {
                        border = new RectOffset(1, 1, 1, 1),
                        fixedHeight = 2 
                    };
                    GUILayout.BeginArea(new Rect(Screen.width - 205, 10, 210, Screen.height - 20));
                    GUILayout.BeginVertical(GUI.skin.box, GUILayout.ExpandHeight(true));
                    GUILayout.Label("Rift  ", titleStyle);
                    GUILayout.Space(10);
                    if (PhotonNetwork.InRoom)
                    {
                        GUILayout.Label("Players: ", playerlistStyle);
                        foreach (Photon.Realtime.Player player in PhotonNetwork.PlayerList)
                        {
                            GUILayout.Label(player.NickName, legacyStyle);
                        }
                        GUILayout.Space(5);
                        GUILayout.Box("", lineStyle, GUILayout.ExpandWidth(true));
                        GUILayout.Space(5);
                    }
                    foreach (ButtonInfo[] buttons in Buttons.buttons)
                    {
                        foreach (ButtonInfo enabled in buttons)
                        {
                            if (enabled.enabled)
                            {
                                if (GUILayout.Button(enabled.buttonText, legacyStyle, GUILayout.Height(30)))
                                {
                                    Toggle(enabled.buttonText);
                                }
                            }
                        }
                    }
                    GUILayout.EndVertical();
                    GUILayout.EndArea();
                } catch { }

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

    private void Tag(VRRig target)
    {
        Advantages.Tag(target);
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