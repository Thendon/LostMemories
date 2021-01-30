using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class POVExtension : CinemachineExtension
{
    protected override void PostPipelineStageCallback(CinemachineVirtualCameraBase vcam, CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        return;
    }
}
