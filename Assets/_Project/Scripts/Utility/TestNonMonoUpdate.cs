using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestNonMonoUpdate
{
    public TestNonMonoUpdate()
    {
        UpdateCaller.Instance.AddUpdateCallback(Update);
    }

    public void Update()
    {
        Debug.Log("Updating...");
    }
}
