using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Zoom : MonoBehaviour
{
    [SerializeField]
    private Camera ppwzCamera;
    private PerfectPixelWithZoom ppwz;

    void Start()
    {
        ppwz = ppwzCamera.GetComponent<PerfectPixelWithZoom>();
    }

    void Update()
    {
        if(Keyboard.current.numpad8Key.wasPressedThisFrame)
        {
            ppwz.ZoomIn();
        }

        if (Keyboard.current.numpad9Key.wasPressedThisFrame)
        {
            ppwz.ZoomOut();
        }
    }
}
