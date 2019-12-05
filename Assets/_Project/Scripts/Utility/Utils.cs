using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static bool GetIndexInBounds(int index, int[] array)
    {
        return (index >= 0) && (index < array.Length);
    }
}
