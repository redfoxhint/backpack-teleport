using BackpackTeleport.Character.PlayerCharacter;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraZoomTrigger : MonoBehaviour
{
    [SerializeField] private CinemachineCameraController camController;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if(player)
        {
            camController.ZoomCameraOut();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        Player player = other.GetComponent<Player>();

        if (player)
        {
            camController.ZoomCameraIn();
        }
    }
}
