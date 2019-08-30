using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceManager : MonoBehaviour
{
	#region Singleton
	private static ForceManager instance;
	public static ForceManager Instance
	{
		get
		{
			if (instance == null)
			{
				instance = FindObjectOfType<ForceManager>();
			}
			return instance;
		}
	}

	#endregion

	// Public Variables
	public float CurrentForceLevel { get { return currentForceLevel; } }
	[SerializeField] private float currentForceLevel;
	[SerializeField] private float forceLevelOneMaxDistance;
	[SerializeField] private float forceLevelTwoMaxDistance;
	[SerializeField] private float forceLevelThreeMaxDistance;

	// Private Variables

	// Componenets

	void Start()
	{
		currentForceLevel = 1;
	}

	public bool HasForceRequired(float distanceToTravel)
	{
		// Incoming numbers need to be divided by 100. [Ex: num = 356, 356/100 = 3.56 --> This is so we can use realistic numbers for Force.]

		switch (currentForceLevel)
		{
			case 1:
				// Max Distance is 150 units.
				if(distanceToTravel <= forceLevelOneMaxDistance)
				{
					return true;
				}
				break;
			case 2:
				// Max Distance is 250 units.
				if(distanceToTravel <= forceLevelTwoMaxDistance)
				{
					return true;
				}
				break;
			case 3:
				// Mac Distance is 350 units.
				if(distanceToTravel <= forceLevelThreeMaxDistance)
				{
					return true;
				}
				break;
		}

		return false;
	}
}
