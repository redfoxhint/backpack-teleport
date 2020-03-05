using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCursor : MonoBehaviour
{
    [SerializeField] private float maxDistance = 10f;

    [Header("Cursor Setup")]

    [Header("Gamepad Configuration")]
    [SerializeField] private float cursorMoveSpeed = 5f;

    // Private Variables

    // Components
    [SerializeField] private CircularBoundaryVisualization circle; // Has the data regarding the circle.
    private InputManager inputManager;

    private void Awake()
    {
        circle = FindObjectOfType<CircularBoundaryVisualization>();
        inputManager = InputManager.Instance;
    }

    private void Start()
    {
        Cursor.visible = false;
    }

    private void Update()
    {
        MoveCursorWithMouse();
        //MoveCursorWithGamepad();
    }

    private void MoveCursorWithGamepad()
    {
        Vector3 center = circle.gameObject.transform.position;
        center.z = 0;

        Vector2 movement = Vector2.zero;
        movement = new Vector2(inputManager.JoystickInput.x, inputManager.JoystickInput.y);

        Vector2 moveDelta = movement * cursorMoveSpeed * Time.deltaTime;

        maxDistance = circle.CircleRadius * 10f;
        float actualDistance = Vector2.Distance(center, moveDelta);

        if (actualDistance > maxDistance)
        {
            Vector3 centerToPosition = (Vector3)moveDelta - center;
            centerToPosition.z = 0;
            centerToPosition.Normalize();
            moveDelta = center + centerToPosition * maxDistance;
            transform.position = moveDelta;
        }
        else
        {
            transform.Translate(moveDelta, Space.World);
        }
    }

    private void MoveCursorWithMouse()
    {
        Vector3 center = circle.gameObject.transform.position;
        center.z = 0;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        mousePos.z = 0;

        maxDistance = circle.CircleRadius * 10f;

        float actualDistance = Vector2.Distance(center, mousePos);

        if (actualDistance > maxDistance)
        {
            Vector3 centerToPosition = mousePos - center;
            centerToPosition.z = 0;
            centerToPosition.Normalize();
            mousePos = center + centerToPosition * maxDistance;
            transform.position = mousePos;
        }

        else
        {
            transform.position = mousePos;
        }
    }
}
