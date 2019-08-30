using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorTools
{
	public static Vector2[] ToVector2Array(this Vector3[] v3)
	{
		return System.Array.ConvertAll<Vector3, Vector2>(v3, GetV3fromV2);
	}

	public static Vector2 GetV3fromV2(Vector3 v3)
	{
		return new Vector2(v3.x, v3.y);
	}
}
