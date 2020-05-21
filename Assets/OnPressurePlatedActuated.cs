using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnPressurePlatedActuated : MonoBehaviour
{
    public void OnPressurePlateActuated()
    {
        GameEvents.onPressurePlateActuated.Invoke(GetComponent<PressurePlate>());
    }
}
