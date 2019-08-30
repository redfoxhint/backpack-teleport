using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateStatsUI : MonoBehaviour
{
	[SerializeField] private Image barImage;

	// Tasks
	private Task barLerpTask;
	private Task regenrateBarTask;

	public void UpdateBar(Stat statInfo)
	{
		if (barLerpTask != null)
		{
			barLerpTask.Stop();
			barLerpTask = null;
		}

		barLerpTask = new Task(UpdateBarGraphics(statInfo));
	}

	public void Regenerate(Stat statInfo)
	{
		if (regenrateBarTask != null)
		{
			regenrateBarTask.Stop();
			regenrateBarTask = null;
		}
		
		regenrateBarTask = new Task(RegenerateBar(statInfo));
	}

	private IEnumerator UpdateBarGraphics(Stat statInfo)
	{
		float current = statInfo.oldValue;
		float newValue = statInfo.runtimeStatValue;

		while (current != newValue)
		{
			current = Mathf.Lerp(current, newValue, 1f * Time.deltaTime);
			statInfo.runtimeStatValue = current;
			SetBarImage(current, statInfo.maxStatValue);
			yield return null;
		}

		yield break;
	}

	private IEnumerator RegenerateBar(Stat statInfo)
	{
		float current = statInfo.runtimeStatValue;
		float target = statInfo.maxStatValue;

		while (current != target)
		{
			current = Mathf.Lerp(current, target, 1f * Time.deltaTime);
			statInfo.runtimeStatValue = current;
			SetBarImage(current, target);
			yield return null;
		}

		yield break;
	}

	private void SetBarImage(float amount, float maxAmount)
	{
		barImage.fillAmount = amount / maxAmount;
	}

}
