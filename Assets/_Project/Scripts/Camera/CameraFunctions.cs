using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using BehaviorDesigner.Runtime.Tasks.Movement;

public class CameraFunctions : Singleton<CameraFunctions>
{
    [SerializeField] private CinemachineVirtualCamera virtualCam;
    [SerializeField] private Volume defaultVolume;

    [Header("Volume Configuration")]
    [SerializeField] private float dofFadeTime = 0.6f;

    private Transform followTarget;
    private float originalZoomAmount;
    private const float defaultZoomAmount = 15f;
    private const float defaultZoomSpeed = 1f;
    private bool isZoomedOut = false;

    private VolumeProfile profile;
    private DepthOfField dof;
    private ColorAdjustments colorAdjustments;

    private Color originalScreenColor;

    public Transform FollowTarget
    {
        get
        {
            return followTarget;
        }

        set
        {
            followTarget = value;
            virtualCam.m_Follow = followTarget;
        }
    }

    private void Awake()
    {
        originalZoomAmount = virtualCam.m_Lens.OrthographicSize;
        originalScreenColor = Color.white;

        if (defaultVolume == null) defaultVolume = FindObjectOfType<Volume>();

        if (defaultVolume != null)
        {
            profile = defaultVolume.profile;
            profile.TryGet<DepthOfField>(out dof);
            profile.TryGet<ColorAdjustments>(out colorAdjustments);
        }
        else
        {
            LogUtils.LogWarning("Intro profile was not found! Make sure it is hooked up in the inspector.");
        }

        GameEvents.onSceneLoaded.AddListener(OnSceneLoaded);
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

    public void FadeDOFOut()
    {
        DOVirtual.Float(dof.focusDistance.value, 10, dofFadeTime, (x) =>
        {
            dof.focusDistance.value = x;
        }).SetUpdate(true);
    }

    public void FadeDOFIn()
    {
        DOVirtual.Float(dof.focusDistance.value, 0, dofFadeTime, (x) =>
        {
            dof.focusDistance.value = x;
        }).SetUpdate(true);
    }

    public void SetDOFValue(float value)
    {
        dof.focusDistance.value = value;
    }

    public void SetScreenColor(Color screenColor)
    {
        DOVirtual.Float(0f, 1f, 3f, (x) =>
        {
            colorAdjustments.colorFilter.Interp(colorAdjustments.colorFilter.value, screenColor, x);
        }).SetUpdate(true);
    }

    private void OnSceneLoaded()
    {
        if(GameManager.Instance.Player != null)
        {
            FollowTarget = GameManager.Instance.Player.transform;
        }
    }
}
