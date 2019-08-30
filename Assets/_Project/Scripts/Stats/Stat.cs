using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stats/New Stat")]
public class Stat : ScriptableObject, ISerializationCallbackReceiver
{
	public float maxStatValue = 10;
	public float runtimeStatValue;
	public StatTypes statType;

	public float oldValue;
	private float currentValue;

	public void Init()
	{
		currentValue = maxStatValue;
	}

	public float RecalculateValue(float amount)
	{	
		currentValue -= amount;
		return currentValue;
	}

	public void SetOldValue()
	{
		oldValue = runtimeStatValue;
	}

	public void OnAfterDeserialize()
	{
		runtimeStatValue = maxStatValue;
		oldValue = 0;
	}

	public void OnBeforeSerialize()
	{

	}

}
