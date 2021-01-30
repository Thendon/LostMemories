using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class Player : MonoBehaviour
{
    [SerializeField]
    InputManager inputManager = null;
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
    static public List<Chair> chairsInRange = new List<Chair>();
    Transform transformAttach = null;
    bool transitioning = false;
    float transitionTime = 0.0f;
    Quaternion turn;

    void Awake()
    {
        turn = headTransform.transform.localRotation;
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if (transformAttach != null)
            MoveToTransformAttach();
        else
            HandleMovement();
        HandleRotation();
        HandleUse();
    }

    void HandleUse()
    {
        if (!inputManager.PressedUseThisFrame())
            return;

        //Check chairs
        if (transformAttach != null)
        {
            ReleasePlayer();
            return;
        }

        Chair closestChair = null;
        float closestChairDist = float.MaxValue;
        foreach (Chair chair in chairsInRange)
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
    }

    void MoveToTransformAttach()
    {
        if (!transitioning)
            return;

        transform.position = Vector3.MoveTowards(transform.position, transformAttach.position, transitionTime / transitionDuration);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, transformAttach.rotation, transitionTime / transitionDuration);

        transitionTime += Time.deltaTime;

        if (transitionTime > transitionDuration)
            transitioning = false;
    }

    void HandleRotation()
    {
        Vector2 turnInput = inputManager.GetPlayerTurn();
        turn.x += turnInput.x * turnSpeed * Time.deltaTime;
        turn.y -= turnInput.y * turnSpeed * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -clampAngle, clampAngle);
        headTransform.localRotation = Quaternion.Euler(turn.y, turn.x, 0.0f);

        transform.rotation.SetLookRotation(cam.transform.forward, transform.up);
    }

    void HandleMovement()
    {
        //input
        Vector2 moveInput = inputManager.GetPlayerMove();
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
        Chair chair = other.gameObject.GetComponent<Chair>();
        if (chair == null)
            return;

        if (chairsInRange.Contains(chair))
            return;

        chairsInRange.Add(chair);
    }

    void OnTriggerExit(Collider other)
    {
        Chair chair = other.gameObject.GetComponent<Chair>();
        if (chair == null)
            return;

        if (!chairsInRange.Contains(chair))
            return;

        chairsInRange.Remove(chair);
    }
}
