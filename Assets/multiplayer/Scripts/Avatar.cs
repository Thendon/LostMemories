using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avatar : MonoBehaviour
{
    public ulong playerID;
    public Vector3 targetPos;
    public Quaternion targetRot;
    public bool attachedToSomething = false;

    [SerializeField]
    Transform headTransform = null;
    [SerializeField]
    float movementThreshold = 0.1f;
    [SerializeField]
    float snapAngle = 60.0f;
    [SerializeField]
    float rotationSpeed = 4.0f;

    internal void Init(ulong id)
    {
        playerID = id;
    }

    void Update()
    {
        float velocity = Vector3.Distance(transform.position, targetPos);
        float ang = Mathf.Acos(Vector3.Dot(headTransform.forward, Vector3.Cross(transform.right, headTransform.up)));

        transform.position = targetPos;
        headTransform.rotation = Quaternion.RotateTowards(headTransform.rotation, targetRot, rotationSpeed * 180.0f * Time.deltaTime);

        if (!attachedToSomething && (velocity > movementThreshold || ang * Mathf.Rad2Deg > snapAngle))
            SnapBodyToHeadRotation();
    }

    void SnapBodyToHeadRotation()
    {
        Vector3 headEuler = headTransform.eulerAngles;
        Vector3 headLocalEuler = headTransform.localEulerAngles;
        Vector3 transformEuler = transform.eulerAngles;

        transformEuler.y = headEuler.y;
        headLocalEuler.y = 0.0f;

        transform.eulerAngles = transformEuler;
        headTransform.localEulerAngles = headLocalEuler;
    }
}
