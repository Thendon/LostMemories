using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Visual;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Reactivate(5));

        m_Visual.SetActive(false);
    }

    IEnumerator Reactivate(int secs)
    {
        yield return new WaitForSeconds(secs);
        m_Visual.SetActive(true);
    }
}
