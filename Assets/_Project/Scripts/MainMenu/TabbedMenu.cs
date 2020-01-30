using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TabbedMenu : MonoBehaviour
{
    private TabbedMenuTab currentlySelectedTab;
    [SerializeField] private TabbedMenuTab defaultTab;

    private void Start()
    {
        foreach (Transform tab in transform)
        {
            TabbedMenuTab _tab = tab.GetComponent<TabbedMenuTab>();
            _tab.Init(this);
        }

        SetCurrentlySelectedTab(defaultTab);
    }

    public void SetCurrentlySelectedTab(TabbedMenuTab tab)
    {
        if (currentlySelectedTab == null)
        {
            currentlySelectedTab = tab;
            currentlySelectedTab.Select();

            return;
        }

        if (tab != currentlySelectedTab && currentlySelectedTab != null)
        {
            currentlySelectedTab.Deselect();
            currentlySelectedTab = tab;
            currentlySelectedTab.Select();
        }
    }
}
