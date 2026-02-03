using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioAlert;
    public AudioSource audioNotification;
    public AudioSource audioWriting;
    public AudioSource audioVoice;

    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioClip optionsBox;
    public AudioClip keyCollected;
    public AudioClip selectTools;
    public AudioClip keyboard;

    public static AudioManager Instance;

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
        if (audioVoice.isPlaying)
        {
            audioVoice.Stop();
        }

        audioVoice.clip = clip;
        audioVoice.Play();
    }

    public void PlayCorrectAnswer()
    {
        audioAlert.PlayOneShot(correctAnswer);
    }

    public void PlayWrongAnswer()
    {
        audioAlert.PlayOneShot(wrongAnswer);
    }

    public void PlayOptionsBox()
    {
        audioNotification.PlayOneShot(optionsBox);
    }

    public void PlayKeyCollected()
    {
        audioNotification.PlayOneShot(keyCollected);
    }

    public void PlaySelectTools()
    {
        audioNotification.PlayOneShot(selectTools);
    }

    public void PlayKeyboard()
    {
        audioWriting.loop = true;
        audioWriting.clip = keyboard;
        audioWriting.Play();
    }

    public void StopKeyboard()
    {
        audioWriting.loop = false;
        audioWriting.Stop();
    }
}
