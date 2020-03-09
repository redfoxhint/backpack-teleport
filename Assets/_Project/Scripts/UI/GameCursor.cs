using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameCursor : MonoBehaviour
{
    [Header("Cursor Setup")]

    [Header("Gamepad Configuration")]
    [SerializeField] private float cursorMoveSpeed = 5f;
    [SerializeField] private bool useGamepad;

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
        MoveCursor();
         
        //MoveCursorWithMouse();
        //MoveCursorWithGamepad();
    }

    private void MoveCursor()
    {
        if(inputManager.UseGamepad)
        {
            Vector2 cursorInput = Vector2.zero;
            cursorInput = new Vector2(inputManager.JoystickInput.x, inputManager.JoystickInput.y);

            Vector2 cursorMoveDelta = cursorInput * cursorMoveSpeed * Time.deltaTime;
            transform.Translate(cursorMoveDelta);
        }
        else
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            transform.position = mousePos;
        }

        ClampToRadius();
    }

    private void ClampToRadius()
    {
        Vector3 center = circle.gameObject.transform.position;
        center.z = 0;

        float maxDistance = circle.CircleRadius * 10f;
        float currentDistanceToCenter = Vector2.Distance(center, transform.position);

        if(currentDistanceToCenter > maxDistance)
        {
            Vector3 centerToPosition = center.DirectionTo(transform.position);
            centerToPosition.z = 0;
            transform.position = center + centerToPosition * maxDistance;
        }
    }
}
