using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class CamAttachment : MonoBehaviour
{
    [SerializeField]
    Vector3 offset;
}
