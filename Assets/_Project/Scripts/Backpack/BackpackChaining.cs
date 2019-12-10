using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Backpack))]
public class BackpackChaining : MonoBehaviour
{
	// Public Variables
	[SerializeField] private VoidEvent onPlaceMarker;
	[SerializeField] private GameObject positionMarkerPrefab;
	[SerializeField] private GameObject damagePrimitivePrefab;
	[SerializeField] private int maxAmountOfMarkers = 3;

	// Properties
	public bool ChainComplete { get { return chainComplete; } }
	public bool StartChain { get { return startChain; } set { startChain = value; } }
	public int CurrentPositionIndex { get { return currentPositionIndex; } set { currentPositionIndex = value; } }
	public int TeleportsLeft { get { return teleportsLeft; } set { teleportsLeft = value; } }

	// Private Variables
	private List<GameObject> markerObjects;
	private List<Vector3> markerPositions;
	private Vector2 nextTeleportLocation;
	private int maxMarkerPositions = 2;
	private int currentMarkerPositions;
	private int currentPositionIndex = 0;
	private int teleportsLeft;
	private bool chainComplete;
	private bool startChain;

	// Components
	private Camera cam;
	private Backpack backpack;
	private BackpackAnimation backpackAnimation;

	// Tasks
	private Task chainingTask;

	private void Awake()
	{
		cam = Camera.main;
		backpack = GetComponent<Backpack>();
		backpackAnimation = GetComponent<BackpackAnimation>();
	}

	private void Start()
	{
		chainComplete = false;
		startChain = false;
		markerObjects = new List<GameObject>();
		markerPositions = new List<Vector3>();
		teleportsLeft = maxAmountOfMarkers;
	}

	private void Update()
	{
		/* If the chain is complete meaning there are max amount of markers placed, then it means that we can create a damage primitive. */
		if (chainComplete)
		{
			if (Input.GetKeyDown(KeyCode.F))
			{
				GameObject newDamagePrimitive = Instantiate(damagePrimitivePrefab);
				newDamagePrimitive.GetComponent<DamagePrimitive>().Initialize(markerPositions);
			}
		}
	}

	public void InitializeChain()
	{
		chainingTask = new Task(ChainingSequence());
		chainingTask.Start();

		backpack.ChainReady = true;
		backpack.CanBeAimed = false;
	}

	private void BeginChain()
	{
		nextTeleportLocation = transform.position;
		markerObjects.Add(gameObject);
		markerPositions.Add(nextTeleportLocation);
		startChain = true;
	}
	private void ClearMarkers()
	{
		GameObject[] markers = GameObject.FindGameObjectsWithTag("Marker");
		for (int i = 0; i < markers.Length; i++)
		{
			Destroy(markers[i]);
		}
	}

	public void ResetChain()
	{
		chainingTask.Stop();
		chainingTask = null;

		startChain = false;
		chainComplete = false;

		currentPositionIndex = 0;
		currentMarkerPositions = 0;

		markerPositions.Clear();
		markerObjects.Clear();

		teleportsLeft = maxAmountOfMarkers; // Should actually be set after the chain is complete to markerPositions.Count

		backpack.ChainReady = false;
		backpack.CanBeAimed = true;

		ClearMarkers();

		//backpack.InitializeState(BackpackStates.RETURNING);
	}

	public void PlaceMarkerAtPosition(Vector2 markerPosition)
	{
		if (currentMarkerPositions < maxMarkerPositions)
		{
			if (!startChain) // This gets set to true when we start a new chain so we can Initialize it.
			{
				BeginChain();
			}

			if (Input.GetMouseButtonDown(0))
			{
				GameObject positionMarker = Instantiate(positionMarkerPrefab, markerPosition, Quaternion.identity);
				markerObjects.Add(positionMarker);
				markerPositions.Add(positionMarker.transform.position);
				currentMarkerPositions += 1;
				onPlaceMarker.Raise();
			}
		}
		else
		{
			if (currentMarkerPositions == maxMarkerPositions)
			{
				// Can start teleport sequence.
				chainComplete = true;
			}
		}
	}

	public Vector2 GetNextPosition()
	{
		nextTeleportLocation = markerPositions[currentPositionIndex];
		return nextTeleportLocation;
	}

	public void DestroyMarker(int index)
	{
		GameObject markerToDestroy = markerObjects[currentPositionIndex];
		Destroy(markerToDestroy);
	}

	IEnumerator ChainingSequence()
	{
		while (teleportsLeft != 0)
		{
			// Cancel the chain
			if (Input.GetKeyDown(KeyCode.R))
			{
				ResetChain();
				backpack.InitializeState(BackpackStates.RETURNING);

				yield break;
			}

			if (Input.GetKeyDown(KeyCode.Mouse1))
			{
				if (teleportsLeft >= maxAmountOfMarkers)
				{
					backpack.InitializeState(BackpackStates.INHAND);
					teleportsLeft -= 1;
					Vector2 nextBackpackPosition = GetNextPosition();
					backpack.Owner.Teleport(nextBackpackPosition);
					currentPositionIndex += 1;
					backpackAnimation.RippleEffect(transform.position);

					yield return null;
				}
				else
				{
					teleportsLeft -= 1;
					Vector2 nextBackpackPosition = GetNextPosition();
					DestroyMarker(currentPositionIndex);
					backpack.Owner.Teleport(nextBackpackPosition);
					currentPositionIndex += 1;
					backpackAnimation.RippleEffect(transform.position);
				}

				yield return null;
			}

			yield return null;
		}

		ResetChain();
		backpack.InitializeState(BackpackStates.RETURNING);

		yield break;
	}
}
