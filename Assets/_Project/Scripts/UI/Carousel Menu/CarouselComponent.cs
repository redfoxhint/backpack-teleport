using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CarouselComponent : MonoBehaviour
{
    [SerializeField] private RectTransform componentContent;

    public void Select() { }
    public void Deselect() { }
}
