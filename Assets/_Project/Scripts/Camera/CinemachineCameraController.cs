using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cinemachine;

public class CinemachineCameraController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera cam;
    [SerializeField] private float zoomSpeed;
    [SerializeField] private float zoomOutAmount;
    [SerializeField] private float originalSize;

    private void Start()
    {
        originalSize = cam.m_Lens.OrthographicSize;
    }

    public void ZoomCameraOut()
    {
        DOVirtual.Float(cam.m_Lens.OrthographicSize, zoomOutAmount, zoomSpeed, (x) =>
        {
            cam.m_Lens.OrthographicSize = x;
        });
    }

    public void ZoomCameraIn()
    {
        DOVirtual.Float(cam.m_Lens.OrthographicSize, originalSize, zoomSpeed, (x) =>
        {
            cam.m_Lens.OrthographicSize = x;
        });
    }
}
