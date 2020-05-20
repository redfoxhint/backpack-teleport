using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterBase : CharacterAnimator
{
    Rigidbody2D rBody;

    private void Awake()
    {
        rBody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }
}
