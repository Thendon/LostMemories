using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attraction : MonoBehaviour
{
    [SerializeField]
    public Memory memory = null;
    [SerializeField]
    public float minUseDuration = 10.0f;
    [SerializeField]
    public int cost = 1;
}
