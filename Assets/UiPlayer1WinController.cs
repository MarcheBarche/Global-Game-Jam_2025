using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiPlayer1WinController : MonoBehaviour
{
    private AudioSource _buttonSfx;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        _buttonSfx = GetComponent<AudioSource>();
        var _newGameButton = root.Q<Button>("NewGame");
        var _mainMenuButton = root.Q<Button>("MainMenu");



        _newGameButton.clicked += OnNewGameButtonClicked;
        _mainMenuButton.clicked += OnMainMenuButtonClicked;
    }

    private void OnNewGameButtonClicked()
    {
        _buttonSfx.Play();

        SceneManager.LoadScene("Arena");
    }
    private void OnMainMenuButtonClicked()
    {
        _buttonSfx.Play();

        SceneManager.LoadScene("MainMenu");
    }
}
