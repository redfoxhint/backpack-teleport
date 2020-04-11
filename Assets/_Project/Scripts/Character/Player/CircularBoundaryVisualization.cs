using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

/* Shader Properties
	 * Texture = _MainTex
	 * Radius = _Radius
	 * Thickness = _Thickness
	 * Color = _Color
	 */

public class CircularBoundaryVisualization : MonoBehaviour
{
	[Header("Circle Configuration")]
	[SerializeField] private GameObject circle;
	[SerializeField] private float circleRadius = 0f;
	[SerializeField] private float circleThickness = 0.01f;
	[SerializeField] private Color circleColor = Color.white;
	[SerializeField] private float circleAnimateTime;

	private bool isActive = false;

	// Components
	[SerializeField] private SpriteRenderer cursorSpriteRenderer;
	[SerializeField] private GameCursor cursorUI;
	
	private Material circleMaterial;

	// Properties
	public float CircleRadius { get => circleRadius; }

	private void Awake()
	{
		circleMaterial = cursorSpriteRenderer.material;
		cursorUI = FindObjectOfType<GameCursor>();
	}

	private void Start()
	{
		circle.SetActive(false);
		cursorUI.gameObject.SetActive(false);
		circleRadius = 0;
		SetProperties();
	}

	private void Update()
	{
		SetProperties();
	}

	private void SetProperties()
	{
		SetRadius(circleRadius);
		SetThickness(circleThickness);
		SetColor(circleColor);
	}

	[ContextMenu("Activate Circle")]
	public void Activate()
	{
		if (isActive) return;
		SetColor(circleColor);

		circle.SetActive(true);
		cursorUI.gameObject.SetActive(true);
		isActive = true;
		DOTween.To(() => circleRadius, x => circleRadius = x, 1f, circleAnimateTime);
	}

	[ContextMenu("Deactivate Circle")]
	public void Deactivate()
	{
		if (!isActive) return;

		DOTween.To(() => circleRadius, x => circleRadius = x, 0f, circleAnimateTime).OnComplete(
			() =>
			{
				circle.SetActive(false);
				isActive = false;
				cursorUI.gameObject.SetActive(false);

			});
	}

	public void SetColor(Color newColor)
	{
		circleMaterial.SetColor("_Color", newColor);
	}

	public void SetThickness(float newThickness)
	{
		circleMaterial.SetFloat("_Thickness", newThickness);
	}

	public void SetRadius(float newRadius)
	{
		circleMaterial.SetFloat("_Radius", newRadius);
	}
}
