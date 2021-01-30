using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBackboard : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.useGravity = false;
            other.attachedRigidbody.isKinematic = true;
        }

        Destroy(other.gameObject, 4f);
    }
}
