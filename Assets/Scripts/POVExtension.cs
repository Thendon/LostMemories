using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVExtension : CinemachineExtension
{
    [SerializeField]
    InputManager input;
    [SerializeField]
    float turnSpeed = 1.0f;
    [SerializeField]
    float clampAngle = 80.0f;

    Vector3 turn;

    protected override void Awake()
    {
        base.Awake();

        turn = transform.localRotation.eulerAngles;
    }

    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (!Application.isPlaying)
            return;

        if (!vcam.Follow)
            return;

        if (stage != CinemachineCore.Stage.Aim)
            return;

        Vector2 turnInput = input.GetPlayerTurn();
        turn.x += turnInput.x * turnSpeed * Time.deltaTime;
        turn.y -= turnInput.y * turnSpeed * Time.deltaTime;
        turn.y = Mathf.Clamp(turn.y, -clampAngle, clampAngle);
        transform.localRotation = Quaternion.Euler(turn.y, turn.x, 0.0f);
    }
}
