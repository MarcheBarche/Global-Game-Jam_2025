using Assets.Scripts;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiHowToPlayController : UiControllerBase
{
    private AudioSource _buttonSfx;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        base.Start();
        var root = GetComponent<UIDocument>().rootVisualElement;
        _buttonSfx = GetComponent<AudioSource>();

        var _backButton = root.Q<Button>("BackToMenu");

        _backButton.clicked += OnBackToMenuButtonClicked;
    }

    private void OnBackToMenuButtonClicked()
    {
        base.StartCoroutine(LoadScene("MainMenu"));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
