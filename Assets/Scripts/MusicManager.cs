using UnityEngine;
using UnityEngine.Rendering;

public class MusicManager : MonoBehaviour
{

    public static MusicManager Instance { get; private set; }

    private AudioSource currentMusic;

    private AudioSource otherMusic;

    [Range(0f, 1f)]
    public float fadeAlpha;

    private float fadeAccumulator;

    private enum State
    {
        Stationary,
        Fade
    }

    private State state;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(gameObject);
        else Instance = this;
    }

    private void Start()
    {
        DontDestroyOnLoad(this);
        AudioSource[] sources=this.GetComponents<AudioSource>();
        currentMusic=sources[0];
        otherMusic=sources[1];
        state= State.Stationary;    
    }

    public void Play()
    {
        currentMusic.Play();
        otherMusic.Play();
    }

    public void Swap()
    {
        /*float volume = currentMusic.volume;
        currentMusic.volume = otherMusic.volume;
        otherMusic.volume = volume;*/
        state = State.Fade;

    }

    public void Pause()
    {
        currentMusic.Pause();
        otherMusic.Pause();
    }

    public void Stop()
    {
        currentMusic.Stop();
        otherMusic.Stop();
    }

    private void Update()
    {

        if (state == State.Fade)
        {
            fadeAccumulator += fadeAlpha*Time.deltaTime;
            if(fadeAccumulator>1)
            {
                fadeAccumulator = 1;
                state=State.Stationary;
            }
            currentMusic.volume=1-fadeAccumulator;
            otherMusic.volume=fadeAccumulator;
        }
        else fadeAccumulator = 0;
    }

}

    
