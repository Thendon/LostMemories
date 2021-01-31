using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hover : MonoBehaviour
{
    [SerializeField]
    float hoverSpeed = 0.1f;
    [SerializeField]
    float hoverDistance = 0.5f;

    Vector3 startPos;
    float randomOffset;

    void Awake()
    {
        randomOffset = Random.Range(0.0f, 1000.0f);
        startPos = transform.position;
    }

    void Update()
    {
        transform.position = startPos + Vector3.up * Mathf.Sin((randomOffset+Time.time) * hoverSpeed) * hoverDistance;
    }
}
