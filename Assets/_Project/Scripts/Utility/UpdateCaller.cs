using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateCaller : PersistentSingleton<UpdateCaller>
{
    private Action updateCallback;

    public void AddUpdateCallback(Action method)
    {
        updateCallback += method;
    }

    private void Update()
    {
        updateCallback();
    }
}
