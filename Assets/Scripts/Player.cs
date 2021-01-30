using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    InputManager input = null;
    [SerializeField]
    Camera cam;
    [SerializeField]
    float turnSpeed = 30.0f;
    [SerializeField]
    float clampAngle = 80.0f;
    [SerializeField]
    float moveSpeed = 1.0f;
    [SerializeField]
    float transitionDuration = 1.0f;
    [SerializeField]
    Transform headTransform;

    CharacterController controller;
    Vector3 velocity = Vector3.zero;
    static public List<CamAttachment> chairsInRange = new List<CamAttachment>();
    Transform transformAttach = null;
    bool transitioning = false;
    float transitionTime = 0.0f;
    Vector2 turn = Vector2.zero;

    void Awake()
    {
        GrabLocalRotation();
        controller = GetComponent<CharacterController>();
    }

    void Start()
    {
        input.LockCursor();
    }

    void Update()
    {
        if (transformAttach != null)
            MoveToTransformAttach();
        else
            HandleMovement();

        if (!transitioning)
            HandleRotation();

        //transform.rotation.SetLookRotation(headTransform.forward, transform.up);

        HandleUse();
    }

    void HandleUse()
    {
        if (!input.PressedUseThisFrame())
            return;

        //Check chairs
        if (transformAttach != null)
        {
            ReleasePlayer();
            return;
        }

        CamAttachment closestChair = null;
        float closestChairDist = float.MaxValue;
        foreach (CamAttachment chair in chairsInRange)
        {
            float chairDist = Vector3.Distance(chair.transform.position, cam.transform.position);
            if (chairDist < closestChairDist)
            {
                closestChair = chair;
                closestChairDist = chairDist;
            }
        }

        if (closestChair != null)
        {
            AttachPlayerTo(closestChair.transform);
            return;
        }
}

    void AttachPlayerTo(Transform t)
    {
        transform.parent = t;
        transformAttach = t;
        transitionTime = 0.0f;
        transitioning = true;
    }

    void ReleasePlayer()
    {
        transform.parent = null;
        transformAttach = null;
        transitioning = false;
        transform.up = Vector3.up;
    }

    void GrabLocalRotation()
    {
        Vector3 localEuler = headTransform.localRotation.eulerAngles;
        turn.x = localEuler.y;
        turn.y = localEuler.x;
    }

    void MoveToTransformAttach()
    {
        if (!transitioning)
            return;

        float t = transitionTime / transitionDuration;
        transform.position = Vector3.Lerp(transform.position, transformAttach.position - transformAttach.up, t);
        transform.rotation = Quaternion.Slerp(transform.rotation, transformAttach.rotation, t);
        headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, Quaternion.identity, t);
        GrabLocalRotation();

        transitionTime += Time.deltaTime;

        if (transitionTime >= transitionDuration)
            transitioning = false;
    }

    void HandleRotation()
    {
        Vector2 turnInput = input.GetPlayerTurn();
        turn.x += turnInput.x * turnSpeed * Time.deltaTime;
        turn.y -= turnInput.y * turnSpeed * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -clampAngle, clampAngle);
        headTransform.localRotation = Quaternion.Euler(turn.y, turn.x, 0.0f);
    }

    void HandleMovement()
    {
        //input
        Vector2 moveInput = input.GetPlayerMove();
        Vector3 moveDir = cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        //physics
        bool grounded = controller.isGrounded;
        if (grounded && velocity.y < 0.0f)
            velocity.y = 0.0f;
        velocity.y += Physics.gravity.y * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        CamAttachment chair = other.gameObject.GetComponent<CamAttachment>();
        if (chair == null)
            return;

        if (chairsInRange.Contains(chair))
            return;

        chairsInRange.Add(chair);
    }

    void OnTriggerExit(Collider other)
    {
        CamAttachment chair = other.gameObject.GetComponent<CamAttachment>();
        if (chair == null)
            return;

        if (!chairsInRange.Contains(chair))
            return;

        chairsInRange.Remove(chair);
    }
}
