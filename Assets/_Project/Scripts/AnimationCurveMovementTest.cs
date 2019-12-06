using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationCurveMovementTest : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speed;

    // Private Variables
    private Vector3 target;
    private Vector3 startPoint;
    private float animationTimePosition;

    private void Start()
    {
        UpdatePath();
    }

    private void Update()
    {
        if(target != transform.position)
        {
            animationTimePosition += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, target, animationCurve.Evaluate(animationTimePosition));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
        }
        else
        {
            UpdatePath();
            animationTimePosition = 0;
        }
    }



    private void UpdatePath()
    {
        startPoint = transform.position;

        if (Input.GetMouseButtonDown(0))
        {
            
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        
        //target = UnityEngine.Random.insideUnitSphere* 5;
    }
}
