using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    [Header("Camera Zoom Trigger Configuration")]

    [Tooltip("How much the camera will be zoomed in.")]
    [SerializeField] private float zoomAmount;
    [Tooltip("How much time it will take for the camera to zoom in.")]
    [SerializeField] private float zoomSpeed;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player)
        {
            CameraFunctions.Instance.ZoomCamera();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            CameraFunctions.Instance.ZoomCamera();
        }
    }
}
