using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AnimationCurveMovementTest : MonoBehaviour
{
    [SerializeField] private AnimationCurve animationCurve;
    [SerializeField] private float speed;

    // Private Variables
    private Vector3 target;
    private Vector3 startPoint;
    private bool destinationReached = true;
    private float animationTimePosition;

    private void Start()
    {
        //UpdatePath();
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && destinationReached)
        {
            MoveBag();
        }
    }

    private void MoveBag()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        target = mousePos;

        StartCoroutine(MoveToPositionWithCurve());
        destinationReached = false;
    }

    private void UpdatePath()
    {
        startPoint = transform.position;
        target = Random.insideUnitSphere * 3f;
        target.z = 0;
    }

    private IEnumerator MoveToPositionWithCurve()
    {
        startPoint = transform.position;
        animationTimePosition = 0;
        target.z = 0;

        while(transform.position != target)
        {
            animationTimePosition += speed * Time.deltaTime;
            transform.position = Vector3.Lerp(startPoint, target, animationCurve.Evaluate(animationTimePosition));
            transform.position = new Vector3(transform.position.x, transform.position.y, 0f);
            yield return null;
        }

        destinationReached = true;

        yield break;
    }
}
