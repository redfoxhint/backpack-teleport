using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Backpack_State_Chaining_Setup : IState
{
    private Backpack backpack;
    private BackpackChainData backpackChainData;

    public Backpack_State_Chaining_Setup(Backpack _backpack)
    {
        backpack = _backpack;
        backpackChainData = new BackpackChainData();
    }

    public void Initialize()
    {
        backpack.CurrentState = BackpackStates.CHAINING_SETUP;

        backpackChainData.markerPositions.Enqueue(backpack.gameObject);
        backpack.AimingAnimation.RadiusCircleController.Activate();

        InputManager.Instance.InputActions.Backpack.Aim.canceled += OnChainingCanceled;
        InputManager.Instance.InputActions.Backpack.PlaceMarker.started += PlaceMarker;
    }

    public void Update()
    {
        //PlaceMarkerAtPosition();

        if (backpackChainData.currentAmountOfMarkers == backpack.MaxMarkerPositions)
        {
            // TODO: Implement Damage Primitive
            backpack.stateMachine.ChangeState(new Backpack_State_Chaining(backpack, backpackChainData));
        }
    }

    public void FixedUpdate()
    {

    }

    public void Exit()
    {
        backpack.AimingAnimation.RadiusCircleController.Deactivate();

        InputManager.Instance.InputActions.Backpack.Aim.canceled -= OnChainingCanceled;
        InputManager.Instance.InputActions.Backpack.PlaceMarker.started -= PlaceMarker;
    }

    private void OnChainingCanceled(InputAction.CallbackContext value)
    {
        if(backpackChainData != null)
        {
            backpackChainData.DeleteAllMarkers();
        }

        backpack.stateMachine.ChangeState(new Backpack_State_Idle(backpack));
    }

    private void PlaceMarker(InputAction.CallbackContext value)
    {
        Vector2 markerPosition = backpack.GameCursor.transform.position;

        if (backpackChainData.currentAmountOfMarkers < backpack.MaxMarkerPositions)
        {
            GameObject positionMarker = GameObject.Instantiate(backpack.MarkerPrefab, markerPosition, Quaternion.identity);
            backpackChainData.markerPositions.Enqueue(positionMarker);
            backpackChainData.currentAmountOfMarkers += 1;
            Debug.Log("Marker Placed");
        }
    }
}

public class BackpackChainData
{
    public Queue<GameObject> markerPositions;
    public int currentAmountOfMarkers;

    public BackpackChainData()
    {
        markerPositions = new Queue<GameObject>();
    }

    public void DeleteAllMarkers()
    {
        GameObject[] markers = markerPositions.ToArray();
        foreach(GameObject marker in markers)
        {
            DeleteMarker(marker);
        }

        markerPositions.Clear();
        markerPositions = null;
        currentAmountOfMarkers = 0;
    }

    public void DeleteMarker(GameObject marker)
    {
        if (marker.CompareTag("Marker"))
        {
            GameObject.Destroy(marker);
        }
    }
}