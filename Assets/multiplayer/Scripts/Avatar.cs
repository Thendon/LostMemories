using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public ulong playerID;
    public Vector3 targetPos;
    public Quaternion targetRot;

    internal void Init(ulong id)
    {
        playerID = id;
    }

    void Update()
    {
        transform.position = targetPos;
        transform.rotation = targetRot;
    }
}
