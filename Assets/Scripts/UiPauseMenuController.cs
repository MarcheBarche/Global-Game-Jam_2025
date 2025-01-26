using Assets.Scripts;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiPauseMenuController : UiControllerBase
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;

        var _continueGameButton = root.Q<Button>("Continue");
        var _mainMenuButton = root.Q<Button>("MainMenu");

        _continueGameButton.clicked += OnContinueGameButtonClicked;
        _mainMenuButton.clicked += OnMainMenuButtonClicked;
    }

    private void OnContinueGameButtonClicked()
    {
        _buttonSfx.Play();
        SceneManager.UnloadSceneAsync("PauseMenu");
    }

    private void OnMainMenuButtonClicked()
    {
        base.StartCoroutine(LoadScene("MainMenu"));
    }

    // Update is called once per frame
    void Update()
    {
    }
}
