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
    float snapAngle = 60.0f;
    [SerializeField]
    float moveSpeed = 1.0f;
    [SerializeField]
    float transitionDuration = 1.0f;
    [SerializeField]
    Transform headTransform;
    [SerializeField]
    MemoryDisplay memoryDisplay;
    [SerializeField]
    GameObject winPrefab;
    [SerializeField]
    float winDistance = 10.0f;
    [SerializeField]
    public WearableAttachmentPoint attachmentPoint;

    CharacterController controller;
    Vector3 velocity = Vector3.zero;
    static public List<CamAttachment> chairsInRange = new List<CamAttachment>();
    CamAttachment chairAttached = null;
    bool transitioning = false;
    float attachedTime = 0.0f;
    Vector2 turn = Vector2.zero;
    bool memoryIsDisplayed = false;
    Memory showMemoryOnLeave = null;
    bool won = false;

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
        if (chairAttached != null)
            HandleBeingAttached();
        else
            HandleMovement();

        if (!transitioning)
            HandleRotation();

        HandleUse();
        HandleAttraction();
    }

    void HandleAttraction()
    {
        if (chairAttached == null)
            return;

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

    void DisplayMemory()
    {
        memoryIsDisplayed = true;

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

    void HandleUse()
    {
        if (!input.PressedUseThisFrame())
            return;

        //Check chairs
        if (chairAttached != null)
        {
            ReleasePlayer();
            return;
        }

        if (memoryIsDisplayed)
        {
            HideMemory();
            return;
        }

        var interactable = GetInteractable();
        if (interactable != null)
        {
            interactable.Interact(this);
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

        if (closestChair != null)// && closestChair.UseChair())
        {
            AttachPlayerTo(closestChair);
            return;
        }
    }

    void AttachPlayerTo(CamAttachment t)
    {
        transform.parent = t.transform;
        chairAttached = t;
        attachedTime = 0.0f;
        transitioning = true;
    }

    IInteractable GetInteractable()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, 2))
        {
            return hit.rigidbody.GetComponent<IInteractable>();
        }
        return null;
    }

    void ReleasePlayer()
    {
        transform.parent = null;
        chairAttached = null;
        transitioning = false;

        Vector3 forward = Vector3.Cross(transform.right, Vector3.up);
        transform.rotation = Quaternion.LookRotation(forward, Vector3.up);

        if (showMemoryOnLeave != null)
            DisplayMemory();
    }

    void GrabLocalRotation()
    {
        Vector3 localEuler = headTransform.localRotation.eulerAngles;
        turn.x = localEuler.y;
        turn.y = localEuler.x;
    }

    void HandleBeingAttached()
    {
        attachedTime += Time.deltaTime;

        if (!transitioning)
            return;

        float t = attachedTime / transitionDuration;
        transform.position = Vector3.Lerp(transform.position, chairAttached.GetPosition(), t);
        transform.rotation = Quaternion.Slerp(transform.rotation, chairAttached.GetRotation(), t);
        headTransform.localRotation = Quaternion.Slerp(headTransform.localRotation, Quaternion.identity, t);
        GrabLocalRotation();

        if (attachedTime >= transitionDuration)
            transitioning = false;
    }

    void HandleRotation()
    {
        if (chairAttached == null && Mathf.Acos(Vector3.Dot(headTransform.forward, Vector3.Cross(transform.right, headTransform.up))) * Mathf.Rad2Deg > snapAngle)
            SnapBodyToHeadRotation();

        Vector2 turnInput = input.GetPlayerTurn();
        turn.x += turnInput.x * turnSpeed * Time.deltaTime;
        turn.y -= turnInput.y * turnSpeed * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -clampAngle, clampAngle);
        headTransform.localRotation = Quaternion.Euler(turn.y, turn.x, 0.0f);
    }

    void SnapBodyToHeadRotation()
    {
        Vector3 headEuler = headTransform.eulerAngles;
        Vector3 transformEuler = transform.eulerAngles;

        transformEuler.y = headEuler.y;
        turn.x = 0.0f;

        transform.eulerAngles = transformEuler;
    }

    void HandleMovement()
    {
        //input
        Vector2 moveInput = input.GetPlayerMove();
        Vector3 moveDir = cam.transform.right * moveInput.x + cam.transform.forward * moveInput.y;
        controller.Move(moveDir * moveSpeed * Time.deltaTime);
        if (moveInput.magnitude > 0.1f)
            SnapBodyToHeadRotation();
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
