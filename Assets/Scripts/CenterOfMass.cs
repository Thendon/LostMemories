using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CenterOfMass : MonoBehaviour
{
    [SerializeField]
    protected Transform m_CenterOfMass;

    void Start()
    {
        Rigidbody r = GetComponent<Rigidbody>();

        r.centerOfMass = m_CenterOfMass.localPosition;
    }
}
