using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [HideInInspector]
    public DefaultControls controls;

    void Awake()
    {
        controls = new DefaultControls();
    }

    void OnEnable()
    {
        controls.Enable();
    }

    void OnDisable()
    {
        controls.Disable();
    }

    public Vector2 GetPlayerMove()
    {
        return controls.DefaultMovement.Movement.ReadValue<Vector2>();
    }

    public Vector2 GetPlayerTurn()
    {
        return controls.DefaultMovement.Turn.ReadValue<Vector2>();
    }

    public bool PressedUseThisFrame()
    {
        return controls.DefaultMovement.Use.triggered;
    }

    public void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ReleaseCursor()
    {
        Cursor.lockState = CursorLockMode.None;
    }
}
