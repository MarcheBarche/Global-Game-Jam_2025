using UnityEngine;

public class PlayerAudioManager : MonoBehaviour
{

    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void play(AudioClip clip)
    {
        if (audioSource.isPlaying && audioSource.clip == clip)
            return;

        audioSource.clip = clip;
        audioSource.Play();
    }
}
