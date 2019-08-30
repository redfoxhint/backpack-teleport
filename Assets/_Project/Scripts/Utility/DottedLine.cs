using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DottedLine : MonoBehaviour
{
	// Public Variables
	[SerializeField] private GameObject dotPrefab;
	[SerializeField] private Transform startingPoint;
	[SerializeField] [Range(0.01f, 1f)] private float dotSize;
	[SerializeField] [Range(0.1f, 2f)] private float delta;

	// Private Variables
	private List<Vector2> positions = new List<Vector2>();
	private List<GameObject> dots = new List<GameObject>();
	private Vector2 centerOfLine;
	private Vector2 startPoint;
	private Vector2 endPoint;

	// Components

	public Color dotColor = Color.white;

	public bool doRender;

	private static DottedLine instance;
	public static DottedLine Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<DottedLine>();
			}
			return instance;
		}
	}

	private void FixedUpdate()
	{
		if (positions.Count > 0)
		{
			DestroyAllDots();
			positions.Clear();
		}
	}

	private void DestroyAllDots()
	{
		foreach (var dot in dots)
		{
			Destroy(dot);
		}
		dots.Clear();
	}

	GameObject GetOneDot()
	{
		var gameObject = Instantiate(dotPrefab, Vector2.zero, Quaternion.identity);
		gameObject.transform.localScale = Vector3.one * dotSize;
		gameObject.transform.parent = transform;

		var sr = gameObject.GetComponent<SpriteRenderer>();
		sr.color = dotColor;

		return gameObject;
	}

	public void DrawDottedLine(Vector2 start, Vector2 end)
	{
		DestroyAllDots();

		Vector2 point = startingPoint.position;
		Vector2 direction = (end - start).normalized;

		while ((end - start).magnitude > (point - start).magnitude)
		{
			positions.Add(point);
			point += (direction * delta);
		}

		endPoint = end;

		Render(start, end);
		centerOfLine = GetCenterOfLine();

	}

	private void Render(Vector2 start, Vector2 end)
	{
		// Render Dots

		foreach (var position in positions)
		{
			var g = GetOneDot();
			g.transform.position = position;
			g.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
			dots.Add(g);
		}
	}

	public Vector2 GetCenterOfLine()
	{
		Vector2 center = Vector2.Lerp(startingPoint.position, endPoint, 0.5f);
		return center;
	}
}


