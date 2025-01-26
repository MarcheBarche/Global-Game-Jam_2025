using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Linq;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;
using Assets.Scripts;

public class UiMainMenuController : UiControllerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;
        
        var _startGameButton = root.Q<Button>("Play");
        var _howToPlayButton = root.Q<Button>("HowToPlay");
        var _quitButton = root.Q<Button>("Quit");

        _startGameButton.clicked += OnStartGameButtonClicked;
        _howToPlayButton.clicked += OnHowToPlayButtonClicked;
        _quitButton.clicked += OnQuitButtonClicked;

    }

    private void OnStartGameButtonClicked()
    {
        MusicManager.Instance.Swap();
        base.StartCoroutine(LoadScene("Arena"));
    }

    private void OnHowToPlayButtonClicked()
    {
        base.StartCoroutine(LoadScene("HowToPlay"));
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
