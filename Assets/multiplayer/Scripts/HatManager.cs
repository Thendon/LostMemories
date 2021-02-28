using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RotaryHeart.Lib.SerializableDictionary;
using TCPSocketNetwork;
using TCPNetPackage;
using LostMemoriesNetMessages;

public class HatManager : MonoBehaviour
{
    [System.Serializable]
    public class HatDictionary : SerializableDictionaryBase<string, GameObject> { }

    [SerializeField]
    HatDictionary hats;

    AvatarManager avatars;
    TCPSocketConnection connection;

    void Awake()
    {
        avatars = GetComponent<AvatarManager>();
        connection = GetComponent<TCPSocketConnection>();

        PackageManager.RegisterNetPackage((uint)NetHat.Package.WearsHat, NetHat.channel, OnPlayerWearsHat);
        PackageManager.RegisterNetPackage((uint)NetHat.Package.ReleasesHat, NetHat.channel, OnPlayerReleaseHat);
        //Todo steal hat?? :D
    }

    void OnPlayerWearsHat(PackageIn data)
    {
        print("onplayerwearshat");
        ulong playerID = data.ReadUnsignedLong();
        print(playerID);
        string hatName = data.ReadString();
        print(hatName);

        connection.ScheduleTask(new Task(delegate {
            Avatar avatar = avatars.GetAvatar(playerID);
            print(avatar);

            if (avatar == null)
                return;

            GameObject hat = Instantiate(hats[hatName]);
            avatar.AttachHat(hat);
        }));
    }

    void OnPlayerReleaseHat(PackageIn data)
    {
        ulong playerID = data.ReadUnsignedLong();

        Avatar avatar = avatars.GetAvatar(playerID);
        if (avatar == null)
            return;

        connection.ScheduleTask(new Task(delegate {
            avatar.RemoveHat();
        }));
    }
}
