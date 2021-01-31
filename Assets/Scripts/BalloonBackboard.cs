using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonBackboard : MonoBehaviour
{
    [SerializeField]
    protected Transform m_Spawnpoint;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.rigidbody)
        {
            collision.rigidbody.useGravity = false;
            collision.rigidbody.isKinematic = true;
        }

        StartCoroutine(Respawn(5, collision));
    }

    IEnumerator Respawn(int secs, Collision collision)
    {
        yield return new WaitForSeconds(secs);
        
        if (collision.rigidbody)
        {
            collision.rigidbody.useGravity = true;
            collision.rigidbody.isKinematic = false;
        }

        collision.gameObject.transform.position = m_Spawnpoint.position;
    }
}
