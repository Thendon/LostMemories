using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewMemory", menuName = "Memory", order = 1)]
public class Memory : ScriptableObject
{
    public Sprite image;
    public string header;
    public string description;
}
