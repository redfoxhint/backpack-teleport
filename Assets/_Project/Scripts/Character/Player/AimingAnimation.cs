using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AimingAnimation : MonoBehaviour
{
	[SerializeField] private GameObject arrow;
	[SerializeField] private TextMeshProUGUI distanceText;

	public float AimAngle
	{
		get
		{
			return aimAngle;
		}
	}

	public bool IsAiming
	{
		set
		{
			isAiming = value;
		}
	}

	// Private Variables
	private float aimAngle;
	private bool isAiming;

	// Components
	private DottedLine dottedLine;
	private Camera cam;

	private void Awake()
	{
		dottedLine = DottedLine.Instance;
		cam = Camera.main;
	}

	private void Start()
	{
        isAiming = false;
	}

	private void Update()
	{
		if (isAiming) arrow.SetActive(true); else arrow.SetActive(false);
	}

	public void DrawGraphics(Vector2 startPoint, Vector2 endPoint)
	{
		dottedLine.DrawDottedLine(startPoint, endPoint);
		arrow.SetActive(true);

		Vector2 arrowPos = cam.WorldToScreenPoint(startPoint);
		arrowPos = (Vector2)Input.mousePosition - arrowPos;

		aimAngle = Mathf.Atan2(arrowPos.y, arrowPos.x) * Mathf.Rad2Deg;

		arrowPos = Quaternion.AngleAxis(aimAngle, Vector3.forward) * (Vector2.right * 1.2f);
		arrow.transform.position = startPoint + arrowPos;
		arrow.transform.rotation = Quaternion.AngleAxis(aimAngle, Vector3.forward);
	}

	public void UpdateUI(float newText, Color newColor)
	{
		distanceText.SetText(newText.ToString());
		distanceText.color = newColor;
		dottedLine.dotColor = newColor;

		Vector2 centerOfLine = dottedLine.GetCenterOfLine(); // +1.5 units on the y so the text is above the line.
		distanceText.rectTransform.position = new Vector2(centerOfLine.x, centerOfLine.y + 1.5f);
	}

	public void RotateText(float newRotation)
	{
		distanceText.rectTransform.localRotation = Quaternion.Euler(0f, 0f, newRotation);
	}
}
