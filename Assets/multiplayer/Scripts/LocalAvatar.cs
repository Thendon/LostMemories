using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TCPSocketNetwork
{
    public class LocalAvatar : MonoBehaviour
    {
        [SerializeField]
        AvatarManager avatars = null;
        [SerializeField]
        Camera cam = null;

        // Update is called once per frame
        void Update()
        {
            avatars.BroadcastLocation(transform.position, transform.rotation);
        }
    }
}
