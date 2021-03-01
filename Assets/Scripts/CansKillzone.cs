using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CansKillzone : MonoBehaviour
{
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
        Destroy(other.gameObject);

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
}
