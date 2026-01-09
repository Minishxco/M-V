using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource audioSource;

    public AudioClip correctAnswer;
    public AudioClip wrongAnswer;
    public AudioClip optionsBox;
    public AudioClip keyCollected;
    public AudioClip selectTools;
    public AudioClip keyboard;

    public void PlayCorrectAnswer()
    {
        audioSource.PlayOneShot(correctAnswer);
    }

    public void PlayWrongAnswer()
    {
        audioSource.PlayOneShot(wrongAnswer);
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
