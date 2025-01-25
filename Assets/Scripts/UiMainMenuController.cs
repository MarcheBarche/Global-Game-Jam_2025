using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiMainMenuController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var _startGameButton = root.Q<VisualElement>("Play");
        var _howToPlayButton = root.Q<VisualElement>("HowToPlay");
        var _quitButton = root.Q<VisualElement>("Quit");

        _startGameButton.RegisterCallback<ClickEvent>(OnStartGameButtonClicked);
        _howToPlayButton.RegisterCallback<ClickEvent>(OnHowToPlayButtonClicked);
        _quitButton.RegisterCallback<ClickEvent>(OnQuitButtonClicked);
    }

    private void OnStartGameButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("Battlefield");
    }

    private void OnHowToPlayButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("HowToPlay");
    }

    private void OnQuitButtonClicked(ClickEvent evt)
    {
        Application.Quit();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
