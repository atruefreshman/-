using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{
    private AudioSource audioSource;
    private StoveCounter stoveCounter;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        stoveCounter = GetComponentInParent<StoveCounter>();
        stoveCounter.OnStateChangeed += StoveCounter_OnStateChangeed;
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
    }

    private void GameInput_OnPauseAction(object sender, System.EventArgs e)
    {
        if (!GameManager.Instance.gamePaused)
            audioSource.Stop();
        else
        {
            audioSource.volume = SoundManager.Instance.SoundEffectVolume;
            audioSource.Play();
        }
    }

    private void StoveCounter_OnStateChangeed(object sender, StoveCounter.OnStateChangeedEventArges e)
    {
        if (e.isFrying)
        {
            audioSource.volume = SoundManager.Instance.SoundEffectVolume;
            audioSource.Play();
        }
        else
            audioSource.Stop();
    }
}
