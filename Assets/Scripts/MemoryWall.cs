using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MemoryWall : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer[] memSprites;

    void Start()
    {
        GameManager.Instance.memoryAdded.AddListener(UpdateSprites);
    }

    void UpdateSprites()
    {
        List<Memory> mems = GameManager.Instance.GetMemories();

        for (int i = 0; i < mems.Count; i++)
            memSprites[i].sprite = mems[i].image;
    }
}
