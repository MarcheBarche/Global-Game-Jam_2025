using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Assets.Scripts
{
    public class UiControllerBase : MonoBehaviour
    {
        public AudioSource _buttonSfx;

        public void Start()
        {
            _buttonSfx = GetComponent<AudioSource>();

            var root = GetComponent<UIDocument>().rootVisualElement;
            var buttons = root.Query<Button>(className: "button-metal").ToList();

            foreach (var button in buttons)
            {
                button.clicked += OnButtonClicked;
            }
        }

        private void OnButtonClicked()
        {
            _buttonSfx.Play();
        }

        public IEnumerator LoadScene(string path)
        {
            yield return new WaitWhile(() => _buttonSfx.isPlaying);
            SceneManager.LoadScene(path);
        }
    }
}
