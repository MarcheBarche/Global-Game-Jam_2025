using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject[] spawnPoints;
    private int playerNumber = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnPlayerJoined()
    {
        PlayerController.spawnPoint = spawnPoints[playerNumber].transform.position;
        playerNumber++;
    }
}
