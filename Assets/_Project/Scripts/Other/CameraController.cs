using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float followSpeed;

    private void FixedUpdate()
    {
        if (target != null)
        {
            Vector2 targetPosition = Vector2.Lerp(transform.position, target.position, followSpeed);
            transform.position = new Vector3(targetPosition.x, targetPosition.y, -10f);
        }
    }

    public void SetCameraPosition(Vector2 newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, -10f); ;
    }
}
