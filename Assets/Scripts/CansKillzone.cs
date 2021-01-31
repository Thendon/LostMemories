using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CansKillzone : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Destroy(other.gameObject);

        GameManager.Instance.AddCoins(3);
    }
}
