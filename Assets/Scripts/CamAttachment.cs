using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CamAttachment : MonoBehaviour
{
    [SerializeField]
    Vector3 posOffset = Vector3.zero;
    [SerializeField]
    Quaternion rotOffset = Quaternion.identity;
    [SerializeField]
    public Attraction attraction = null;

    public Vector3 GetPosition()
    {
        return transform.TransformPoint(posOffset);
        //return transform.position + transform.forward * posOffset.z + transform.right * posOffset.x + transform.up * posOffset.y;
    }

    public Quaternion GetRotation()
    {
        //evll anders herum :D
        return transform.rotation * rotOffset;
    }

    public bool UseChair()
    {
        return GameManager.Instance.RemoveCoins(attraction.cost);
    }
}
