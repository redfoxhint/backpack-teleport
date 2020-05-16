using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayCertainDistanceAway : MonoBehaviour
{
    [SerializeField] private AIDestinationSetter aiDestinationSetter;
    [SerializeField] private float activeDistance = 15f;
    [SerializeField] private float minDistance = 9f;
    [SerializeField] private float maxDistance = 12f;
    [SerializeField] private float targetDistance = 5f;
     
    private Transform target;
    private float distance;

    private GameObject targetObject;
    private IWalkable walkable;


    private void Awake()
    {
        aiDestinationSetter = GetComponent<AIDestinationSetter>();
        aiDestinationSetter.target = null;
        target = GameManager.Instance.Player.transform;
        targetObject = new GameObject("TargetObject");
        targetObject.transform.SetParent(transform);
        walkable = GetComponent<IWalkable>();
    }

    private void Update()
    {
        distance = Vector3.Distance(target.position, transform.position);

        if(Input.GetMouseButton(0))
        {
            Debug.Log(distance);
        }

        if (distance <= activeDistance)
        {
            if (distance <= maxDistance && distance >= minDistance)
            {
                walkable.ToggleMovement(false);
            }

            else if (distance > maxDistance)
            {
                aiDestinationSetter.target = GameManager.Instance.Player.transform;
                walkable.ToggleMovement(true);
            }

            else if (distance < minDistance)
            {
                Vector2 dirToPlayer = transform.position.DirectionTo(target.position).normalized;
                Vector2 targetDir = -dirToPlayer;

                targetObject.transform.position = targetDir * targetDistance;
                aiDestinationSetter.target = targetObject.transform;
                walkable.ToggleMovement(true);

            }
        }
    }
}
