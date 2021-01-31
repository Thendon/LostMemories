using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MemoryDisplay : MonoBehaviour
{
    [SerializeField]
    TextMeshPro header;
    [SerializeField]
    TextMeshPro body;
    [SerializeField]
    SpriteRenderer image;

    public void Display(string h, string b, Sprite img)
    {
        header.text = h;
        body.text = b;
        image.sprite = img;
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
