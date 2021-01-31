using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Balloon : MonoBehaviour
{
    [SerializeField]
    protected GameObject m_Visual;

    [SerializeField]
    protected VisualEffect m_Particle;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Reactivate(5));

        m_Visual.SetActive(false);

        m_Particle.Play();

        GameManager.Instance.AddCoins(3);
    }

    IEnumerator Reactivate(int secs)
    {
        yield return new WaitForSeconds(secs);
        m_Visual.SetActive(true);
    }
}
