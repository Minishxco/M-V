using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyManager : MonoBehaviour
{
    public GameObject fadeOut;
    public GameObject[] fullKeyImage;
    public AudioManager audioManager;

    public int initialKeys = 0;
    private int key;

    public bool enableVideo = false;
    public PlayVideo playVideo;
    public float correctAnimDuration = 2f;
    public float delayBeforeFade = 1f;
    public float fadeDuration = 1f;

    void Start()
    {
        key = initialKeys;

        for (int i = 0; i < key && i < fullKeyImage.Length; i++)
        {
            fullKeyImage[i].SetActive(true);
        }
    }

    public void updateKey()
    {
        if (key >= fullKeyImage.Length) return;

        audioManager.PlayKeyCollected();

        fullKeyImage[key].SetActive(true);
        key++;

        if (fadeOut != null)
        {
            fadeOut.SetActive(true);

            if(enableVideo)
            {
                StartCoroutine(SecuenciaCorrecta());
            }
        }
    }

    IEnumerator SecuenciaCorrecta()
    {
        yield return new WaitForSeconds(correctAnimDuration);

        yield return new WaitForSeconds(delayBeforeFade);

        fadeOut.SetActive(true);

        yield return new WaitForSeconds(fadeDuration);

        playVideo.PlayVideoFromButton();
    }
}