using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.XR.Interaction.Toolkit;

public class Wearable : MonoBehaviour
{
    public ParentConstraint parentContraint;
    public XRGrabInteractable grabInteractable;

    public void Awake()
    {
        parentContraint = GetComponentInParent<ParentConstraint>();
        grabInteractable = GetComponentInParent<XRGrabInteractable>();
    }


    private void OnTriggerStay(Collider other)
    {
        if (grabInteractable.attachTransform != null)
        {
            return;
        }

        Debug.Log("Hat collision with: " + other.gameObject.name);

        WearableAttachmentPoint attachmentPoint = other.GetComponent<WearableAttachmentPoint>();
        if (attachmentPoint != null)
        {
            var source = parentContraint.GetSource(0);
            source.sourceTransform = attachmentPoint.transform;
            parentContraint.SetSource(0, source);
        }
    }
}
