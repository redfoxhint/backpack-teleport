using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Interactable Object Data", menuName = "Interactables/New Object Data")]
public class InteractableData : ScriptableObject
{
    public string interactionText;
    public string objectName;
    public string objectDescription;
}
