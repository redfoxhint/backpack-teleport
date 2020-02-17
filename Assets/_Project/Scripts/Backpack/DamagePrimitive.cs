using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePrimitive : MonoBehaviour
{
	// Private Variables
	private Vector3[] newVertices;
	private int[] newTriangles;
	private Vector3[] normals;
	private Vector2[] uv;

	// Components
	private LineRenderer lr;
	private BackpackChaining backpackChaining;
	private Backpack backpack;
	private MeshRenderer meshRenderer;
	private MeshFilter meshFilter;
	private Mesh mesh;

	// Tasks
	private Task damagePrimitiveTask;

	private void Awake()
	{
		lr = GetComponent<LineRenderer>();
		meshRenderer = GetComponent<MeshRenderer>();
		meshFilter = GetComponent<MeshFilter>();
		backpackChaining = FindObjectOfType<BackpackChaining>();
		backpack = FindObjectOfType<Backpack>();
	}

	private void Start()
	{
		meshRenderer.sortingLayerName = "Player";
	}

	public void Initialize(List<Vector3> primitivePoints)
	{
		ConfigureLineRenderer(primitivePoints);

		if (damagePrimitiveTask != null)
		{
			damagePrimitiveTask.Stop();
			damagePrimitiveTask = null;

		}
		damagePrimitiveTask = new Task(DamagePrimitiveSequence(primitivePoints));
	}

	private void ConfigureLineRenderer(List<Vector3> primitivePoints)
	{
		lr.positionCount = 3;
		lr.widthMultiplier = 0.2f;
		lr.enabled = false;

		lr.SetPosition(0, primitivePoints[0]);
		lr.SetPosition(1, primitivePoints[1]);
		lr.SetPosition(2, primitivePoints[2]);

		GetArea(primitivePoints);
	}

	IEnumerator DamagePrimitiveSequence(List<Vector3> primitivePoints)
	{
		lr.enabled = true;
		CreateMesh(primitivePoints);

		yield return new WaitForSeconds(0.2f);

		ClearMesh();
		backpackChaining.ResetChain();
		backpack.stateMachine.ChangeState(new Backpack_State_Returning(backpack));
		lr.enabled = false;
		Destroy(gameObject);
		yield break;
	}

	private void GetArea(List<Vector3> primitivePoints)
	{
		float area = 0f;

		for (int i = 0; i < primitivePoints.Count; i++)
		{
			Vector3 a = primitivePoints[0];
			Vector3 b = primitivePoints[1];
			Vector3 c = primitivePoints[2];
			Vector3 v = Vector3.Cross(a - b, a - c);
			area += v.magnitude * 0.5f;
		}

		Debug.Log(area);
	}

	private void CreateMesh(List<Vector3> primitivePoints)
	{
		newVertices = new Vector3[3];
		newVertices[0] = transform.InverseTransformPoint(primitivePoints[0]);
		newVertices[1] = transform.InverseTransformPoint(primitivePoints[1]);
		newVertices[2] = transform.InverseTransformPoint(primitivePoints[2]);

		uv = new Vector2[3];
		uv[0] = primitivePoints[0];
		uv[1] = primitivePoints[1];
		uv[2] = primitivePoints[2];

		newTriangles = new int[3];
		newTriangles[0] = 0;
		newTriangles[1] = 1;
		newTriangles[2] = 2;

		mesh = new Mesh();
		meshFilter.mesh = mesh;

		mesh.Clear();
		mesh.vertices = newVertices;
		mesh.uv = uv;
		mesh.triangles = newTriangles;

		mesh.RecalculateNormals();
		mesh.Optimize();

		PolygonCollider2D polyCol = gameObject.AddComponent<PolygonCollider2D>();
		polyCol.isTrigger = true;
		Vector2[] points = newVertices.ToVector2Array();
		polyCol.SetPath(0, points);
	}

	private void ClearMesh()
	{
		mesh.Clear();
		mesh = null;
		PolygonCollider2D poly = GetComponent<PolygonCollider2D>();
		Destroy(poly);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Damageable"))
		{
			Vector2 dir = transform.position - other.transform.position;

			other.GetComponent<IDamageable>().TakeDamage(gameObject, 1f);
			Debug.Log("Collided with IDamageable Object");
		}
	}
}
