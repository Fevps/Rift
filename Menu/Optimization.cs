using GorillaLocomotion;
using GorillaNetworking;
using HarmonyLib;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;
using Player = Photon.Realtime.Player;
using static StupidTemplate.Menu.Main;

namespace StupidTemplate.Menu
{
    internal class Optimization
    {
        public static bool Infected(VRRig p)
        {
            return p.mainSkin.material.name.Contains("It") || p.mainSkin.material.name.Contains("fected");
        }

        public static void Category(int i) { buttonsType = i; pageNumber = 0; }

        public static VRRig GetVRRigFromPlayer(Player p)
        {
            return GorillaGameManager.instance.FindPlayerVRRig(p);
        }

        public static void Join(string room)
        {
            if (!PhotonNetwork.InRoom && room == RoomCode)
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(room, 0);
            }
            else if (room != RoomCode)
            {
                PhotonNetworkController.Instance.AttemptToJoinSpecificRoom(room, 0);
            }
        }

        public static void Name(string newName)
        {
            PhotonNetwork.LocalPlayer.NickName = newName;
            GorillaComputer.instance.currentName = newName;
            GorillaComputer.instance.offlineVRRigNametagText.text = newName;
            GorillaComputer.instance.savedName = newName;
            PlayerPrefs.SetString("playerName", newName);
            PlayerPrefs.Save();
        }

        public static void CreateTracer(Vector3 Position1, Vector3 Position2, Color32 Color, float Size)
        {
            wrigj4wgj++;
            GameObject lineFollow = new GameObject("Line" + wrigj4wgj);
            LineRenderer lineUser = lineFollow.AddComponent<LineRenderer>();
            lineUser.material.shader = Shader.Find("GUI/Text Shader");
            lineUser.startColor = Color;
            lineUser.endColor = Color;
            lineUser.startWidth = Size;
            lineUser.endWidth = Size;
            lineUser.useWorldSpace = true;
            lineUser.positionCount = 2;
            lineUser.SetPosition(0, Position1);
            lineUser.SetPosition(1, Position2);
            Object.Destroy(lineFollow, Time.deltaTime);
        }
        private static int wrigj4wgj = 0;

        public static void CreateBox(Color Color, VRRig player)
        {
            Vector2 initialScaledHitbox = new Vector2(player.transform.localScale.x * 0.35f, player.transform.localScale.y * 0.7f);
            Vector3 center = player.transform.position - new Vector3(0f, 0.075f, 0f);
            Vector3 minBound = center - new Vector3(initialScaledHitbox.x / 2, initialScaledHitbox.y / 2, 0);
            Vector3 maxBound = center + new Vector3(initialScaledHitbox.x / 2, initialScaledHitbox.y / 2, 0);
            Vector3[] handPositions = { player.rightHand.rigTarget.transform.position, player.leftHand.rigTarget.transform.position };
            foreach (Vector3 handPos in handPositions)
            {
                minBound = Vector3.Min(minBound, handPos);
                maxBound = Vector3.Max(maxBound, handPos);
            }
            Vector2 finalSize = new Vector2(maxBound.x - minBound.x, maxBound.y - minBound.y);
            if (finalSize.x < initialScaledHitbox.x) finalSize.x = initialScaledHitbox.x;
            if (finalSize.y < initialScaledHitbox.y) finalSize.y = initialScaledHitbox.y;
            try
            {
                Camera cam = GameObject.Find("Shoulder Camera").GetComponent<Camera>();
                GameObject lineFollow = new GameObject("Line");
                LineRenderer lineUser = lineFollow.AddComponent<LineRenderer>();
                lineUser.startColor = Color;
                lineUser.endColor = Color;
                lineUser.startWidth = 0.0225f;
                lineUser.endWidth = 0.0225f;
                lineUser.useWorldSpace = false;
                Vector3[] points = new Vector3[5];
                points[0] = new Vector3(-finalSize.x / 2, finalSize.y / 2, 0f);
                points[1] = new Vector3(finalSize.x / 2, finalSize.y / 2, 0f);
                points[2] = new Vector3(finalSize.x / 2, -finalSize.y / 2, 0f);
                points[3] = new Vector3(-finalSize.x / 2, -finalSize.y / 2, 0f);
                points[4] = points[0];
                lineUser.positionCount = points.Length;
                lineUser.SetPositions(points);
                lineUser.material = new Material(Shader.Find("GUI/Text Shader"));
                lineFollow.transform.position = center;
                lineFollow.transform.LookAt(cam.transform.position);
                GameObject.Destroy(lineFollow, Time.deltaTime);
            }
            catch { }
        }

        public static void CreateTextObject(Color Color, Vector3 Position, string Text)
        {
            GameObject mymemorymymemory = new GameObject("mymemory");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 18;
            memorymesh.fontStyle = FontStyle.Bold;
            memorymesh.characterSize = 0.1f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color;
            mymemorymymemory.transform.position = Position;
            memorymesh.text = Text;
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Object.Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void CreateTextObjectNoBlur(Color Color, Vector3 Position, string Text)
        {
            GameObject mymemorymymemory = new GameObject("mymemory");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = 100;
            memorymesh.fontStyle = FontStyle.Bold;
            memorymesh.characterSize = 0.01f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = TextAlignment.Center;
            memorymesh.color = Color;
            mymemorymymemory.transform.position = Position;
            memorymesh.text = Text;
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Object.Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void CreateTextObject(Color Color, Vector3 Position, float Size, string Text, TextAlignment Alignment)
        {
            GameObject mymemorymymemory = new GameObject("mymemory");
            TextMesh memorymesh = mymemorymymemory.AddComponent<TextMesh>();
            memorymesh.fontSize = (int)Size;
            memorymesh.fontStyle = FontStyle.Bold;
            memorymesh.characterSize = 0.1f;
            memorymesh.anchor = TextAnchor.MiddleCenter;
            memorymesh.alignment = Alignment;
            memorymesh.color = Color;
            mymemorymymemory.transform.position = Position;
            memorymesh.text = Text;
            mymemorymymemory.transform.LookAt(Camera.main.transform.position);
            mymemorymymemory.transform.Rotate(0, 180, 0);
            Object.Destroy(mymemorymymemory, Time.deltaTime);
        }

        public static void CreatePlatform(Color Color, Vector3 Position, Quaternion Rotation, float T)
        {
            GameObject Platform = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(Platform.GetComponent<Collider>());
            Platform.transform.position = Position;
            Platform.transform.rotation = Rotation;
            Platform.transform.localScale = new Vector3(0.025f, 0.23f, 0.32f);
            Platform.GetComponent<Renderer>().material.color = Color;
            Object.Destroy(Platform, T);

            GameObject Outline = GameObject.CreatePrimitive(PrimitiveType.Cube);
            Object.Destroy(Outline.GetComponent<Collider>());
            Outline.transform.position = Position;
            Outline.transform.rotation = Rotation;
            Outline.transform.localScale = new Vector3(0.023f, 0.235f, 0.325f);
            Outline.GetComponent<Renderer>().material.color = Color.black;
            Object.Destroy(Outline, T);
        }

        public static string yourName()
        {
            return PhotonNetwork.LocalPlayer.NickName;
        }

        public static string SeparateText(string input)
        {
            return string.Join(" ", input.ToCharArray());
        }

        public static string GetMasterINFORMATION(Player i)
        {
            return i.IsMasterClient ? "Master Client" : "";
        }

        public static void ToggleColliders(bool input)
        {
            foreach (MeshCollider collider in Resources.FindObjectsOfTypeAll<MeshCollider>())
            {
                collider.enabled = input;
            }
        }

        public static void ConfigureTeleport(Vector3 Position)
        {
            GorillaTagger.Instance.transform.position = Position;
            GorillaTagger.Instance.offlineVRRig.transform.position = Position;
            GorillaTagger.Instance.myVRRig.transform.position = Position;

            GorillaTagger.Instance.rigidbody.velocity = Vector3.zero;
            GorillaTagger.Instance.offlineVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;
            GorillaTagger.Instance.myVRRig.GetComponent<Rigidbody>().velocity = Vector3.zero;

            GorillaTagger.Instance.offlineVRRig.enabled = true;
        }

        public static void Teleport(Vector3 Position)
        {
            GorillaTagger.Instance.offlineVRRig.enabled = false;
            ConfigureTeleport(Position);
        }

        public static VRRig GetRandomVRRig(bool includeSelf)
        {
            VRRig random = GorillaParent.instance.vrrigs[Random.Range(0, GorillaParent.instance.vrrigs.Count - 1)];
            if (includeSelf)
            {
                return random;
            }
            else
            {
                if (random != GorillaTagger.Instance.offlineVRRig)
                {
                    return random;
                }
                else
                {
                    return GetRandomVRRig(includeSelf);
                }
            }
        }

        public static VRRig GetClosestVRRig()
        {
            float num = float.MaxValue;
            VRRig outRig = null;
            foreach (VRRig vrrig in GorillaParent.instance.vrrigs)
            {
                if (Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position) < num)
                {
                    num = Vector3.Distance(GorillaTagger.Instance.bodyCollider.transform.position, vrrig.transform.position);
                    outRig = vrrig;
                }
            }
            return outRig;
        }

        public static PhotonView GetPhotonViewFromVRRig(VRRig p)
        {
            return (PhotonView)Traverse.Create(p).Field("photonView").GetValue();
        }

        public static Player GetRandomPlayer(bool includeSelf)
        {
            if (includeSelf)
            {
                return PhotonNetwork.PlayerList[Random.Range(0, PhotonNetwork.PlayerList.Length - 1)];
            }
            else
            {
                return PhotonNetwork.PlayerListOthers[Random.Range(0, PhotonNetwork.PlayerListOthers.Length - 1)];
            }
        }

        public static Player GetPlayerFromVRRig(VRRig p)
        {
            return GetPhotonViewFromVRRig(p).Owner;
        }

        public static Player GetPlayerFromID(string id)
        {
            Player found = null;
            foreach (Player target in PhotonNetwork.PlayerList)
            {
                if (target.UserId == id)
                {
                    found = target;
                    break;
                }
            }
            return found;
        }

        public static void GetBugOwnership()
        {
            foreach (ThrowableBug bug in Resources.FindObjectsOfTypeAll<ThrowableBug>())
            {
                bug.allowPlayerStealing = true;
                bug.allowWorldSharableInstance = true;
                bug.OnOwnershipRequest(PhotonNetwork.LocalPlayer);
                bug.WorldShareableRequestOwnership();
            }
        }

        public static void TeleportBug(Vector3 Position)
        {
            foreach (ThrowableBug bug in Resources.FindObjectsOfTypeAll<ThrowableBug>())
            {
                bug.transform.position = Position;
            }
        }
    }
}
