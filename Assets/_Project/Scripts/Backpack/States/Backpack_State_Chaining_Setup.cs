using System;
using System.Collections.Generic;
using UnityEngine;

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
    }

    public void Update()
    {
        PlaceMarkerAtPosition();

        if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            backpackChainData.DeleteAllMarkers();
            backpack.stateMachine.ChangeState(new Backpack_State_Idle(backpack));
        }
    }

    public void Exit()
    {
        
    }

    private void PlaceMarkerAtPosition()
    {
        Vector2 markerPosition = backpack.Cam.ScreenToWorldPoint(Input.mousePosition);

        if (backpackChainData.currentAmountOfMarkers < backpack.MaxMarkerPositions)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                GameObject positionMarker = GameObject.Instantiate(backpack.MarkerPrefab, markerPosition, Quaternion.identity);
                backpackChainData.markerPositions.Enqueue(positionMarker);
                backpackChainData.currentAmountOfMarkers += 1;
            }
        }
        else if (backpackChainData.currentAmountOfMarkers == backpack.MaxMarkerPositions)
        {
            // TODO: Implement Damage Primitive
            backpack.stateMachine.ChangeState(new Backpack_State_Chaining(backpack, backpackChainData));
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
    }

    public void DeleteMarker(GameObject marker)
    {
        if (marker.CompareTag("Marker"))
        {
            GameObject.Destroy(marker);
        }
    }
}