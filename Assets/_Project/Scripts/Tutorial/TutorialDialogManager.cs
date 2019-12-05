using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TutorialDialogManager : MonoBehaviour
{
    // Inspector Fields
    [Header("UI Elements")]
    [SerializeField] private GameObject tutorialDialogUI;
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    [SerializeField] private TextMeshProUGUI nextButtonText;
    [SerializeField] private TextMeshProUGUI pageText;
    [SerializeField] private Button nextPageButton;
    [SerializeField] private Button lastPageButton;
    [SerializeField] private Image tutorialImage;

    [SerializeField] private Tutorial testTutorial; // DEBUG

    // Private Variables
    private Tutorial currentTutorial;
    private bool lastPage;
    [SerializeField] private int currentPageIndex;

    private void Start()
    {
        tutorialDialogUI.SetActive(false);
    }

    public void SetTutorial(Tutorial tutorial)
    {
        tutorialDialogUI.SetActive(true);
        Time.timeScale = 0;
        Cursor.visible = true;

        nextPageButton.onClick.AddListener(NextPage);
        currentTutorial = tutorial;
        currentPageIndex = 0;
        SwitchToPage(0);
    }

    public void NextPage()
    {
        if (currentTutorial == null) return;
        if (lastPage) return;

        if(currentPageIndex + 1 == currentTutorial.AmountOfPages - 1)
        {
            currentPageIndex++;
            nextButtonText.SetText("Close");
            nextPageButton.onClick.RemoveAllListeners();
            nextPageButton.onClick.AddListener(CloseDialog);
            SwitchToPage(currentPageIndex);
            lastPage = true;
            return;
        }

        if(currentPageIndex + 1 >= 0 && currentPageIndex + 1 < currentTutorial.AmountOfPages)
        {
            currentPageIndex++;
            SwitchToPage(currentPageIndex);
        }
    }

    public void PreviousPage()
    {
        if (currentTutorial == null) return;

        if(lastPage)
        {
            lastPage = false;
            nextButtonText.SetText("Next Page");

            nextPageButton.onClick.RemoveAllListeners();
            nextPageButton.onClick.AddListener(NextPage);
        }

        if (currentPageIndex - 1 < 0)
        {
            currentPageIndex = 0;
            return;
        }

        currentPageIndex--;
        SwitchToPage(currentPageIndex);
    }

    private void SwitchToPage(int index)
    {
        if (currentTutorial == null) return;

        int totalPages = currentTutorial.AmountOfPages;

        TutorialPage newPage = currentTutorial.pages[index];
        UpdatePage(newPage, totalPages);
    }

    private void UpdatePage(TutorialPage page, int totalPages)
    {
        if (currentTutorial == null) return;

        titleText.SetText(page.title);
        descriptionText.SetText(page.description);
        pageText.SetText($"{currentPageIndex + 1}/{totalPages}");
        tutorialImage.sprite = page.tutorialImage;
    }

    private void CloseDialog()
    {
        tutorialDialogUI.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
    }
}
