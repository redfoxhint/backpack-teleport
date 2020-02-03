using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI.Extensions;

public class CarouselMenu : MonoBehaviour
{
    [SerializeField] private HorizontalScrollSnap horizontalScrollSnap;
    [SerializeField] private int defaultPageIndex; // Default page to show when the menu is opened.
    [SerializeField] private Transform content;

    [HideInInspector] public UnityEvent OnPageChangedEvent = new UnityEvent();

    private Transform defaultPage;

    private void Awake()
    {
        horizontalScrollSnap.OnSelectionPageChangedEvent.AddListener(OnPageChanged);
        SetDefaultPage();
    }

    private void OnPageChanged(int pageNumber)
    {
        OnPageChangedEvent?.Invoke();
    }

    public Transform GetCurrentPageObject()
    {
        Transform currentPage = horizontalScrollSnap.CurrentPageObject();

        if (currentPage != null) return currentPage;

        return null;
    }

    public Transform GetDefaultPage()
    {
        return defaultPage;
    }

    private void SetDefaultPage()
    {
        defaultPage = content.GetChild(defaultPageIndex);
    }
}
