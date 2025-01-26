using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiMainMenuController : MonoBehaviour
{

    private int index = 0;
    private List<VisualElement> buttons = new List<VisualElement>();

    private Color unselectedButtonColor = new Color(255 / 255, 255 / 255, 255 / 255);
    private Color unselectedTextColor = new Color(101/255, 101/255, 101 / 255);
    private Color selectedButtonColor = new Color(101 / 255 , 101 / 255 , 101 / 255 );
    private Color selectedTextColor = new Color(255 / 255    , 255 / 255, 255 / 255);
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var _startGameButton = root.Q<Button>("Play");
        var _howToPlayButton = root.Q<Button>("HowToPlay");
        var _quitButton = root.Q<Button>("Quit");



        _startGameButton.clicked += OnStartGameButtonClicked;
        _howToPlayButton.clicked += OnHowToPlayButtonClicked;
        _howToPlayButton.clicked += OnQuitButtonClicked;
    }

    private void OnStartGameButtonClicked()
    {
        MusicManager.Instance.Swap();
        SceneManager.LoadScene("Arena");
    }

    private void OnHowToPlayButtonClicked()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnQuitButtonClicked()
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
