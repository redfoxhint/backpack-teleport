using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;

public class AimingAnimation : MonoBehaviour
{
	[SerializeField] private GameObject arrow;
	[SerializeField] private LayerMask aimFilter;

	private CircularBoundaryVisualization radiusCircleController;
	private GameCursor gameCursor;

	public CircularBoundaryVisualization RadiusCircleController { get => radiusCircleController; }

	// Components
	private DottedLine dottedLine;
	private Camera cam;

	private void Awake()
	{
		dottedLine = DottedLine.Instance;
		cam = Camera.main;

		radiusCircleController = GetComponent<CircularBoundaryVisualization>();
		gameCursor = FindObjectOfType<GameCursor>();
	}

	private void Start()
	{
		DisableArrow();
	}

	public void DrawDottedLineAndArrow(Vector2 startPoint, Vector2 endPoint)
	{
		dottedLine.DrawDottedLine(startPoint, endPoint);
		PointArrowAt(gameCursor.gameObject);
	}

	private void PointArrowAt(GameObject objectToPointTo)
	{
		arrow.SetActive(true);

		Vector3 directionToTarget = transform.position.DirectionTo(objectToPointTo.transform.position);
		directionToTarget.z = 0f;

		Vector3 targetPos = transform.position + directionToTarget;
		targetPos.z = 0;

		float angle = Mathf.Atan2(directionToTarget.y, directionToTarget.x) * Mathf.Rad2Deg;
		Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

		arrow.transform.position = targetPos;
		arrow.transform.rotation = rotation;
	}

	public void DisableArrow()
	{
		arrow.SetActive(false);
	}

	public bool IsOverlappingObstacle(Vector3 startPoint, Vector3 endPoint)
	{
		Vector3 direction = startPoint.DirectionTo(endPoint);

		RaycastHit2D hit = Physics2D.Raycast(startPoint, direction, Mathf.Infinity, aimFilter);
		Debug.DrawRay(startPoint, direction);

		if (hit)
		{
			return true;
		}

		return false;
	}
}
