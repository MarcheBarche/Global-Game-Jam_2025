using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class UiHowToPlayController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;

        var _backButton = root.Q<VisualElement>("BackToMenu");

        _backButton.RegisterCallback<ClickEvent>(OnBackToMenuButtonClicked);
    }

    private void OnBackToMenuButtonClicked(ClickEvent evt)
    {
        SceneManager.LoadScene("MainMenu");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
