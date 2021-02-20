using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class Wearable : MonoBehaviour
{
    public ParentConstraint parentContraint;
    public XRGrabInteractable grabInteractable;

    private WearableAttachmentPoint attachmentPoint;

    public bool isWorn = false;
    public bool isInteracted = false;

    public void GetWornBy(WearableAttachmentPoint newAttachmentPoint)
    {
        if (newAttachmentPoint.wearableAttached)
            return;

        attachmentPoint = newAttachmentPoint;

        var source = parentContraint.GetSource(0);
        source.sourceTransform = attachmentPoint.transform;
        parentContraint.SetSource(0, source);
        parentContraint.weight = 1;
        parentContraint.constraintActive = true;
        isWorn = true;
        attachmentPoint.wearableAttached = true;
    }

    public void Release()
    {
        if (isWorn)
        {
            var source = parentContraint.GetSource(0);
            source.sourceTransform = null;
            parentContraint.SetSource(0, source);
            parentContraint.weight = 0;
            parentContraint.constraintActive = false;
            isWorn = false;
            attachmentPoint.wearableAttached = false;
            attachmentPoint = null;
        }
    }

    public void Awake()
    {
        parentContraint = GetComponentInParent<ParentConstraint>();
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
    }

    public void OnGrabInteractableActivated(SelectEnterEventArgs args)
    {
        //Debug.Log("Activated");

        isInteracted = true;
        Release();

    }

    public void OnGrabInteractableDeactivated(SelectExitEventArgs args)
    {
        //Debug.Log("Deactivated");
        isInteracted = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (isWorn) return;
        if (isInteracted) return;

        if (grabInteractable.attachTransform != null)
        {
            return;
        }

        //Debug.Log("Hat collision with: " + other.gameObject.name);

        var wearableAttachmentPoint = other.GetComponent<WearableAttachmentPoint>();
        if (wearableAttachmentPoint != null)
        {
            GetWornBy(wearableAttachmentPoint);
        }
    }

}
