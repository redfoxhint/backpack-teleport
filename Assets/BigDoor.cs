using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BigDoor : MonoBehaviour
{
    [SerializeField] private List<PressurePlate> pressurePlates = new List<PressurePlate>();

    private bool notOpen = true;

    private void Awake()
    {
        //GameEvents.onPressurePlateActuated.AddListener(OnPressurePlateActuated);
    }

    private void Update()
    {
        if(notOpen)
        {
            bool allDeactuated = pressurePlates.Any() && pressurePlates.All(item => item.IsActuated);
            if (allDeactuated)
            {
                GetComponent<DoorActivateable>().Deactivate();
                Debug.Log("DOOR OPENED");
                notOpen = false;
            }
        }
    }
}
