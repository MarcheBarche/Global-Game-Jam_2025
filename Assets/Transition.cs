using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transition : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static string nextScene;
    [SerializeField] private float speed = 15f;
    public static bool isRunning;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (!isRunning)
            return;
        this.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + speed * Time.deltaTime, this.transform.position.z);

        if(this.transform.position.y >= 5)
            SceneManager.LoadScene(nextScene);
    }
}
