using UnityEngine;
using System.Collections;

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

    private int character;
    public AudioClip narrationVega;
    public AudioClip narrationMillan;

    public static AudioManager Instance;

    public float startDelay = 2f;
    public GameObject objectToActivate;
    public GameObject fade;

    private void Awake()
    {
        character = UserDataLoader.LoadCharacter();

        if (fade != null)
        {
            fade.SetActive(true);
        }

        StartCoroutine(StartWithDelay());
    }

    IEnumerator StartWithDelay()
    {
        yield return new WaitForSeconds(startDelay);

        // Desactivar fade antes de la narración
        if (fade != null)
        {
            fade.SetActive(false);
        }

        if (objectToActivate != null)
        {
            objectToActivate.SetActive(true);
        }

        // Reproducir narración según personaje
        if (character == 1)
        {
            PlayNarrator(narrationMillan);
        }
        else if (character == 2)
        {
            PlayNarrator(narrationVega);
        }
    }

    public void PlayNarrator(AudioClip clip)
    {
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