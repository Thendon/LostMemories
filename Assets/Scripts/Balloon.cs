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

    [SerializeField]
    public Attraction attraction = null;

    Player player = null;
    PlayerVR playerVR = null;

    void Awake()
    {
        player = FindObjectOfType<Player>();
        playerVR = FindObjectOfType<PlayerVR>();
    }

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Reactivate(5));

        m_Visual.SetActive(false);

        m_Particle.Play();

        GameManager.Instance.AddCoins(3);
        GameManager.Instance.AddMemory(attraction.memory);

        if (player != null)
        {
            player.DisplayMemory();
        }
        else if (playerVR != null)
        {
            playerVR.DisplayMemory();
        }
    }

    IEnumerator Reactivate(int secs)
    {
        yield return new WaitForSeconds(secs);
        m_Visual.SetActive(true);
    }
}
