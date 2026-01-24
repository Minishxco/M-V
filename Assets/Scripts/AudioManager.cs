using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioSource audioSource2;
    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioClip optionsBox;
    public AudioClip keyCollected;
    public AudioClip selectTools;
    public AudioClip keyboard;

    public static AudioManager Instance;
    public AudioSource narratorSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayNarrator(AudioClip clip)
    {
        // Si hay otro audio narrado, se corta
        if (narratorSource.isPlaying)
        {
            narratorSource.Stop();
        }

        narratorSource.clip = clip;
        narratorSource.Play();
    }

    public void PlayCorrectAnswer()
    {
        audioSource2.PlayOneShot(correctAnswer);
    }

    public void PlayWrongAnswer()
    {
        audioSource2.PlayOneShot(wrongAnswer);
    }

    public void PlayOptionsBox()
    {
        audioSource.PlayOneShot(optionsBox);
    }

    public void PlayKeyCollected()
    {
        audioSource.PlayOneShot(keyCollected);
    }

    public void PlaySelectTools()
    {
        audioSource.PlayOneShot(selectTools);
    }

    public void PlayKeyboard()
    {
        audioSource.loop = true;
        audioSource.clip = keyboard;
        audioSource.Play();
    }

    public void StopKeyboard()
    {
        audioSource.loop = false;
        audioSource.Stop();
    }
}
