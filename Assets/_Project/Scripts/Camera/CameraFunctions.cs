using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CameraFunctions : Singleton<CameraFunctions>
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;

    private Transform followTarget;
    private float originalZoomAmount;
    private const float defaultZoomAmount = 15f;
    private const float defaultZoomSpeed = 1f;
    private bool isZoomedOut = false;
    public Transform FollowTarget
    {
        set
        {
            followTarget = value;
            virtualCam.m_Follow = followTarget;
        }
    }

    private void Awake()
    {
        originalZoomAmount = virtualCam.m_Lens.OrthographicSize;
    }

    public void ZoomCamera(float amount = defaultZoomAmount, float zoomSpeed = defaultZoomSpeed)
    {
        isZoomedOut = !isZoomedOut;

        if (!isZoomedOut)
        {
            ResetZoom(zoomSpeed);
        }
        else
        {
            ZoomOut(amount, zoomSpeed);
        }
    }

    public void SetZoomImmediate(float amount)
    {
        virtualCam.m_Lens.OrthographicSize = amount;
        isZoomedOut = false;
    }

    private void ZoomOut(float zoomAmount, float zoomSpeed)
    {
        DOVirtual.Float(virtualCam.m_Lens.OrthographicSize, zoomAmount, zoomSpeed, (x) =>
        {
            virtualCam.m_Lens.OrthographicSize = x;
        });
    }

    public void ResetZoom(float zoomSpeed = defaultZoomSpeed)
    {
        DOVirtual.Float(virtualCam.m_Lens.OrthographicSize, originalZoomAmount, zoomSpeed, (x) =>
        {
            virtualCam.m_Lens.OrthographicSize = x;
        });
    }
}
