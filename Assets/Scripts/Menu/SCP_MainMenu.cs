using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class SCP_MainMenu : MonoBehaviour
{
    // Main Menu
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject aunty;
    private Button startButton, chaptersButton, optionsButton, creditsButton;

    // Chapter Selection
    [SerializeField] private GameObject chapterSelection;
    private Button ch1Button, ch2Button, ch3Button, ch4Button, chBackButton;

    // Credits
    [SerializeField] private GameObject creditsMenu;
    private Button crBackButton;

    void Awake()
    {
        // Main Menu
        startButton = mainMenu.transform.GetChild(0).GetComponent<Button>();
        chaptersButton = mainMenu.transform.GetChild(1).GetComponent<Button>();
        optionsButton = mainMenu.transform.GetChild(2).GetComponent<Button>();
        creditsButton = mainMenu.transform.GetChild(3).GetComponent<Button>();

        // Chapter Selection
        ch1Button = chapterSelection.transform.GetChild(0).GetComponent<Button>();
        ch2Button = chapterSelection.transform.GetChild(1).GetComponent<Button>();
        ch3Button = chapterSelection.transform.GetChild(2).GetComponent<Button>();
        ch4Button = chapterSelection.transform.GetChild(3).GetComponent<Button>();
        chBackButton = chapterSelection.transform.GetChild(4).GetComponent<Button>();

        // Credits
        crBackButton = creditsMenu.transform.Find("BTN_Back").GetComponent<Button>(); 
    }

    // Start is called before the first frame update
    void Start()
    {
        // Main Menu
        startButton.onClick.AddListener(PressStartButton);
        chaptersButton.onClick.AddListener(PressChaptersButton);
        optionsButton.onClick.AddListener(PressOptionsButton);
        creditsButton.onClick.AddListener(PressCreditsButton);

        // Chapter Selection
        ch1Button.onClick.AddListener(delegate { PressChapterButtons(1); });
        ch2Button.onClick.AddListener(delegate { PressChapterButtons(2); });
        ch3Button.onClick.AddListener(delegate { PressChapterButtons(3); });
        ch4Button.onClick.AddListener(delegate { PressChapterButtons(4); });
        chBackButton.onClick.AddListener(PressChapterBackButton);

        // Credits
        crBackButton.onClick.AddListener(PressCreditBackButton);
    }

    // Start a new game from the beginning
    void PressStartButton()
    {
        Debug.Log("Start new game!");
        SceneManager.LoadScene(1);
    }

    // Open chapter selection menu
    void PressChaptersButton()
    {
        Debug.Log("Open chapter selection menu!");
        mainMenu.SetActive(false);
        aunty.SetActive(false);
        chapterSelection.SetActive(true);
    }

    // Open options menu
    void PressOptionsButton()
    {
        Debug.Log("Open options menu!");
    }

    // Open credits menu
    void PressCreditsButton()
    {
        Debug.Log("Open credits menu!");
        mainMenu.SetActive(false);
        aunty.SetActive(false);
        creditsMenu.SetActive(true);
    }

    // Start the game from certain chapter
    void PressChapterButtons(int chapter)
    {
        if (chapter == 1)
            PressStartButton();
        else if (chapter >= 2 && chapter <= 4)
        {
            Debug.Log("Start Chapter " + chapter);
            SceneManager.LoadScene(chapter);
        }
        else
            Debug.LogError("Incorrect chapter index!");
    }

    // Close chapter selection menu and go back to main menu
    void PressChapterBackButton()
    {
        Debug.Log("Close chapter selection menu!");
        chapterSelection.SetActive(false);
        mainMenu.SetActive(true);
        aunty.SetActive(true);
    }

    // Close credits menu and go back to main menu
    void PressCreditBackButton()
    {
        Debug.Log("Close credits menu!");
        creditsMenu.SetActive(false);
        mainMenu.SetActive(true);
        aunty.SetActive(true);
    }
}
