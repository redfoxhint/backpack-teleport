using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* USAGE:
 *  Create instance in other class and thats it lol
 */

public class TestNonMonoUpdate
{
    public TestNonMonoUpdate()
    {
        UpdateCaller.Instance.AddUpdateCallback(Update);
    }

    public void Update()
    {
        
    }
}
