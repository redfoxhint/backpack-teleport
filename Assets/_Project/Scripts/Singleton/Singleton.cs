using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Singleton<T> : MonoBehaviour where T : Component
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<T>();
                if (instance == null)
                {
                    Type tType = typeof(T);
                    var obj = new GameObject(tType.FullName);
                    instance = obj.AddComponent<T>();
                }
            }

            return instance;
        }
    }
}
