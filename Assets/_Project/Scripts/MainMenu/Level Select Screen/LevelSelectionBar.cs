using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LevelSelectorEvent : UnityEvent<LevelSelector> { }

public class LevelSelectionBar : MonoBehaviour
{
    private LevelSelector currentlySelectedLevel;
    [HideInInspector] public LevelSelectorEvent OnLevelSelected = new LevelSelectorEvent();

    public void SetCurrentLevelSelected(LevelSelector levelSelector)
    {
        if (currentlySelectedLevel == null)
        {
            currentlySelectedLevel = levelSelector;
            currentlySelectedLevel.Select();

            OnLevelSelected.Invoke(currentlySelectedLevel);

            return;
        }

        if (levelSelector != currentlySelectedLevel && currentlySelectedLevel != null)
        {
            currentlySelectedLevel.Deselect();
            currentlySelectedLevel = levelSelector;
            currentlySelectedLevel.Select();

            OnLevelSelected.Invoke(currentlySelectedLevel);
        }
    }
    public LevelSelector GetCurrentSelector()
    {
        return currentlySelectedLevel;
    }
}
