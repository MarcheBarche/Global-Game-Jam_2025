using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using static PlayerAnimation;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    [SerializeField] private GameObject[] UIs;
    private int playerNumber = 0;
    [SerializeField] private GameObject transition;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerJoined(PlayerInput playerInput)
    {
        if (playerNumber < GetComponent<PlayerInputManager>().maxPlayerCount)
        {
            print(playerInput.gameObject.name);
            playerInput.gameObject.GetComponent<PlayerController>().spawnPoint = spawnPoints[playerNumber].transform.position;
            playerInput.gameObject.GetComponent<PlayerController>().playerIndex = playerNumber;
            playerInput.gameObject.GetComponent<PlayerController>().UI = UIs[playerNumber];
            playerNumber++;
            if (playerNumber >= spawnPoints.Length) {
                PlayerController.isBegin = true;
                GetComponent<PlayerInputManager>().enabled = false;
            }
        }
    }
    public static void LoseGame(int playerIndex)
    {
        switch (playerIndex)
        {
            case 0:
                Transition.nextScene = "Player2Win";
                break;
            case 1:
                Transition.nextScene = "Player1Win";
                break;
        }
        Transition.isRunning = true;
    }

}
