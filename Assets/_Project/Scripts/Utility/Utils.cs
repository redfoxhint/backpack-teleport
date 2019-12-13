using UnityEngine;
using DG.Tweening;

public static class Utils
{
    // Check if given index is within bounds of the given array.
    public static bool GetIndexInBounds(int index, int[] array)
    {
        return (index >= 0) && (index < array.Length);
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRadian = angle * (Mathf.PI / 180f);
        return new Vector3(Mathf.Cos(angleRadian), Mathf.Sin(angleRadian));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float heading = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (heading < 0) heading += 360;

        return heading;
    }

    public static int GetAngleFromVector(Vector3 dir)
    {
        dir = dir.normalized;
        float heading = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (heading < 0) heading += 360;

        int angle = Mathf.RoundToInt(heading);

        return angle;
    }

    public static int GetAngleFromVector180(Vector3 dir)
    {
        dir = dir.normalized;
        float heading = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        int angle = Mathf.RoundToInt(heading);

        return angle;
    }

    // Get Mouse Position in World with Z = 0f
    public static Vector3 GetMouseWorldPosition()
    {
        Vector3 vec = GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
        vec.z = 0f;
        return vec;
    }
    public static Vector3 GetMouseWorldPositionWithZ()
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, Camera.main);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Camera worldCamera)
    {
        return GetMouseWorldPositionWithZ(Input.mousePosition, worldCamera);
    }
    public static Vector3 GetMouseWorldPositionWithZ(Vector3 screenPosition, Camera worldCamera)
    {
        Vector3 worldPosition = worldCamera.ScreenToWorldPoint(screenPosition);
        return worldPosition;
    }

    public static void ShakeCameraPosition(Transform cameraMount, float duration, float strength, int vibrato, float randomness, bool fadeOut)
    {
        if (cameraMount == null) return;

        cameraMount.DOShakePosition(duration, strength, vibrato, randomness, fadeOut);
    }

    #region Vector3 Extension Methods
    public static Vector3 DirectionTo(this Vector3 from, Vector3 to)
    {
        return Vector3.Normalize(to - from);
    }

    #endregion
}
