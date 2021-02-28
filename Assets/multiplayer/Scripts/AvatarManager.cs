using System.Collections;
using System.Collections.Generic;
using TCPNetPackage;
using LostMemoriesNetMessages;
using UnityEngine;

namespace TCPSocketNetwork
{
    public class AvatarManager : MonoBehaviour
    {
        [SerializeField]
        GameObject avatarPrefab = null;

        public Dictionary<ulong, Avatar> avatars = new Dictionary<ulong, Avatar>();

        TCPSocketConnection connection;

        void Awake()
        {
            connection = GetComponent<TCPSocketConnection>();
            PackageManager.RegisterNetPackage((uint)NetUser.Package.Location, NetUser.channel, OnClientLocationChanged);

            TCPSocketConnection.OnInitialized += OnInitialized;
            TCPSocketConnection.OnPlayerConnected += OnPlayerConnected;
            TCPSocketConnection.OnPlayerDisconnected += OnPlayerDisconnected;
        }

        public void BroadcastLocation(Vector3 pos, Quaternion rot)
        {
            if (!connection.Ready())
                return;

            using (PackageOut package = new PackageOut())
            {
                package.AddVector3(pos.x, pos.y, pos.z);
                package.AddQuaternion(rot.x, rot.y, rot.z, rot.w);

                PackageManager.SendData(package, (uint)NetUser.Package.Location, NetUser.channel);
            }
        }

        void OnInitialized()
        {
            foreach (ulong playerID in connection.connectedPlayers)
                ScheduleAvatarSpawn(playerID);
        }

        void OnPlayerDisconnected(ulong playerID)
        {
            connection.ScheduleTask(new Task(delegate
            {
                print("other cl disconnected " + playerID);

                if (avatars.ContainsKey(playerID))
                    DeleteAvatar(playerID);
            }));
        }

        private void OnPlayerConnected(ulong playerID)
        {
            ScheduleAvatarSpawn(playerID);
        }

        private void OnClientLocationChanged(PackageIn data)
        {
            ulong playerID = data.ReadUnsignedLong();

            //TODO: Request missing player!
            if (!avatars.ContainsKey(playerID))
                return;

            Vector3 pos = Vector3.zero;
            data.ReadVector3(ref pos.x, ref pos.y, ref pos.z);
            Quaternion rot = Quaternion.identity;
            data.ReadQuaternion(ref rot.x, ref rot.y, ref rot.z, ref rot.w);

            avatars[playerID].targetPos = pos;
            avatars[playerID].targetRot = rot;
        }

        void GenerateAvatar(ulong playerID)
        {
            Avatar avatar = Instantiate(avatarPrefab).GetComponent<Avatar>();
            avatar.Init(playerID);
            avatars.Add(playerID, avatar);
        }

        void ScheduleAvatarSpawn(ulong playerID)
        {
            connection.ScheduleTask(new Task(delegate
            {
                print("other cl connected " + playerID);

                if (avatars.ContainsKey(playerID))
                    DeleteAvatar(playerID);

                GenerateAvatar(playerID);
            }));
        }

        void DeleteAvatar(ulong playerID)
        {
            if (!avatars.ContainsKey(playerID))
                return;

            Destroy(avatars[playerID].gameObject);
            avatars.Remove(playerID);
        }

        public Avatar GetAvatar(ulong playerID)
        {
            if (!avatars.ContainsKey(playerID))
                return null;

            return avatars[playerID];
        }
    }
}
