using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DartArea : MonoBehaviour
{
    [SerializeField]
    protected Transform m_Spawnpoint;

    void OnTriggerExit(Collider other)
    {
        StartCoroutine(Respawn(5, other));
    }

    IEnumerator Respawn(int secs, Collider other)
    {
        yield return new WaitForSeconds(secs);

        if (other.attachedRigidbody)
        {
            other.attachedRigidbody.angularVelocity = Vector3.zero;
            other.attachedRigidbody.velocity = Vector3.zero;

            other.attachedRigidbody.useGravity = true;
            other.attachedRigidbody.isKinematic = false;
        }

        other.gameObject.transform.position = m_Spawnpoint.position;
    }
}
