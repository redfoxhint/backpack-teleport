using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraFunctions : Singleton<CameraFunctions>
{
    [Header("Camera Zoom Configuration")]
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomAmount;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    private Transform followTarget;
    private float originalCameraSize;
    private bool isCameraZoomed = false;
    
    public Transform FollowTarget
    {
        set
        {
            followTarget = value;
            virtualCam.m_Follow = followTarget;
        }
    }

    public void ZoomCamera()
    {
        
    }

    private void ZoomCameraIn()
    {
        DOVirtual.Float(virtualCam.m_Lens.OrthographicSize, originalCameraSize, zoomSpeed, (x) =>
        {
            virtualCam.m_Lens.OrthographicSize = x;
        });
    }

    private void ZoomCameraOut()
    {
        DOVirtual.Float(virtualCam.m_Lens.OrthographicSize, originalCameraSize, zoomSpeed, (x) =>
        {
            virtualCam.m_Lens.OrthographicSize = x;
        });
    }
}
