using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleRotator : MonoBehaviour
{
	[SerializeField] private float rotateSpeed = 4f;

	private void Update()
	{
        transform.Rotate(new Vector2(0f, rotateSpeed * Time.deltaTime), Space.Self);
	}
}
