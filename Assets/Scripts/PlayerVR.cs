using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVR : MonoBehaviour
{
    [SerializeField]
    MemoryDisplay memoryDisplay;
    [SerializeField]
    GameObject winPrefab;
    [SerializeField]
    float winDistance = 10.0f;

    CamAttachment chairAttached = null;
    float attachedTime = 0.0f;
    float memoryTime = 0.0f;
    bool memoryIsDisplayed = false;
    Memory showMemoryOnLeave = null;
    bool won = false;

    void Update()
    {
        HandleAttraction();
        if (memoryIsDisplayed)
        {
            memoryTime += Time.deltaTime;
            if (memoryTime > 10.0f)
                HideMemory();
        }
    }

    void HandleAttraction()
    {
        if (chairAttached == null)
            return;

        attachedTime += Time.deltaTime;

        Attraction attraction = chairAttached.attraction;
        if (attraction == null)
            return;

        if (attachedTime < attraction.minUseDuration)
            return;

        Memory memory = attraction.memory;
        if (memory == null)
            return;

        if (GameManager.Instance.AddMemory(memory))
            showMemoryOnLeave = memory;
    }

    public void DisplayMemory()
    {
        memoryIsDisplayed = true;
        memoryTime = 0.0f;

        memoryDisplay.Display(showMemoryOnLeave.header, showMemoryOnLeave.description, showMemoryOnLeave.image);
        showMemoryOnLeave = null;
    }

    void HideMemory()
    {
        memoryIsDisplayed = false;

        memoryDisplay.Hide();

        if (!won && GameManager.Instance.IsGameover())
        {
            Win();
        }
    }

    void Win()
    {
        won = true;
        GameObject winObject = Instantiate(winPrefab);
        winObject.transform.position = transform.position + transform.forward * winDistance;
    }

    public void AttachPlayerTo(CamAttachment t)
    {
        chairAttached = t;

        attachedTime = 0.0f;
    }

    public void ReleasePlayer()
    {
        chairAttached = null;

        if (showMemoryOnLeave != null)
            DisplayMemory();
    }
}
