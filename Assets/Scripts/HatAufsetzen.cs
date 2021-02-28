using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatAufsetzen : MonoBehaviour, IInteractable
{

    bool isInteractable => (wearable != null) && !wearable.isInteracted && !wearable.isWorn;
    private Transform parentTransform;
    private Wearable wearable;
    private Player interactor;

    private void Awake()
    {
        wearable = GetComponentInChildren<Wearable>();
    }


    public void Interact(Player player)
    {
        if (!isInteractable) return;
        if (player.attachmentPoint.wearableAttached) return;
        interactor = player;
        parentTransform = interactor.attachmentPoint.transform;
        StartCoroutine(MoveTowards());
    }

    public IEnumerator MoveTowards()
    {
        var shouldMove = true;
        while (shouldMove)
        {
            Vector3 towards = ((parentTransform.position + Vector3.up * 0.05f) - this.transform.position);
            float distance = towards.magnitude;
            Vector3 towardsNormalized = towards / distance;

            this.transform.position = this.transform.position + towardsNormalized * 10f * Time.deltaTime;

            if (distance < 0.1f)
            {
                shouldMove = false;
                WearableAttachmentPoint attachmentPoint = interactor.attachmentPoint;
                wearable.GetWornBy(attachmentPoint);
                yield break;
            }
            yield return null;
        }
    }

}
