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
    public AudioClip narratorVoice1;

    void Start()
    {
        PlayNarratorAudio1();
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

    public void PlayNarratorAudio1()
    {
        audioSource.PlayOneShot(narratorVoice1);
    }
}
